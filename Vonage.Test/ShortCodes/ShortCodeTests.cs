#region
using System.Threading.Tasks;
using Vonage.Request;
using Vonage.Serialization;
using Vonage.ShortCodes;
using Vonage.Test.Common;
using Xunit;
#endregion

namespace Vonage.Test.ShortCodes;

[Trait("Category", "Legacy")]
public class ShortCodeTests : TestBase
{
    private readonly SerializationTestHelper helper = new SerializationTestHelper(typeof(ShortCodeTests).Namespace,
        JsonSerializerBuilder.BuildWithCamelCase());

    [Fact]
    public async Task ManageOptIn()
    {
        this.Setup($"{this.RestUrl}/sc/us/alert/opt-in/manage/json?msisdn=15559301529&", this.helper.GetResponseJson());
        var response = await this.BuildShortCodesClient()
            .ManageOptInAsync(ShortCodeTestData.CreateOptInManageRequest());
        response.ShouldMatchExpectedOptInResponse();
    }

    [Fact]
    public async Task QueryOptIns()
    {
        this.Setup($"{this.RestUrl}/sc/us/alert/opt-in/query/json", this.helper.GetResponseJson());
        var response = await this.BuildShortCodesClient()
            .QueryOptInsAsync(ShortCodeTestData.CreateBasicOptInQueryRequest());
        response.ShouldMatchExpectedOptInQueryResponse();
    }

    [Fact]
    public async Task SendAlert()
    {
        this.Setup($"{this.RestUrl}/sc/us/alert/json?to=16365553226&", this.helper.GetResponseJson());
        var response = await this.BuildShortCodesClient().SendAlertAsync(ShortCodeTestData.CreateBasicAlertRequest());
        response.ShouldMatchExpectedAlertResponse();
    }

    [Fact]
    public async Task SendTwoFactorAuth()
    {
        this.Setup($"{this.RestUrl}/sc/us/2fa/json", this.helper.GetResponseJson());
        var response = await this.BuildShortCodesClient()
            .SendTwoFactorAuthAsync(ShortCodeTestData.CreateTwoFactorAuthRequest());
        response.ShouldMatchExpectedTwoFactorAuthResponse();
    }

    private IShortCodesClient BuildShortCodesClient() =>
        this.BuildVonageClient(Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret)).ShortCodesClient;
}