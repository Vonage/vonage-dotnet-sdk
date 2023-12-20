using System;
using System.Net;
using System.Threading.Tasks;
using Vonage.Test.Unit.Common.Extensions;
using Vonage.Video.Broadcast.GetBroadcast;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.Unit.Video.Broadcast.GetBroadcast
{
    [Trait("Category", "E2E")]
    public class E2ETest : E2EBase
    {
        public E2ETest() : base(typeof(E2ETest).Namespace)
        {
        }

        [Fact]
        public async Task GetBroadcast()
        {
            this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath(
                        "/v2/project/5e782e3b-9f63-426f-bd2e-b7d618d546cd/broadcast/97425ae1-4722-4dbf-b395-6169f08ebab3")
                    .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                    .UsingGet())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
            await this.Helper.VonageClient.VideoClient.BroadcastClient.GetBroadcastAsync(GetBroadcastRequest
                    .Build()
                    .WithApplicationId(Guid.Parse("5e782e3b-9f63-426f-bd2e-b7d618d546cd"))
                    .WithBroadcastId(Guid.Parse("97425ae1-4722-4dbf-b395-6169f08ebab3"))
                    .Create())
                .Should()
                .BeSuccessAsync(SerializationTest.VerifyBroadcast);
        }
    }
}