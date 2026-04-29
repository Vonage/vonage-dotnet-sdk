using Vonage.AccountsNew.CreateSecret;
using Vonage.Test.Common.Extensions;
using Vonage.Test.Common.TestHelpers;
using Xunit;

namespace Vonage.Test.AccountsNew.CreateSecret;

[Trait("Category", "Request")]
[Trait("Product", "AccountsNew")]
public class RequestBuilderTest
{
    [Fact]
    public void Build_ShouldSetApiKey() =>
        CreateSecretRequest.Build()
            .WithApiKey("abcd1234")
            .WithSecret("example-4PI-s3cret")
            .Create()
            .Map(r => r.ApiKey)
            .Should()
            .BeSuccess("abcd1234");

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Build_ShouldReturnFailure_GivenApiKeyIsNullOrWhitespace(string invalidKey) =>
        CreateSecretRequest.Build()
            .WithApiKey(invalidKey)
            .WithSecret("example-4PI-s3cret")
            .Create()
            .Should()
            .BeParsingFailure("ApiKey cannot be null or whitespace.");

    [Fact]
    public void Build_ShouldSetSecret() =>
        CreateSecretRequest.Build()
            .WithApiKey("abcd1234")
            .WithSecret("example-4PI-s3cret")
            .Create()
            .Map(r => r.Secret)
            .Should()
            .BeSuccess("example-4PI-s3cret");

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Build_ShouldReturnFailure_GivenSecretIsNullOrWhitespace(string invalidSecret) =>
        CreateSecretRequest.Build()
            .WithApiKey("abcd1234")
            .WithSecret(invalidSecret)
            .Create()
            .Should()
            .BeParsingFailure("Secret cannot be null or whitespace.");

    [Fact]
    public void Build_ShouldReturnFailure_GivenSecretIsTooShort() =>
        CreateSecretRequest.Build()
            .WithApiKey("abcd1234")
            .WithSecret("Ab1defg")
            .Create()
            .Should()
            .BeParsingFailure("Secret length cannot be lower than 8.");

    [Fact]
    public void Build_ShouldReturnFailure_GivenSecretIsTooLong() =>
        CreateSecretRequest.Build()
            .WithApiKey("abcd1234")
            .WithSecret(StringHelper.GenerateString(25) + "Ab1")
            .Create()
            .Should()
            .BeParsingFailure("Secret length cannot be higher than 25.");

    [Fact]
    public void Build_ShouldReturnFailure_GivenSecretHasNoLowercase() =>
        CreateSecretRequest.Build()
            .WithApiKey("abcd1234")
            .WithSecret("EXAMPLE4PISECRET")
            .Create()
            .Should()
            .BeParsingFailure("Secret must contain at least 1 lowercase character.");

    [Fact]
    public void Build_ShouldReturnFailure_GivenSecretHasNoUppercase() =>
        CreateSecretRequest.Build()
            .WithApiKey("abcd1234")
            .WithSecret("example4pisecret")
            .Create()
            .Should()
            .BeParsingFailure("Secret must contain at least 1 uppercase character.");

    [Fact]
    public void Build_ShouldReturnFailure_GivenSecretHasNoDigit() =>
        CreateSecretRequest.Build()
            .WithApiKey("abcd1234")
            .WithSecret("ExamplePiSecret!")
            .Create()
            .Should()
            .BeParsingFailure("Secret must contain at least 1 digit.");
}
