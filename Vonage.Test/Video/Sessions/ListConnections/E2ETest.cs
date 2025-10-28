#region
using System;
using System.Net;
using System.Threading.Tasks;
using Vonage.Test.Common.Extensions;
using Vonage.Video.Sessions.ListConnections;
using WireMock.ResponseBuilders;
using Xunit;
#endregion

namespace Vonage.Test.Video.Sessions.ListConnections;

[Trait("Category", "E2E")]
public class E2ETest() : E2EBase(typeof(E2ETest).Namespace)
{
    [Fact]
    public async Task GetStreams()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath(
                    "/v2/project/e9f8c166-6c67-440d-994a-04fb6dfed007/session/b40ef09b-3811-4726-b508-e41a0f96c68f/connection")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
        await this.Helper.VonageClient.VideoClient.SessionClient.ListConnections(ListConnectionsRequest.Build()
                .WithApplicationId(Guid.Parse("e9f8c166-6c67-440d-994a-04fb6dfed007"))
                .WithSessionId("b40ef09b-3811-4726-b508-e41a0f96c68f")
                .Create())
            .Should()
            .BeSuccessAsync(SerializationTest.VerifyConnections);
    }
}