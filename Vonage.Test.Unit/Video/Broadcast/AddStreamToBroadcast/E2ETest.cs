using System;
using System.Net;
using System.Threading.Tasks;
using Vonage.Test.Unit.Common.Extensions;
using Vonage.Video.Broadcast.AddStreamToBroadcast;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.Unit.Video.Broadcast.AddStreamToBroadcast
{
    [Trait("Category", "E2E")]
    public class E2ETest : E2EBase
    {
        public E2ETest() : base(typeof(E2ETest).Namespace)
        {
        }

        [Fact]
        public async Task AddDefaultStreamToBroadcast()
        {
            this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath(
                        "/v2/project/5e782e3b-9f63-426f-bd2e-b7d618d546cd/broadcast/97425ae1-4722-4dbf-b395-6169f08ebab3/streams")
                    .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                    .WithBody(this.Serialization.GetRequestJson(nameof(SerializationTest
                        .ShouldSerializeWithDefaultValues)))
                    .UsingPatch())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK));
            await this.Helper.VonageClient.VideoClient.BroadcastClient.AddStreamToBroadcastAsync(
                    AddStreamToBroadcastRequest.Build()
                        .WithApplicationId(Guid.Parse("5e782e3b-9f63-426f-bd2e-b7d618d546cd"))
                        .WithBroadcastId(Guid.Parse("97425ae1-4722-4dbf-b395-6169f08ebab3"))
                        .WithStreamId(Guid.Parse("12312312-3811-4726-b508-e41a0f96c68f"))
                        .Create())
                .Should()
                .BeSuccessAsync();
        }

        [Fact]
        public async Task AddStreamToBroadcast()
        {
            this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath(
                        "/v2/project/5e782e3b-9f63-426f-bd2e-b7d618d546cd/broadcast/97425ae1-4722-4dbf-b395-6169f08ebab3/streams")
                    .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                    .WithBody(this.Serialization.GetRequestJson(nameof(SerializationTest.ShouldSerialize)))
                    .UsingPatch())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK));
            await this.Helper.VonageClient.VideoClient.BroadcastClient.AddStreamToBroadcastAsync(
                    AddStreamToBroadcastRequest.Build()
                        .WithApplicationId(Guid.Parse("5e782e3b-9f63-426f-bd2e-b7d618d546cd"))
                        .WithBroadcastId(Guid.Parse("97425ae1-4722-4dbf-b395-6169f08ebab3"))
                        .WithStreamId(Guid.Parse("12312312-3811-4726-b508-e41a0f96c68f"))
                        .WithDisabledAudio()
                        .WithDisabledVideo()
                        .Create())
                .Should()
                .BeSuccessAsync();
        }
    }
}