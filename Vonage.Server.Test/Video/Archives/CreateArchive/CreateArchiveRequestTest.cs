using System;
using AutoFixture;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Video.Archives.CreateArchive;
using Xunit;

namespace Vonage.Server.Test.Video.Archives.CreateArchive
{
    public class CreateArchiveRequestTest
    {
        private readonly Guid applicationId;
        private readonly string sessionId;

        public CreateArchiveRequestTest()
        {
            var fixture = new Fixture();
            this.applicationId = fixture.Create<Guid>();
            this.sessionId = fixture.Create<string>();
        }

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            CreateArchiveRequestBuilder.Build(this.applicationId, this.sessionId)
                .Create()
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess($"/v2/project/{this.applicationId}/archive");
    }
}