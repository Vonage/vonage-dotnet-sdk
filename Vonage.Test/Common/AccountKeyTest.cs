using FsCheck;
using FsCheck.Xunit;
using Vonage.Common;
using Vonage.Common.Failures;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.Common;

public class AccountKeyTest
{
    [Theory]
    [InlineData("1 3a5Acs")]
    [InlineData("12-EE6e8")]
    [InlineData("1/34s6QQ")]
    [InlineData("_E34s6QQ")]
    public void Parse_ShouldReturnFailure_GivenApiKeyContainsNonAlphaNumericCharacters(string invalidApiKey) =>
        AccountKey.Parse(invalidApiKey)
            .Should()
            .BeFailure(ResultFailure.FromErrorMessage("AccountKey should only contain alphanumeric characters."));

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Parse_ShouldReturnFailure_GivenApiKeyIsNullOrWhitespace(string invalidApiKey) =>
        AccountKey.Parse(invalidApiKey)
            .Should()
            .BeFailure(ResultFailure.FromErrorMessage("AccountKey cannot be null or whitespace."));

    [Property]
    public Property Parse_ShouldReturnFailure_GivenApiKeyLengthIsDifferentThan8() =>
        Prop.ForAll(
            Arb.From<string>().MapFilter(_ => _, value => !string.IsNullOrWhiteSpace(value) && value.Length != 8),
            invalidApiKey => AccountKey.Parse(invalidApiKey)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("AccountKey length should be 8.")));

    [Fact]
    public void Parse_ShouldReturnSuccess_GivenApiKeyMatchesCriteria() =>
        AccountKey.Parse("1E34s6QQ")
            .Map(key => key.ApiKey)
            .Should()
            .BeSuccess("1E34s6QQ");

    [Fact]
    public void ToString_ShouldReturnApiKey() =>
        AccountKey.Parse("1E34s6QQ")
            .Map(key => key.ToString())
            .Should()
            .BeSuccess("1E34s6QQ");
}