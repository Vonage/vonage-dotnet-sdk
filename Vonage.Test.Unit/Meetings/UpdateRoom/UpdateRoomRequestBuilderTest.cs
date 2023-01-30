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
        private readonly Room.Features features;
        private readonly Room.JoinOptions joinOptions;
        private readonly RoomApprovalLevel approvalLevel;
        private readonly string roomId;
        private readonly string expiresAt;
        private readonly string themeId;

        public UpdateRoomRequestBuilderTest()
        {
            var fixture = new Fixture();
            fixture.Customize(new SupportMutableValueTypesCustomization());
            this.roomId = fixture.Create<string>();
            fixture.Create<string>();
            fixture.Create<RoomType>();
            this.expiresAt = fixture.Create<string>();
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
                    success.ExpiresAt.Should().BeNull();
                    success.JoinApprovalLevel.Should().Be(RoomApprovalLevel.None);
                    success.ExpiresAfterUse.Should().BeFalse();
                    success.ThemeId.Should().BeNull();
                    success.CallbackUrls.Should().BeNull();
                    success.AvailableFeatures.IsChatAvailable.Should().BeTrue();
                    success.AvailableFeatures.IsRecordingAvailable.Should().BeTrue();
                    success.AvailableFeatures.IsWhiteboardAvailable.Should().BeTrue();
                    success.InitialJoinOptions.MicrophoneState.Should().Be(RoomMicrophoneState.Default);
                });

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Build_ShouldReturnFailure_GivenRoomIdIsNullOrWhitespace(string invalidRoomId) =>
            UpdateRoomRequestBuilder
                .Build(invalidRoomId)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("RoomId cannot be null or whitespace."));

        [Fact]
        public void Build_ShouldReturnSuccess_() =>
            UpdateRoomRequestBuilder
                .Build(this.roomId)
                .WithExpiresAt(this.expiresAt)
                .ExpiresAfterUse()
                .WithThemeId(this.themeId)
                .WithApprovalLevel(this.approvalLevel)
                .WithInitialJoinOptions(this.joinOptions)
                .WithFeatures(this.features)
                .WithCallback(this.callback)
                .Create()
                .Should()
                .BeSuccess(success =>
                {
                    success.ExpiresAt.Should().Be(this.expiresAt);
                    success.ExpiresAfterUse.Should().BeTrue();
                    success.ThemeId.Should().Be(this.themeId);
                    success.JoinApprovalLevel.Should().Be(this.approvalLevel);
                    success.InitialJoinOptions.Should().Be(this.joinOptions);
                    success.AvailableFeatures.Should().Be(this.features);
                    success.CallbackUrls.Should().Be(this.callback);
                });
    }
}