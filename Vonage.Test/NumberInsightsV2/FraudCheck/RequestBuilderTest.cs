using FluentAssertions;
using Vonage.NumberInsightV2.FraudCheck;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.NumberInsightsV2.FraudCheck;

[Trait("Category", "Request")]
public class RequestBuilderTest
{
    private const string FraudScoreInsight = "fraud_score";
    private const string SimSwapInsight = "sim_swap";
    private const string ValidPhone = "447009000000";

    [Fact]
    public void Build_ShouldAddFraudScoreInsight() =>
        BuildValidRequest()
            .WithFraudScore()
            .Create()
            .Map(request => request.Insights)
            .Should()
            .BeSuccess(insights => insights.Should().BeEquivalentTo(FraudScoreInsight));

    [Fact]
    public void Build_ShouldAddSimSwapInsight() =>
        BuildValidRequest()
            .WithSimSwap()
            .Create()
            .Map(request => request.Insights)
            .Should()
            .BeSuccess(insights => insights.Should().BeEquivalentTo(SimSwapInsight));

    [Fact]
    public void Build_ShouldNotDuplicateInsights() =>
        BuildValidRequest()
            .WithFraudScore()
            .WithFraudScore()
            .WithSimSwap()
            .WithSimSwap()
            .Create()
            .Map(request => request.Insights)
            .Should()
            .BeSuccess(insights => insights.Should().BeEquivalentTo(FraudScoreInsight, SimSwapInsight));

    [Fact]
    public void Build_ShouldReturnFailure_GivenNoInsightHasBeenSelected() =>
        BuildValidRequest()
            .Create()
            .Should()
            .BeParsingFailure("Insights cannot be empty.");

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Build_ShouldReturnFailure_GivenPhoneIsNullOrWhitespace(string invalidPhoneNumber) =>
        FraudCheckRequest
            .Build()
            .WithPhone(invalidPhoneNumber)
            .WithFraudScore()
            .WithSimSwap()
            .Create()
            .Should()
            .BeParsingFailure("Number cannot be null or whitespace.");

    [Fact]
    public void Build_ShouldSetPhone() =>
        BuildValidRequest()
            .WithFraudScore()
            .WithSimSwap()
            .Create()
            .Map(request => request.Phone.Number)
            .Should()
            .BeSuccess(ValidPhone);

    private static IBuilderForOptional BuildValidRequest() =>
        FraudCheckRequest
            .Build()
            .WithPhone(ValidPhone);
}