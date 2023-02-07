using System;
using AutoFixture;
using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Video.Archives.DeleteArchive;
using Xunit;

namespace Vonage.Server.Test.Video.Archives.DeleteArchive
{
    public class DeleteArchiveRequestTest
    {
        private readonly Guid applicationId;
        private readonly Guid archiveId;

        public DeleteArchiveRequestTest()
        {
            var fixture = new Fixture();
            this.applicationId = fixture.Create<Guid>();
            this.archiveId = fixture.Create<Guid>();
        }

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            DeleteArchiveRequest.Parse(this.applicationId, this.archiveId)
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess($"/v2/project/{this.applicationId}/archive/{this.archiveId}");

        [Fact]
        public void Parse_ShouldReturnFailure_GivenApplicationIdIsNullOrWhitespace() =>
            DeleteArchiveRequest.Parse(Guid.Empty, this.archiveId)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ApplicationId cannot be empty."));

        [Fact]
        public void Parse_ShouldReturnFailure_GivenArchiveIdIsNullOrWhitespace() =>
            DeleteArchiveRequest.Parse(this.applicationId, Guid.Empty)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ArchiveId cannot be empty."));

        [Fact]
        public void Parse_ShouldReturnSuccess_GivenValuesAreProvided() =>
            DeleteArchiveRequest.Parse(this.applicationId, this.archiveId)
                .Should()
                .BeSuccess(request =>
                {
                    request.ApplicationId.Should().Be(this.applicationId);
                    request.ArchiveId.Should().Be(this.archiveId);
                });
    }
}