using Vonage.Serialization;
using Vonage.SimSwap.Authenticate;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.SimSwap.Authenticate;

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
    
    internal static AuthorizeResponse GetExpectedAuthorizeResponse() => new AuthorizeResponse("123456789", 120, 2);
    
    internal static GetTokenResponse GetExpectedTokenResponse() => new GetTokenResponse("ABCDEFG", "Bearer", 3600);
}