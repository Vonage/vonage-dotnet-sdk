using FluentAssertions;
using Vonage.Applications;
using Vonage.Applications.Capabilities;
using Vonage.Applications.UpdateApplication;
using Vonage.Common.Monads;
using Vonage.Test.Common.Extensions;
using Xunit;
using UpdateApplicationRequest = Vonage.Applications.UpdateApplication.UpdateApplicationRequest;

namespace Vonage.Test.Applications.UpdateApplication;

[Trait("Category", "Request")]
[Trait("Product", "ApplicationsNew")]
public class RequestBuilderTest
{
    private static IBuilderForName BuildBase() =>
        UpdateApplicationRequest.Build().WithApplicationId("78d335fa-323d-0114-9c3d-d6f0d48968cf");

    [Fact]
    public void Build_ShouldSetApplicationId() =>
        BuildBase()
            .WithName("My App")
            .Create()
            .Map(request => request.ApplicationId)
            .Should()
            .BeSuccess("78d335fa-323d-0114-9c3d-d6f0d48968cf");

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Build_ShouldReturnFailure_GivenApplicationIdIsNullOrWhitespace(string invalidId) =>
        UpdateApplicationRequest.Build()
            .WithApplicationId(invalidId)
            .WithName("My App")
            .Create()
            .Should()
            .BeParsingFailure("ApplicationId cannot be null or whitespace.");

    [Fact]
    public void Build_ShouldSetName() =>
        BuildBase()
            .WithName("My App")
            .Create()
            .Map(request => request.Name)
            .Should()
            .BeSuccess("My App");

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Build_ShouldReturnFailure_GivenNameIsNullOrWhitespace(string invalidName) =>
        BuildBase()
            .WithName(invalidName)
            .Create()
            .Should()
            .BeParsingFailure("Name cannot be null or whitespace.");

    [Fact]
    public void Build_ShouldHaveNoVoice_GivenDefault() =>
        BuildBase()
            .WithName("My App")
            .Create()
            .Map(request => request.Voice)
            .Should()
            .BeSuccess(Maybe<VoiceCapability>.None);

    [Fact]
    public void Build_ShouldSetVbc() =>
        BuildBase()
            .WithName("My App")
            .WithVbc(new VbcCapability())
            .Create()
            .Map(request => request.Vbc)
            .Should()
            .BeSuccess(vbc => vbc.IsSome.Should().BeTrue());

    [Fact]
    public void Build_ShouldHaveNoKeys_GivenDefault() =>
        BuildBase()
            .WithName("My App")
            .Create()
            .Map(request => request.Keys)
            .Should()
            .BeSuccess(Maybe<ApplicationKeys>.None);

    [Fact]
    public void Build_ShouldSetKeys() =>
        BuildBase()
            .WithName("My App")
            .WithKeys(new ApplicationKeys("-----BEGIN PUBLIC KEY-----"))
            .Create()
            .Map(request => request.Keys)
            .Should()
            .BeSuccess(keys => keys.IsSome.Should().BeTrue());

    [Fact]
    public void Build_ShouldHaveNoPrivacy_GivenDefault() =>
        BuildBase()
            .WithName("My App")
            .Create()
            .Map(request => request.Privacy)
            .Should()
            .BeSuccess(Maybe<ApplicationPrivacy>.None);

    [Fact]
    public void Build_ShouldSetPrivacy() =>
        BuildBase()
            .WithName("My App")
            .WithPrivacy(new ApplicationPrivacy(true))
            .Create()
            .Map(request => request.Privacy)
            .Should()
            .BeSuccess(privacy => privacy.IsSome.Should().BeTrue());
}
