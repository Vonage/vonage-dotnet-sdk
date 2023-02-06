﻿using System;
using FluentAssertions;
using Vonage.Common;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Meetings.Common;
using Vonage.Meetings.GetAvailableRooms;
using Xunit;

namespace Vonage.Test.Unit.Meetings.GetAvailableRooms
{
    public class GetAvailableRoomsSerializationTest
    {
        private readonly SerializationTestHelper helper;

        public GetAvailableRoomsSerializationTest() =>
            this.helper = new SerializationTestHelper(typeof(GetAvailableRoomsSerializationTest).Namespace,
                JsonSerializer.BuildWithSnakeCase());

        [Fact]
        public void ShouldDeserialize200() =>
            this.helper.Serializer
                .DeserializeObject<GetAvailableRoomsResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(success =>
                {
                    success.PageSize.Should().Be(10);
                    success.TotalItems.Should().Be(30);
                    success.Links.First.Href.Should().Be("https://api.nexmo.com/v0.1/meetings/rooms?page_size=10");
                    success.Links.Previous.Href.Should()
                        .Be("https://api.nexmo.com/v0.1/meetings/rooms?page_size=10&end_id=20");
                    success.Links.Next.Href.Should()
                        .Be("https://api.nexmo.com/v0.1/meetings/rooms?page_size=10&start_id=30");
                    success.Links.Self.Href.Should()
                        .Be("https://api.nexmo.com/v0.1/meetings/rooms?page_size=10&start_id=20");
                    success.Embedded.Rooms.Count.Should().Be(1);
                    success.Embedded.Rooms[0].CallbackUrls.RecordingsCallbackUrl.Should().Be("https://example.com");
                    success.Embedded.Rooms[0].CallbackUrls.RoomsCallbackUrl.Should().Be("https://example.com");
                    success.Embedded.Rooms[0].CallbackUrls.SessionsCallbackUrl.Should().Be("https://example.com");
                    success.Embedded.Rooms[0].AvailableFeatures.IsChatAvailable.Should().BeTrue();
                    success.Embedded.Rooms[0].AvailableFeatures.IsRecordingAvailable.Should().BeTrue();
                    success.Embedded.Rooms[0].AvailableFeatures.IsWhiteboardAvailable.Should().BeTrue();
                    success.Embedded.Rooms[0].Id.Should().Be(new Guid("934f95c2-28e5-486b-ab8e-1126dbc180f9"));
                    success.Embedded.Rooms[0].Metadata.Should().Be("abc123");
                    success.Embedded.Rooms[0].Recording.AutoRecord.Should().BeFalse();
                    success.Embedded.Rooms[0].Recording.RecordOnlyOwner.Should().BeFalse();
                    success.Embedded.Rooms[0].Type.Should().Be(RoomType.Instant);
                    success.Embedded.Rooms[0].CreatedAt.Should().Be(new DateTime(2023, 02, 06, 11, 13, 50));
                    success.Embedded.Rooms[0].DisplayName.Should().Be("abc123");
                    success.Embedded.Rooms[0].ExpiresAt.Should().Be(new DateTime(2023, 02, 06, 11, 13, 50));
                    success.Embedded.Rooms[0].IsAvailable.Should().BeFalse();
                    success.Embedded.Rooms[0].MeetingCode.Should().Be("123456789");
                    success.Embedded.Rooms[0].ThemeId.Should().Be("abc123");
                    success.Embedded.Rooms[0].ExpiresAfterUse.Should().BeFalse();
                    success.Embedded.Rooms[0].InitialJoinOptions.MicrophoneState.Should()
                        .Be(RoomMicrophoneState.Default);
                    success.Embedded.Rooms[0].JoinApprovalLevel.Should().Be(RoomApprovalLevel.None);
                    success.Embedded.Rooms[0].Links.GuestUrl.Href.Should().Be("https://meetings.vonage.com/123456789");
                    success.Embedded.Rooms[0].Links.HostUrl.Href.Should()
                        .Be("https://meetings.vonage.com/123456789?participant_token=xyz");
                });
    }
}