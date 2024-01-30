using System;
using System.Net;
using System.Threading.Tasks;
using Vonage.Meetings.GetRoom;
using Vonage.Test.Common.Extensions;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.Meetings.GetRoom;

[Trait("Category", "E2E")]
public class E2ETest : E2EBase
{
    public E2ETest() : base(typeof(E2ETest).Namespace)
    {
    }

    [Fact]
    public async Task GetRoom()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v1/meetings/rooms/934f95c2-28e5-486b-ab8e-1126dbc180f9")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
        await this.Helper.VonageClient.MeetingsClient.GetRoomAsync(
                GetRoomRequest.Parse(new Guid("934f95c2-28e5-486b-ab8e-1126dbc180f9")))
            .Should()
            .BeSuccessAsync(SerializationTest.VerifyRoom);
    }
}