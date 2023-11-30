using System;
using System.Net;
using System.Threading.Tasks;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Video.Archives.AddStream;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Server.Test.Video.Archives.AddStream
{
    [Trait("Category", "E2E")]
    public class E2ETest : E2EBase
    {
        public E2ETest() : base(typeof(E2ETest).Namespace)
        {
        }

        [Fact]
        public async Task AddStream()
        {
            this.SetUpServer(nameof(SerializationTest.ShouldSerialize));
            await this.Helper.VonageClient.ArchiveClient.AddStreamAsync(GetRequestBuilder().Create())
                .Should()
                .BeSuccessAsync();
        }

        [Fact]
        public async Task AddStreamWithoutAudio()
        {
            this.SetUpServer(nameof(SerializationTest.ShouldSerializeWithoutAudio));
            await this.Helper.VonageClient.ArchiveClient.AddStreamAsync(GetRequestBuilder().DisableAudio().Create())
                .Should()
                .BeSuccessAsync();
        }

        [Fact]
        public async Task AddStreamWithoutVideo()
        {
            this.SetUpServer(nameof(SerializationTest.ShouldSerializeWithoutVideo));
            await this.Helper.VonageClient.ArchiveClient.AddStreamAsync(GetRequestBuilder().DisableVideo().Create())
                .Should()
                .BeSuccessAsync();
        }

        private static IBuilderForOptional GetRequestBuilder() =>
            AddStreamRequest
                .Build()
                .WithApplicationId(Guid.Parse("5e782e3b-9f63-426f-bd2e-b7d618d546cd"))
                .WithArchiveId(Guid.Parse("97425ae1-4722-4dbf-b395-6169f08ebab3"))
                .WithStreamId(Guid.Parse("12312312-3811-4726-b508-e41a0f96c68f"));

        private void SetUpServer(string request)
        {
            this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath(
                        "/v2/project/5e782e3b-9f63-426f-bd2e-b7d618d546cd/archive/97425ae1-4722-4dbf-b395-6169f08ebab3/streams")
                    .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                    .WithBody(this.Serialization.GetRequestJson(request))
                    .UsingPatch())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK));
        }
    }
}