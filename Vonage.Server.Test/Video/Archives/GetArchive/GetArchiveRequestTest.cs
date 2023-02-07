using System;
using AutoFixture;
using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Video.Archives.GetArchive;
using Xunit;

namespace Vonage.Server.Test.Video.Archives.GetArchive
{
    public class GetArchiveRequestTest
    {
        private readonly Guid applicationId;
        private readonly Guid archiveId;

        public GetArchiveRequestTest()
        {
            var fixture = new Fixture();
            this.applicationId = fixture.Create<Guid>();
            this.archiveId = fixture.Create<Guid>();
        }

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            GetArchiveRequest.Parse(this.applicationId, this.archiveId)
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess($"/v2/project/{this.applicationId}/archive/{this.archiveId}");

        [Fact]
        public void Parse_ShouldReturnFailure_GivenApplicationIdIsEmpty() =>
            GetArchiveRequest.Parse(Guid.Empty, this.archiveId)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ApplicationId cannot be empty."));

        [Fact]
        public void Parse_ShouldReturnFailure_GivenArchiveIdIsNullEmpty() =>
            GetArchiveRequest.Parse(this.applicationId, Guid.Empty)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ArchiveId cannot be empty."));

        [Fact]
        public void Parse_ShouldReturnSuccess_GivenValuesAreProvided() =>
            GetArchiveRequest.Parse(this.applicationId, this.archiveId)
                .Should()
                .BeSuccess(request =>
                {
                    request.ApplicationId.Should().Be(this.applicationId);
                    request.ArchiveId.Should().Be(this.archiveId);
                });
    }
}