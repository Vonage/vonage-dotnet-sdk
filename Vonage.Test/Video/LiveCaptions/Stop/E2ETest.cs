#region
using System;
using System.Net;
using System.Threading.Tasks;
using Vonage.Test.Common.Extensions;
using Vonage.Video.LiveCaptions.Stop;
using WireMock.ResponseBuilders;
using Xunit;
#endregion

namespace Vonage.Test.Video.LiveCaptions.Stop;

[Trait("Category", "E2E")]
public class E2ETest() : E2EBase(typeof(E2ETest).Namespace)
{
    [Fact]
    public async Task Stop()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v2/project/e3e78a75-221d-41ec-8846-25ae3db1943a/captions/CAP-123/stop")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .UsingDelete())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK));
        await this.Helper.VonageClient.VideoClient.LiveCaptionsClient
            .StopAsync(StopRequest.Parse(new Guid("e3e78a75-221d-41ec-8846-25ae3db1943a"), "CAP-123"))
            .Should()
            .BeSuccessAsync();
    }
}