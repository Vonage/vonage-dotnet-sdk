using System;
using AutoFixture;
using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Video.Archives.RemoveStream;
using Xunit;

namespace Vonage.Server.Test.Video.Archives.RemoveStream
{
    public class RemoveStreamRequestTest
    {
        private readonly Guid applicationId;
        private readonly Guid archiveId;
        private readonly Guid streamId;

        public RemoveStreamRequestTest()
        {
            var fixture = new Fixture();
            this.applicationId = fixture.Create<Guid>();
            this.archiveId = fixture.Create<Guid>();
            this.streamId = fixture.Create<Guid>();
        }

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            RemoveStreamRequest.Parse(this.applicationId, this.archiveId, this.streamId)
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess($"/v2/project/{this.applicationId}/archive/{this.archiveId}/streams");

        [Fact]
        public void Parse_ShouldReturnFailure_GivenApplicationIdIsNullOrWhitespace() =>
            RemoveStreamRequest.Parse(Guid.Empty, this.archiveId, this.streamId)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ApplicationId cannot be empty."));

        [Fact]
        public void Parse_ShouldReturnFailure_GivenArchiveIdIsNullOrWhitespace() =>
            RemoveStreamRequest.Parse(this.applicationId, Guid.Empty, this.streamId)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ArchiveId cannot be empty."));

        [Fact]
        public void Parse_ShouldReturnFailure_GivenStreamIdIsNullOrWhitespace() =>
            RemoveStreamRequest.Parse(this.applicationId, this.archiveId, Guid.Empty)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("StreamId cannot be empty."));

        [Fact]
        public void Parse_ShouldReturnSuccess_GivenValuesAreProvided() =>
            RemoveStreamRequest.Parse(this.applicationId, this.archiveId, this.streamId)
                .Should()
                .BeSuccess(request =>
                {
                    request.ApplicationId.Should().Be(this.applicationId);
                    request.ArchiveId.Should().Be(this.archiveId);
                    request.StreamId.Should().Be(this.streamId);
                });
    }
}