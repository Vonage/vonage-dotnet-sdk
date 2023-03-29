using AutoFixture;
using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.VerifyV2.StartVerification;
using Vonage.VerifyV2.StartVerification.WhatsAppInteractive;
using Xunit;

namespace Vonage.Test.Unit.VerifyV2.StartVerification.WhatsAppInteractive
{
    public class StartWhatsAppInteractiveVerificationRequestBuilderTest
    {
        private readonly Fixture fixture;

        public StartWhatsAppInteractiveVerificationRequestBuilderTest() => this.fixture = new Fixture();

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Create_ShouldReturnFailure_GivenBrandIsNullOrWhitespace(string value) =>
            StartVerificationRequestBuilder.ForWhatsAppInteractive()
                .WithBrand(value)
                .WithWorkflow(WhatsAppInteractiveWorkflow.Parse("123456789"))
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Brand cannot be null or whitespace."));

        [Fact]
        public void Create_ShouldReturnFailure_GivenChannelTimeoutIsHigherThanMaximum() =>
            StartVerificationRequestBuilder.ForWhatsAppInteractive()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(WhatsAppInteractiveWorkflow.Parse("123456789"))
                .WithChannelTimeout(901)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ChannelTimeout cannot be higher than 900."));

        [Fact]
        public void Create_ShouldReturnFailure_GivenChannelTimeoutIsLowerThanMinimum() =>
            StartVerificationRequestBuilder.ForWhatsAppInteractive()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(WhatsAppInteractiveWorkflow.Parse("123456789"))
                .WithChannelTimeout(59)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ChannelTimeout cannot be lower than 60."));

        [Fact]
        public void Create_ShouldReturnFailure_GivenCodeLengthIsHigherThanMaximum() =>
            StartVerificationRequestBuilder.ForWhatsAppInteractive()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(WhatsAppInteractiveWorkflow.Parse("123456789"))
                .WithCodeLength(11)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("CodeLength cannot be higher than 10."));

        [Fact]
        public void Create_ShouldReturnFailure_GivenCodeLengthIsLowerThanMinimum() =>
            StartVerificationRequestBuilder.ForWhatsAppInteractive()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(WhatsAppInteractiveWorkflow.Parse("123456789"))
                .WithCodeLength(3)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("CodeLength cannot be lower than 4."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Create_ShouldReturnFailure_GivenWorkflowToIsNullOrWhitespace(string value) =>
            StartVerificationRequestBuilder.ForWhatsAppInteractive()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(WhatsAppInteractiveWorkflow.Parse(value))
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Number cannot be null or whitespace."));

        [Theory]
        [InlineData(60)]
        [InlineData(900)]
        public void Create_ShouldSetChannelTimeout(int value) =>
            StartVerificationRequestBuilder.ForWhatsAppInteractive()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(WhatsAppInteractiveWorkflow.Parse("123456789"))
                .WithChannelTimeout(value)
                .Create()
                .Map(request => request.ChannelTimeout)
                .Should()
                .BeSuccess(value);

        [Fact]
        public void Create_ShouldSetClientReference() =>
            StartVerificationRequestBuilder.ForWhatsAppInteractive()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(WhatsAppInteractiveWorkflow.Parse("123456789"))
                .WithClientReference("client ref")
                .Create()
                .Map(request => request.ClientReference)
                .Should()
                .BeSuccess("client ref");

        [Theory]
        [InlineData(4)]
        [InlineData(10)]
        public void Create_ShouldSetCodeLength(int value) =>
            StartVerificationRequestBuilder.ForWhatsAppInteractive()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(WhatsAppInteractiveWorkflow.Parse("123456789"))
                .WithCodeLength(value)
                .Create()
                .Map(request => request.CodeLength)
                .Should()
                .BeSuccess(value);

        [Fact]
        public void Create_ShouldSetLocale() =>
            StartVerificationRequestBuilder.ForWhatsAppInteractive()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(WhatsAppInteractiveWorkflow.Parse("123456789"))
                .WithLocale(Locale.FrFr)
                .Create()
                .Map(request => request.Locale)
                .Should()
                .BeSuccess(Locale.FrFr);

        [Fact]
        public void Create_WithMandatoryInformation() =>
            StartVerificationRequestBuilder.ForWhatsAppInteractive()
                .WithBrand("some brand")
                .WithWorkflow(WhatsAppInteractiveWorkflow.Parse("123456789"))
                .Create()
                .Should()
                .BeSuccess(request =>
                {
                    request.Brand.Should().Be("some brand");
                    request.Workflows.Should().HaveCount(1);
                    request.Workflows[0].Channel.Should().Be("whatsapp_interactive");
                    request.Workflows[0].To.Number.Should().Be("123456789");
                    request.Locale.Should().Be(Locale.EnUs);
                    request.CodeLength.Should().Be(4);
                    request.ClientReference.Should().BeNone();
                    request.ChannelTimeout.Should().Be(300);
                });
    }
}