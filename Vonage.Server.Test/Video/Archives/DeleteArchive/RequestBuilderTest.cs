using System;
using AutoFixture;
using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Video.Archives.DeleteArchive;
using Xunit;

namespace Vonage.Server.Test.Video.Archives.DeleteArchive
{
    public class RequestBuilderTest
    {
        private readonly Guid applicationId;
        private readonly Guid archiveId;

        public RequestBuilderTest()
        {
            var fixture = new Fixture();
            this.applicationId = fixture.Create<Guid>();
            this.archiveId = fixture.Create<Guid>();
        }

        [Fact]
        public void Build_ShouldReturnFailure_GivenApplicationIdIsEmpty() =>
            DeleteArchiveRequest.Build()
                .WithApplicationId(Guid.Empty)
                .WithArchiveId(this.archiveId)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ApplicationId cannot be empty."));

        [Fact]
        public void Build_ShouldReturnFailure_GivenArchiveIdIsEmpty() =>
            DeleteArchiveRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithArchiveId(Guid.Empty)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ArchiveId cannot be empty."));

        [Fact]
        public void Build_ShouldReturnSuccess_GivenValuesAreProvided() =>
            DeleteArchiveRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithArchiveId(this.archiveId)
                .Create()
                .Should()
                .BeSuccess(request =>
                {
                    request.ApplicationId.Should().Be(this.applicationId);
                    request.ArchiveId.Should().Be(this.archiveId);
                });
    }
}