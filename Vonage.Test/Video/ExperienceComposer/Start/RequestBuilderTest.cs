#region
using System;
using FsCheck;
using FsCheck.Xunit;
using Vonage.Server;
using Vonage.Test.Common.Extensions;
using Vonage.Video.ExperienceComposer.Start;
using Xunit;
#endregion

namespace Vonage.Test.Video.ExperienceComposer.Start;

[Trait("Category", "Request")]
public class RequestBuilderTest
{
    private const string ValidSessionId = "session-id";
    private const string ValidToken = "token";
    private const RenderResolution ValidResolution = RenderResolution.FullHighDefinitionPortrait;
    private const string ValidName = "name";
    private readonly Guid validApplicationId = Guid.NewGuid();
    private readonly Uri validUri = new Uri("https://example.com");

    [Fact]
    public void Build_ShouldHaveDefaultMaxDuration() =>
        this.BuildValidRequest()
            .Create()
            .Map(request => request.MaxDuration)
            .Should()
            .BeSuccess(7200);

    [Fact]
    public void Build_ShouldReturnFailure_GivenMaxDurationIsAboveMaximum() =>
        Prop.ForAll(
            GetDurationsAboveMaximum(),
            invalidDuration => this.BuildValidRequest()
                .WithMaxDuration(invalidDuration)
                .Create()
                .Should()
                .BeParsingFailure("MaxDuration cannot be higher than 36000."));

    [Property]
    public Property Build_ShouldReturnFailure_GivenMaxDurationIsBelowMinimum() =>
        Prop.ForAll(
            GetDurationsBelowMinimum(),
            invalidDuration => this.BuildValidRequest()
                .WithMaxDuration(invalidDuration)
                .Create()
                .Should()
                .BeParsingFailure("MaxDuration cannot be lower than 60."));

    [Fact]
    public void Build_ShouldSetApplicationId() =>
        this.BuildValidRequest()
            .Create()
            .Map(request => request.ApplicationId)
            .Should()
            .BeSuccess(this.validApplicationId);

    [Theory]
    [InlineData(60)]
    [InlineData(36000)]
    public void Build_ShouldSetMaxDuration(int validDuration) =>
        this.BuildValidRequest()
            .WithMaxDuration(validDuration)
            .Create()
            .Map(request => request.MaxDuration)
            .Should()
            .BeSuccess(validDuration);

    [Fact]
    public void Build_ShouldSetName() =>
        this.BuildValidRequest()
            .Create()
            .Map(request => request.Properties)
            .Should()
            .BeSuccess(new StartProperties(ValidName));

    [Fact]
    public void Build_ShouldSetResolution() =>
        this.BuildValidRequest()
            .Create()
            .Map(request => request.Resolution)
            .Should()
            .BeSuccess(ValidResolution);

    [Fact]
    public void Build_ShouldSetSessionId() =>
        this.BuildValidRequest()
            .Create()
            .Map(request => request.SessionId)
            .Should()
            .BeSuccess(ValidSessionId);

    [Fact]
    public void Build_ShouldSetToken() =>
        this.BuildValidRequest()
            .Create()
            .Map(request => request.Token)
            .Should()
            .BeSuccess(ValidToken);

    [Fact]
    public void Build_ShouldSetUrl() =>
        this.BuildValidRequest()
            .Create()
            .Map(request => request.Url)
            .Should()
            .BeSuccess(this.validUri);

    [Fact]
    public void Parse_ShouldReturnFailure_GivenApplicationIdIsEmpty() =>
        StartRequest
            .Build()
            .WithApplicationId(Guid.Empty)
            .WithSessionId(ValidSessionId)
            .WithToken(ValidToken)
            .WithUrl(this.validUri)
            .WithResolution(ValidResolution)
            .WithName(ValidName)
            .Create()
            .Should()
            .BeParsingFailure("ApplicationId cannot be empty.");

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Parse_ShouldReturnFailure_GivenNameIsEmpty(string invalidName) =>
        StartRequest
            .Build()
            .WithApplicationId(this.validApplicationId)
            .WithSessionId(ValidSessionId)
            .WithToken(ValidToken)
            .WithUrl(this.validUri)
            .WithResolution(ValidResolution)
            .WithName(invalidName)
            .Create()
            .Should()
            .BeParsingFailure("Name cannot be null or whitespace.");

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
            .WithUrl(this.validUri)
            .WithResolution(ValidResolution)
            .WithName(ValidName)
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
            .WithUrl(this.validUri)
            .WithResolution(ValidResolution)
            .WithName(ValidName)
            .Create()
            .Should()
            .BeParsingFailure("Token cannot be null or whitespace.");

    private IBuilderForOptional BuildValidRequest() =>
        StartRequest
            .Build()
            .WithApplicationId(this.validApplicationId)
            .WithSessionId(ValidSessionId)
            .WithToken(ValidToken)
            .WithUrl(this.validUri)
            .WithResolution(ValidResolution)
            .WithName(ValidName);

    private static Arbitrary<int> GetDurationsAboveMaximum() =>
        Gen.Choose(36001, int.MaxValue).ToArbitrary();

    private static Arbitrary<int> GetDurationsBelowMinimum() =>
        Gen.Choose(59, -int.MaxValue).ToArbitrary();
}