using System;
using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Test.Unit.Common.Extensions;
using Vonage.VerifyV2.StartVerification.SilentAuth;
using Xunit;

namespace Vonage.Test.Unit.VerifyV2.StartVerification.Workflows
{
    public class SilentAuthWorkflowTest
    {
        private const string ExpectedChannel = "silent_auth";
        private const string ValidNumber = "123456789";
        private const string ValidRedirectUrl = "https://example.com";

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Parse_ShouldReturnFailure_GivenNumberIsNullOrWhitespace(string value) =>
            SilentAuthWorkflow.Parse(value)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Number cannot be null or whitespace."));

        [Fact]
        public void Parse_ShouldReturnSuccess() =>
            SilentAuthWorkflow.Parse(ValidNumber)
                .Should()
                .BeSuccess(workflow =>
                {
                    workflow.Channel.Should().Be(ExpectedChannel);
                    workflow.To.Number.Should().Be(ValidNumber);
                    workflow.RedirectUrl.Should().BeNone();
                });

        [Fact]
        public void Parse_ShouldReturnSuccess_GivenRedirectIsNull() =>
            SilentAuthWorkflow.Parse(ValidNumber, null)
                .Should()
                .BeSuccess(workflow =>
                {
                    workflow.Channel.Should().Be(ExpectedChannel);
                    workflow.To.Number.Should().Be(ValidNumber);
                    workflow.RedirectUrl.Should().BeNone();
                });

        [Fact]
        public void Parse_ShouldReturnSuccessWithRedirect() =>
            SilentAuthWorkflow.Parse(ValidNumber, new Uri(ValidRedirectUrl))
                .Should()
                .BeSuccess(workflow =>
                {
                    workflow.Channel.Should().Be(ExpectedChannel);
                    workflow.To.Number.Should().Be(ValidNumber);
                    workflow.RedirectUrl.Should().BeSome(new Uri(ValidRedirectUrl));
                });
    }
}