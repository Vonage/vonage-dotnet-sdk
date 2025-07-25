﻿#region
using AutoFixture;
using Vonage.SubAccounts.TransferAmount;
using Vonage.Test.Common.Extensions;
using Xunit;
#endregion

namespace Vonage.Test.SubAccounts.TransferAmount;

[Trait("Category", "Request")]
public class RequestTest
{
    private readonly Fixture fixture;
    public RequestTest() => this.fixture = new Fixture();

    [Fact]
    public void ReqeustUri_ShouldReturnApiEndpoint_GivenBalanceTransfer() =>
        TransferAmountRequest
            .Build()
            .WithFrom(this.fixture.Create<string>())
            .WithTo(this.fixture.Create<string>())
            .WithAmount(this.fixture.Create<decimal>())
            .Create()
            .Map(request => request.WithApiKey("489dsSS564652"))
            .Map(request => request.WithEndpoint(TransferAmountRequest.BalanceTransfer))
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess("/accounts/489dsSS564652/balance-transfers");

    [Fact]
    public void ReqeustUri_ShouldReturnApiEndpoint_GivenCreditTransfer() =>
        TransferAmountRequest
            .Build()
            .WithFrom(this.fixture.Create<string>())
            .WithTo(this.fixture.Create<string>())
            .WithAmount(this.fixture.Create<decimal>())
            .Create()
            .Map(request => request.WithApiKey("489dsSS564652"))
            .Map(request => request.WithEndpoint(TransferAmountRequest.CreditTransfer))
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess("/accounts/489dsSS564652/credit-transfers");

    [Fact]
    public void ReqeustUri_ShouldReturnApiEndpoint_GivenKeyAndEndpointAreMissing() =>
        TransferAmountRequest
            .Build()
            .WithFrom(this.fixture.Create<string>())
            .WithTo(this.fixture.Create<string>())
            .WithAmount(this.fixture.Create<decimal>())
            .Create()
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess("/accounts//");
}