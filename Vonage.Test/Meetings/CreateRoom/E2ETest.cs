using System;
using System.Net;
using System.Threading.Tasks;
using Vonage.Meetings.Common;
using Vonage.Meetings.CreateRoom;
using Vonage.Test.Common.Extensions;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.Meetings.CreateRoom
{
    public class E2ETest : E2EBase
    {
        public E2ETest() : base(typeof(E2ETest).Namespace)
        {
        }

        [Fact]
        public async Task CreateInstantRoom()
        {
            this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath("/v1/meetings/rooms")
                    .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                    .WithBody(this.Serialization.GetRequestJson(nameof(SerializationTest
                        .ShouldSerialize)))
                    .UsingPost())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
            await this.Helper.VonageClient.MeetingsClient.CreateRoomAsync(CreateRoomRequest
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
                    .Create())
                .Should()
                .BeSuccessAsync(SerializationTest.VerifyRoom);
        }

        [Fact]
        public async Task CreateLongTermRoom()
        {
            this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath("/v1/meetings/rooms")
                    .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                    .WithBody(this.Serialization.GetRequestJson(nameof(SerializationTest
                        .ShouldSerializeWithLongTermRoom)))
                    .UsingPost())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
            await this.Helper.VonageClient.MeetingsClient.CreateRoomAsync(CreateRoomRequest
                    .Build()
                    .WithDisplayName("string")
                    .AsLongTermRoom(new DateTime(2023, 02, 07, 20, 10, 05))
                    .ExpireAfterUse()
                    .Create())
                .Should()
                .BeSuccessAsync(SerializationTest.VerifyRoom);
        }

        [Fact]
        public async Task CreateRoomWithDefaultValues()
        {
            this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath("/v1/meetings/rooms")
                    .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                    .WithBody(this.Serialization.GetRequestJson(nameof(SerializationTest
                        .ShouldSerializeWithDefaultValues)))
                    .UsingPost())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
            await this.Helper.VonageClient.MeetingsClient.CreateRoomAsync(CreateRoomRequest
                    .Build()
                    .WithDisplayName("string")
                    .Create())
                .Should()
                .BeSuccessAsync(SerializationTest.VerifyRoom);
        }
    }
}