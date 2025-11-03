#region
using System.Net;
using System.Threading.Tasks;
using Vonage.Accounts;
using Vonage.Request;
using Vonage.Serialization;
using Vonage.Test.Common;
using Xunit;
#endregion

namespace Vonage.Test.Accounts;

[Trait("Category", "Legacy")]
public class AccountTest : TestBase
{
    private readonly SerializationTestHelper helper = new SerializationTestHelper(typeof(AccountTest).Namespace,
        JsonSerializerBuilder.BuildWithCamelCase());

    [Fact]
    public async Task CreateApiSecret()
    {
        this.Setup($"https://api.nexmo.com/accounts/{this.ApiKey}/secrets", this.helper.GetResponseJson());
        var secret = await this.BuildAccountClient()
            .CreateApiSecretAsync(AccountTestData.CreateBasicSecretRequest(), this.ApiKey);
        secret.ShouldMatchExpectedSecret();
    }

    [Fact]
    public async Task GetAccountBalance()
    {
        this.Setup($"{this.RestUrl}/account/get-balance", this.helper.GetResponseJson());
        var balance = await this.BuildAccountClient().GetAccountBalanceAsync();
        balance.ShouldMatchExpectedBalance();
    }

    [Fact]
    public async Task RetrieveApiSecrets()
    {
        this.Setup($"https://api.nexmo.com/accounts/{this.ApiKey}/secrets", this.helper.GetResponseJson());
        var secrets = await this.BuildAccountClient().RetrieveApiSecretsAsync(this.ApiKey);
        secrets.ShouldMatchExpectedSecretsResult();
    }

    [Fact]
    public async Task RetrieveSecret()
    {
        this.Setup($"https://api.nexmo.com/accounts/{this.ApiKey}/secrets/ad6dc56f-07b5-46e1-a527-85530e625800",
            this.helper.GetResponseJson());
        var secret = await this.BuildAccountClient()
            .RetrieveApiSecretAsync("ad6dc56f-07b5-46e1-a527-85530e625800", this.ApiKey);
        secret.ShouldMatchExpectedSecret();
    }

    [Fact]
    public async Task RevokeSecret()
    {
        this.Setup($"https://api.nexmo.com/accounts/{this.ApiKey}/secrets/ad6dc56f-07b5-46e1-a527-85530e625800",
            this.helper.GetResponseJson());
        var response = await this.BuildAccountClient()
            .RevokeApiSecretAsync("ad6dc56f-07b5-46e1-a527-85530e625800", this.ApiKey);
        response.ShouldBeSuccessfulRevocation();
    }

    [Fact]
    public async Task SetSettings()
    {
        this.Setup($"{this.RestUrl}/account/settings", this.helper.GetResponseJson(),
            $"moCallBackUrl={WebUtility.UrlEncode("https://example.com/webhooks/inbound-sms")}&drCallBackUrl={WebUtility.UrlEncode("https://example.com/webhooks/delivery-receipt")}&");
        var result = await this.BuildAccountClient()
            .ChangeAccountSettingsAsync(AccountTestData.CreateBasicSettingsRequest());
        result.ShouldMatchExpectedAccountSettings();
    }

    [Fact]
    public async Task TopUp()
    {
        this.Setup($"{this.RestUrl}/account/top-up?trx=00X123456Y7890123Z&", this.helper.GetResponseJson());
        var response = await this.BuildAccountClient()
            .TopUpAccountBalanceAsync(AccountTestData.CreateBasicTopUpRequest());
        response.ShouldMatchExpectedTopUpResult();
    }

    private IAccountClient BuildAccountClient() =>
        this.BuildVonageClient(Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret)).AccountClient;
}