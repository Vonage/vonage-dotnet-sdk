using AutoFixture;
using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.VerifyV2.StartVerification;
using Vonage.VerifyV2.StartVerification.Sms;
using Xunit;

namespace Vonage.Test.Unit.VerifyV2.StartVerification.Sms
{
    public class StartSmsVerificationRequestBuilderTest
    {
        private readonly Fixture fixture;

        public StartSmsVerificationRequestBuilderTest() => this.fixture = new Fixture();

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Create_ShouldReturnFailure_GivenBrandIsNullOrWhitespace(string value) =>
            StartVerificationRequestBuilder.ForSms()
                .WithBrand(value)
                .WithWorkflow(new Workflow(this.fixture.Create<string>(), this.fixture.Create<string>()))
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Brand cannot be null or whitespace."));

        [Fact]
        public void Create_ShouldReturnFailure_GivenChannelTimeoutIsHigherThanMaximum() =>
            StartVerificationRequestBuilder.ForSms()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(new Workflow(this.fixture.Create<string>(), this.fixture.Create<string>()))
                .WithChannelTimeout(901)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ChannelTimeout cannot be higher than 900."));

        [Fact]
        public void Create_ShouldReturnFailure_GivenChannelTimeoutIsLowerThanMinimum() =>
            StartVerificationRequestBuilder.ForSms()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(new Workflow(this.fixture.Create<string>(), this.fixture.Create<string>()))
                .WithChannelTimeout(59)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ChannelTimeout cannot be lower than 60."));

        [Fact]
        public void Create_ShouldReturnFailure_GivenCodeLengthIsHigherThanMaximum() =>
            StartVerificationRequestBuilder.ForSms()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(new Workflow(this.fixture.Create<string>(), this.fixture.Create<string>()))
                .WithCodeLength(11)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("CodeLength cannot be higher than 10."));

        [Fact]
        public void Create_ShouldReturnFailure_GivenCodeLengthIsLowerThanMinimum() =>
            StartVerificationRequestBuilder.ForSms()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(new Workflow(this.fixture.Create<string>(), this.fixture.Create<string>()))
                .WithCodeLength(3)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("CodeLength cannot be lower than 4."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void Create_ShouldReturnFailure_GivenHashIsProvidedButEmpty(string value) =>
            StartVerificationRequestBuilder.ForSms()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(new Workflow(this.fixture.Create<string>(), this.fixture.Create<string>(), value))
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Hash cannot be null or whitespace."));

        [Theory]
        [InlineData("1234567890")]
        [InlineData("123456789012")]
        public void Create_ShouldReturnFailure_GivenHashIsProvidedButLengthIsNot11(string value) =>
            StartVerificationRequestBuilder.ForSms()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(new Workflow(this.fixture.Create<string>(), this.fixture.Create<string>(), value))
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Hash length should be 11."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Create_ShouldReturnFailure_GivenWorkflowChannelIsNullOrWhitespace(string value) =>
            StartVerificationRequestBuilder.ForSms()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(new Workflow(value, this.fixture.Create<string>()))
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Channel cannot be null or whitespace."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Create_ShouldReturnFailure_GivenWorkflowToIsNullOrWhitespace(string value) =>
            StartVerificationRequestBuilder.ForSms()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(new Workflow(this.fixture.Create<string>(), value))
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("To cannot be null or whitespace."));

        [Theory]
        [InlineData(60)]
        [InlineData(900)]
        public void Create_ShouldSetChannelTimeout(int value) =>
            StartVerificationRequestBuilder.ForSms()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(new Workflow(this.fixture.Create<string>(), this.fixture.Create<string>()))
                .WithChannelTimeout(value)
                .Create()
                .Map(request => request.ChannelTimeout)
                .Should()
                .BeSuccess(value);

        [Fact]
        public void Create_ShouldSetClientReference() =>
            StartVerificationRequestBuilder.ForSms()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(new Workflow(this.fixture.Create<string>(), this.fixture.Create<string>()))
                .WithClientReference("client ref")
                .Create()
                .Map(request => request.ClientReference)
                .Should()
                .BeSuccess("client ref");

        [Theory]
        [InlineData(4)]
        [InlineData(10)]
        public void Create_ShouldSetCodeLength(int value) =>
            StartVerificationRequestBuilder.ForSms()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(new Workflow(this.fixture.Create<string>(), this.fixture.Create<string>()))
                .WithCodeLength(value)
                .Create()
                .Map(request => request.CodeLength)
                .Should()
                .BeSuccess(value);

        [Fact]
        public void Create_ShouldSetLocale() =>
            StartVerificationRequestBuilder.ForSms()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(new Workflow(this.fixture.Create<string>(), this.fixture.Create<string>()))
                .WithLocale(Locale.FrFr)
                .Create()
                .Map(request => request.Locale)
                .Should()
                .BeSuccess(Locale.FrFr);

        [Fact]
        public void Create_WithMandatoryInformation() =>
            StartVerificationRequestBuilder.ForSms()
                .WithBrand("some brand")
                .WithWorkflow(new Workflow("channel", "to"))
                .Create()
                .Should()
                .BeSuccess(request =>
                {
                    request.Brand.Should().Be("some brand");
                    request.Workflows.Should().HaveCount(1);
                    request.Workflows[0].Should().Be(new Workflow("channel", "to"));
                    request.Locale.Should().Be(Locale.EnUs);
                    request.CodeLength.Should().Be(4);
                    request.ClientReference.Should().BeNone();
                    request.ChannelTimeout.Should().Be(300);
                });
    }
}