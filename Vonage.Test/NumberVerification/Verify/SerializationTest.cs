using Vonage.NumberVerification.Authenticate;
using Vonage.NumberVerification.Verify;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.NumberVerification.Verify;

[Trait("Category", "Serialization")]
public class SerializationTest
{
    private readonly SerializationTestHelper helper = new SerializationTestHelper(
        typeof(SerializationTest).Namespace,
        JsonSerializerBuilder.BuildWithSnakeCase());

    [Fact]
    public void ShouldDeserializeAuthorize() => this.helper.Serializer
        .DeserializeObject<AuthorizeResponse>(this.helper.GetResponseJson())
        .Should()
        .BeSuccess(GetExpectedAuthorizeResponse());

    [Fact]
    public void ShouldDeserializeAccessToken() => this.helper.Serializer
        .DeserializeObject<GetTokenResponse>(this.helper.GetResponseJson())
        .Should()
        .BeSuccess(GetExpectedTokenResponse());

    [Fact]
    public void ShouldDeserializeVerify() => this.helper.Serializer
        .DeserializeObject<VerifyResponse>(this.helper.GetResponseJson())
        .Should()
        .BeSuccess(GetExpectedResponse());

    [Fact]
    public void ShouldSerialize() =>
        VerifyRequest.Parse("346661113334")
            .GetStringContent()
            .Should()
            .BeSuccess(this.helper.GetRequestJson());

    private static AuthorizeResponse GetExpectedAuthorizeResponse() => new AuthorizeResponse("123456789", 120, 2);

    private static GetTokenResponse GetExpectedTokenResponse() => new GetTokenResponse("ABCDEFG", "Bearer", 3600);

    private static VerifyResponse GetExpectedResponse() => new VerifyResponse(true);
}