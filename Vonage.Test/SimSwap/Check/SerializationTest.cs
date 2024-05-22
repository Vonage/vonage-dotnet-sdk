using Vonage.Serialization;
using Vonage.SimSwap.Authenticate;
using Vonage.SimSwap.Check;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.SimSwap.Check;

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
    public void ShouldDeserializeCheck() => this.helper.Serializer
        .DeserializeObject<CheckResponse>(this.helper.GetResponseJson())
        .Should()
        .BeSuccess(GetExpectedResponse());
    
    [Fact]
    public void ShouldSerialize() =>
        CheckRequest.Build()
            .WithPhoneNumber("346661113334")
            .Create()
            .GetStringContent()
            .Should()
            .BeSuccess(this.helper.GetRequestJson());
    
    [Fact]
    public void ShouldSerializeWithPeriod() =>
        CheckRequest.Build()
            .WithPhoneNumber("346661113334")
            .WithPeriod(15)
            .Create()
            .GetStringContent()
            .Should()
            .BeSuccess(this.helper.GetRequestJson());
    
    private static AuthorizeResponse GetExpectedAuthorizeResponse() => new AuthorizeResponse("123456789", "120", "2");
    
    private static GetTokenResponse GetExpectedTokenResponse() => new GetTokenResponse("ABCDEFG", "Bearer", 3600);
    
    private static CheckResponse GetExpectedResponse() => new CheckResponse(true);
}