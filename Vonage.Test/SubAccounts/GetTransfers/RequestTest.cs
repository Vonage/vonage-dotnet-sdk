#region
using System;
using Vonage.SubAccounts.GetTransfers;
using Vonage.Test.Common.Extensions;
using Xunit;
#endregion

namespace Vonage.Test.SubAccounts.GetTransfers;

[Trait("Category", "Request")]
public class RequestTest
{
    private readonly DateTimeOffset endDate;
    private readonly DateTimeOffset startDate;
    private readonly string subAccountKey;

    public RequestTest()
    {
        this.startDate = DateTimeOffset.Parse("2018-03-02T17:34:49Z");
        this.endDate = DateTimeOffset.Parse("2020-06-30T12:00:00Z");
        this.subAccountKey = "123AZs456";
    }

    [Fact]
    public void RequestUri_ShouldReturnApiEndpoint() =>
        GetTransfersRequest
            .Build()
            .WithStartDate(this.startDate)
            .Create()
            .Map(request => request.WithApiKey("489dsSS564652"))
            .Map(request => request.WithEndpoint(GetTransfersRequest.CreditTransfer))
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess("/accounts/489dsSS564652/credit-transfers?start_date=2018-03-02T17%3A34%3A49Z");

    [Fact]
    public void RequestUri_ShouldReturnApiEndpoint_GivenAllParametersAreProvided() =>
        GetTransfersRequest
            .Build()
            .WithStartDate(this.startDate)
            .WithEndDate(this.endDate)
            .WithSubAccountKey(this.subAccountKey)
            .Create()
            .Map(request => request.WithApiKey("489dsSS564652"))
            .Map(request => request.WithEndpoint(GetTransfersRequest.CreditTransfer))
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess(
                "/accounts/489dsSS564652/credit-transfers?start_date=2018-03-02T17%3A34%3A49Z&end_date=2020-06-30T12%3A00%3A00Z&subaccount=123AZs456");

    [Fact]
    public void RequestUri_ShouldReturnApiEndpoint_GivenEndDateIsProvided() =>
        GetTransfersRequest
            .Build()
            .WithStartDate(this.startDate)
            .WithEndDate(this.endDate)
            .Create()
            .Map(request => request.WithApiKey("489dsSS564652"))
            .Map(request => request.WithEndpoint(GetTransfersRequest.CreditTransfer))
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess(
                "/accounts/489dsSS564652/credit-transfers?start_date=2018-03-02T17%3A34%3A49Z&end_date=2020-06-30T12%3A00%3A00Z");

    [Fact]
    public void RequestUri_ShouldReturnApiEndpoint_GivenKeyAndEndpointAreMissing() =>
        GetTransfersRequest
            .Build()
            .WithStartDate(this.startDate)
            .Create()
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess("/accounts//?start_date=2018-03-02T17%3A34%3A49Z");

    [Fact]
    public void RequestUri_ShouldReturnApiEndpoint_GivenSubAccountKeyIsProvided() =>
        GetTransfersRequest
            .Build()
            .WithStartDate(this.startDate)
            .WithSubAccountKey(this.subAccountKey)
            .Create()
            .Map(request => request.WithApiKey("489dsSS564652"))
            .Map(request => request.WithEndpoint(GetTransfersRequest.CreditTransfer))
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess(
                "/accounts/489dsSS564652/credit-transfers?start_date=2018-03-02T17%3A34%3A49Z&subaccount=123AZs456");
}