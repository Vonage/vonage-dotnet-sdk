#region
using System;
using FsCheck;
using FsCheck.Xunit;
using Vonage.Common.Monads;
using Vonage.Test.Common.Extensions;
using Vonage.Video.LiveCaptions.Start;
using Xunit;
#endregion

namespace Vonage.Test.Video.LiveCaptions.Start;

[Trait("Category", "Request")]
public class RequestBuilderTest
{
    private const string ValidSessionId = "session-id";
    private const string ValidToken = "token";
    private readonly Guid validApplicationId = Guid.NewGuid();

    [Fact]
    public void Build_ShouldDisablePartialCaptions() =>
        StartRequest
            .Build()
            .WithApplicationId(this.validApplicationId)
            .WithSessionId(ValidSessionId)
            .WithToken(ValidToken)
            .DisablePartialCaptions()
            .Create()
            .Map(request => request.PartialCaptions)
            .Should()
            .BeSuccess(false);

    [Fact]
    public void Build_ShouldHaveDefaultLanguage() =>
        StartRequest
            .Build()
            .WithApplicationId(this.validApplicationId)
            .WithSessionId(ValidSessionId)
            .WithToken(ValidToken)
            .Create()
            .Map(request => request.Language)
            .Should()
            .BeSuccess("en-US");

    [Fact]
    public void Build_ShouldHaveNoStatusCallbackUrl_GivenDefault() =>
        StartRequest
            .Build()
            .WithApplicationId(this.validApplicationId)
            .WithSessionId(ValidSessionId)
            .WithToken(ValidToken)
            .Create()
            .Map(request => request.StatusCallbackUrl)
            .Should()
            .BeSuccess(Maybe<Uri>.None);

    [Fact]
    public void Build_ShouldHavePartialCaptionsEnabled_GivenDefault() =>
        StartRequest
            .Build()
            .WithApplicationId(this.validApplicationId)
            .WithSessionId(ValidSessionId)
            .WithToken(ValidToken)
            .Create()
            .Map(request => request.PartialCaptions)
            .Should()
            .BeSuccess(true);

    [Property]
    public Property Build_ShouldReturnFailure_GivenMaxDurationExceedsMaximum() =>
        Prop.ForAll(
            GetDurationsAboveMaximum(),
            invalidDuration => StartRequest
                .Build()
                .WithApplicationId(this.validApplicationId)
                .WithSessionId(ValidSessionId)
                .WithToken(ValidToken)
                .WithMaxDuration(invalidDuration)
                .Create()
                .Should()
                .BeParsingFailure("MaxDuration cannot be higher than 14400."));

    [Property]
    public Property Build_ShouldReturnFailure_GivenMaxDurationIsBelowMinimum() =>
        Prop.ForAll(
            GetDurationsBelowMinimum(),
            invalidDuration => StartRequest
                .Build()
                .WithApplicationId(this.validApplicationId)
                .WithSessionId(ValidSessionId)
                .WithToken(ValidToken)
                .WithMaxDuration(invalidDuration)
                .Create()
                .Should()
                .BeParsingFailure("MaxDuration cannot be lower than 300."));

    [Fact]
    public void Build_ShouldSetApplicationId() =>
        StartRequest
            .Build()
            .WithApplicationId(this.validApplicationId)
            .WithSessionId(ValidSessionId)
            .WithToken(ValidToken)
            .Create()
            .Map(request => request.ApplicationId)
            .Should()
            .BeSuccess(this.validApplicationId);

    [Fact]
    public void Build_ShouldSetLanguage() =>
        StartRequest
            .Build()
            .WithApplicationId(this.validApplicationId)
            .WithSessionId(ValidSessionId)
            .WithToken(ValidToken)
            .WithLanguage("fr-FR")
            .Create()
            .Map(request => request.Language)
            .Should()
            .BeSuccess("fr-FR");

    [Theory]
    [InlineData(300)]
    [InlineData(14400)]
    public void Build_ShouldSetMaxDuration(int validDuration) =>
        StartRequest
            .Build()
            .WithApplicationId(this.validApplicationId)
            .WithSessionId(ValidSessionId)
            .WithToken(ValidToken)
            .WithMaxDuration(validDuration)
            .Create()
            .Map(request => request.MaxDuration)
            .Should()
            .BeSuccess(validDuration);

    [Fact]
    public void Build_ShouldSetSessionId() =>
        StartRequest
            .Build()
            .WithApplicationId(this.validApplicationId)
            .WithSessionId(ValidSessionId)
            .WithToken(ValidToken)
            .Create()
            .Map(request => request.SessionId)
            .Should()
            .BeSuccess(ValidSessionId);

    [Fact]
    public void Build_ShouldSetStatusCallbackUrl() =>
        StartRequest
            .Build()
            .WithApplicationId(this.validApplicationId)
            .WithSessionId(ValidSessionId)
            .WithToken(ValidToken)
            .WithStatusCallbackUrl(new Uri("https://example.com"))
            .Create()
            .Map(request => request.StatusCallbackUrl)
            .Should()
            .BeSuccess(new Uri("https://example.com"));

    [Fact]
    public void Build_ShouldSetToken() =>
        StartRequest
            .Build()
            .WithApplicationId(this.validApplicationId)
            .WithSessionId(ValidSessionId)
            .WithToken(ValidToken)
            .Create()
            .Map(request => request.Token)
            .Should()
            .BeSuccess(ValidToken);

    private static Arbitrary<int> GetDurationsAboveMaximum() =>
        Gen.Choose(14401, int.MaxValue).ToArbitrary();

    private static Arbitrary<int> GetDurationsBelowMinimum() =>
        Gen.Choose(299, -int.MaxValue).ToArbitrary();

    [Fact]
    public void Parse_ShouldReturnFailure_GivenApplicationIdIsEmpty() =>
        StartRequest
            .Build()
            .WithApplicationId(Guid.Empty)
            .WithSessionId(ValidSessionId)
            .WithToken(ValidToken)
            .Create()
            .Should()
            .BeParsingFailure("ApplicationId cannot be empty.");

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Parse_ShouldReturnFailure_GivenSessionIdIsEmpty(string invalidId) =>
        StartRequest
            .Build()
            .WithApplicationId(this.validApplicationId)
            .WithSessionId(invalidId)
            .WithToken(ValidToken)
            .Create()
            .Should()
            .BeParsingFailure("SessionId cannot be null or whitespace.");

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Parse_ShouldReturnFailure_GivenTokenIsEmpty(string invalidToken) =>
        StartRequest
            .Build()
            .WithApplicationId(this.validApplicationId)
            .WithSessionId(invalidToken)
            .WithToken(ValidToken)
            .Create()
            .Should()
            .BeParsingFailure("SessionId cannot be null or whitespace.");
}