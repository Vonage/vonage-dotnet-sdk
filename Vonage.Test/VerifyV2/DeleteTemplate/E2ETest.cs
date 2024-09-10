#region
using System;
using System.Net;
using System.Threading.Tasks;
using Vonage.Test.Common.Extensions;
using Vonage.VerifyV2.DeleteTemplate;
using WireMock.ResponseBuilders;
using Xunit;
#endregion

namespace Vonage.Test.VerifyV2.DeleteTemplate;

[Trait("Category", "E2E")]
public class E2ETest : E2EBase
{
    [Fact]
    public async Task CancelVerification()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v2/verify/templates/68c2b32e-55ba-4a8e-b3fa-43b3ae6cd1fb")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .UsingDelete())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK));
        await this.Helper.VonageClient.VerifyV2Client.DeleteTemplateAsync(
                DeleteTemplateRequest.Parse(Guid.Parse("68c2b32e-55ba-4a8e-b3fa-43b3ae6cd1fb")))
            .Should()
            .BeSuccessAsync();
    }
}