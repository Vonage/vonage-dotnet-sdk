using System;
using System.Net;
using System.Threading.Tasks;
using Vonage.Common.Test.Extensions;
using Vonage.Server;
using Vonage.Video.Broadcast.ChangeBroadcastLayout;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.Unit.Video.Broadcast.ChangeBroadcastLayout
{
    [Trait("Category", "E2E")]
    public class E2ETest : E2EBase
    {
        public E2ETest() : base(typeof(E2ETest).Namespace)
        {
        }

        [Fact]
        public async Task ChangeBroadcastLayout()
        {
            this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath(
                        "/v2/project/5e782e3b-9f63-426f-bd2e-b7d618d546cd/broadcast/97425ae1-4722-4dbf-b395-6169f08ebab3/layout")
                    .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                    .WithBody(this.Serialization.GetRequestJson(nameof(SerializationTest.ShouldSerialize)))
                    .UsingPost())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK));
            await this.Helper.VonageClient.VideoClient.BroadcastClient.ChangeBroadcastLayoutAsync(ChangeBroadcastLayoutRequest
                    .Build()
                    .WithApplicationId(Guid.Parse("5e782e3b-9f63-426f-bd2e-b7d618d546cd"))
                    .WithBroadcastId(Guid.Parse("97425ae1-4722-4dbf-b395-6169f08ebab3"))
                    .WithLayout(new Layout(LayoutType.Pip,
                        "stream.instructor {position: absolute; width: 100%;  height:50%;}", LayoutType.BestFit))
                    .Create())
                .Should()
                .BeSuccessAsync();
        }
    }
}