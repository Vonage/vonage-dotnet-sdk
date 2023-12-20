using System.Net;
using System.Threading.Tasks;
using Vonage.Meetings.GetRooms;
using Vonage.Test.Unit.Common.Extensions;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.Unit.Meetings.GetRooms
{
    public class E2ETest : E2EBase
    {
        public E2ETest() : base(typeof(E2ETest).Namespace)
        {
        }

        [Fact]
        public async Task GetRooms()
        {
            this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath("/v1/meetings/rooms")
                    .WithParam("page_size", "15")
                    .WithParam("start_id", "15")
                    .WithParam("end_id", "60")
                    .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                    .UsingGet())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
            await this.Helper.VonageClient.MeetingsClient.GetRoomsAsync(
                    GetRoomsRequest.Build()
                        .WithPageSize(15)
                        .WithStartId(15)
                        .WithEndId(60)
                        .Create())
                .Should()
                .BeSuccessAsync(SerializationTest.VerifyRooms);
        }
    }
}