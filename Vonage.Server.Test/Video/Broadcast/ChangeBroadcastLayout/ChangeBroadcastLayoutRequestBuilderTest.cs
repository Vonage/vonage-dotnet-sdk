using System;
using AutoFixture;
using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Common;
using Vonage.Server.Video.Broadcast.ChangeBroadcastLayout;
using Xunit;

namespace Vonage.Server.Test.Video.Broadcast.ChangeBroadcastLayout
{
    public class ChangeBroadcastLayoutRequestBuilderTest
    {
        private readonly ArchiveLayout layout;
        private readonly Guid applicationId;
        private readonly string broadcastId;

        public ChangeBroadcastLayoutRequestBuilderTest()
        {
            var fixture = new Fixture();
            fixture.Customize(new SupportMutableValueTypesCustomization());
            this.applicationId = fixture.Create<Guid>();
            this.broadcastId = fixture.Create<string>();
            this.layout = fixture.Create<ArchiveLayout>();
        }

        [Fact]
        public void Build_ShouldReturnFailure_GivenApplicationIdIsEmpty() =>
            ChangeBroadcastLayoutRequestBuilder.Build()
                .WithApplicationId(Guid.Empty)
                .WithBroadcastId(this.broadcastId)
                .WithLayout(this.layout)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ApplicationId cannot be empty."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Build_ShouldReturnFailure_GivenBroadcastIdIsNullOrWhitespace(string value) =>
            ChangeBroadcastLayoutRequestBuilder.Build()
                .WithApplicationId(this.applicationId)
                .WithBroadcastId(value)
                .WithLayout(this.layout)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("BroadcastId cannot be null or whitespace."));

        [Fact]
        public void Build_ShouldReturnSuccess_WithMandatoryValues() =>
            ChangeBroadcastLayoutRequestBuilder.Build()
                .WithApplicationId(this.applicationId)
                .WithBroadcastId(this.broadcastId)
                .WithLayout(this.layout)
                .Create()
                .Should()
                .BeSuccess(success =>
                {
                    success.ApplicationId.Should().Be(this.applicationId);
                    success.BroadcastId.Should().Be(this.broadcastId);
                    success.Layout.Should().Be(this.layout);
                });
    }
}