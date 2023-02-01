using FluentAssertions;
using Vonage.Common;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Meetings.Common;
using Xunit;

namespace Vonage.Test.Unit.Meetings.UpdateRoom
{
    public class UpdateRoomDeserializationTest
    {
        private readonly SerializationTestHelper helper;

        public UpdateRoomDeserializationTest() =>
            this.helper = new SerializationTestHelper(typeof(UpdateRoomDeserializationTest).Namespace,
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
    }
}