#region
using System;
using System.Net;
using System.Threading.Tasks;
using Vonage.Test.Common.Extensions;
using Vonage.VerifyV2.UpdateTemplate;
using WireMock.ResponseBuilders;
using Xunit;
#endregion

namespace Vonage.Test.VerifyV2.UpdateTemplate;

[Trait("Category", "E2E")]
public class E2ETest() : E2EBase(typeof(E2ETest).Namespace)
{
    [Fact]
    public async Task UpdateTemplate()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v2/verify/templates/68c2b32e-55ba-4a8e-b3fa-43b3ae6cd1fb")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .WithBody(this.Serialization.GetRequestJson(nameof(SerializationTest.ShouldSerialize)))
                .UsingPatch())
            .RespondWith(Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
        await this.Helper.VonageClient.VerifyV2Client.UpdateTemplateAsync(UpdateTemplateRequest.Build()
                .WithId(new Guid("68c2b32e-55ba-4a8e-b3fa-43b3ae6cd1fb"))
                .WithName("my-template")
                .SetAsDefaultTemplate()
                .Create())
            .Should()
            .BeSuccessAsync(SerializationTest.VerifyExpectedResponse);
    }

    [Fact]
    public async Task UpdateTemplateWithEmptyBody()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v2/verify/templates/68c2b32e-55ba-4a8e-b3fa-43b3ae6cd1fb")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .WithBody(this.Serialization.GetRequestJson(nameof(SerializationTest.ShouldSerializeEmpty)))
                .UsingPatch())
            .RespondWith(Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
        await this.Helper.VonageClient.VerifyV2Client.UpdateTemplateAsync(UpdateTemplateRequest.Build()
                .WithId(new Guid("68c2b32e-55ba-4a8e-b3fa-43b3ae6cd1fb"))
                .Create())
            .Should()
            .BeSuccessAsync(SerializationTest.VerifyExpectedResponse);
    }
}