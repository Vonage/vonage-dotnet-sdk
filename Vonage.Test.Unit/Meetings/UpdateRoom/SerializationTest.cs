using System;
using FluentAssertions;
using Vonage.Common;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Meetings.Common;
using Vonage.Meetings.UpdateRoom;
using Xunit;

namespace Vonage.Test.Unit.Meetings.UpdateRoom
{
    public class SerializationTest
    {
        private readonly SerializationTestHelper helper;

        public SerializationTest() =>
            this.helper = new SerializationTestHelper(typeof(SerializationTest).Namespace,
                JsonSerializer.BuildWithSnakeCase());

        [Fact]
        public void ShouldDeserialize200() =>
            this.helper.Serializer
                .DeserializeObject<Room>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(success =>
                {
                    success.CallbackUrls.RecordingsCallbackUrl.Should().Be("https://example.com");
                    success.CallbackUrls.RoomsCallbackUrl.Should().Be("https://example.com");
                    success.CallbackUrls.SessionsCallbackUrl.Should().Be("https://example.com");
                    success.AvailableFeatures.IsChatAvailable.Should().BeTrue();
                    success.AvailableFeatures.IsRecordingAvailable.Should().BeTrue();
                    success.AvailableFeatures.IsWhiteboardAvailable.Should().BeTrue();
                    success.Id.Should().Be(new Guid("934f95c2-28e5-486b-ab8e-1126dbc180f9"));
                    success.Metadata.Should().Be("abc123");
                    success.Recording.AutoRecord.Should().BeFalse();
                    success.Recording.RecordOnlyOwner.Should().BeFalse();
                    success.Type.Should().Be(RoomType.Instant);
                    success.CreatedAt.Should().Be(new DateTime(2023, 02, 06, 11, 13, 50));
                    success.DisplayName.Should().Be("abc123");
                    success.ExpiresAt.Should().Be(new DateTime(2023, 02, 06, 11, 13, 50));
                    success.IsAvailable.Should().BeFalse();
                    success.MeetingCode.Should().Be("123456789");
                    success.ThemeId.Should().Be("abc123");
                    success.ExpiresAfterUse.Should().BeFalse();
                    success.InitialJoinOptions.MicrophoneState.Should()
                        .Be(RoomMicrophoneState.Default);
                    success.JoinApprovalLevel.Should().Be(RoomApprovalLevel.None);
                    success.Links.GuestUrl.Href.Should().Be("https://meetings.vonage.com/123456789");
                    success.Links.HostUrl.Href.Should()
                        .Be("https://meetings.vonage.com/123456789?participant_token=xyz");
                });

        [Fact]
        public void ShouldSerialize() =>
            UpdateRoomRequest
                .Build()
                .WithRoomId(new Guid("934f95c2-28e5-486b-ab8e-1126dbc180f9"))
                .WithExpiresAt(new DateTime(2019, 08, 24))
                .ExpireAfterUse()
                .WithThemeId("ef2b46f3-8ebb-437e-a671-272e4990fbc8")
                .WithApprovalLevel(RoomApprovalLevel.None)
                .WithInitialJoinOptions(new Room.JoinOptions {MicrophoneState = RoomMicrophoneState.Default})
                .WithFeatures(new Room.Features
                {
                    IsChatAvailable = false, IsRecordingAvailable = false, IsWhiteboardAvailable = false,
                    IsCaptionsAvailable = true, IsLocaleSwitcherAvailable = false,
                })
                .WithCallback(new Room.Callback
                {
                    RecordingsCallbackUrl = "https://example.com", SessionsCallbackUrl = "https://example.com",
                    RoomsCallbackUrl = "https://example.com",
                })
                .WithUserInterfaceSettings(new UiSettings(UiSettings.UserInterfaceLanguage.De))
                .Create()
                .GetStringContent()
                .Should()
                .BeSuccess(this.helper.GetRequestJson());
    }
}