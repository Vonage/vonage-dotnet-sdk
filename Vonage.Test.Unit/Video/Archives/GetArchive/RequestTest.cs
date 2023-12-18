using System;
using AutoFixture;
using Vonage.Common.Test.Extensions;
using Vonage.Video.Archives.GetArchive;
using Xunit;

namespace Vonage.Test.Unit.Video.Archives.GetArchive
{
    public class RequestTest
    {
        private readonly Guid applicationId;
        private readonly Guid archiveId;

        public RequestTest()
        {
            var fixture = new Fixture();
            this.applicationId = fixture.Create<Guid>();
            this.archiveId = fixture.Create<Guid>();
        }

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            GetArchiveRequest
                .Build()
                .WithApplicationId(this.applicationId)
                .WithArchiveId(this.archiveId)
                .Create()
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess($"/v2/project/{this.applicationId}/archive/{this.archiveId}");
    }
}