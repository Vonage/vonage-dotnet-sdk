using System.Net;
using System.Threading.Tasks;
using Vonage.Test.Common.Extensions;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.Video.ExperienceComposer.Start;

[Trait("Category", "E2E")]
public class E2ETest : E2EBase
{
    public E2ETest() : base(typeof(E2ETest).Namespace)
    {
    }

    [Fact]
    public async Task Start()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v2/project/e3e78a75-221d-41ec-8846-25ae3db1943a/render")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .WithBody(this.Serialization.GetRequestJson(nameof(SerializationTest.ShouldSerialize)))
                .UsingPost())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.Serialization.GetResponseJson(nameof(SessionSerializationTest.ShouldDeserialize200))));
        await this.Helper.VonageClient.VideoClient.ExperienceComposerClient
            .StartAsync(SerializationTest.BuildRequest())
            .Should()
            .BeSuccessAsync(SessionSerializationTest.BuildExpectedSession());
    }
}