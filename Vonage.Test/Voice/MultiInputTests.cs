#region
using Newtonsoft.Json;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Voice.EventWebhooks;
using Vonage.Voice.Nccos;
using Xunit;
#endregion

namespace Vonage.Test.Voice;

[Trait("Category", "Legacy")]
public class MultiInputTests
{
    private readonly SerializationTestHelper helper = new SerializationTestHelper(typeof(MultiInputTests).Namespace,
        JsonSerializerBuilder.BuildWithCamelCase());

    [Fact]
    public void TestSerializeNccoAllProperties()
    {
        var ncco = new Ncco(MultiInputTestsTestData.CreateMultiInputActionWithAllProperties());
        var actual = JsonConvert.SerializeObject(ncco, VonageSerialization.SerializerSettings);
        actual.ShouldMatchExpectedJson(this.helper.GetResponseJson());
    }

    [Fact]
    public void TestSerializeNccoAllPropertiesEmpty()
    {
        var ncco = new Ncco(MultiInputTestsTestData.CreateEmptyMultiInputAction());
        var actual = JsonConvert.SerializeObject(ncco, VonageSerialization.SerializerSettings);
        actual.ShouldMatchExpectedJson(this.helper.GetResponseJson());
    }

    [Fact]
    public void TestWebhookSerialization()
    {
        var inboundString = this.helper.GetResponseJson();
        var serialized = JsonConvert.DeserializeObject<MultiInput>(inboundString);
        serialized.ShouldMatchExpectedWebhookSerialization();
    }

    [Fact]
    public void TestWebhookSerializationSpeechOverridden()
    {
        var inboundString = this.helper.GetResponseJson();
        var serialized = JsonConvert.DeserializeObject<MultiInput>(inboundString);
        serialized.ShouldMatchExpectedWebhookSerializationWithSpeechOverridden();
    }
}