using System;
using FluentAssertions;
using Vonage.Common.Monads;
using Vonage.Test.Common.Extensions;
using Vonage.VerifyV2.StartVerification;
using Xunit;

namespace Vonage.Test.VerifyV2.StartVerification;

[Trait("Category", "Request")]
public class StartVerificationResponseTest
{
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void BuildVerificationRequest_ShouldReturnFailure_GivenCodeIsNullOrWhitespace(string value) =>
        new StartVerificationResponse(Guid.NewGuid(), Maybe<Uri>.None)
            .BuildVerificationRequest(value)
            .Should()
            .BeParsingFailure("Code cannot be null or whitespace.");

    [Fact]
    public void BuildVerificationRequest_ShouldReturnSuccess() =>
        new StartVerificationResponse(new Guid("06547d61-7ac0-43bb-94bd-503b24b2a3a5"), Maybe<Uri>.None)
            .BuildVerificationRequest("Some code.")
            .Should()
            .BeSuccess(request =>
            {
                request.RequestId.Should().Be(new Guid("06547d61-7ac0-43bb-94bd-503b24b2a3a5"));
                request.Code.Should().Be("Some code.");
            });
}