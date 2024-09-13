#region
using System;
using System.Net;
using System.Threading.Tasks;
using Vonage.Test.Common.Extensions;
using Vonage.VerifyV2.DeleteTemplateFragment;
using WireMock.ResponseBuilders;
using Xunit;
#endregion

namespace Vonage.Test.VerifyV2.DeleteTemplateFragment;

[Trait("Category", "E2E")]
public class E2ETest : E2EBase
{
    [Fact]
    public async Task CancelVerification()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath(
                    "/v2/verify/templates/f3a065af-ac5a-47a4-8dfe-819561a7a287/template_fragments/7e4fea73-afe6-4c34-b3e9-8b5ce2e2253a")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .UsingDelete())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK));
        await this.Helper.VonageClient.VerifyV2Client.DeleteTemplateFragmentAsync(
                DeleteTemplateFragmentRequest.Build()
                    .WithTemplateId(new Guid("f3a065af-ac5a-47a4-8dfe-819561a7a287"))
                    .WithTemplateFragmentId(new Guid("7e4fea73-afe6-4c34-b3e9-8b5ce2e2253a"))
                    .Create())
            .Should()
            .BeSuccessAsync();
    }
}