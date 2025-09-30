#region
using System;
using System.Net;
using System.Threading.Tasks;
using Vonage.Test.Common.Extensions;
using Vonage.VerifyV2.GetTemplateFragment;
using WireMock.ResponseBuilders;
using Xunit;
#endregion

namespace Vonage.Test.VerifyV2.GetTemplateFragment;

[Trait("Category", "E2E")]
public class E2ETest() : E2EBase(typeof(E2ETest).Namespace)
{
    [Fact]
    public async Task GetTemplateFragment()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath(
                    "/v2/verify/templates/f3a065af-ac5a-47a4-8dfe-819561a7a287/template_fragments/68c2b32e-55ba-4a8e-b3fa-43b3ae6cd1fb")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
        await this.Helper.VonageClient.VerifyV2Client
            .GetTemplateFragmentAsync(GetTemplateFragmentRequest.Parse(new Guid("f3a065af-ac5a-47a4-8dfe-819561a7a287"),
                new Guid("68c2b32e-55ba-4a8e-b3fa-43b3ae6cd1fb")))
            .Should()
            .BeSuccessAsync(SerializationTest.VerifyExpectedResponse);
    }
}