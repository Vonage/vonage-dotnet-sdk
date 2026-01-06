#region
using System;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.TestHelpers;
using WireMock.ResponseBuilders;
using Xunit;
#endregion

namespace Vonage.Test.Accounts;

[Trait("Category", "Legacy")]
public class AccountTest : IDisposable
{
    private readonly TestingContext context = TestingContext.WithBasicCredentials();

    private readonly SerializationTestHelper helper = new SerializationTestHelper(typeof(AccountTest).Namespace,
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

    [Fact]
    public async Task CreateApiSecret()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath($"/accounts/{this.context.VonageClient.Credentials.ApiKey}/secrets")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingPost())
            .RespondWith(this.RespondWithSuccess());
        var secret = await this.context.VonageClient.AccountClient
            .CreateApiSecretAsync(AccountTestData.CreateBasicSecretRequest(),
                this.context.VonageClient.Credentials.ApiKey);
        secret.ShouldMatchExpectedSecret();
    }

    [Fact]
    public async Task GetAccountBalance()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/account/get-balance")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(this.RespondWithSuccess());
        var balance = await this.context.VonageClient.AccountClient.GetAccountBalanceAsync();
        balance.ShouldMatchExpectedBalance();
    }

    [Fact]
    public async Task RetrieveApiSecrets()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath($"/accounts/{this.context.VonageClient.Credentials.ApiKey}/secrets")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(this.RespondWithSuccess());
        var secrets =
            await this.context.VonageClient.AccountClient.RetrieveApiSecretsAsync(this.context.VonageClient.Credentials
                .ApiKey);
        secrets.ShouldMatchExpectedSecretsResult();
    }

    [Fact]
    public async Task RetrieveSecret()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath(
                    $"/accounts/{this.context.VonageClient.Credentials.ApiKey}/secrets/ad6dc56f-07b5-46e1-a527-85530e625800")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(this.RespondWithSuccess());
        var secret = await this.context.VonageClient.AccountClient
            .RetrieveApiSecretAsync("ad6dc56f-07b5-46e1-a527-85530e625800",
                this.context.VonageClient.Credentials.ApiKey);
        secret.ShouldMatchExpectedSecret();
    }

    [Fact]
    public async Task RevokeSecret()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath(
                    $"/accounts/{this.context.VonageClient.Credentials.ApiKey}/secrets/ad6dc56f-07b5-46e1-a527-85530e625800")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingDelete())
            .RespondWith(this.RespondWithSuccess());
        var response = await this.context.VonageClient.AccountClient
            .RevokeApiSecretAsync("ad6dc56f-07b5-46e1-a527-85530e625800", this.context.VonageClient.Credentials.ApiKey);
        response.ShouldBeSuccessfulRevocation();
    }

    [Fact]
    public async Task SetSettings()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/account/settings")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .WithBody(
                    $"moCallBackUrl={WebUtility.UrlEncode("https://example.com/webhooks/inbound-sms")}&drCallBackUrl={WebUtility.UrlEncode("https://example.com/webhooks/delivery-receipt")}&")
                .UsingPost())
            .RespondWith(this.RespondWithSuccess());
        var result = await this.context.VonageClient.AccountClient
            .ChangeAccountSettingsAsync(AccountTestData.CreateBasicSettingsRequest());
        result.ShouldMatchExpectedAccountSettings();
    }

    [Fact]
    public async Task TopUp()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/account/top-up")
                .WithParam("trx", "00X123456Y7890123Z")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(this.RespondWithSuccess());
        var response = await this.context.VonageClient.AccountClient
            .TopUpAccountBalanceAsync(AccountTestData.CreateBasicTopUpRequest());
        response.ShouldMatchExpectedTopUpResult();
    }
}