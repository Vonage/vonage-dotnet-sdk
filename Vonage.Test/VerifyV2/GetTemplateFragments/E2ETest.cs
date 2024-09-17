#region
using System;
using System.Net;
using System.Threading.Tasks;
using Vonage.Test.Common.Extensions;
using Vonage.VerifyV2.GetTemplateFragments;
using WireMock.ResponseBuilders;
using Xunit;
#endregion

namespace Vonage.Test.VerifyV2.GetTemplateFragments;

[Trait("Category", "E2E")]
public class E2ETest : E2EBase
{
    public E2ETest() : base(typeof(E2ETest).Namespace)
    {
    }

    [Fact]
    public async Task GetTemplateFragmentsEmpty()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v2/verify/templates/68c2b32e-55ba-4a8e-b3fa-43b3ae6cd1fb/template_fragments")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
        await this.Helper.VonageClient.VerifyV2Client
            .GetTemplateFragmentsAsync(GetTemplateFragmentsRequest
                .Build()
                .WithTemplateId(RequestBuilderTest.ValidTemplateId)
                .Create())
            .Should()
            .BeSuccessAsync(SerializationTest.VerifyExpectedResponse);
    }

    [Fact]
    public async Task GetTemplatesEmptyFromResponse()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v2/verify/templates/68c2b32e-55ba-4a8e-b3fa-43b3ae6cd1fb/template_fragments")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
        await this.Helper.VonageClient.VerifyV2Client
            .GetTemplateFragmentsAsync(
                new GetTemplateFragmentsHalLink(new Uri(
                        "https://api.nexmo.com/v2/verify/templates/68c2b32e-55ba-4a8e-b3fa-43b3ae6cd1fb/template_fragments"))
                    .BuildRequest())
            .Should()
            .BeSuccessAsync(SerializationTest.VerifyExpectedResponse);
    }

    [Fact]
    public async Task GetTemplatesFromResponse()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v2/verify/templates/68c2b32e-55ba-4a8e-b3fa-43b3ae6cd1fb/template_fragments")
                .WithParam("page_size", "15")
                .WithParam("page", "50")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
        await this.Helper.VonageClient.VerifyV2Client
            .GetTemplateFragmentsAsync(
                new GetTemplateFragmentsHalLink(new Uri(
                        "https://api.nexmo.com/v2/verify/templates/68c2b32e-55ba-4a8e-b3fa-43b3ae6cd1fb/template_fragments?page_size=15&page=50"))
                    .BuildRequest())
            .Should()
            .BeSuccessAsync(SerializationTest.VerifyExpectedResponse);
    }

    [Fact]
    public async Task GetTemplates()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v2/verify/templates/68c2b32e-55ba-4a8e-b3fa-43b3ae6cd1fb/template_fragments")
                .WithParam("page_size", "15")
                .WithParam("page", "50")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
        await this.Helper.VonageClient.VerifyV2Client
            .GetTemplateFragmentsAsync(GetTemplateFragmentsRequest
                .Build()
                .WithTemplateId(RequestBuilderTest.ValidTemplateId)
                .WithPageSize(15).WithPage(50).Create())
            .Should()
            .BeSuccessAsync(SerializationTest.VerifyExpectedResponse);
    }
}