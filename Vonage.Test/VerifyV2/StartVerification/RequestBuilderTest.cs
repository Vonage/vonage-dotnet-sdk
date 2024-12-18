﻿#region
using System;
using FluentAssertions;
using FsCheck;
using FsCheck.Xunit;
using Vonage.Common.Failures;
using Vonage.Common.Monads;
using Vonage.Test.Common.Extensions;
using Vonage.Test.Common.TestHelpers;
using Vonage.VerifyV2.StartVerification;
using Vonage.VerifyV2.StartVerification.Email;
using Vonage.VerifyV2.StartVerification.SilentAuth;
using Vonage.VerifyV2.StartVerification.Voice;
using Vonage.VerifyV2.StartVerification.WhatsApp;
using Vonage.VerifyV2.StartVerification.WhatsAppInteractive;
using Xunit;
#endregion

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
    public void Create_ShouldReturnFailure_GivenBrandExceeds16Characters() =>
        StartVerificationRequest.Build()
            .WithBrand(StringHelper.GenerateString(17))
            .WithWorkflow(EmailWorkflow.Parse(ValidEmail))
            .Create()
            .Should()
            .BeParsingFailure("Brand length cannot be higher than 16.");

    [Fact]
    public void Create_ShouldReturnFailure_GivenChannelTimeoutIsHigherThanMaximum() =>
        Prop.ForAll(
            GetChannelTimeoutsAboveMaximum(),
            invalidTimeout => BuildBaseRequest()
                .WithWorkflow(EmailWorkflow.Parse(ValidEmail))
                .WithChannelTimeout(invalidTimeout)
                .Create()
                .Should()
                .BeParsingFailure("ChannelTimeout cannot be higher than 900."));

    [Property]
    public Property Create_ShouldReturnFailure_GivenChannelTimeoutIsLowerThanMinimum() =>
        Prop.ForAll(
            GetChannelTimeoutsBelowMinimum(),
            invalidTimeout => BuildBaseRequest()
                .WithWorkflow(EmailWorkflow.Parse(ValidEmail))
                .WithChannelTimeout(invalidTimeout)
                .Create()
                .Should()
                .BeParsingFailure("ChannelTimeout cannot be lower than 15."));

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
            .WithFallbackWorkflow(WhatsAppWorkflow.Parse("123456789", "123456789"))
            .WithFallbackWorkflow(
                Result<VoiceWorkflow>.FromFailure(ResultFailure.FromErrorMessage("Random message.")))
            .Create()
            .Map(request => request.Workflows)
            .Should()
            .BeFailure(ResultFailure.FromErrorMessage("Random message."));

    [Fact]
    public void Create_ShouldSetBrand() =>
        StartVerificationRequest.Build()
            .WithBrand("Brand Custom 123")
            .WithWorkflow(SilentAuthWorkflow.Parse("123456789"))
            .Create()
            .Map(request => request.Brand)
            .Should()
            .BeSuccess("Brand Custom 123");

    [Property]
    public Property Create_ShouldSetChannelTimeout() =>
        Prop.ForAll(
            GetValidChannelTimeouts(),
            validTimeout => BuildBaseRequest()
                .WithWorkflow(EmailWorkflow.Parse(ValidEmail))
                .WithChannelTimeout(validTimeout)
                .Create()
                .Map(request => request.ChannelTimeout)
                .Should()
                .BeSuccess(validTimeout));

    private static Arbitrary<int> GetChannelTimeoutsAboveMaximum() =>
        Gen.Choose(901, int.MaxValue).ToArbitrary();

    private static Arbitrary<int> GetChannelTimeoutsBelowMinimum() =>
        Gen.Choose(14, -int.MaxValue).ToArbitrary();

    private static Arbitrary<int> GetValidChannelTimeouts() =>
        Gen.Choose(15, 900).ToArbitrary();

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
            .WithFallbackWorkflow(WhatsAppWorkflow.Parse("123456789", "123456780"))
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
                fallbackWorkflowOne.From.Number.Should().Be("123456780");
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

    [Fact]
    public void Create_ShouldSetTemplateId() =>
        BuildBaseRequest()
            .WithWorkflow(EmailWorkflow.Parse(ValidEmail))
            .WithTemplateId(new Guid("e42581ff-951b-4774-9f3f-b495636e3eef"))
            .Create()
            .Map(request => request.TemplateId)
            .Should()
            .BeSuccess(new Guid("e42581ff-951b-4774-9f3f-b495636e3eef"));
}