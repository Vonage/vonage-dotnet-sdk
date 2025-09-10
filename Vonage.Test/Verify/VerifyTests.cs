#region
using System.Threading.Tasks;
using Vonage.Request;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Verify;
using Xunit;
#endregion

namespace Vonage.Test.Verify;

[Trait("Category", "Legacy")]
public class VerifyTests : TestBase
{
    private readonly SerializationTestHelper helper = new SerializationTestHelper(typeof(VerifyTests).Namespace,
        JsonSerializerBuilder.BuildWithCamelCase());

    [Fact]
    public async Task Psd2Verification()
    {
        this.Setup($"{this.ApiUrl}/verify/psd2/json", this.helper.GetResponseJson());
        var response = await this.BuildVerifyClient()
            .VerifyRequestWithPSD2Async(VerifyTestData.CreateBasicPsd2Request());
        response.ShouldMatchExpectedVerifyResponse();
    }

    [Fact]
    public async Task RequestVerification()
    {
        this.Setup($"{this.ApiUrl}/verify/json", this.helper.GetResponseJson());
        var response = await this.BuildVerifyClient().VerifyRequestAsync(VerifyTestData.CreateBasicVerifyRequest());
        response.ShouldMatchExpectedVerifyResponse();
    }

    [Fact]
    public async Task TestCheckVerification()
    {
        this.Setup($"{this.ApiUrl}/verify/check/json", this.helper.GetResponseJson());
        var response = await this.BuildVerifyClient().VerifyCheckAsync(VerifyTestData.CreateBasicVerifyCheckRequest());
        response.ShouldMatchExpectedVerifyCheckResponse();
    }

    [Fact]
    public async Task TestControlVerify()
    {
        this.Setup($"{this.ApiUrl}/verify/control/json", this.helper.GetResponseJson());
        var response = await this.BuildVerifyClient().VerifyControlAsync(VerifyTestData.CreateVerifyControlRequest());
        response.ShouldMatchExpectedVerifyControlResponse();
    }

    [Fact]
    public async Task TestControlVerifyInvalidCredentials()
    {
        this.Setup($"{this.ApiUrl}/verify/control/json", this.helper.GetResponseJson());
        var ex = await Assert.ThrowsAsync<VonageVerifyResponseException>(() =>
            this.BuildVerifyClient().VerifyControlAsync(VerifyTestData.CreateVerifyControlRequest()));
        ex.ShouldThrowVerifyResponseException();
    }

    [Fact]
    public async Task TestVerifySearch()
    {
        this.Setup(
            $"{this.ApiUrl}/verify/search/json?request_id=abcdef0123456789abcdef0123456789&api_key={this.ApiKey}&api_secret={this.ApiSecret}&",
            this.helper.GetResponseJson());
        var response = await this.BuildVerifyClient().VerifySearchAsync(VerifyTestData.CreateVerifySearchRequest());
        response.ShouldMatchExpectedVerifySearchResponse();
    }

    private IVerifyClient BuildVerifyClient() =>
        this.BuildVonageClient(Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret)).VerifyClient;
}