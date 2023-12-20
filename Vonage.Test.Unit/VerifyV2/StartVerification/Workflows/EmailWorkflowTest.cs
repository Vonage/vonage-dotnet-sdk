using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Test.Unit.Common.Extensions;
using Vonage.VerifyV2.StartVerification.Email;
using Xunit;

namespace Vonage.Test.Unit.VerifyV2.StartVerification.Workflows
{
    public class EmailWorkflowTest
    {
        private const string ExpectedChannel = "email";
        private const string ValidFromEmail = "bob@company.com";
        private const string ValidToEmail = "alice@company.com";

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void Parse_ShouldReturnFailure_GivenFromEmailIsProvidedButEmpty(string value) =>
            EmailWorkflow.Parse(ValidToEmail, value)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Email is invalid."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Parse_ShouldReturnFailure_GivenToEmailIsNullOrWhitespace(string value) =>
            EmailWorkflow.Parse(value)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Email is invalid."));

        [Fact]
        public void Parse_ShouldReturnSuccess() =>
            EmailWorkflow.Parse(ValidToEmail)
                .Should()
                .BeSuccess(workflow =>
                {
                    workflow.Channel.Should().Be(ExpectedChannel);
                    workflow.To.Address.Should().Be(ValidToEmail);
                    workflow.From.Should().BeNone();
                });

        [Fact]
        public void Parse_ShouldReturnSuccessWithFromEmail() =>
            EmailWorkflow.Parse(ValidToEmail, ValidFromEmail)
                .Map(workflow => workflow.From.Map(address => address.Address))
                .Should()
                .BeSuccess(ValidFromEmail);
    }
}