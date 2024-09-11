#region
using System;
using System.Net;
using System.Threading.Tasks;
using Vonage.Test.Common.Extensions;
using Vonage.VerifyV2.GetTemplate;
using WireMock.ResponseBuilders;
using Xunit;
#endregion

namespace Vonage.Test.VerifyV2.GetTemplate;

[Trait("Category", "E2E")]
public class E2ETest : E2EBase
{
    public E2ETest() : base(typeof(E2ETest).Namespace)
    {
    }

    [Fact]
    public async Task GetTemplate()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v2/verify/templates/68c2b32e-55ba-4a8e-b3fa-43b3ae6cd1fb")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
        await this.Helper.VonageClient.VerifyV2Client
            .GetTemplateAsync(GetTemplateRequest.Parse(new Guid("68c2b32e-55ba-4a8e-b3fa-43b3ae6cd1fb")))
            .Should()
            .BeSuccessAsync(SerializationTest.VerifyExpectedResponse);
    }
}