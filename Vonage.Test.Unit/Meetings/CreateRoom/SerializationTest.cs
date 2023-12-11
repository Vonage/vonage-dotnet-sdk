using System;
using FluentAssertions;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Meetings.Common;
using Vonage.Meetings.CreateRoom;
using Vonage.Serialization;
using Xunit;

namespace Vonage.Test.Unit.Meetings.CreateRoom
{
    public class SerializationTest
    {
        private readonly SerializationTestHelper helper;

        public SerializationTest() =>
            this.helper = new SerializationTestHelper(typeof(SerializationTest).Namespace,
                JsonSerializerBuilder.BuildWithSnakeCase());

        [Fact]
        public void ShouldDeserialize200() =>
            this.helper.Serializer
                .DeserializeObject<Room>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(VerifyRoom);

        [Fact]
        public void ShouldSerialize() =>
            CreateRoomRequest
                .Build()
                .WithDisplayName("string")
                .WithMetadata("string")
                .WithThemeId("ef2b46f3-8ebb-437e-a671-272e4990fbc8")
                .WithApprovalLevel(RoomApprovalLevel.None)
                .WithRecordingOptions(new Room.RecordingOptions {AutoRecord = true, RecordOnlyOwner = true})
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

        [Fact]
        public void ShouldSerializeWithDefaultValues() =>
            CreateRoomRequest
                .Build()
                .WithDisplayName("string")
                .Create()
                .GetStringContent()
                .Should()
                .BeSuccess(this.helper.GetRequestJson());

        [Fact]
        public void ShouldSerializeWithLongTermRoom() =>
            CreateRoomRequest
                .Build()
                .WithDisplayName("string")
                .AsLongTermRoom(new DateTime(2023, 02, 07, 20, 10, 05))
                .ExpireAfterUse()
                .Create()
                .GetStringContent()
                .Should()
                .BeSuccess(this.helper.GetRequestJson());

        internal static void VerifyRoom(Room success)
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
        }
    }
}