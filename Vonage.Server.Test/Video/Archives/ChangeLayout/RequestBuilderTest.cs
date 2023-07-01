using System;
using AutoFixture;
using FluentAssertions;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Video.Archives.ChangeLayout;
using Xunit;

namespace Vonage.Server.Test.Video.Archives.ChangeLayout
{
    public class RequestBuilderTest
    {
        private readonly Guid applicationId;
        private readonly Guid archiveId;
        private readonly Layout layout;

        public RequestBuilderTest()
        {
            var fixture = new Fixture();
            fixture.Customize(new SupportMutableValueTypesCustomization());
            this.applicationId = fixture.Create<Guid>();
            this.archiveId = fixture.Create<Guid>();
            this.layout = fixture.Create<Layout>();
        }

        [Fact]
        public void Build_ShouldReturnFailure_GivenApplicationIdIsEmpty() =>
            ChangeLayoutRequest.Build()
                .WithApplicationId(Guid.Empty)
                .WithArchiveId(this.archiveId)
                .WithLayout(this.layout)
                .Create()
                .Should()
                .BeParsingFailure("ApplicationId cannot be empty.");

        [Fact]
        public void Build_ShouldReturnFailure_GivenArchiveIdIsEmpty() =>
            ChangeLayoutRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithArchiveId(Guid.Empty)
                .WithLayout(this.layout)
                .Create()
                .Should()
                .BeParsingFailure("ArchiveId cannot be empty.");

        [Fact]
        public void Build_ShouldReturnSuccess_GivenAllValuesAreProvided() =>
            ChangeLayoutRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithArchiveId(this.archiveId)
                .WithLayout(this.layout)
                .Create()
                .Should()
                .BeSuccess(request =>
                {
                    request.ApplicationId.Should().Be(this.applicationId);
                    request.ArchiveId.Should().Be(this.archiveId);
                    request.Layout.Should().Be(this.layout);
                });
    }
}