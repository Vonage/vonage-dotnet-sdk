using System;
using System.Net;
using System.Threading.Tasks;
using Vonage.Test.Common.Extensions;
using Vonage.Video.ExperienceComposer.Stop;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.Video.ExperienceComposer.Stop;

[Trait("Category", "E2E")]
public class E2ETest : E2EBase
{
    public E2ETest() : base(typeof(E2ETest).Namespace)
    {
    }

    [Fact]
    public async Task Stop()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v2/project/e3e78a75-221d-41ec-8846-25ae3db1943a/render/EXP-123")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .UsingDelete())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK));
        await this.Helper.VonageClient.VideoClient.ExperienceComposerClient
            .StopAsync(StopRequest.Parse(new Guid("e3e78a75-221d-41ec-8846-25ae3db1943a"), "EXP-123"))
            .Should()
            .BeSuccessAsync();
    }
}