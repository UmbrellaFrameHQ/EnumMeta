using UmbrellaFrame.EnumMeta.Core;
using System;
using System.Collections.Generic;
using Xunit;

namespace UmbrellaFrame.EnumMeta.Tests
{
    public enum TestStatus
    {
        [Status("New Record Created.", StatusType.Success)]
        NewRecord,

        [Status("Registration Updated.", StatusType.Success)]
        UpdatedRecord,

        [Status("Record Deleted.", StatusType.Success)]
        DeletedRecord,

        [Status("User Information Could Not Be Verified", StatusType.Error, Code = "USER_VERIFY_FAILED", ExternalCode = "USR-422")]
        UserInformationCouldNotBeVerified,

        WithoutStatusMetadata
    }

    public class CustomModel
    {
        public TestStatus Status { get; set; }
    }

    public class EnumExtensionsTests
    {
        [Theory]
        [InlineData(TestStatus.NewRecord, "New Record Created.", StatusType.Success)]
        [InlineData(TestStatus.UpdatedRecord, "Registration Updated.", StatusType.Success)]
        [InlineData(TestStatus.DeletedRecord, "Record Deleted.", StatusType.Success)]
        [InlineData(TestStatus.UserInformationCouldNotBeVerified, "User Information Could Not Be Verified", StatusType.Error)]
        public void TestStatusAttributes(TestStatus status, string expectedMessage, StatusType expectedType)
        {
            // Arrange
            var getEnumStatus = status.GetEnumStatus();

            // Act & Assert
            Assert.NotNull(getEnumStatus);
            Assert.Equal(expectedMessage, getEnumStatus.Message);
            Assert.Equal(expectedType, getEnumStatus.Type);
        }

        [Fact]
        public void TryGetEnumStatusReturnsFalseWhenStatusAttributeIsMissing()
        {
            var hasMetadata = TestStatus.WithoutStatusMetadata.TryGetEnumStatus(out var metadata);

            Assert.False(hasMetadata);
            Assert.Null(metadata);
        }

        [Fact]
        public void GetEnumStatusThrowsClearExceptionWhenStatusAttributeIsMissing()
        {
            var exception = Assert.Throws<InvalidOperationException>(
                () => TestStatus.WithoutStatusMetadata.GetEnumStatus());

            Assert.Contains(nameof(StatusAttribute), exception.Message);
        }

        [Fact]
        public void GetStatusMetadataReturnsTypedConvenienceModel()
        {
            var metadata = TestStatus.UserInformationCouldNotBeVerified.GetStatusMetadata();

            Assert.Equal(TestStatus.UserInformationCouldNotBeVerified, metadata.Value);
            Assert.Equal("User Information Could Not Be Verified", metadata.Message);
            Assert.Equal(StatusType.Error, metadata.Type);
            Assert.Equal("USER_VERIFY_FAILED", metadata.Code);
            Assert.Equal("USR-422", metadata.ExternalCode);
            Assert.False(metadata.IsSuccess);
            Assert.True(metadata.IsError);
        }

        [Fact]
        public void GetStatusMetadataGenericReturnsStronglyTypedConvenienceModel()
        {
            var metadata = TestStatus.UserInformationCouldNotBeVerified.GetStatusMetadata<TestStatus, StatusType>();

            Assert.Equal(TestStatus.UserInformationCouldNotBeVerified, metadata.Value);
            Assert.Equal("User Information Could Not Be Verified", metadata.Message);
            Assert.Equal(StatusType.Error, metadata.Type);
            Assert.Equal("USER_VERIFY_FAILED", metadata.Code);
            Assert.Equal("USR-422", metadata.ExternalCode);
        }

        [Fact]
        public void TryGetStatusMetadataGenericReturnsTypedConvenienceModel()
        {
            var hasMetadata = TestStatus.UserInformationCouldNotBeVerified.TryGetStatusMetadata<TestStatus, StatusType>(
                out var metadata);

            Assert.True(hasMetadata);
            Assert.NotNull(metadata);
            Assert.Equal(TestStatus.UserInformationCouldNotBeVerified, metadata.Value);
            Assert.Equal(StatusType.Error, metadata.Type);
        }

        [Fact]
        public void TryGetEnumStatusReturnsFalseForNullValue()
        {
            Enum status = null;

            var hasMetadata = status.TryGetEnumStatus(out var metadata);

            Assert.False(hasMetadata);
            Assert.Null(metadata);
        }

        [Fact]
        public void GetResolvedMessageUsesCallerProvidedResolver()
        {
            var messages = new Dictionary<TestStatus, string>
            {
                [TestStatus.UserInformationCouldNotBeVerified] = "Custom external message."
            };

            var message = TestStatus.UserInformationCouldNotBeVerified.GetResolvedMessage(
                status => messages.TryGetValue((TestStatus)status, out var resolvedMessage)
                    ? resolvedMessage
                    : null);

            Assert.Equal("Custom external message.", message);
        }

        [Fact]
        public void GetLocalizedMessageUsesCallerProvidedCultureAwareResolver()
        {
            var message = TestStatus.UserInformationCouldNotBeVerified.GetLocalizedMessage(
                (status, culture) => culture.TwoLetterISOLanguageName == "tr"
                    ? "Kullanici bilgileri dogrulanamadi."
                    : "User information could not be verified.",
                "tr");

            Assert.Equal("Kullanici bilgileri dogrulanamadi.", message);
        }
    }
}
