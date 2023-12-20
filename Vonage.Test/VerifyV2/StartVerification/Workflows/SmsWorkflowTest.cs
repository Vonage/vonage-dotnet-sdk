using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Test.Common.Extensions;
using Vonage.VerifyV2.StartVerification.Sms;
using Xunit;

namespace Vonage.Test.VerifyV2.StartVerification.Workflows
{
    public class SmsWorkflowTest
    {
        private const string ExpectedChannel = "sms";
        private const string ValidHash = "12345678901";
        private const string ValidNumber = "123456789";

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void Parse_ShouldReturnFailure_GivenHashIsProvidedButEmpty(string value) =>
            SmsWorkflow.Parse(ValidNumber, value)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Hash cannot be null or whitespace."));

        [Theory]
        [InlineData("1234567890")]
        [InlineData("123456789012")]
        public void Parse_ShouldReturnFailure_GivenHashIsProvidedButLengthIsNot11(string value) =>
            SmsWorkflow.Parse(ValidNumber, value)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Hash length should be 11."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Parse_ShouldReturnFailure_GivenNumberIsNullOrWhitespace(string value) =>
            SmsWorkflow.Parse(value)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Number cannot be null or whitespace."));

        [Fact]
        public void Parse_ShouldReturnSuccess() =>
            SmsWorkflow.Parse(ValidNumber)
                .Should()
                .BeSuccess(workflow =>
                {
                    workflow.Channel.Should().Be(ExpectedChannel);
                    workflow.To.Number.Should().Be(ValidNumber);
                    workflow.Hash.Should().BeNone();
                });

        [Fact]
        public void Parse_ShouldReturnSuccessWithHash() =>
            SmsWorkflow.Parse(ValidNumber, ValidHash)
                .Should()
                .BeSuccess(workflow =>
                {
                    workflow.Channel.Should().Be(ExpectedChannel);
                    workflow.To.Number.Should().Be(ValidNumber);
                    workflow.Hash.Should().BeSome(ValidHash);
                });
    }
}