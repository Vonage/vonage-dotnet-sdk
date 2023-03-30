using System.Linq;
using AutoFixture;
using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.VerifyV2.StartVerification;
using Vonage.VerifyV2.StartVerification.Email;
using Xunit;

namespace Vonage.Test.Unit.VerifyV2.StartVerification.Email
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
            StartVerificationRequestBuilder.ForEmail()
                .WithBrand(value)
                .WithWorkflow(EmailWorkflow.Parse(ValidEmail))
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Brand cannot be null or whitespace."));

        [Fact]
        public void Create_ShouldReturnFailure_GivenChannelTimeoutIsHigherThanMaximum() =>
            StartVerificationRequestBuilder.ForEmail()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(EmailWorkflow.Parse(ValidEmail))
                .WithChannelTimeout(901)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ChannelTimeout cannot be higher than 900."));

        [Fact]
        public void Create_ShouldReturnFailure_GivenChannelTimeoutIsLowerThanMinimum() =>
            StartVerificationRequestBuilder.ForEmail()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(EmailWorkflow.Parse(ValidEmail))
                .WithChannelTimeout(59)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ChannelTimeout cannot be lower than 60."));

        [Fact]
        public void Create_ShouldReturnFailure_GivenCodeLengthIsHigherThanMaximum() =>
            StartVerificationRequestBuilder.ForEmail()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(EmailWorkflow.Parse(ValidEmail))
                .WithCodeLength(11)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("CodeLength cannot be higher than 10."));

        [Fact]
        public void Create_ShouldReturnFailure_GivenCodeLengthIsLowerThanMinimum() =>
            StartVerificationRequestBuilder.ForEmail()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(EmailWorkflow.Parse(ValidEmail))
                .WithCodeLength(3)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("CodeLength cannot be lower than 4."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void Create_ShouldReturnFailure_GivenFromIsProvidedButEmpty(string value) =>
            StartVerificationRequestBuilder.ForEmail()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(EmailWorkflow.Parse(ValidEmail, value))
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Email is invalid."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Create_ShouldReturnFailure_GivenWorkflowToIsNullOrWhitespace(string value) =>
            StartVerificationRequestBuilder.ForEmail()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(EmailWorkflow.Parse(value))
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Email is invalid."));

        [Theory]
        [InlineData(60)]
        [InlineData(900)]
        public void Create_ShouldSetChannelTimeout(int value) =>
            StartVerificationRequestBuilder.ForEmail()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(EmailWorkflow.Parse(ValidEmail))
                .WithChannelTimeout(value)
                .Create()
                .Map(request => request.ChannelTimeout)
                .Should()
                .BeSuccess(value);

        [Fact]
        public void Create_ShouldSetClientReference() =>
            StartVerificationRequestBuilder.ForEmail()
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
            StartVerificationRequestBuilder.ForEmail()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(EmailWorkflow.Parse(ValidEmail))
                .WithCodeLength(value)
                .Create()
                .Map(request => request.CodeLength)
                .Should()
                .BeSuccess(value);

        [Fact]
        public void Create_ShouldSetFrom() =>
            StartVerificationRequestBuilder.ForEmail()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(EmailWorkflow.Parse(ValidEmail, "bob@company.com"))
                .WithLocale(Locale.FrFr)
                .Create()
                .Map(request => request.Workflows.First().From.Map(address => address.Address))
                .Should()
                .BeSuccess("bob@company.com");

        [Fact]
        public void Create_ShouldSetLocale() =>
            StartVerificationRequestBuilder.ForEmail()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(EmailWorkflow.Parse(ValidEmail))
                .WithLocale(Locale.FrFr)
                .Create()
                .Map(request => request.Locale)
                .Should()
                .BeSuccess(Locale.FrFr);

        [Fact]
        public void Create_WithMandatoryInformation() =>
            StartVerificationRequestBuilder.ForEmail()
                .WithBrand("some brand")
                .WithWorkflow(EmailWorkflow.Parse(ValidEmail))
                .Create()
                .Should()
                .BeSuccess(request =>
                {
                    request.Brand.Should().Be("some brand");
                    request.Workflows.Should().HaveCount(1);
                    request.Workflows[0].Channel.Should().Be("email");
                    request.Workflows[0].To.Address.Should().Be(ValidEmail);
                    request.Workflows[0].From.Should().BeNone();
                    request.Locale.Should().Be(Locale.EnUs);
                    request.CodeLength.Should().Be(4);
                    request.ClientReference.Should().BeNone();
                    request.ChannelTimeout.Should().Be(300);
                });
    }
}