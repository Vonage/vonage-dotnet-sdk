using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Common.Monads;
using Vonage.Test.Common.Extensions;
using Vonage.VerifyV2.StartVerification;
using Vonage.VerifyV2.StartVerification.Email;
using Vonage.VerifyV2.StartVerification.SilentAuth;
using Vonage.VerifyV2.StartVerification.Voice;
using Vonage.VerifyV2.StartVerification.WhatsApp;
using Vonage.VerifyV2.StartVerification.WhatsAppInteractive;
using Xunit;

namespace Vonage.Test.VerifyV2.StartVerification;

[Trait("Category", "Request")]
public class RequestBuilderTest
{
    private const string ValidEmail = "alice@company.com";

    [Fact]
    public void Create_ShouldEnableFraudCheck_ByDefault() =>
        BuildBaseRequest()
            .WithWorkflow(EmailWorkflow.Parse(ValidEmail))
            .Create()
            .Map(request => request.FraudCheck)
            .Should()
            .BeSuccess(true);

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Create_ShouldReturnFailure_GivenBrandIsNullOrWhitespace(string value) =>
        StartVerificationRequest.Build()
            .WithBrand(value)
            .WithWorkflow(EmailWorkflow.Parse(ValidEmail))
            .Create()
            .Should()
            .BeParsingFailure("Brand cannot be null or whitespace.");

    [Fact]
    public void Create_ShouldReturnFailure_GivenChannelTimeoutIsHigherThanMaximum() =>
        BuildBaseRequest()
            .WithWorkflow(EmailWorkflow.Parse(ValidEmail))
            .WithChannelTimeout(901)
            .Create()
            .Should()
            .BeParsingFailure("ChannelTimeout cannot be higher than 900.");

    [Fact]
    public void Create_ShouldReturnFailure_GivenChannelTimeoutIsLowerThanMinimum() =>
        BuildBaseRequest()
            .WithWorkflow(EmailWorkflow.Parse(ValidEmail))
            .WithChannelTimeout(59)
            .Create()
            .Should()
            .BeParsingFailure("ChannelTimeout cannot be lower than 60.");

    [Fact]
    public void Create_ShouldReturnFailure_GivenCodeLengthIsHigherThanMaximum() =>
        BuildBaseRequest()
            .WithWorkflow(EmailWorkflow.Parse(ValidEmail))
            .WithCodeLength(11)
            .Create()
            .Should()
            .BeParsingFailure("CodeLength cannot be higher than 10.");

    [Fact]
    public void Create_ShouldReturnFailure_GivenCodeLengthIsLowerThanMinimum() =>
        BuildBaseRequest()
            .WithWorkflow(EmailWorkflow.Parse(ValidEmail))
            .WithCodeLength(3)
            .Create()
            .Should()
            .BeParsingFailure("CodeLength cannot be lower than 4.");

    [Fact]
    public void Create_ShouldReturnFailure_GivenOneFallbackWorkflowIsFailure() =>
        BuildBaseRequest()
            .WithWorkflow(WhatsAppInteractiveWorkflow.Parse("123456789"))
            .WithFallbackWorkflow(WhatsAppWorkflow.Parse("123456789"))
            .WithFallbackWorkflow(
                Result<VoiceWorkflow>.FromFailure(ResultFailure.FromErrorMessage("Random message.")))
            .Create()
            .Map(request => request.Workflows)
            .Should()
            .BeFailure(ResultFailure.FromErrorMessage("Random message."));

    [Fact]
    public void Create_ShouldSetBrand() =>
        BuildBaseRequest()
            .WithWorkflow(SilentAuthWorkflow.Parse("123456789"))
            .Create()
            .Map(request => request.Brand)
            .Should()
            .BeSuccess("some brand");

    [Theory]
    [InlineData(60)]
    [InlineData(900)]
    public void Create_ShouldSetChannelTimeout(int value) =>
        BuildBaseRequest()
            .WithWorkflow(EmailWorkflow.Parse(ValidEmail))
            .WithChannelTimeout(value)
            .Create()
            .Map(request => request.ChannelTimeout)
            .Should()
            .BeSuccess(value);

    [Fact]
    public void Create_ShouldSetClientReference() =>
        BuildBaseRequest()
            .WithWorkflow(EmailWorkflow.Parse(ValidEmail))
            .WithClientReference("client ref")
            .Create()
            .Map(request => request.ClientReference)
            .Should()
            .BeSuccess("client ref");

    [Fact]
    public void Create_ShouldSetCode() =>
        BuildBaseRequest()
            .WithWorkflow(EmailWorkflow.Parse(ValidEmail))
            .WithCode("123456")
            .Create()
            .Map(request => request.Code)
            .Should()
            .BeSuccess("123456");

    [Theory]
    [InlineData(4)]
    [InlineData(10)]
    public void Create_ShouldSetCodeLength(int value) =>
        BuildBaseRequest()
            .WithWorkflow(EmailWorkflow.Parse(ValidEmail))
            .WithCodeLength(value)
            .Create()
            .Map(request => request.CodeLength)
            .Should()
            .BeSuccess(value);

    [Fact]
    public void Create_ShouldSetFallbackWorkflows() =>
        BuildBaseRequest()
            .WithWorkflow(WhatsAppInteractiveWorkflow.Parse("123456789"))
            .WithFallbackWorkflow(WhatsAppWorkflow.Parse("123456789"))
            .WithFallbackWorkflow(VoiceWorkflow.Parse("123456789"))
            .Create()
            .Map(request => request.Workflows)
            .Should()
            .BeSuccess(workflows =>
            {
                workflows.Should().HaveCount(3);
                var mainWorkflow = workflows[0] as WhatsAppInteractiveWorkflow? ?? default;
                mainWorkflow.Channel.Should().Be("whatsapp_interactive");
                mainWorkflow.To.Number.Should().Be("123456789");
                var fallbackWorkflowOne = workflows[1] as WhatsAppWorkflow? ?? default;
                fallbackWorkflowOne.Channel.Should().Be("whatsapp");
                fallbackWorkflowOne.To.Number.Should().Be("123456789");
                fallbackWorkflowOne.From.Should().BeNone();
                var fallbackWorkflowTwo = workflows[2] as VoiceWorkflow? ?? default;
                fallbackWorkflowTwo.Channel.Should().Be("voice");
                fallbackWorkflowTwo.To.Number.Should().Be("123456789");
            });

    [Fact]
    public void Create_ShouldSetLocale_UsingLocaleValue() =>
        BuildBaseRequest()
            .WithWorkflow(EmailWorkflow.Parse(ValidEmail))
            .WithLocale(Locale.FrFr)
            .Create()
            .Map(request => request.Locale)
            .Should()
            .BeSuccess(Locale.FrFr);

    [Fact]
    public void Create_ShouldSetLocale_UsingStringValue() =>
        BuildBaseRequest()
            .WithWorkflow(EmailWorkflow.Parse(ValidEmail))
            .WithLocale(Locale.FrFr.Language)
            .Create()
            .Map(request => request.Locale)
            .Should()
            .BeSuccess(Locale.FrFr);

    [Fact]
    public void Create_ShouldSkipFraudCheck_GivenSkipFraudCheckIsUsed() =>
        BuildBaseRequest()
            .WithWorkflow(EmailWorkflow.Parse(ValidEmail))
            .SkipFraudCheck()
            .Create()
            .Map(request => request.FraudCheck)
            .Should()
            .BeSuccess(false);

    private static IBuilderForWorkflow BuildBaseRequest() =>
        StartVerificationRequest.Build().WithBrand("some brand");
}