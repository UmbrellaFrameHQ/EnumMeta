using UmbrellaFrame.EnumMeta.Core;
using System;
using Xunit;

namespace GetEnumInfoTest.Tests
{
    public enum TestStatus
    {
        [Info("Active", "The status is active")]
        Active,

        [Info("Inactive", "The status is inactive")]
        Inactive,

        [Info("Pending")]
        Pending,

        WithoutInfoMetadata
    }

    public class GetEnumInfosTest
    {
        [Fact]
        public void TestGetEnumInfos()
        {
            // Arrange
            var status = TestStatus.Active;

            // Act
            var infos = status.GetEnumInfos();

            // Assert
            Assert.Equal(2, infos.Length);
            Assert.Equal("Active", infos[0]);
            Assert.Equal("The status is active", infos[1]);
        }

        [Fact]
        public void TestEnumGetEnumInfo()
        {
            // Arrange
            var status = TestStatus.Inactive;

            // Assert
            Assert.Equal("Inactive", status.GetEnumInfo(InfoType.Name));
            Assert.Equal("The status is inactive", status.GetEnumInfo(InfoType.Description));
        }

        [Fact]
        public void TryGetEnumInfosReturnsFalseWhenInfoAttributeIsMissing()
        {
            var hasMetadata = TestStatus.WithoutInfoMetadata.TryGetEnumInfos(out var infos);

            Assert.False(hasMetadata);
            Assert.Empty(infos);
        }

        [Fact]
        public void GetEnumInfoThrowsClearExceptionWhenRequestedInfoIsMissing()
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(
                () => TestStatus.Pending.GetEnumInfo(InfoType.Description));

            Assert.Contains("index 1", exception.Message);
        }

        [Fact]
        public void GetEnumInfoOrDefaultReturnsFallbackWhenRequestedInfoIsMissing()
        {
            var description = TestStatus.Pending.GetEnumInfoOrDefault(InfoType.Description, "No description");

            Assert.Equal("No description", description);
        }

        [Fact]
        public void GetInfoMetadataReturnsTypedConvenienceModel()
        {
            var metadata = TestStatus.Active.GetInfoMetadata();

            Assert.Equal(TestStatus.Active, metadata.Value);
            Assert.Equal("Active", metadata.Name);
            Assert.Equal("The status is active", metadata.Description);
        }

        [Fact]
        public void TryGetInfoMetadataReturnsFalseWhenInfoAttributeIsMissing()
        {
            var hasMetadata = TestStatus.WithoutInfoMetadata.TryGetInfoMetadata(out var metadata);

            Assert.False(hasMetadata);
            Assert.Null(metadata);
        }
    }
}
