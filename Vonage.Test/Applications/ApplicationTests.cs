#region
using System;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Vonage.Applications;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.TestHelpers;
using WireMock.ResponseBuilders;
using Xunit;
#endregion

namespace Vonage.Test.Applications;

[Trait("Category", "Legacy")]
public class ApplicationTests : IDisposable
{
    private readonly TestingContext context = TestingContext.WithBasicCredentials();

    private readonly SerializationTestHelper helper = new SerializationTestHelper(typeof(ApplicationTests).Namespace,
        JsonSerializerBuilder.BuildWithCamelCase());

    public void Dispose()
    {
        this.context?.Dispose();
        GC.SuppressFinalize(this);
    }

    private IResponseBuilder RespondWithSuccess([CallerMemberName] string testName = null) =>
        Response.Create()
            .WithStatusCode(HttpStatusCode.OK)
            .WithBody(this.helper.GetResponseJson(testName));

    private IResponseBuilder RespondWithRequestJson([CallerMemberName] string testName = null) =>
        Response.Create()
            .WithStatusCode(HttpStatusCode.OK)
            .WithBody(this.helper.GetResponseJson(testName));

    [Fact]
    public async Task CreateApplication()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v2/applications")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .WithBodyAsJson(this.helper.GetRequestJson())
                .UsingPost())
            .RespondWith(this.RespondWithRequestJson());
        var response =
            await this.context.VonageClient.ApplicationClient.CreateApplicationAsync(
                ApplicationTestData.CreateRequest());
        response.ShouldMatchExpectedApplication();
    }

    [Fact]
    public async Task CreateVideoApplication()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v2/applications")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .WithBodyAsJson(this.helper.GetRequestJson())
                .UsingPost())
            .RespondWith(this.RespondWithRequestJson());
        var response = await this.context.VonageClient.ApplicationClient
            .CreateApplicationAsync(ApplicationTestData.CreateVideoRequest());
        response.ShouldMatchVideoApplication();
    }

    [Fact]
    public async Task CreateVideoFullApplication()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v2/applications")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .WithBodyAsJson(this.helper.GetRequestJson())
                .UsingPost())
            .RespondWith(this.RespondWithRequestJson());
        var response = await this.context.VonageClient.ApplicationClient
            .CreateApplicationAsync(ApplicationTestData.CreateVideoFullRequest());
        response.ShouldMatchVideoFullApplication();
    }

    [Fact]
    public async Task DeleteApplication()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v2/applications/78d335fa323d01149c3dd6f0d48968cf")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingDelete())
            .RespondWith(Response.Create()
                .WithStatusCode(HttpStatusCode.NoContent)
                .WithBody(""));
        var result =
            await this.context.VonageClient.ApplicationClient.DeleteApplicationAsync(
                "78d335fa323d01149c3dd6f0d48968cf");
        Assert.True(result);
    }

    [Fact]
    public async Task GetApplication()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v2/applications/78d335fa323d01149c3dd6f0d48968cf")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(this.RespondWithSuccess());
        var application =
            await this.context.VonageClient.ApplicationClient.GetApplicationAsync("78d335fa323d01149c3dd6f0d48968cf");
        application.ShouldMatchExpectedApplication();
    }

    [Fact]
    public async Task ListApplications()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v2/applications")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(this.RespondWithSuccess());
        var applications =
            await this.context.VonageClient.ApplicationClient.ListApplicationsAsync(new ListApplicationsRequest());
        applications.ShouldHaveExpectedApplicationInList();
    }

    [Fact]
    public async Task ListApplications_WithPagination()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v2/applications")
                .WithParam("page_size", "10")
                .WithParam("page", "1")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.helper.GetResponseJson(nameof(this.ListApplications))));
        var applications = await this.context.VonageClient.ApplicationClient
            .ListApplicationsAsync(new ListApplicationsRequest {Page = 1, PageSize = 10});
        applications.ShouldHaveExpectedApplicationInList();
    }

    [Fact]
    public async Task UpdateApplication()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v2/applications/78d335fa323d01149c3dd6f0d48968cf")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .WithBodyAsJson(this.helper.GetRequestJson())
                .UsingPut())
            .RespondWith(this.RespondWithRequestJson());
        var response = await this.context.VonageClient.ApplicationClient.UpdateApplicationAsync(
            "78d335fa323d01149c3dd6f0d48968cf",
            ApplicationTestData.CreateRequest());
        response.ShouldMatchExpectedApplication();
    }
}