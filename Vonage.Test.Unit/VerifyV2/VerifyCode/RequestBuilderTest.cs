using AutoFixture;
using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.VerifyV2.VerifyCode;
using Xunit;

namespace Vonage.Test.Unit.VerifyV2.VerifyCode
{
    public class RequestBuilderTest
    {
        private readonly Fixture fixture;

        public RequestBuilderTest() => this.fixture = new Fixture();

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Create_ShouldReturnFailure_GivenCodeIsNullOrWhitespace(string value) =>
            VerifyCodeRequest.Build()
                .WithRequestId(this.fixture.Create<string>())
                .WithCode(value)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Code cannot be null or whitespace."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Create_ShouldReturnFailure_GivenRequestIdIsNullOrWhitespace(string value) =>
            VerifyCodeRequest.Build()
                .WithRequestId(value)
                .WithCode(this.fixture.Create<string>())
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("RequestId cannot be null or whitespace."));

        [Fact]
        public void Create_ShouldReturnSuccess() =>
            VerifyCodeRequest.Build()
                .WithRequestId("Some request id.")
                .WithCode("Some code.")
                .Create()
                .Should()
                .BeSuccess(request =>
                {
                    request.RequestId.Should().Be("Some request id.");
                    request.Code.Should().Be("Some code.");
                });
    }
}