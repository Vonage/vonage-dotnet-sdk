using FluentAssertions;
using Vonage.ApplicationsNew;
using Vonage.ApplicationsNew.Capabilities;
using Vonage.ApplicationsNew.CreateApplication;
using Vonage.Common.Monads;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.ApplicationsNew.CreateApplication;

[Trait("Category", "Request")]
[Trait("Product", "ApplicationsNew")]
public class RequestBuilderTest
{
    [Fact]
    public void Build_ShouldSetName() =>
        CreateApplicationRequest.Build()
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
        CreateApplicationRequest.Build()
            .WithName(invalidName)
            .Create()
            .Should()
            .BeParsingFailure("Name cannot be null or whitespace.");

    [Fact]
    public void Build_ShouldHaveNoVoice_GivenDefault() =>
        CreateApplicationRequest.Build()
            .WithName("My App")
            .Create()
            .Map(request => request.Voice)
            .Should()
            .BeSuccess(Maybe<VoiceCapability>.None);

    [Fact]
    public void Build_ShouldSetVoice() =>
        CreateApplicationRequest.Build()
            .WithName("My App")
            .WithVoice(new VoiceCapability())
            .Create()
            .Map(request => request.Voice)
            .Should()
            .BeSuccess(voice => voice.IsSome.Should().BeTrue());

    [Fact]
    public void Build_ShouldSetMessages() =>
        CreateApplicationRequest.Build()
            .WithName("My App")
            .WithMessages(new MessagesCapability())
            .Create()
            .Map(request => request.Messages)
            .Should()
            .BeSuccess(messages => messages.IsSome.Should().BeTrue());

    [Fact]
    public void Build_ShouldSetVbc() =>
        CreateApplicationRequest.Build()
            .WithName("My App")
            .WithVbc(new VbcCapability())
            .Create()
            .Map(request => request.Vbc)
            .Should()
            .BeSuccess(vbc => vbc.IsSome.Should().BeTrue());

    [Fact]
    public void Build_ShouldHaveNoKeys_GivenDefault() =>
        CreateApplicationRequest.Build()
            .WithName("My App")
            .Create()
            .Map(request => request.Keys)
            .Should()
            .BeSuccess(Maybe<ApplicationKeys>.None);

    [Fact]
    public void Build_ShouldSetKeys() =>
        CreateApplicationRequest.Build()
            .WithName("My App")
            .WithKeys(new ApplicationKeys("-----BEGIN PUBLIC KEY-----"))
            .Create()
            .Map(request => request.Keys)
            .Should()
            .BeSuccess(keys => keys.Should().BeSome(applicationKeys => applicationKeys.PublicKey.Should().Be("-----BEGIN PUBLIC KEY-----")));

    [Fact]
    public void Build_ShouldHaveNoPrivacy_GivenDefault() =>
        CreateApplicationRequest.Build()
            .WithName("My App")
            .Create()
            .Map(request => request.Privacy)
            .Should()
            .BeSuccess(Maybe<ApplicationPrivacy>.None);

    [Fact]
    public void Build_ShouldSetPrivacy() =>
        CreateApplicationRequest.Build()
            .WithName("My App")
            .WithPrivacy(new ApplicationPrivacy(true))
            .Create()
            .Map(request => request.Privacy)
            .Should()
            .BeSuccess(privacy => privacy.Should().BeSome(settings => settings.ImproveAi.Should().BeTrue()));
}
