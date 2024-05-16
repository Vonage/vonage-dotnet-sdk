using System;
using Vonage.Common.Monads;
using Vonage.NumberInsightV2.FraudCheck;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.NumberInsightsV2.FraudCheck;

[Trait("Category", "Serialization")]
public class SerializationTest
{
    private readonly SerializationTestHelper helper = new SerializationTestHelper(
        typeof(SerializationTest).Namespace,
        JsonSerializerBuilder.BuildWithSnakeCase());
    
    [Fact]
    public void ShouldDeserialize200() => this.helper.Serializer
        .DeserializeObject<FraudCheckResponse>(this.helper.GetResponseJson())
        .Should()
        .BeSuccess(GetExpectedFraudCheckResponse());
    
    [Fact]
    public void ShouldDeserialize200WithoutFraudScore() => this.helper.Serializer
        .DeserializeObject<FraudCheckResponse>(this.helper.GetResponseJson())
        .Should()
        .BeSuccess(GetExpectedFraudCheckResponseWithoutFraudScore());
    
    [Fact]
    public void ShouldDeserialize200WithoutSimSwap() => this.helper.Serializer
        .DeserializeObject<FraudCheckResponse>(this.helper.GetResponseJson())
        .Should()
        .BeSuccess(GetExpectedFraudCheckResponseWithoutSimSwap());
    
    [Fact]
    public void ShouldSerializeWithFraudScore() => BuildRequestWithFraudScore()
        .GetStringContent()
        .Should()
        .BeSuccess(this.helper.GetRequestJson());
    
    [Fact]
    public void ShouldSerializeWithFraudScoreAndSimSwap() => BuildRequestWithFraudScoreAndSimSwap()
        .GetStringContent()
        .Should()
        .BeSuccess(this.helper.GetRequestJson());
    
    [Fact]
    public void ShouldSerializeWithSimSwap() => BuildRequestWithSimSwap()
        .GetStringContent()
        .Should()
        .BeSuccess(this.helper.GetRequestJson());
    
    private static FraudCheckResponse GetExpectedFraudCheckResponseWithoutFraudScore() => new FraudCheckResponse(
        new Guid("6cb4c489-0fc8-4c40-8c3d-95e7e74f9450"),
        "phone",
        new PhoneData("16197363066", "Orange France", "MOBILE"),
        Maybe<FraudScore>.None,
        Maybe<NumberInsightV2.FraudCheck.SimSwap>.Some(new NumberInsightV2.FraudCheck.SimSwap(SimSwapStatus.Completed,
            true, "Mobile Network Operator Not Supported")));
    
    private static FraudCheckResponse GetExpectedFraudCheckResponseWithoutSimSwap() => new FraudCheckResponse(
        new Guid("6cb4c489-0fc8-4c40-8c3d-95e7e74f9450"),
        "phone",
        new PhoneData("16197363066", "Orange France", "MOBILE"),
        Maybe<FraudScore>.Some(new FraudScore("54", RiskRecommendation.Block, FraudScoreLabel.Low, "completed")),
        Maybe<NumberInsightV2.FraudCheck.SimSwap>.None);
    
    internal static Result<FraudCheckRequest> BuildRequestWithFraudScore() =>
        FraudCheckRequest.Build()
            .WithPhone("447009000000")
            .WithFraudScore()
            .Create();
    
    internal static Result<FraudCheckRequest> BuildRequestWithFraudScoreAndSimSwap() =>
        FraudCheckRequest.Build()
            .WithPhone("447009000000")
            .WithFraudScore()
            .WithSimSwap()
            .Create();
    
    internal static Result<FraudCheckRequest> BuildRequestWithSimSwap() =>
        FraudCheckRequest.Build()
            .WithPhone("447009000000")
            .WithSimSwap()
            .Create();
    
    internal static FraudCheckResponse GetExpectedFraudCheckResponse() => new FraudCheckResponse(
        new Guid("6cb4c489-0fc8-4c40-8c3d-95e7e74f9450"),
        "phone",
        new PhoneData("16197363066", "Orange France", "MOBILE"),
        Maybe<FraudScore>.Some(new FraudScore("54", RiskRecommendation.Flag, FraudScoreLabel.Medium, "completed")),
        Maybe<NumberInsightV2.FraudCheck.SimSwap>.Some(new NumberInsightV2.FraudCheck.SimSwap(SimSwapStatus.Failed,
            true, "Mobile Network Operator Not Supported")));
}