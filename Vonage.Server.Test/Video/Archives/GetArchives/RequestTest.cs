using System;
using AutoFixture;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Video.Archives.GetArchives;
using Xunit;

namespace Vonage.Server.Test.Video.Archives.GetArchives
{
    public class RequestTest
    {
        private readonly Guid applicationId;

        public RequestTest()
        {
            var fixture = new Fixture();
            this.applicationId = fixture.Create<Guid>();
        }

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint_WithDefaultOffsetAndCount() =>
            GetArchivesRequestBuilder.Build(this.applicationId)
                .Create()
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess($"/v2/project/{this.applicationId}/archive?offset=0&count=50");

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint_WithOffsetAndCount() =>
            GetArchivesRequestBuilder.Build(this.applicationId)
                .WithCount(100)
                .WithOffset(1000)
                .Create()
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess($"/v2/project/{this.applicationId}/archive?offset=1000&count=100");

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint_WithSessionId() =>
            GetArchivesRequestBuilder.Build(this.applicationId)
                .WithSessionId("123456")
                .Create()
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess($"/v2/project/{this.applicationId}/archive?offset=0&count=50&sessionId=123456");
    }
}