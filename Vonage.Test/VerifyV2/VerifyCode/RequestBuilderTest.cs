using System;
using AutoFixture;
using FluentAssertions;
using Vonage.Test.Common.Extensions;
using Vonage.VerifyV2.VerifyCode;
using Xunit;

namespace Vonage.Test.VerifyV2.VerifyCode;

public class RequestBuilderTest
{
    private readonly Fixture fixture = new Fixture();

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Create_ShouldReturnFailure_GivenCodeIsNullOrWhitespace(string value) =>
        VerifyCodeRequest.Build()
            .WithRequestId(this.fixture.Create<Guid>())
            .WithCode(value)
            .Create()
            .Should()
            .BeParsingFailure("Code cannot be null or whitespace.");

    [Fact]
    public void Create_ShouldReturnFailure_GivenRequestIdIsEmpty() =>
        VerifyCodeRequest.Build()
            .WithRequestId(Guid.Empty)
            .WithCode(this.fixture.Create<string>())
            .Create()
            .Should()
            .BeParsingFailure("RequestId cannot be empty.");

    [Fact]
    public void Create_ShouldReturnSuccess() =>
        VerifyCodeRequest.Build()
            .WithRequestId(new Guid("06547d61-7ac0-43bb-94bd-503b24b2a3a5"))
            .WithCode("Some code.")
            .Create()
            .Should()
            .BeSuccess(request =>
            {
                request.RequestId.Should().Be(new Guid("06547d61-7ac0-43bb-94bd-503b24b2a3a5"));
                request.Code.Should().Be("Some code.");
            });
}