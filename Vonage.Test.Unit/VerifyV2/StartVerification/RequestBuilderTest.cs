using System.Linq;
using AutoFixture;
using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.VerifyV2.StartVerification;
using Vonage.VerifyV2.StartVerification.Email;
using Vonage.VerifyV2.StartVerification.SilentAuth;
using Vonage.VerifyV2.StartVerification.Sms;
using Vonage.VerifyV2.StartVerification.Voice;
using Vonage.VerifyV2.StartVerification.WhatsApp;
using Vonage.VerifyV2.StartVerification.WhatsAppInteractive;
using Xunit;

namespace Vonage.Test.Unit.VerifyV2.StartVerification
{
    public class RequestBuilderTest
    {
        private const string ValidEmail = "alice@company.com";
        private readonly Fixture fixture;

        public RequestBuilderTest() => this.fixture = new Fixture();

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Create_ShouldReturnFailure_GivenBrandIsNullOrWhitespace(string value) =>
            StartVerificationRequestBuilder.Build()
                .WithBrand(value)
                .WithWorkflow(EmailWorkflow.Parse(ValidEmail))
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Brand cannot be null or whitespace."));

        [Fact]
        public void Create_ShouldReturnFailure_GivenChannelTimeoutIsHigherThanMaximum() =>
            StartVerificationRequestBuilder.Build()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(EmailWorkflow.Parse(ValidEmail))
                .WithChannelTimeout(901)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ChannelTimeout cannot be higher than 900."));

        [Fact]
        public void Create_ShouldReturnFailure_GivenChannelTimeoutIsLowerThanMinimum() =>
            StartVerificationRequestBuilder.Build()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(EmailWorkflow.Parse(ValidEmail))
                .WithChannelTimeout(59)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ChannelTimeout cannot be lower than 60."));

        [Fact]
        public void Create_ShouldReturnFailure_GivenCodeLengthIsHigherThanMaximum() =>
            StartVerificationRequestBuilder.Build()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(EmailWorkflow.Parse(ValidEmail))
                .WithCodeLength(11)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("CodeLength cannot be higher than 10."));

