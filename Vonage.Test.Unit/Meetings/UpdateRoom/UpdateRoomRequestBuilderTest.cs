using System;
using AutoFixture;
using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.Meetings.Common;
using Vonage.Meetings.UpdateRoom;
using Xunit;

namespace Vonage.Test.Unit.Meetings.UpdateRoom
{
    public class UpdateRoomRequestBuilderTest
    {
        private readonly Room.Callback callback;
        private readonly DateTime expiresAt;
        private readonly Room.Features features;
        private readonly Guid roomId;
        private readonly Room.JoinOptions joinOptions;
        private readonly RoomApprovalLevel approvalLevel;
        private readonly string themeId;

        public UpdateRoomRequestBuilderTest()
        {
            var fixture = new Fixture();
            fixture.Customize(new SupportMutableValueTypesCustomization());
            this.roomId = fixture.Create<Guid>();
            fixture.Create<string>();
            fixture.Create<RoomType>();
            this.expiresAt = fixture.Create<DateTime>();
            this.approvalLevel = fixture.Create<RoomApprovalLevel>();
            fixture.Create<Room.RecordingOptions>();
            this.joinOptions = fixture.Create<Room.JoinOptions>();
            this.features = fixture.Create<Room.Features>();
            this.callback = fixture.Create<Room.Callback>();
            this.themeId = fixture.Create<string>();
        }

        [Fact]
        public void Build_ShouldHaveDefaultValues() =>
            UpdateRoomRequestBuilder
                .Build(this.roomId)
                .Create()
                .Should()
                .BeSuccess(success =>
                {
                    success.ExpiresAt.Should().BeNone();
                    success.JoinApprovalLevel.Should().Be(RoomApprovalLevel.None);
                    success.ExpireAfterUse.Should().BeFalse();
                    success.ThemeId.Should().BeNone();
                    success.CallbackUrls.Should().BeNone();
                    success.AvailableFeatures.IsChatAvailable.Should().BeTrue();
                    success.AvailableFeatures.IsRecordingAvailable.Should().BeTrue();
                    success.AvailableFeatures.IsWhiteboardAvailable.Should().BeTrue();
                    success.InitialJoinOptions.MicrophoneState.Should().Be(RoomMicrophoneState.Default);
                });

        [Fact]
        public void Build_ShouldReturnFailure_GivenRoomIdIsNullOrWhitespace() =>
            UpdateRoomRequestBuilder
                .Build(Guid.Empty)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("RoomId cannot be empty."));

        [Fact]
        public void Build_ShouldReturnSuccess_() =>
            UpdateRoomRequestBuilder
                .Build(this.roomId)
                .WithExpiresAt(this.expiresAt)
                .ExpireAfterUse()
                .WithThemeId(this.themeId)
                .WithApprovalLevel(this.approvalLevel)
                .WithInitialJoinOptions(this.joinOptions)
                .WithFeatures(this.features)
                .WithCallback(this.callback)
                .Create()
                .Should()
                .BeSuccess(success =>
                {
                    success.ExpiresAt.Should().BeSome(this.expiresAt);
                    success.ExpireAfterUse.Should().BeTrue();
                    success.ThemeId.Should().BeSome(this.themeId);
                    success.JoinApprovalLevel.Should().Be(this.approvalLevel);
                    success.InitialJoinOptions.Should().Be(this.joinOptions);
                    success.AvailableFeatures.Should().Be(this.features);
                    success.CallbackUrls.Should().BeSome(this.callback);
                });
    }
}