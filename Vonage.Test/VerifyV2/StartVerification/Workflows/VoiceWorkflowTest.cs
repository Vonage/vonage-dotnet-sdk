using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Test.Common.Extensions;
using Vonage.VerifyV2.StartVerification.Voice;
using Xunit;

namespace Vonage.Test.VerifyV2.StartVerification.Workflows
{
    public class VoiceWorkflowTest
    {
        private const string ExpectedChannel = "voice";
        private const string ValidNumber = "123456789";

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Parse_ShouldReturnFailure_GivenNumberIsNullOrWhitespace(string value) =>
            VoiceWorkflow.Parse(value)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Number cannot be null or whitespace."));

        [Fact]
        public void Parse_ShouldReturnSuccess() =>
            VoiceWorkflow.Parse(ValidNumber)
                .Should()
                .BeSuccess(workflow =>
                {
                    workflow.Channel.Should().Be(ExpectedChannel);
                    workflow.To.Number.Should().Be(ValidNumber);
                });
    }
}