using System;
using System.Net;
using System.Threading.Tasks;
using Vonage.Test.Common.Extensions;
using Vonage.VerifyV2.NextWorkflow;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.VerifyV2.NextWorkflow;

[Trait("Category", "E2E")]
public class E2ETest : E2EBase
{
    [Fact]
    public async Task NextWorkflow()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v2/verify/68c2b32e-55ba-4a8e-b3fa-43b3ae6cd1fb/next_workflow")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .UsingPost())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK));
        await this.Helper.VonageClient.VerifyV2Client.NextWorkflowAsync(
                NextWorkflowRequest.Parse(Guid.Parse("68c2b32e-55ba-4a8e-b3fa-43b3ae6cd1fb")))
            .Should()
            .BeSuccessAsync();
    }
}