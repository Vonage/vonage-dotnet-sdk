#region
using Vonage.Common.Monads;
using Vonage.IdentityInsights.GetInsights;
using Vonage.Test.Common.Extensions;
using Xunit;
#endregion

namespace Vonage.Test.IdentityInsights.GetInsights;

[Trait("Category", "Request")]
[Trait("Product", "IdentityInsights")]
public class RequestBuilderTest
{
    internal const string ValidPhoneNumber = "33601020304";

    [Fact]
    public void Build_ShouldSetPhoneNumber() =>
        GetInsightsRequest.Build()
            .WithPhoneNumber(ValidPhoneNumber)
            .Create()
            .Map(request => request.PhoneNumber.Number)
            .Should()
            .BeSuccess(ValidPhoneNumber);

    [Fact]
    public void Build_ShouldReturnFailure_GivenPhoneNumberIsInvalid() =>
        GetInsightsRequest.Build()
            .WithPhoneNumber("dzdzzadzad")
            .Create()
            .Should()
            .BeResultFailure("Number can only contain digits.");

    [Fact]
    public void Build_ShouldEnableCurrentCarrier() =>
        GetInsightsRequest.Build()
            .WithPhoneNumber(ValidPhoneNumber)
            .WithCurrentCarrier()
            .Create()
            .Map(request => request.CurrentCarrier)
            .Should()
            .BeSuccess(true);

    [Fact]
    public void Build_ShouldHaveCurrentCarrierDisabled_GivenDefault() =>
        BuildRequestWithDefaultValues()
            .Map(request => request.CurrentCarrier)
            .Should()
            .BeSuccess(false);

    [Fact]
    public void Build_ShouldHaveOriginalCarrierDisabled_GivenDefault() =>
        BuildRequestWithDefaultValues()
            .Map(request => request.OriginalCarrier)
            .Should()
            .BeSuccess(false);

    [Fact]
    public void Build_ShouldEnableOriginalCarrier() =>
        GetInsightsRequest.Build()
            .WithPhoneNumber(ValidPhoneNumber)
            .WithOriginalCarrier()
            .Create()
            .Map(request => request.OriginalCarrier)
            .Should()
            .BeSuccess(true);

    [Fact]
    public void Build_ShouldHaveFormatDisabled_GivenDefault() =>
        BuildRequestWithDefaultValues()
            .Map(request => request.Format)
            .Should()
            .BeSuccess(false);

    [Fact]
    public void Build_ShouldEnableFormat() =>
        GetInsightsRequest.Build()
            .WithPhoneNumber(ValidPhoneNumber)
            .WithFormat()
            .Create()
            .Map(request => request.Format)
            .Should()
            .BeSuccess(true);

    [Fact]
    public void Build_ShouldHaveEmptyPurpose_GivenDefault() =>
        BuildRequestWithDefaultValues()
            .Map(request => request.Purpose)
            .Should()
            .BeSuccess(Maybe<string>.None);

    [Fact]
    public void Build_ShouldHaveEmptySimSwap_GivenDefault() =>
        BuildRequestWithDefaultValues()
            .Map(request => request.SimSwap)
            .Should()
            .BeSuccess(Maybe<SimSwapRequest>.None);

    [Fact]
    public void Build_ShouldSetSimSwap() =>
        GetInsightsRequest.Build()
            .WithPhoneNumber(ValidPhoneNumber)
            .WithSimSwap(new SimSwapRequest(50))
            .Create()
            .Map(request => request.SimSwap)
            .Should()
            .BeSuccess(new SimSwapRequest(50));

    [Fact]
    public void Build_ShouldReturnFailure_GivenSimSwapPeriodHigherThanMaximum() =>
        GetInsightsRequest.Build()
            .WithPhoneNumber(ValidPhoneNumber)
            .WithSimSwap(new SimSwapRequest(2401))
            .Create()
            .Should()
            .BeParsingFailure("Period cannot be higher than 2400.");

    [Fact]
    public void Build_ShouldReturnFailure_GivenSimSwapPeriodLowerThanMinimum() =>
        GetInsightsRequest.Build()
            .WithPhoneNumber(ValidPhoneNumber)
            .WithSimSwap(new SimSwapRequest(0))
            .Create()
            .Should()
            .BeParsingFailure("Period cannot be lower than 1.");

    internal static Result<GetInsightsRequest> BuildRequestWithDefaultValues() =>
        GetInsightsRequest
            .Build()
            .WithPhoneNumber(ValidPhoneNumber)
            .Create();

    internal static Result<GetInsightsRequest> BuildRequest() =>
        GetInsightsRequest
            .Build()
            .WithPhoneNumber(ValidPhoneNumber)
            .WithFormat()
            .WithCurrentCarrier()
            .WithOriginalCarrier()
            .WithSimSwap(new SimSwapRequest(250))
            .WithPurpose("FraudPreventionAndDetection")
            .Create();
}