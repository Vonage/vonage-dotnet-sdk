using System;
using AutoFixture;
using FluentAssertions;
using Vonage.Test.Unit.Common.Extensions;
using Vonage.Video.Archives.GetArchive;
using Xunit;

namespace Vonage.Test.Unit.Video.Archives.GetArchive
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
            GetArchiveRequest
                .Build()
                .WithApplicationId(Guid.Empty)
                .WithArchiveId(this.archiveId)
                .Create()
                .Should()
                .BeParsingFailure("ApplicationId cannot be empty.");

        [Fact]
        public void Build_ShouldReturnFailure_GivenArchiveIdIsNullEmpty() =>
            GetArchiveRequest
                .Build()
                .WithApplicationId(this.applicationId)
                .WithArchiveId(Guid.Empty)
                .Create()
                .Should()
                .BeParsingFailure("ArchiveId cannot be empty.");

        [Fact]
        public void Build_ShouldReturnSuccess_GivenValuesAreProvided() =>
            GetArchiveRequest
                .Build()
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