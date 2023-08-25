using System;
using System.Net;
using System.Threading.Tasks;
using Vonage.Common.Test.Extensions;
using Vonage.Meetings.Common;
using Vonage.Meetings.UpdateRoom;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.Unit.Meetings.UpdateRoom
{
    public class E2ETest : E2EBase
    {
        public E2ETest() : base(typeof(E2ETest).Namespace)
        {
        }

        [Fact]
        public async Task UpdateRoom()
        {
            this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath("/v1/meetings/rooms/e86a7335-35fe-45e1-b961-5777d4748022")
                    .WithHeader("Authorization", "Bearer *")
                    .WithBody(this.Serialization.GetRequestJson(nameof(SerializationTest.ShouldSerialize)))
                    .UsingPatch())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
            await this.Helper.VonageClient.MeetingsClient.UpdateRoomAsync(
                    UpdateRoomRequest
                        .Build()
                        .WithRoomId(new Guid("e86a7335-35fe-45e1-b961-5777d4748022"))
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
                        .Create())
                .Should()
                .BeSuccessAsync(SerializationTest.VerifyRoom);
        }
    }
}