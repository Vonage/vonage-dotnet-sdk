using FluentAssertions;
using Vonage.Common;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Meetings.Common;
using Vonage.Meetings.CreateRoom;
using Xunit;

namespace Vonage.Test.Unit.Meetings.CreateRoom
{
    public class CreateRoomSerializationTest
    {
        private readonly SerializationTestHelper helper;

        public CreateRoomSerializationTest() =>
            this.helper = new SerializationTestHelper(typeof(CreateRoomSerializationTest).Namespace,
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
                    success.Id.Should().Be("abc123");
                    success.Metadata.Should().Be("abc123");
                    success.Recording.AutoRecord.Should().BeFalse();
                    success.Recording.RecordOnlyOwner.Should().BeFalse();
                    success.Type.Should().Be(RoomType.Instant);
                    success.CreatedAt.Should().Be("abc123");
                    success.DisplayName.Should().Be("abc123");
                    success.ExpiresAt.Should().Be("abc123");
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
            CreateRoomRequestBuilder
                .Build("string")
                .WithMetadata("string")
                .WithRoomType(RoomType.Instant)
                .WithExpiresAt("2019-08-24")
                .ExpireAfterUse()
                .WithThemeId("ef2b46f3-8ebb-437e-a671-272e4990fbc8")
                .WithApprovalLevel(RoomApprovalLevel.None)
                .WithRecordingOptions(new Room.RecordingOptions {AutoRecord = true, RecordOnlyOwner = true})
                .WithInitialJoinOptions(new Room.JoinOptions {MicrophoneState = RoomMicrophoneState.Default})
                .WithFeatures(new Room.Features
                    {IsChatAvailable = true, IsRecordingAvailable = true, IsWhiteboardAvailable = true})
                .WithCallback(new Room.Callback
                {
                    RecordingsCallbackUrl = "https://example.com", SessionsCallbackUrl = "https://example.com",
                    RoomsCallbackUrl = "https://example.com",
                })
                .Create()
                .GetStringContent()
                .Should()
                .BeSuccess(this.helper.GetRequestJson());
    }
}