using System;
using System.Net;
using System.Threading.Tasks;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Video.Broadcast.RemoveStreamFromBroadcast;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Server.Test.Video.Broadcast.RemoveStreamFromBroadcast
{
    [Trait("Category", "E2E")]
    public class E2ETest : E2EBase
    {
        public E2ETest() : base(typeof(E2ETest).Namespace)
        {
        }

        [Fact]
        public async Task RemoveStreamFromBroadcast()
        {
            this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath(
                        "/v2/project/5e782e3b-9f63-426f-bd2e-b7d618d546cd/broadcast/97425ae1-4722-4dbf-b395-6169f08ebab3/streams")
                    .WithHeader("Authorization", "Bearer *")
                    .WithBody(this.Serialization.GetRequestJson(nameof(SerializationTest.ShouldSerialize)))
                    .UsingPatch())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK));
            await this.Helper.VonageClient.BroadcastClient.RemoveStreamFromBroadcastAsync(
                    RemoveStreamFromBroadcastRequest.Build()
                        .WithApplicationId(Guid.Parse("5e782e3b-9f63-426f-bd2e-b7d618d546cd"))
                        .WithBroadcastId(Guid.Parse("97425ae1-4722-4dbf-b395-6169f08ebab3"))
                        .WithStreamId(new Guid("12312312-3811-4726-b508-e41a0f96c68f"))
                        .Create())
                .Should()
                .BeSuccessAsync();
        }
    }
}