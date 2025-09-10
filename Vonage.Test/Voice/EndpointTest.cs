#region
using Newtonsoft.Json;
using Vonage.Serialization;
using Xunit;
#endregion

namespace Vonage.Test.Voice;

[Trait("Category", "Legacy")]
public class EndpointTest
{
    [Fact]
    public void TestNccoEndpoint()
    {
        var websocketEndpoint = EndpointTestTestData.CreateWebsocketEndpointWithHeaders();
        var json = JsonConvert.SerializeObject(websocketEndpoint, VonageSerialization.SerializerSettings);
        json.ShouldMatchExpectedNccoEndpointJson();
    }

    [Fact]
    public void TestWebhookEndpoint()
    {
        var websocketEndpoint = EndpointTestTestData.CreateCallEndpointWithHeaders();
        var json = JsonConvert.SerializeObject(websocketEndpoint, VonageSerialization.SerializerSettings);
        json.ShouldMatchExpectedWebhookEndpointJson();
    }
}