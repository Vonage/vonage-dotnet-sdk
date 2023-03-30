using AutoFixture;
using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.VerifyV2.StartVerification;
using Vonage.VerifyV2.StartVerification.Voice;
using Xunit;

namespace Vonage.Test.Unit.VerifyV2.StartVerification.Voice
{
    public class RequestBuilderTest
    {
        private readonly Fixture fixture;

        public RequestBuilderTest() => this.fixture = new Fixture();

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Create_ShouldReturnFailure_GivenBrandIsNullOrWhitespace(string value) =>
            StartVerificationRequestBuilder.ForVoice()
                .WithBrand(value)
                .WithWorkflow(VoiceWorkflow.Parse("123456789"))
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Brand cannot be null or whitespace."));

        [Fact]
        public void Create_ShouldReturnFailure_GivenChannelTimeoutIsHigherThanMaximum() =>
            StartVerificationRequestBuilder.ForVoice()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(VoiceWorkflow.Parse("123456789"))
                .WithChannelTimeout(901)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ChannelTimeout cannot be higher than 900."));

        [Fact]
        public void Create_ShouldReturnFailure_GivenChannelTimeoutIsLowerThanMinimum() =>
            StartVerificationRequestBuilder.ForVoice()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(VoiceWorkflow.Parse("123456789"))
                .WithChannelTimeout(59)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ChannelTimeout cannot be lower than 60."));

        [Fact]
        public void Create_ShouldReturnFailure_GivenCodeLengthIsHigherThanMaximum() =>
            StartVerificationRequestBuilder.ForVoice()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(VoiceWorkflow.Parse("123456789"))
                .WithCodeLength(11)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("CodeLength cannot be higher than 10."));

        [Fact]
        public void Create_ShouldReturnFailure_GivenCodeLengthIsLowerThanMinimum() =>
            StartVerificationRequestBuilder.ForVoice()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(VoiceWorkflow.Parse("123456789"))
                .WithCodeLength(3)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("CodeLength cannot be lower than 4."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Create_ShouldReturnFailure_GivenWorkflowToIsNullOrWhitespace(string value) =>
            StartVerificationRequestBuilder.ForVoice()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(VoiceWorkflow.Parse(value))
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Number cannot be null or whitespace."));

        [Theory]
        [InlineData(60)]
        [InlineData(900)]
        public void Create_ShouldSetChannelTimeout(int value) =>
            StartVerificationRequestBuilder.ForVoice()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(VoiceWorkflow.Parse("123456789"))
                .WithChannelTimeout(value)
                .Create()
                .Map(request => request.ChannelTimeout)
                .Should()
                .BeSuccess(value);

        [Fact]
        public void Create_ShouldSetClientReference() =>
            StartVerificationRequestBuilder.ForVoice()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(VoiceWorkflow.Parse("123456789"))
                .WithClientReference("client ref")
                .Create()
                .Map(request => request.ClientReference)
                .Should()
                .BeSuccess("client ref");

        [Theory]
        [InlineData(4)]
        [InlineData(10)]
        public void Create_ShouldSetCodeLength(int value) =>
            StartVerificationRequestBuilder.ForVoice()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(VoiceWorkflow.Parse("123456789"))
                .WithCodeLength(value)
                .Create()
                .Map(request => request.CodeLength)
                .Should()
                .BeSuccess(value);

        [Fact]
        public void Create_ShouldSetLocale() =>
            StartVerificationRequestBuilder.ForVoice()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(VoiceWorkflow.Parse("123456789"))
                .WithLocale(Locale.FrFr)
                .Create()
                .Map(request => request.Locale)
                .Should()
                .BeSuccess(Locale.FrFr);

        [Fact]
        public void Create_WithMandatoryInformation() =>
            StartVerificationRequestBuilder.ForVoice()
                .WithBrand("some brand")
                .WithWorkflow(VoiceWorkflow.Parse("123456789"))
                .Create()
                .Should()
                .BeSuccess(request =>
                {
                    request.Brand.Should().Be("some brand");
                    request.Workflows.Should().HaveCount(1);
                    request.Workflows[0].Channel.Should().Be("voice");
                    request.Workflows[0].To.Number.Should().Be("123456789");
                    request.Locale.Should().Be(Locale.EnUs);
                    request.CodeLength.Should().Be(4);
                    request.ClientReference.Should().BeNone();
                    request.ChannelTimeout.Should().Be(300);
                });
    }
}