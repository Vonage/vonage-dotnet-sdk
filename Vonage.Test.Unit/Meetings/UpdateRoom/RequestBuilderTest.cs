using System;
using AutoFixture;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.Meetings.Common;
using Vonage.Meetings.UpdateRoom;
using Xunit;

namespace Vonage.Test.Unit.Meetings.UpdateRoom
{
    public class RequestBuilderTest
    {
        private readonly Room.Callback callback;
        private readonly DateTime expiresAt;
        private readonly Room.Features value;
        private readonly Guid roomId;
        private readonly Room.JoinOptions joinOptions;
        private readonly RoomApprovalLevel approvalLevel;
        private readonly string themeId;
        private readonly UiSettings uiSettings;

        public RequestBuilderTest()
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
            this.value = fixture.Create<Room.Features>();
            this.callback = fixture.Create<Room.Callback>();
            this.themeId = fixture.Create<string>();
            this.uiSettings = fixture.Create<UiSettings>();
        }

        [Fact]
        public void Build_ShouldReturnFailure_GivenNoValueHasBeenModified() =>
            UpdateRoomRequest
                .Build()
                .WithRoomId(this.roomId)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("At least one property must be updated."));

        [Fact]
        public void Build_ShouldReturnFailure_GivenRoomIdIsNullOrWhitespace() =>
            UpdateRoomRequest
                .Build()
                .WithRoomId(Guid.Empty)
                .WithExpiresAt(this.expiresAt)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("RoomId cannot be empty."));

        [Fact]
        public void Build_ShouldSetValues() =>
            UpdateRoomRequest
                .Build()
                .WithRoomId(this.roomId)
                .WithExpiresAt(this.expiresAt)
                .ExpireAfterUse()
                .WithThemeId(this.themeId)
                .WithApprovalLevel(this.approvalLevel)
                .WithInitialJoinOptions(this.joinOptions)
                .WithFeatures(this.value)
                .WithCallback(this.callback)
                .WithUserInterfaceSettings(this.uiSettings)
                .Create()
                .Should()
                .BeSuccess(success =>
                {
                    success.ExpiresAt.Should().BeSome(this.expiresAt);
                    success.ExpireAfterUse.Should().BeSome(true);
                    success.ThemeId.Should().BeSome(this.themeId);
                    success.JoinApprovalLevel.Should().Be(this.approvalLevel);
                    success.InitialJoinOptions.Should().Be(this.joinOptions);
                    success.AvailableFeatures.Should().Be(this.value);
                    success.CallbackUrls.Should().BeSome(this.callback);
                    success.UserInterfaceSettings.Should().BeSome(this.uiSettings);
                });
    }
}