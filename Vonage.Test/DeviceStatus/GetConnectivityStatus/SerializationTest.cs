using Vonage.DeviceStatus.Authenticate;
using Vonage.DeviceStatus.GetConnectivityStatus;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.DeviceStatus.GetConnectivityStatus;

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
    public void ShouldDeserializeConnectivity() => this.helper.Serializer
        .DeserializeObject<GetConnectivityStatusResponse>(this.helper.GetResponseJson())
        .Should()
        .BeSuccess(GetExpectedResponse());
    
    [Theory]
    [InlineData("123456789")]
    [InlineData("+123456789")]
    public void ShouldSerialize(string number) =>
        GetConnectivityStatusRequest.Build()
            .WithPhoneNumber(number)
            .Create()
            .GetStringContent()
            .Should()
            .BeSuccess(this.helper.GetRequestJson());
    
    private static AuthorizeResponse GetExpectedAuthorizeResponse() => new AuthorizeResponse("123456789", 120, 2);
    
    private static GetTokenResponse GetExpectedTokenResponse() => new GetTokenResponse("ABCDEFG", "Bearer", 3600);
    
    internal static GetConnectivityStatusResponse GetExpectedResponse() => new GetConnectivityStatusResponse("CONNECTED_DATA");
}