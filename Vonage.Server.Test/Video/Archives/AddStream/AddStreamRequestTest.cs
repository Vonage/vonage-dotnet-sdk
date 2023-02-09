using System;
using AutoFixture;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Video.Archives.AddStream;
using Xunit;

namespace Vonage.Server.Test.Video.Archives.AddStream
{
    public class AddStreamRequestTest
    {
        private readonly Guid applicationId;
        private readonly Guid archiveId;
        private readonly Guid streamId;

        public AddStreamRequestTest()
        {
            var fixture = new Fixture();
            this.applicationId = fixture.Create<Guid>();
            this.archiveId = fixture.Create<Guid>();
            this.streamId = fixture.Create<Guid>();
        }

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            AddStreamRequestBuilder.Build(this.applicationId, this.archiveId, this.streamId)
                .Create()
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess($"/v2/project/{this.applicationId}/archive/{this.archiveId}/streams");
    }
}