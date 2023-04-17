using System;
using AutoFixture;
using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Video.Archives.ChangeLayout;
using Xunit;

namespace Vonage.Server.Test.Video.Archives.ChangeLayout
{
    public class RequestTest
    {
        private readonly Fixture fixture;
        private readonly Guid applicationId;
        private readonly Guid archiveId;
        private readonly Layout layout;

        public RequestTest()
        {
            this.fixture = new Fixture();
            this.fixture.Customize(new SupportMutableValueTypesCustomization());
            this.applicationId = this.fixture.Create<Guid>();
            this.archiveId = this.fixture.Create<Guid>();
            this.layout = this.fixture.Create<Layout>();
        }

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            ChangeLayoutRequest.Parse(this.applicationId, this.archiveId, this.layout)
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess($"/v2/project/{this.applicationId}/archive/{this.archiveId}/layout");

        [Fact]
        public void Parse_ShouldReturnFailure_GivenApplicationIdIsEmpty() =>
            ChangeLayoutRequest.Parse(Guid.Empty, this.archiveId, this.layout)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ApplicationId cannot be empty."));

        [Fact]
        public void Parse_ShouldReturnFailure_GivenArchiveIdIsEmpty() =>
            ChangeLayoutRequest.Parse(this.applicationId, Guid.Empty, this.layout)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ArchiveId cannot be empty."));

        [Fact]
        public void Parse_ShouldReturnSuccess_GivenAllValuesAreProvided() =>
            ChangeLayoutRequest.Parse(this.applicationId, this.archiveId, this.layout)
                .Should()
                .BeSuccess(request =>
                {
                    request.ApplicationId.Should().Be(this.applicationId);
                    request.ArchiveId.Should().Be(this.archiveId);
                });
    }
}