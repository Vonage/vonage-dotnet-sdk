#region
using System;
using FsCheck;
using FsCheck.Fluent;
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
        this.BuildValidRequest()
            .DisablePartialCaptions()
            .Create()
            .Map(request => request.PartialCaptions)
            .Should()
            .BeSuccess(false);

    [Fact]
    public void Build_ShouldHaveDefaultLanguage() =>
        this.BuildValidRequest()
            .Create()
            .Map(request => request.Language)
            .Should()
            .BeSuccess("en-US");

    [Fact]
    public void Build_ShouldHaveNoStatusCallbackUrl_GivenDefault() =>
        this.BuildValidRequest()
            .Create()
            .Map(request => request.StatusCallbackUrl)
            .Should()
            .BeSuccess(Maybe<Uri>.None);

    [Fact]
    public void Build_ShouldHavePartialCaptionsEnabled_GivenDefault() =>
        this.BuildValidRequest()
            .Create()
            .Map(request => request.PartialCaptions)
            .Should()
            .BeSuccess(true);

    [Property]
    public Property Build_ShouldReturnFailure_GivenMaxDurationExceedsMaximum() =>
        Prop.ForAll(
            GetDurationsAboveMaximum(),
            invalidDuration => this.BuildValidRequest()
                .WithMaxDuration(invalidDuration)
                .Create()
                .Should()
                .BeParsingFailure("MaxDuration cannot be higher than 14400."));

    [Property]
    public Property Build_ShouldReturnFailure_GivenMaxDurationIsBelowMinimum() =>
        Prop.ForAll(
            GetDurationsBelowMinimum(),
            invalidDuration => this.BuildValidRequest()
                .WithMaxDuration(invalidDuration)
                .Create()
                .Should()
                .BeParsingFailure("MaxDuration cannot be lower than 300."));

    [Fact]
    public void Build_ShouldSetApplicationId() =>
        this.BuildValidRequest()
            .Create()
            .Map(request => request.ApplicationId)
            .Should()
            .BeSuccess(this.validApplicationId);

    [Fact]
    public void Build_ShouldSetLanguage() =>
        this.BuildValidRequest()
            .WithLanguage("fr-FR")
            .Create()
            .Map(request => request.Language)
            .Should()
            .BeSuccess("fr-FR");

    [Theory]
    [InlineData(300)]
    [InlineData(14400)]
    public void Build_ShouldSetMaxDuration(int validDuration) =>
        this.BuildValidRequest()
            .WithMaxDuration(validDuration)
            .Create()
            .Map(request => request.MaxDuration)
            .Should()
            .BeSuccess(validDuration);

    [Fact]
    public void Build_ShouldSetSessionId() =>
        this.BuildValidRequest()
            .Create()
            .Map(request => request.SessionId)
            .Should()
            .BeSuccess(ValidSessionId);

    [Fact]
    public void Build_ShouldSetStatusCallbackUrl() =>
        this.BuildValidRequest()
            .WithStatusCallbackUrl(new Uri("https://example.com"))
            .Create()
            .Map(request => request.StatusCallbackUrl)
            .Should()
            .BeSuccess(new Uri("https://example.com"));

    [Fact]
    public void Build_ShouldSetToken() =>
        this.BuildValidRequest()
            .Create()
            .Map(request => request.Token)
            .Should()
            .BeSuccess(ValidToken);

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
            .WithSessionId(ValidSessionId)
            .WithToken(invalidToken)
            .Create()
            .Should()
            .BeParsingFailure("Token cannot be null or whitespace.");

    private IBuilderForOptional BuildValidRequest() =>
        StartRequest
            .Build()
            .WithApplicationId(this.validApplicationId)
            .WithSessionId(ValidSessionId)
            .WithToken(ValidToken);

    private static Arbitrary<int> GetDurationsAboveMaximum() =>
        Gen.Choose(14401, int.MaxValue).ToArbitrary();

    private static Arbitrary<int> GetDurationsBelowMinimum() =>
        Gen.Choose(299, -int.MaxValue).ToArbitrary();
}