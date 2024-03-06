using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Test.Common.Extensions;
using Vonage.VerifyV2.StartVerification.WhatsApp;
using Xunit;

namespace Vonage.Test.VerifyV2.StartVerification.Workflows;

[Trait("Category", "Request")]
public class WhatsAppWorkflowTest
{
    private const string ExpectedChannel = "whatsapp";
    private const string ValidFromNumber = "987654321";
    private const string ValidToNumber = "123456789";

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Parse_ShouldReturnFailure_GivenFromIsNullOrWhitespace(string value) =>
        WhatsAppWorkflow.Parse(ValidToNumber, value)
            .Should()
            .BeFailure(ResultFailure.FromErrorMessage("Number cannot be null or whitespace."));

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Parse_ShouldReturnFailure_GivenToIsNullOrWhitespace(string value) =>
        WhatsAppWorkflow.Parse(value, ValidFromNumber)
            .Should()
            .BeFailure(ResultFailure.FromErrorMessage("Number cannot be null or whitespace."));

    [Fact]
    public void Parse_ShouldSetWhatsAppWorkflow() =>
        WhatsAppWorkflow.Parse(ValidToNumber, ValidFromNumber)
            .Should()
            .BeSuccess(workflow =>
            {
                workflow.Channel.Should().Be(ExpectedChannel);
                workflow.To.Number.Should().Be(ValidToNumber);
                workflow.From.Number.Should().Be(ValidFromNumber);
            });
}