#region
using System.Net;
using System.Threading.Tasks;
using Vonage.DeviceStatus.GetConnectivityStatus;
using Vonage.Test.Common.Extensions;
using WireMock.ResponseBuilders;
using Xunit;
#endregion

namespace Vonage.Test.DeviceStatus.GetConnectivityStatus;

[Trait("Category", "E2E")]
public class E2ETest : E2EBase
{
    public E2ETest() : base(typeof(E2ETest).Namespace)
    {
    }

    [Fact]
    public async Task GetConnectivityStatusAsync()
    {
        this.SetupAuthorization();
        this.SetupToken();
        this.SetupConnectivity(nameof(SerializationTest.ShouldSerialize));
        await this.Helper.VonageClient.DeviceStatusClient
            .GetConnectivityStatusAsync(GetConnectivityStatusRequest.Build().WithPhoneNumber("123456789").Create())
            .Should()
            .BeSuccessAsync(SerializationTest.GetExpectedResponse());
    }

    private void SetupConnectivity(string expectedOutput) =>
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/camara/device-status/v050/connectivity")
                .WithHeader("Authorization", "Bearer ABCDEFG")
                .WithBody(this.Serialization.GetRequestJson(expectedOutput))
                .UsingPost())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserializeConnectivity))));

    private void SetupToken() =>
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/oauth2/token")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .WithBody("auth_req_id=123456789&grant_type=urn:openid:params:grant-type:ciba")
                .UsingPost())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserializeAccessToken))));

    private void SetupAuthorization() =>
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/oauth2/bc-authorize")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .WithBody(
                    "login_hint=tel:%2B123456789&scope=dpv%3ANotApplicable%23device-status%3Aconnectivity%3Aread")
                .UsingPost())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserializeAuthorize))));
}