using System;
using System.Net;
using System.Threading.Tasks;
using Vonage.Meetings.GetRoomsByTheme;
using Vonage.Test.Common.Extensions;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.Meetings.GetRoomsByTheme
{
    public class E2ETest : E2EBase
    {
        public E2ETest() : base(typeof(E2ETest).Namespace)
        {
        }

        [Fact]
        public async Task GetRoomsByTheme()
        {
            this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath("/v1/meetings/themes/cf7f7327-c8f3-4575-b113-0598571b499a/rooms")
                    .WithParam("page_size", "15")
                    .WithParam("start_id", "15")
                    .WithParam("end_id", "60")
                    .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                    .UsingGet())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
            await this.Helper.VonageClient.MeetingsClient.GetRoomsByThemeAsync(
                    GetRoomsByThemeRequest.Build()
                        .WithThemeId(new Guid("cf7f7327-c8f3-4575-b113-0598571b499a"))
                        .WithPageSize(15)
                        .WithStartId(15)
                        .WithEndId(60)
                        .Create())
                .Should()
                .BeSuccessAsync(SerializationTest.VerifyRooms);
        }
    }
}