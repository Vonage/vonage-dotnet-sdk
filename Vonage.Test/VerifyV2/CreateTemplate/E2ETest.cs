#region
using System.Net;
using System.Threading.Tasks;
using Vonage.Test.Common.Extensions;
using Vonage.VerifyV2.CreateTemplate;
using WireMock.ResponseBuilders;
using Xunit;
#endregion

namespace Vonage.Test.VerifyV2.CreateTemplate;

[Trait("Category", "E2E")]
public class E2ETest : E2EBase
{
    public E2ETest() : base(typeof(E2ETest).Namespace)
    {
    }

    [Fact]
    public async Task CreateTemplate()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v2/verify/templates")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .WithBody(this.Serialization.GetRequestJson(nameof(SerializationTest.ShouldSerialize)))
                .UsingPost())
            .RespondWith(Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
        await this.Helper.VonageClient.VerifyV2Client.CreateTemplateAsync(CreateTemplateRequest.Build()
                .WithName("my-template").Create())
            .Should()
            .BeSuccessAsync(SerializationTest.VerifyExpectedResponse);
    }
}