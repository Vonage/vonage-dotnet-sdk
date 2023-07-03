using System;
using AutoFixture;
using FluentAssertions;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Video.Archives.StopArchive;
using Xunit;

namespace Vonage.Server.Test.Video.Archives.StopArchive
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
            StopArchiveRequest.Build()
                .WithApplicationId(Guid.Empty)
                .WithArchiveId(this.archiveId)
                .Create()
                .Should()
                .BeParsingFailure("ApplicationId cannot be empty.");

        [Fact]
        public void Build_ShouldReturnFailure_GivenArchiveIdIsEmpty() =>
            StopArchiveRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithArchiveId(Guid.Empty)
                .Create()
                .Should()
                .BeParsingFailure("ArchiveId cannot be empty.");

        [Fact]
        public void Build_ShouldReturnSuccess_GivenValuesAreProvided() =>
            StopArchiveRequest.Build()
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