#region
using System.Threading.Tasks;
using Vonage.Applications;
using Vonage.Common.Monads;
using Vonage.Request;
using Vonage.Serialization;
using Vonage.Test.Common;
using Xunit;
#endregion

namespace Vonage.Test.Applications;

[Trait("Category", "Legacy")]
public class ApplicationTests : TestBase
{
    private readonly SerializationTestHelper helper = new SerializationTestHelper(typeof(ApplicationTests).Namespace,
        JsonSerializerBuilder.BuildWithCamelCase());

    private Credentials Credentials => Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);

    [Fact]
    public async Task CreateApplication()
    {
        this.Setup($"{this.ApiUrl}/v2/applications", this.helper.GetResponseJson(), this.helper.GetRequestJson());
        var response = await this.BuildApplicationClient().CreateApplicationAsync(ApplicationTestData.CreateRequest());
        response.ShouldMatchExpectedApplication();
    }

    [Fact]
    public async Task CreateVideoApplication()
    {
        this.Setup($"{this.ApiUrl}/v2/applications", this.helper.GetResponseJson(), this.helper.GetRequestJson());
        var response = await this.BuildApplicationClient()
            .CreateApplicationAsync(ApplicationTestData.CreateVideoRequest());
        response.ShouldMatchVideoApplication();
    }

    [Fact]
    public async Task CreateVideoFullApplication()
    {
        this.Setup($"{this.ApiUrl}/v2/applications", this.helper.GetResponseJson(), this.helper.GetRequestJson());
        var response = await this.BuildApplicationClient()
            .CreateApplicationAsync(ApplicationTestData.CreateVideoFullRequest());
        response.ShouldMatchVideoFullApplication();
    }

    [Fact]
    public async Task DeleteApplication()
    {
        this.Setup($"{this.ApiUrl}/v2/applications/78d335fa323d01149c3dd6f0d48968cf", Maybe<string>.Some(""));
        var result = await this.BuildApplicationClient().DeleteApplicationAsync("78d335fa323d01149c3dd6f0d48968cf");
        Assert.True(result);
    }

    [Fact]
    public async Task GetApplication()
    {
        this.Setup($"{this.ApiUrl}/v2/applications/78d335fa323d01149c3dd6f0d48968cf", this.helper.GetResponseJson());
        var application = await this.BuildApplicationClient().GetApplicationAsync("78d335fa323d01149c3dd6f0d48968cf");
        application.ShouldMatchExpectedApplication();
    }

    [Fact]
    public async Task ListApplications()
    {
        this.Setup($"{this.ApiUrl}/v2/applications", this.helper.GetResponseJson());
        var applications = await this.BuildApplicationClient().ListApplicationsAsync(new ListApplicationsRequest());
        applications.ShouldHaveExpectedApplicationInList();
    }

    [Fact]
    public async Task ListApplications_WithPagination()
    {
        this.Setup($"{this.ApiUrl}/v2/applications?page_size=10&page=1&",
            this.helper.GetResponseJson(nameof(this.ListApplications)));
        var applications = await this.BuildApplicationClient()
            .ListApplicationsAsync(new ListApplicationsRequest {Page = 1, PageSize = 10});
        applications.ShouldHaveExpectedApplicationInList();
    }

    [Fact]
    public async Task UpdateApplication()
    {
        this.Setup($"{this.ApiUrl}/v2/applications/78d335fa323d01149c3dd6f0d48968cf", this.helper.GetResponseJson(),
            this.helper.GetRequestJson());
        var response = await this.BuildApplicationClient().UpdateApplicationAsync("78d335fa323d01149c3dd6f0d48968cf",
            ApplicationTestData.CreateRequest());
        response.ShouldMatchExpectedApplication();
    }

    private IApplicationClient BuildApplicationClient() => this.BuildVonageClient(this.Credentials).ApplicationClient;
}