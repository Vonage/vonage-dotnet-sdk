#region
using System;
using System.Net;
using System.Threading.Tasks;
using Vonage.Test.Common.Extensions;
using Vonage.Video.Sessions.ChangeStreamLayout;
using WireMock.ResponseBuilders;
using Xunit;
#endregion

namespace Vonage.Test.Video.Sessions.ChangeStreamLayout;

[Trait("Category", "E2E")]
public class E2ETest : E2EBase
{
    public E2ETest() : base(typeof(E2ETest).Namespace)
    {
    }

    [Fact]
    public async Task ChangeStreamLayout()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath(
                    "/v2/project/5e782e3b-9f63-426f-bd2e-b7d618d546cd/session/flR1ZSBPY3QgMjkgMTI6MTM6MjMgUERUIDIwMTN/stream")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .WithBody(this.Serialization.GetRequestJson(nameof(SerializationTest.ShouldSerialize)))
                .UsingPut())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK));
        await this.Helper.VonageClient.VideoClient.SessionClient.ChangeStreamLayoutAsync(ChangeStreamLayoutRequest
                .Build()
                .WithApplicationId(Guid.Parse("5e782e3b-9f63-426f-bd2e-b7d618d546cd"))
                .WithSessionId("flR1ZSBPY3QgMjkgMTI6MTM6MjMgUERUIDIwMTN")
                .WithItems(new[]
                {
                    new ChangeStreamLayoutRequest.LayoutItem("8b732909-0a06-46a2-8ea8-074e64d43422",
                        new[] {"full"}),
                })
                .Create())
            .Should()
            .BeSuccessAsync();
    }
}