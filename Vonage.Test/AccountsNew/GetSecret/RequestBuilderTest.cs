using Vonage.AccountsNew.GetSecret;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.AccountsNew.GetSecret;

[Trait("Category", "Request")]
[Trait("Product", "AccountsNew")]
public class RequestBuilderTest
{
    [Fact]
    public void Build_ShouldSetApiKey() =>
        GetSecretRequest.Build()
            .WithApiKey("abcd1234")
            .WithSecretId("ad6dc56f-07b5-46e1-a527-85530e625800")
            .Create()
            .Map(r => r.ApiKey)
            .Should()
            .BeSuccess("abcd1234");

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Build_ShouldReturnFailure_GivenApiKeyIsNullOrWhitespace(string invalidKey) =>
        GetSecretRequest.Build()
            .WithApiKey(invalidKey)
            .WithSecretId("ad6dc56f-07b5-46e1-a527-85530e625800")
            .Create()
            .Should()
            .BeParsingFailure("ApiKey cannot be null or whitespace.");

    [Fact]
    public void Build_ShouldSetSecretId() =>
        GetSecretRequest.Build()
            .WithApiKey("abcd1234")
            .WithSecretId("ad6dc56f-07b5-46e1-a527-85530e625800")
            .Create()
            .Map(r => r.SecretId)
            .Should()
            .BeSuccess("ad6dc56f-07b5-46e1-a527-85530e625800");

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Build_ShouldReturnFailure_GivenSecretIdIsNullOrWhitespace(string invalidId) =>
        GetSecretRequest.Build()
            .WithApiKey("abcd1234")
            .WithSecretId(invalidId)
            .Create()
            .Should()
            .BeParsingFailure("SecretId cannot be null or whitespace.");
}
