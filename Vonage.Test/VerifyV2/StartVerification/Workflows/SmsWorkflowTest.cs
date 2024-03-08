using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Test.Common.Extensions;
using Vonage.Test.Common.TestHelpers;
using Vonage.VerifyV2.StartVerification.Sms;
using Xunit;

namespace Vonage.Test.VerifyV2.StartVerification.Workflows;

[Trait("Category", "Request")]
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
    [InlineData("")]
    [InlineData(" ")]
    public void Parse_ShouldReturnFailure_GivenEntityIdIsProvidedButEmpty(string value) =>
        SmsWorkflow.Parse(ValidNumber, entityId: value)
            .Should()
            .BeFailure(ResultFailure.FromErrorMessage("EntityId cannot be null or whitespace."));

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void Parse_ShouldReturnFailure_GivenContentIdIsProvidedButEmpty(string value) =>
        SmsWorkflow.Parse(ValidNumber, contentId: value)
            .Should()
            .BeFailure(ResultFailure.FromErrorMessage("ContentId cannot be null or whitespace."));

    [Theory]
    [InlineData("1234567890")]
    [InlineData("123456789012")]
    public void Parse_ShouldReturnFailure_GivenHashIsProvidedButLengthIsNot11(string value) =>
        SmsWorkflow.Parse(ValidNumber, value)
            .Should()
            .BeFailure(ResultFailure.FromErrorMessage("Hash length should be 11."));

    [Theory]
    [InlineData("1234567abc123", "Number can only contain digits.")]
    [InlineData("123456", "Number length cannot be lower than 7.")]
    [InlineData("1234567890123456", "Number length cannot be higher than 15.")]
    public void Parse_ShouldReturnFailure_GivenFromIsInvalid(string value, string message) =>
        SmsWorkflow.Parse(ValidNumber, from: value)
            .Should()
            .BeFailure(ResultFailure.FromErrorMessage(message));

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Parse_ShouldReturnFailure_GivenNumberIsNullOrWhitespace(string value) =>
        SmsWorkflow.Parse(value)
            .Should()
            .BeFailure(ResultFailure.FromErrorMessage("Number cannot be null or whitespace."));

    [Fact]
    public void Parse_ShouldReturnFailure_GivenEntityIdLengthIsHigherThan200Characters() =>
        SmsWorkflow.Parse(ValidNumber, entityId: StringHelper.GenerateString(201))
            .Map(workflow => workflow.EntityId)
            .Should()
            .BeFailure(ResultFailure.FromErrorMessage("EntityId length cannot be higher than 200."));

    [Fact]
    public void Parse_ShouldReturnFailure_GivenContentIdLengthIsHigherThan200Characters() =>
        SmsWorkflow.Parse(ValidNumber, contentId: StringHelper.GenerateString(201))
            .Map(workflow => workflow.ContentId)
            .Should()
            .BeFailure(ResultFailure.FromErrorMessage("ContentId length cannot be higher than 200."));

    [Fact]
    public void Parse_ShouldSetTo() =>
        SmsWorkflow.Parse(ValidNumber)
            .Map(workflow => workflow.To.Number)
            .Should()
            .BeSuccess(ValidNumber);

    [Fact]
    public void Parse_ShouldSetSmsChannel() =>
        SmsWorkflow.Parse(ValidNumber)
            .Map(workflow => workflow.Channel)
            .Should()
            .BeSuccess(ExpectedChannel);

    [Fact]
    public void Parse_ShouldSetHash() =>
        SmsWorkflow.Parse(ValidNumber, ValidHash)
            .Map(workflow => workflow.Hash)
            .Should()
            .BeSuccess(ValidHash);

    [Fact]
    public void Parse_ShouldSetFrom() =>
        SmsWorkflow.Parse(ValidNumber, from: "123456789012345")
            .Map(workflow => workflow.From)
            .Should()
            .BeSuccess(from => from.GetUnsafe().Number.Should().Be("123456789012345"));

    [Fact]
    public void Parse_ShouldHaveEmptyHash_GivenDefault() =>
        SmsWorkflow.Parse(ValidNumber)
            .Map(workflow => workflow.Hash)
            .Should()
            .BeSuccess(hash => hash.Should().BeNone());

    [Fact]
    public void Parse_ShouldHaveEmptyEntityId_GivenDefault() =>
        SmsWorkflow.Parse(ValidNumber)
            .Map(workflow => workflow.EntityId)
            .Should()
            .BeSuccess(hash => hash.Should().BeNone());

    [Fact]
    public void Parse_ShouldHaveEmptyFrom_GivenDefault() =>
        SmsWorkflow.Parse(ValidNumber)
            .Map(workflow => workflow.From)
            .Should()
            .BeSuccess(from => from.Should().BeNone());

    [Fact]
    public void Parse_ShouldSetEntityId_GivenLengthIsLowerThan200Characters() =>
        SmsWorkflow.Parse(ValidNumber, entityId: StringHelper.GenerateString(200))
            .Map(workflow => workflow.EntityId)
            .Should()
            .BeSuccess(hash => hash.Should().BeSome(StringHelper.GenerateString(200)));

    [Fact]
    public void Parse_ShouldHaveEmptyContentId_GivenDefault() =>
        SmsWorkflow.Parse(ValidNumber)
            .Map(workflow => workflow.ContentId)
            .Should()
            .BeSuccess(hash => hash.Should().BeNone());
}