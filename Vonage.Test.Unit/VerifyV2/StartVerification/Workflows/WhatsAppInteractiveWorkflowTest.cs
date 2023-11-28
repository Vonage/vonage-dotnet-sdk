using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.VerifyV2.StartVerification.WhatsAppInteractive;
using Xunit;

namespace Vonage.Test.Unit.VerifyV2.StartVerification.Workflows
{
    public class WhatsAppInteractiveWorkflowTest
    {
        private const string ExpectedChannel = "whatsapp_interactive";
        private const string ValidNumber = "123456789";

        [Fact]
        public void Create_ShouldSetWhatsAppInteractiveWorkflow() =>
            WhatsAppInteractiveWorkflow.Parse(ValidNumber)
                .Should()
                .BeSuccess(workflow =>
                {
                    workflow.Channel.Should().Be(ExpectedChannel);
                    workflow.To.Number.Should().Be(ValidNumber);
                });

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Parse_ShouldReturnFailure_GivenNumberIsNullOrWhitespace(string value) =>
            WhatsAppInteractiveWorkflow.Parse(value)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Number cannot be null or whitespace."));
    }
}