#region
using System;
using Vonage.Test.Common.Extensions;
using Vonage.VerifyV2;
using Vonage.VerifyV2.CreateTemplateFragment;
using Vonage.VerifyV2.StartVerification;
using Xunit;
#endregion

namespace Vonage.Test.VerifyV2.CreateTemplateFragment;

[Trait("Category", "Request")]
public class RequestBuilderTest
{
    private const string ValidName = "my-fragment";
    private static readonly Guid ValidTemplateId = new Guid("f3a065af-ac5a-47a4-8dfe-819561a7a287");

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Create_ShouldReturnFailure_GivenTextIsNullOrWhitespace(string value) =>
        CreateTemplateFragmentRequest.Build()
            .WithTemplateId(ValidTemplateId)
            .WithText(value)
            .WithLocale(Locale.EnUs)
            .WithChannel(VerificationChannel.Sms)
            .Create()
            .Should()
            .BeParsingFailure("Text cannot be null or whitespace.");

    [Fact]
    public void Create_ShouldReturnFailure_GivenTemplateIdIsEmpty() =>
        CreateTemplateFragmentRequest.Build()
            .WithTemplateId(Guid.Empty)
            .WithText(ValidName)
            .WithLocale(Locale.EnUs)
            .WithChannel(VerificationChannel.Sms)
            .Create()
            .Should()
            .BeParsingFailure("TemplateId cannot be empty.");

    [Theory]
    [InlineData(VerificationChannel.SilentAuth)]
    [InlineData(VerificationChannel.WhatsApp)]
    [InlineData(VerificationChannel.WhatsAppInteractive)]
    [InlineData(VerificationChannel.Email)]
    public void Create_ShouldReturnFailure_GivenChannelIsNotSupported(VerificationChannel channel) =>
        CreateTemplateFragmentRequest.Build()
            .WithTemplateId(ValidTemplateId)
            .WithText(ValidName)
            .WithLocale(Locale.EnUs)
            .WithChannel(channel)
            .Create()
            .Should()
            .BeParsingFailure("Channel must be one of Sms, Voice or Email.");

    [Fact]
    public void Create_ShouldSetName() =>
        CreateTemplateFragmentRequest.Build()
            .WithTemplateId(ValidTemplateId)
            .WithText("my-fragment")
            .WithLocale(Locale.EnUs)
            .WithChannel(VerificationChannel.Sms)
            .Create()
            .Map(request => request.Text)
            .Should()
            .BeSuccess("my-fragment");

    [Fact]
    public void Create_ShouldSetTemplateId() =>
        CreateTemplateFragmentRequest.Build()
            .WithTemplateId(ValidTemplateId)
            .WithText("my-fragment")
            .WithLocale(Locale.EnUs)
            .WithChannel(VerificationChannel.Sms)
            .Create()
            .Map(request => request.TemplateId)
            .Should()
            .BeSuccess(ValidTemplateId);

    [Fact]
    public void Create_ShouldSetLocale() =>
        CreateTemplateFragmentRequest.Build()
            .WithTemplateId(ValidTemplateId)
            .WithText("my-fragment")
            .WithLocale(Locale.EnUs)
            .WithChannel(VerificationChannel.Sms)
            .Create()
            .Map(request => request.Locale)
            .Should()
            .BeSuccess(Locale.EnUs);

    [Theory]
    [InlineData(VerificationChannel.Sms)]
    [InlineData(VerificationChannel.Voice)]
    public void Create_ShouldSetChannel(VerificationChannel channel) =>
        CreateTemplateFragmentRequest.Build()
            .WithTemplateId(ValidTemplateId)
            .WithText(ValidName)
            .WithLocale(Locale.EnUs)
            .WithChannel(channel)
            .Create()
            .Map(request => request.Channel)
            .Should()
            .BeSuccess(channel);
}