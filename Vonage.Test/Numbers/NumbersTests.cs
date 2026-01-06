#region
using System;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using FluentAssertions;
using Vonage.Numbers;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.TestHelpers;
using WireMock.ResponseBuilders;
using Xunit;
#endregion

namespace Vonage.Test.Numbers;

[Trait("Category", "Legacy")]
public class NumbersTests : IDisposable
{
    private readonly TestingContext context = TestingContext.WithBasicCredentials();

    private readonly SerializationTestHelper helper = new SerializationTestHelper(typeof(NumbersTests).Namespace,
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

    private INumbersClient BuildNumbersClient() => this.context.VonageClient.NumbersClient;

    [Fact]
    public async Task BuyNumber()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/number/buy")
                .WithBody("country=GB&msisdn=447700900000&")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingPost())
            .RespondWith(this.RespondWithSuccess());
        var response = await this.BuildNumbersClient().BuyANumberAsync(NumbersTestData.CreateBasicTransactionRequest());
        response.ShouldBeSuccessfulTransaction();
    }

    [Fact]
    public async Task CancelNumber()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/number/cancel")
                .WithBody("country=GB&msisdn=447700900000&")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingPost())
            .RespondWith(this.RespondWithSuccess());
        var response = await this.BuildNumbersClient()
            .CancelANumberAsync(NumbersTestData.CreateBasicTransactionRequest());
        response.ShouldBeSuccessfulTransaction();
    }

    [Fact]
    public async Task FailedPurchase()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/number/buy")
                .WithBody("country=GB&msisdn=447700900000&")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingPost())
            .RespondWith(this.RespondWithSuccess());
        var act = () => this.BuildNumbersClient().BuyANumberAsync(NumbersTestData.CreateBasicTransactionRequest());
        (await act.Should().ThrowExactlyAsync<VonageNumberResponseException>()).Which.Response.Should()
            .BeEquivalentTo(new NumberTransactionResponse
                {ErrorCode = "401", ErrorCodeLabel = "authentication failed"});
    }

    [Fact]
    public async Task GetAvailableNumbers()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/number/search")
                .WithParam("country", "GB")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(this.RespondWithSuccess());
        var response = await this.BuildNumbersClient()
            .GetAvailableNumbersAsync(NumbersTestData.CreateBasicSearchRequest());
        response.ShouldMatchBasicNumberSearch();
    }

    [Fact]
    public async Task GetAvailableNumbersWithAdditionalData()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/number/search")
                .WithParam("country", "GB")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(this.RespondWithSuccess());
        var response = await this.BuildNumbersClient()
            .GetAvailableNumbersAsync(NumbersTestData.CreateBasicSearchRequest());
        response.ShouldMatchNumberSearchWithAdditionalData();
    }

    [Fact]
    public async Task GetOwnedNumbers()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/account/numbers")
                .WithParam("country", "GB")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(this.RespondWithSuccess());
        var response = await this.BuildNumbersClient().GetOwnedNumbersAsync(NumbersTestData.CreateBasicSearchRequest());
        response.ShouldMatchOwnedNumbersBasic();
    }

    [Fact]
    public async Task GetOwnedNumbersWithAdditionalData()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/account/numbers")
                .WithParam("country", "GB")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(this.RespondWithSuccess());
        var response = await this.BuildNumbersClient().GetOwnedNumbersAsync(NumbersTestData.CreateBasicSearchRequest());
        response.ShouldMatchOwnedNumbersWithAdditionalData();
    }

    [Fact]
    public async Task UpdateNumber()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/number/update")
                .WithBody("country=GB&msisdn=447700900000&")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingPost())
            .RespondWith(this.RespondWithSuccess());
        var response = await this.BuildNumbersClient().UpdateANumberAsync(NumbersTestData.CreateBasicUpdateRequest());
        response.ShouldBeSuccessfulTransaction();
    }
}