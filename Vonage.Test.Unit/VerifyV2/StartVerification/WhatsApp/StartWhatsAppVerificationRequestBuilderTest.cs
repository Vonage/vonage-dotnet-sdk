using AutoFixture;
using FluentAssertions;
using Vonage.Common;
using Vonage.Common.Failures;
using Vonage.Common.Monads;
using Vonage.Common.Test.Extensions;
using Vonage.VerifyV2.StartVerification;
using Vonage.VerifyV2.StartVerification.WhatsApp;
using Xunit;

namespace Vonage.Test.Unit.VerifyV2.StartVerification.WhatsApp
{
    public class StartWhatsAppVerificationRequestBuilderTest
    {
        private readonly Fixture fixture;

        public StartWhatsAppVerificationRequestBuilderTest() => this.fixture = new Fixture();

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Create_ShouldReturnFailure_GivenBrandIsNullOrWhitespace(string value) =>
            StartVerificationRequestBuilder.ForWhatsApp()
                .WithBrand(value)
                .WithWorkflow(WhatsAppWorkflow.Parse(GetValidNumber()))
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Brand cannot be null or whitespace."));

        [Fact]
        public void Create_ShouldReturnFailure_GivenChannelTimeoutIsHigherThanMaximum() =>
            StartVerificationRequestBuilder.ForWhatsApp()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(WhatsAppWorkflow.Parse(GetValidNumber()))
                .WithChannelTimeout(901)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ChannelTimeout cannot be higher than 900."));

        [Fact]
        public void Create_ShouldReturnFailure_GivenChannelTimeoutIsLowerThanMinimum() =>
            StartVerificationRequestBuilder.ForWhatsApp()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(WhatsAppWorkflow.Parse(GetValidNumber()))
                .WithChannelTimeout(59)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ChannelTimeout cannot be lower than 60."));

        [Fact]
        public void Create_ShouldReturnFailure_GivenCodeLengthIsHigherThanMaximum() =>
            StartVerificationRequestBuilder.ForWhatsApp()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(WhatsAppWorkflow.Parse(GetValidNumber()))
                .WithCodeLength(11)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("CodeLength cannot be higher than 10."));

        [Fact]
        public void Create_ShouldReturnFailure_GivenCodeLengthIsLowerThanMinimum() =>
            StartVerificationRequestBuilder.ForWhatsApp()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(WhatsAppWorkflow.Parse(GetValidNumber()))
                .WithCodeLength(3)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("CodeLength cannot be lower than 4."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void Create_ShouldReturnFailure_GivenFromIsProvidedButEmpty(string value) =>
            StartVerificationRequestBuilder.ForWhatsApp()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(WhatsAppWorkflow.Parse(GetValidNumber(), PhoneNumber.Parse(value)))
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Number cannot be null or whitespace."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Create_ShouldReturnFailure_GivenWorkflowToIsNullOrWhitespace(string value) =>
            StartVerificationRequestBuilder.ForWhatsApp()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(WhatsAppWorkflow.Parse(PhoneNumber.Parse(value)))
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Number cannot be null or whitespace."));

        [Theory]
        [InlineData(60)]
        [InlineData(900)]
        public void Create_ShouldSetChannelTimeout(int value) =>
            StartVerificationRequestBuilder.ForWhatsApp()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(WhatsAppWorkflow.Parse(GetValidNumber()))
                .WithChannelTimeout(value)
                .Create()
                .Map(request => request.ChannelTimeout)
                .Should()
                .BeSuccess(value);

        [Fact]
        public void Create_ShouldSetClientReference() =>
            StartVerificationRequestBuilder.ForWhatsApp()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(WhatsAppWorkflow.Parse(GetValidNumber()))
                .WithClientReference("client ref")
                .Create()
                .Map(request => request.ClientReference)
                .Should()
                .BeSuccess("client ref");

        [Theory]
        [InlineData(4)]
        [InlineData(10)]
        public void Create_ShouldSetCodeLength(int value) =>
            StartVerificationRequestBuilder.ForWhatsApp()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(WhatsAppWorkflow.Parse(GetValidNumber()))
                .WithCodeLength(value)
                .Create()
                .Map(request => request.CodeLength)
                .Should()
                .BeSuccess(value);

        [Fact]
        public void Create_ShouldSetLocale() =>
            StartVerificationRequestBuilder.ForWhatsApp()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(WhatsAppWorkflow.Parse(GetValidNumber()))
                .WithLocale(Locale.FrFr)
                .Create()
                .Map(request => request.Locale)
                .Should()
                .BeSuccess(Locale.FrFr);

        [Fact]
        public void Create_WithMandatoryInformation() =>
            StartVerificationRequestBuilder.ForWhatsApp()
                .WithBrand("some brand")
                .WithWorkflow(WhatsAppWorkflow.Parse(GetValidNumber()))
                .Create()
                .Should()
                .BeSuccess(request =>
                {
                    request.Brand.Should().Be("some brand");
                    request.Workflows.Should().HaveCount(1);
                    request.Workflows[0].Channel.Should().Be("whatsapp");
                    request.Workflows[0].To.Number.Should().Be("123456789");
                    request.Workflows[0].From.Should().BeNone();
                    request.Locale.Should().Be(Locale.EnUs);
                    request.CodeLength.Should().Be(4);
                    request.ClientReference.Should().BeNone();
                    request.ChannelTimeout.Should().Be(300);
                });

        private static Result<PhoneNumber> GetValidNumber() => PhoneNumber.Parse("123456789");
    }
}