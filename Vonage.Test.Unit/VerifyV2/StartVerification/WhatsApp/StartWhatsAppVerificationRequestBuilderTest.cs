using AutoFixture;
using FluentAssertions;
using Vonage.Common.Failures;
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
                .WithWorkflow(new WhatsAppWorkflow(this.fixture.Create<string>()))
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Brand cannot be null or whitespace."));

        [Fact]
        public void Create_ShouldReturnFailure_GivenChannelTimeoutIsHigherThanMaximum() =>
            StartVerificationRequestBuilder.ForWhatsApp()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(new WhatsAppWorkflow(this.fixture.Create<string>()))
                .WithChannelTimeout(901)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ChannelTimeout cannot be higher than 900."));

        [Fact]
        public void Create_ShouldReturnFailure_GivenChannelTimeoutIsLowerThanMinimum() =>
            StartVerificationRequestBuilder.ForWhatsApp()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(new WhatsAppWorkflow(this.fixture.Create<string>()))
                .WithChannelTimeout(59)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ChannelTimeout cannot be lower than 60."));

        [Fact]
        public void Create_ShouldReturnFailure_GivenCodeLengthIsHigherThanMaximum() =>
            StartVerificationRequestBuilder.ForWhatsApp()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(new WhatsAppWorkflow(this.fixture.Create<string>()))
                .WithCodeLength(11)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("CodeLength cannot be higher than 10."));

        [Fact]
        public void Create_ShouldReturnFailure_GivenCodeLengthIsLowerThanMinimum() =>
            StartVerificationRequestBuilder.ForWhatsApp()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(new WhatsAppWorkflow(this.fixture.Create<string>()))
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
                .WithWorkflow(new WhatsAppWorkflow(this.fixture.Create<string>(), value))
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("From cannot be null or whitespace."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Create_ShouldReturnFailure_GivenWorkflowToIsNullOrWhitespace(string value) =>
            StartVerificationRequestBuilder.ForWhatsApp()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(new WhatsAppWorkflow(value))
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("To cannot be null or whitespace."));

        [Theory]
        [InlineData(60)]
        [InlineData(900)]
        public void Create_ShouldSetChannelTimeout(int value) =>
            StartVerificationRequestBuilder.ForWhatsApp()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(new WhatsAppWorkflow(this.fixture.Create<string>()))
                .WithChannelTimeout(value)
                .Create()
                .Map(request => request.ChannelTimeout)
                .Should()
                .BeSuccess(value);

        [Fact]
        public void Create_ShouldSetClientReference() =>
            StartVerificationRequestBuilder.ForWhatsApp()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(new WhatsAppWorkflow(this.fixture.Create<string>()))
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
                .WithWorkflow(new WhatsAppWorkflow(this.fixture.Create<string>()))
                .WithCodeLength(value)
                .Create()
                .Map(request => request.CodeLength)
                .Should()
                .BeSuccess(value);

        [Fact]
        public void Create_ShouldSetLocale() =>
            StartVerificationRequestBuilder.ForWhatsApp()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(new WhatsAppWorkflow(this.fixture.Create<string>()))
                .WithLocale(Locale.FrFr)
                .Create()
                .Map(request => request.Locale)
                .Should()
                .BeSuccess(Locale.FrFr);

        [Fact]
        public void Create_WithMandatoryInformation() =>
            StartVerificationRequestBuilder.ForWhatsApp()
                .WithBrand("some brand")
                .WithWorkflow(new WhatsAppWorkflow("to"))
                .Create()
                .Should()
                .BeSuccess(request =>
                {
                    request.Brand.Should().Be("some brand");
                    request.Workflows.Should().HaveCount(1);
                    request.Workflows[0].Should().Be(new WhatsAppWorkflow("to"));
                    request.Locale.Should().Be(Locale.EnUs);
                    request.CodeLength.Should().Be(4);
                    request.ClientReference.Should().BeNone();
                    request.ChannelTimeout.Should().Be(300);
                });
    }
}