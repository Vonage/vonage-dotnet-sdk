using AutoFixture;
using FluentAssertions;
using Vonage.Video.Beta.Common.Failures;
using Vonage.Video.Beta.Common.Identifiers;
using Vonage.Video.Beta.Test.Extensions;
using Xunit;

namespace Vonage.Video.Beta.Test.Common.Identifiers
{
    public class StringIdentifierTest
    {
        private readonly string value;

        public StringIdentifierTest() => this.value = new Fixture().Create<string>();

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Parse_ShouldReturnFailure_GivenValueIsEmpty(string emptyValue) =>
            StringIdentifier.Parse(emptyValue)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Value cannot be null or whitespace."));

        [Fact]
        public void Parse_ShouldReturnSuccess_GivenValuesAreProvided() =>
            StringIdentifier.Parse(this.value)
                .Should()
                .BeSuccess(request => request.Value.Should().Be(this.value));
    }
}