        [Fact]
        public void Create_ShouldReturnFailure_GivenCodeLengthIsLowerThanMinimum() =>
            StartVerificationRequestBuilder.Build()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(EmailWorkflow.Parse(ValidEmail))
                .WithCodeLength(3)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("CodeLength cannot be lower than 4."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Create_ShouldReturnFailure_GivenEmailWorkflowToIsNullOrWhitespace(string value) =>
            StartVerificationRequestBuilder.Build()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(EmailWorkflow.Parse(value))
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Email is invalid."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void Create_ShouldReturnFailure_GivenFromIsProvidedButEmpty(string value) =>
            StartVerificationRequestBuilder.Build()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(EmailWorkflow.Parse(ValidEmail, value))
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Email is invalid."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Create_ShouldReturnFailure_GivenSilentAuthWorkflowToIsNullOrWhitespace(string value) =>
            StartVerificationRequestBuilder.Build()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(SilentAuthWorkflow.Parse(value))
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Number cannot be null or whitespace."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void Create_ShouldReturnFailure_GivenSmsWorkflowHashIsProvidedButEmpty(string value) =>
            StartVerificationRequestBuilder.Build()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(SmsWorkflow.Parse("123456789", value))
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Hash cannot be null or whitespace."));

        [Theory]
        [InlineData("1234567890")]
        [InlineData("123456789012")]
        public void Create_ShouldReturnFailure_GivenSmsWorkflowHashIsProvidedButLengthIsNot11(string value) =>
            StartVerificationRequestBuilder.Build()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(SmsWorkflow.Parse("123456789", value))
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Hash length should be 11."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Create_ShouldReturnFailure_GivenSmsWorkflowToIsNullOrWhitespace(string value) =>
            StartVerificationRequestBuilder.Build()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(SmsWorkflow.Parse(value))
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Number cannot be null or whitespace."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Create_ShouldReturnFailure_GivenVoiceWorkflowToIsNullOrWhitespace(string value) =>
            StartVerificationRequestBuilder.Build()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(VoiceWorkflow.Parse(value))
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Number cannot be null or whitespace."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Create_ShouldReturnFailure_GivenWhatsAppInteractiveWorkflowToIsNullOrWhitespace(string value) =>
            StartVerificationRequestBuilder.Build()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(WhatsAppInteractiveWorkflow.Parse(value))
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Number cannot be null or whitespace."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void Create_ShouldReturnFailure_GivenWhatsAppWorkflowFromIsProvidedButEmpty(string value) =>
            StartVerificationRequestBuilder.Build()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(WhatsAppWorkflow.Parse("123456789", value))
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Number cannot be null or whitespace."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Create_ShouldReturnFailure_GivenWhatsAppWorkflowToIsNullOrWhitespace(string value) =>
            StartVerificationRequestBuilder.Build()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(WhatsAppWorkflow.Parse(value))
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Number cannot be null or whitespace."));

        [Fact]
        public void Create_ShouldSetBrand() =>
            StartVerificationRequestBuilder.Build()
                .WithBrand("some brand")
                .WithWorkflow(SilentAuthWorkflow.Parse("123456789"))
                .Create()
                .Map(request => request.Brand)
                .Should()
                .BeSuccess("some brand");

        [Theory]
        [InlineData(60)]
        [InlineData(900)]
        public void Create_ShouldSetChannelTimeout(int value) =>
            StartVerificationRequestBuilder.Build()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(EmailWorkflow.Parse(ValidEmail))
                .WithChannelTimeout(value)
                .Create()
                .Map(request => request.ChannelTimeout)
                .Should()
                .BeSuccess(value);

        [Fact]
        public void Create_ShouldSetClientReference() =>
            StartVerificationRequestBuilder.Build()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(EmailWorkflow.Parse(ValidEmail))
                .WithClientReference("client ref")
                .Create()
                .Map(request => request.ClientReference)
                .Should()
                .BeSuccess("client ref");

        [Theory]
        [InlineData(4)]
        [InlineData(10)]
        public void Create_ShouldSetCodeLength(int value) =>
            StartVerificationRequestBuilder.Build()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(EmailWorkflow.Parse(ValidEmail))
                .WithCodeLength(value)
                .Create()
                .Map(request => request.CodeLength)
                .Should()
                .BeSuccess(value);

        [Fact]
        public void Create_ShouldSetEmailWorkflow() =>
            StartVerificationRequestBuilder.Build()
                .WithBrand("some brand")
                .WithWorkflow(EmailWorkflow.Parse(ValidEmail))
                .Create()
                .Map(request => request.Workflows)
                .Should()
                .BeSuccess(workflows =>
                {
                    workflows.Should().HaveCount(1);
                    var workflow = workflows[0] as EmailWorkflow? ?? default;
                    workflow.Channel.Should().Be("email");
                    workflow.To.Address.Should().Be(ValidEmail);
                    workflow.From.Should().BeNone();
                });

        [Fact]
        public void Create_ShouldSetEmailWorkflowFrom() =>
            StartVerificationRequestBuilder.Build()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(EmailWorkflow.Parse(ValidEmail, "bob@company.com"))
                .WithLocale(Locale.FrFr)
                .Create()
                .Map(request =>
                    (request.Workflows.First() is EmailWorkflow ? (EmailWorkflow) request.Workflows.First() : default)
                    .From.Map(address => address.Address))
                .Should()
                .BeSuccess("bob@company.com");

        [Fact]
        public void Create_ShouldSetLocale() =>
            StartVerificationRequestBuilder.Build()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(EmailWorkflow.Parse(ValidEmail))
                .WithLocale(Locale.FrFr)
                .Create()
                .Map(request => request.Locale)
                .Should()
                .BeSuccess(Locale.FrFr);

        [Fact]
        public void Create_ShouldSetSilentAuthWorkflow() =>
            StartVerificationRequestBuilder.Build()
                .WithBrand("some brand")
                .WithWorkflow(SilentAuthWorkflow.Parse("123456789"))
                .Create()
                .Map(request => request.Workflows)
                .Should()
                .BeSuccess(workflows =>
                {
                    workflows.Should().HaveCount(1);
                    var workflow = workflows[0] as SilentAuthWorkflow? ?? default;
                    workflow.Channel.Should().Be("silent_auth");
                    workflow.To.Number.Should().Be("123456789");
                });

        [Fact]
        public void Create_ShouldSetSmsWorkflow() =>
            StartVerificationRequestBuilder.Build()
                .WithBrand("some brand")
                .WithWorkflow(SmsWorkflow.Parse("123456789", "12345678901"))
                .Create()
                .Map(request => request.Workflows)
                .Should()
                .BeSuccess(workflows =>
                {
                    workflows.Should().HaveCount(1);
                    var workflow = workflows[0] as SmsWorkflow? ?? default;
                    workflow.Channel.Should().Be("sms");
                    workflow.To.Number.Should().Be("123456789");
                    workflow.Hash.Should().BeSome("12345678901");
                });

        [Fact]
        public void Create_ShouldSetVoiceWorkflow() =>
            StartVerificationRequestBuilder.Build()
                .WithBrand("some brand")
                .WithWorkflow(VoiceWorkflow.Parse("123456789"))
                .Create()
                .Map(request => request.Workflows)
                .Should()
                .BeSuccess(workflows =>
                {
                    workflows.Should().HaveCount(1);
                    var workflow = workflows[0] as VoiceWorkflow? ?? default;
                    workflow.Channel.Should().Be("voice");
                    workflow.To.Number.Should().Be("123456789");
                });

        [Fact]
        public void Create_ShouldSetWhatsAppWorkflow() =>
            StartVerificationRequestBuilder.Build()
                .WithBrand("some brand")
                .WithWorkflow(WhatsAppWorkflow.Parse("123456789"))
                .Create()
                .Map(request => request.Workflows)
                .Should()
                .BeSuccess(workflows =>
                {
                    workflows.Should().HaveCount(1);
                    var workflow = workflows[0] as WhatsAppWorkflow? ?? default;
                    workflow.Channel.Should().Be("whatsapp");
                    workflow.To.Number.Should().Be("123456789");
                    workflow.From.Should().BeNone();
                });

        [Fact]
        public void Create_WithMandatoryInformation() =>
            StartVerificationRequestBuilder.Build()
                .WithBrand("some brand")
                .WithWorkflow(WhatsAppInteractiveWorkflow.Parse("123456789"))
                .Create()
                .Map(request => request.Workflows)
                .Should()
                .BeSuccess(workflows =>
                {
                    workflows.Should().HaveCount(1);
                    var workflow = workflows[0] as WhatsAppInteractiveWorkflow? ?? default;
                    workflow.Channel.Should().Be("whatsapp_interactive");
                    workflow.To.Number.Should().Be("123456789");
                });
    }
}