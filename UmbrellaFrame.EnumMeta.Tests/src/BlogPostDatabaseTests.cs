using UmbrellaFrame.EnumMeta.Core;
using Xunit;

namespace BlogPostTests.Tests
{
    public enum BlogPostStatus
    {
        [Status("Blog post created.", StatusType.Success)]
        Created = 0,

        [Status("Blog post updated.", StatusType.Success)]
        Updated = 1,

        [Status("Main topic was not found.", StatusType.Error)]
        MainTopicNotFound = 2
    }

    public class BlogPostDatabaseTests
    {
        [Theory]
        [InlineData(0, BlogPostStatus.Created, "Blog post created.", StatusType.Success)]
        [InlineData(1, BlogPostStatus.Updated, "Blog post updated.", StatusType.Success)]
        [InlineData(2, BlogPostStatus.MainTopicNotFound, "Main topic was not found.", StatusType.Error)]
        public void StatusCodeCanBeMappedToEnumMetadata(
            int statusCode,
            BlogPostStatus expectedStatus,
            string expectedMessage,
            StatusType expectedType)
        {
            var status = (BlogPostStatus)statusCode;
            var metadata = status.GetEnumStatus();

            Assert.Equal(expectedStatus, status);
            Assert.Equal(expectedMessage, metadata.Message);
            Assert.Equal(expectedType, metadata.Type);
        }

        [Fact]
        public void TryGetEnumStatusReturnsFalseForUndefinedStatusCode()
        {
            var status = (BlogPostStatus)999;

            var hasMetadata = status.TryGetEnumStatus(out var metadata);

            Assert.False(hasMetadata);
            Assert.Null(metadata);
        }
    }
}
