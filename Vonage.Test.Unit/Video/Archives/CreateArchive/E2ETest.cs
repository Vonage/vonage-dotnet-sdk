using System;
using System.Net;
using System.Threading.Tasks;
using Vonage.Server;
using Vonage.Test.Unit.Common.Extensions;
using Vonage.Video.Archives.CreateArchive;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.Unit.Video.Archives.CreateArchive
{
    [Trait("Category", "E2E")]
    public class E2ETest : E2EBase
    {
        public E2ETest() : base(typeof(E2ETest).Namespace)
        {
        }

        [Fact]
        public async Task CreateArchive()
        {
            this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath("/v2/project/5e782e3b-9f63-426f-bd2e-b7d618d546cd/archive")
                    .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                    .UsingPost())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
            await this.Helper.VonageClient.VideoClient.ArchiveClient.CreateArchiveAsync(CreateArchiveRequest.Build()
                    .WithApplicationId(Guid.Parse("5e782e3b-9f63-426f-bd2e-b7d618d546cd"))
                    .WithSessionId("flR1ZSBPY3QgMjkgMTI6MTM6MjMgUERUIDIwMTN")
                    .WithArchiveLayout(new Layout(LayoutType.Pip,
                        "stream.instructor {position: absolute; width: 100%;  height:50%;}", LayoutType.BestFit))
                    .WithName("Foo")
                    .WithOutputMode(OutputMode.Individual)
                    .WithRenderResolution(RenderResolution.HighDefinitionLandscape)
                    .WithStreamMode(StreamMode.Manual)
                    .DisableVideo()
                    .DisableAudio()
                    .Create())
                .Should()
                .BeSuccessAsync(SerializationTest.VerifyArchive);
        }

        [Fact]
        public async Task CreateDefaultArchive()
        {
            this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath("/v2/project/5e782e3b-9f63-426f-bd2e-b7d618d546cd/archive")
                    .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                    .UsingPost())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
            await this.Helper.VonageClient.VideoClient.ArchiveClient.CreateArchiveAsync(CreateArchiveRequest.Build()
                    .WithApplicationId(Guid.Parse("5e782e3b-9f63-426f-bd2e-b7d618d546cd"))
                    .WithSessionId("flR1ZSBPY3QgMjkgMTI6MTM6MjMgUERUIDIwMTN")
                    .Create())
                .Should()
                .BeSuccessAsync(SerializationTest.VerifyArchive);
        }
    }
}