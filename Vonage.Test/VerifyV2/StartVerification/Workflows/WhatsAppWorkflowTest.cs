#region
using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Common.Monads;
using Vonage.Test.Common.Extensions;
using Vonage.VerifyV2.StartVerification.WhatsApp;
using Xunit;
#endregion

namespace Vonage.Test.VerifyV2.StartVerification.Workflows;

[Trait("Category", "Request")]
[Trait("Product", "VerifyV2")]
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
    public void Parse_ShouldSetFrom() =>
        BuildValidWorkflow()
            .Map(workflow => workflow.From.Number)
            .Should()
            .BeSuccess(ValidFromNumber);
    
    [Fact]
    public void Parse_ShouldSetTo() =>
        BuildValidWorkflow()
            .Map(workflow => workflow.To.Number)
            .Should()
            .BeSuccess(ValidToNumber);
    
    [Fact]
    public void Parse_ShouldSetChannel() =>
        BuildValidWorkflow()
            .Map(workflow => workflow.Channel)
            .Should()
            .BeSuccess(ExpectedChannel);
    
    [Fact]
    public void Parse_ShouldSetOtpMode_GivenDefault() =>
        BuildValidWorkflow()
            .Map(workflow => workflow.Mode)
            .Should()
            .BeSuccess(WhatsAppWorkflow.WhatsAppMode.OptCode);
    
    
    [Fact]
    public void WithZeroTap_ShouldSetModeToZeroTap() =>
        WhatsAppWorkflow.ParseWithZeroTap(ValidToNumber, ValidFromNumber)
            .Map(workflow => workflow.Mode)
            .Should()
            .BeSuccess(WhatsAppWorkflow.WhatsAppMode.ZeroTap);

    private static Result<WhatsAppWorkflow> BuildValidWorkflow() => WhatsAppWorkflow.Parse(ValidToNumber, ValidFromNumber);
}