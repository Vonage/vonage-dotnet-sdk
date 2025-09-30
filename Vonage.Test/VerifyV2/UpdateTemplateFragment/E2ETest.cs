#region
using System.Net;
using System.Threading.Tasks;
using Vonage.Test.Common.Extensions;
using Vonage.VerifyV2.UpdateTemplateFragment;
using WireMock.ResponseBuilders;
using Xunit;
#endregion

namespace Vonage.Test.VerifyV2.UpdateTemplateFragment;

[Trait("Category", "E2E")]
public class E2ETest() : E2EBase(typeof(E2ETest).Namespace)
{
    [Fact]
    public async Task UpdateTemplateFragment()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath(
                    "/v2/verify/templates/68c2b32e-55ba-4a8e-b3fa-43b3ae6cd1fb/template_fragments/c41a9862-93d6-4c15-b5eb-d5ea6d574654")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .WithBody(this.Serialization.GetRequestJson(nameof(SerializationTest.ShouldSerialize)))
                .UsingPatch())
            .RespondWith(Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
        await this.Helper.VonageClient.VerifyV2Client.UpdateTemplateFragmentAsync(UpdateTemplateFragmentRequest.Build()
                .WithId(RequestBuilderTest.ValidTemplateId)
                .WithFragmentId(RequestBuilderTest.ValidTemplateFragmentId)
                .WithText(RequestBuilderTest.ValidText)
                .Create())
            .Should()
            .BeSuccessAsync(SerializationTest.VerifyExpectedResponse);
    }
}