using System;
using Vonage.Serialization;
using Vonage.SimSwap.Authenticate;
using Vonage.SimSwap.GetSwapDate;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.SimSwap.GetSwapDate;

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
    public void ShouldDeserializeGetSwapDate() => this.helper.Serializer
        .DeserializeObject<GetSwapDateResponse>(this.helper.GetResponseJson())
        .Should()
        .BeSuccess(GetExpectedResponse());
    
    [Fact]
    public void ShouldSerialize() =>
        GetSwapDateRequest.Parse("346661113334")
            .GetStringContent()
            .Should()
            .BeSuccess(this.helper.GetRequestJson());
    
    private static AuthorizeResponse GetExpectedAuthorizeResponse() => new AuthorizeResponse("123456789", "120", "2");
    
    private static GetTokenResponse GetExpectedTokenResponse() => new GetTokenResponse("ABCDEFG", "Bearer", 3600);
    
    private static GetSwapDateResponse GetExpectedResponse() =>
        new GetSwapDateResponse(DateTimeOffset.Parse("2019-08-24T14:15:22Z"));
}