#region
using FluentAssertions;
using Vonage.SubAccounts.GetSubAccount;
using Vonage.Test.Common.Extensions;
using Xunit;
#endregion

namespace Vonage.Test.SubAccounts.GetSubAccount;

[Trait("Category", "Request")]
public class RequestTest
{
    [Fact]
    public void GetEndpointPath_ShouldReturnApiEndpoint() =>
        GetSubAccountRequest.Parse("456iFuDL099")
            .Map(request => request.WithApiKey("123abCD"))
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess("/accounts/123abCD/subaccounts/456iFuDL099");

    [Fact]
    public void GetEndpointPath_ShouldReturnApiEndpoint_WithoutPrimaryAccountKeyKey() =>
        GetSubAccountRequest.Parse("456iFuDL099")
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess("/accounts//subaccounts/456iFuDL099");

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Parse_ShouldReturnFailure_GivenRoomIdIsNullOrWhitespace(string value) =>
        GetSubAccountRequest.Parse(value)
            .Should()
            .BeParsingFailure("SubAccountKey cannot be null or whitespace.");

    [Fact]
    public void Parse_ShouldReturnSuccess_GivenValuesAreProvided() =>
        GetSubAccountRequest.Parse("123456789")
            .Should()
            .BeSuccess(request => request.SubAccountKey.Should().Be("123456789"));
}