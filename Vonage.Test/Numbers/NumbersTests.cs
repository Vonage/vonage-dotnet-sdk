#region
using System.Threading.Tasks;
using FluentAssertions;
using Vonage.Numbers;
using Vonage.Request;
using Vonage.Serialization;
using Vonage.Test.Common;
using Xunit;
#endregion

namespace Vonage.Test.Numbers;

[Trait("Category", "Legacy")]
public class NumbersTests : TestBase
{
    private readonly SerializationTestHelper helper = new SerializationTestHelper(typeof(NumbersTests).Namespace,
        JsonSerializerBuilder.BuildWithCamelCase());

    [Fact]
    public async Task BuyNumber()
    {
        this.Setup($"{this.RestUrl}/number/buy", this.helper.GetResponseJson(), "country=GB&msisdn=447700900000&");
        var response = await this.BuildNumbersClient().BuyANumberAsync(NumbersTestData.CreateBasicTransactionRequest());
        response.ShouldBeSuccessfulTransaction();
    }

    [Fact]
    public async Task CancelNumber()
    {
        this.Setup($"{this.RestUrl}/number/cancel", this.helper.GetResponseJson(), "country=GB&msisdn=447700900000&");
        var response = await this.BuildNumbersClient()
            .CancelANumberAsync(NumbersTestData.CreateBasicTransactionRequest());
        response.ShouldBeSuccessfulTransaction();
    }

    [Fact]
    public async Task FailedPurchase()
    {
        this.Setup($"{this.RestUrl}/number/buy", this.helper.GetResponseJson(), "country=GB&msisdn=447700900000&");
        var act = () => this.BuildNumbersClient().BuyANumberAsync(NumbersTestData.CreateBasicTransactionRequest());
        (await act.Should().ThrowExactlyAsync<VonageNumberResponseException>()).Which.Response.Should()
            .BeEquivalentTo(new NumberTransactionResponse
                {ErrorCode = "401", ErrorCodeLabel = "authentication failed"});
    }

    [Fact]
    public async Task GetAvailableNumbers()
    {
        this.Setup($"{this.RestUrl}/number/search?country=GB&", this.helper.GetResponseJson());
        var response = await this.BuildNumbersClient()
            .GetAvailableNumbersAsync(NumbersTestData.CreateBasicSearchRequest());
        response.ShouldMatchBasicNumberSearch();
    }

    [Fact]
    public async Task GetAvailableNumbersWithAdditionalData()
    {
        this.Setup($"{this.RestUrl}/number/search?country=GB&", this.helper.GetResponseJson());
        var response = await this.BuildNumbersClient()
            .GetAvailableNumbersAsync(NumbersTestData.CreateBasicSearchRequest());
        response.ShouldMatchNumberSearchWithAdditionalData();
    }

    [Fact]
    public async Task GetOwnedNumbers()
    {
        this.Setup($"{this.RestUrl}/account/numbers?country=GB&", this.helper.GetResponseJson());
        var response = await this.BuildNumbersClient().GetOwnedNumbersAsync(NumbersTestData.CreateBasicSearchRequest());
        response.ShouldMatchOwnedNumbersBasic();
    }

    [Fact]
    public async Task GetOwnedNumbersWithAdditionalData()
    {
        this.Setup($"{this.RestUrl}/account/numbers?country=GB&", this.helper.GetResponseJson());
        var response = await this.BuildNumbersClient().GetOwnedNumbersAsync(NumbersTestData.CreateBasicSearchRequest());
        response.ShouldMatchOwnedNumbersWithAdditionalData();
    }

    [Fact]
    public async Task UpdateNumber()
    {
        this.Setup($"{this.RestUrl}/number/update",
            this.helper.GetResponseJson(), "country=GB&msisdn=447700900000&");
        var response = await this.BuildNumbersClient().UpdateANumberAsync(NumbersTestData.CreateBasicUpdateRequest());
        response.ShouldBeSuccessfulTransaction();
    }

    private INumbersClient BuildNumbersClient() =>
        this.BuildVonageClient(Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret)).NumbersClient;
}