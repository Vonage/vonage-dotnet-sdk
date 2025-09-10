#region
using Newtonsoft.Json;
using Vonage.Serialization;
using Xunit;
#endregion

namespace Vonage.Test.Voice;

[Trait("Category", "Legacy")]
public class StreamActionTest
{
    [Fact]
    public void TestStreamUrl()
    {
        var action = StreamActionTestTestData.CreateStreamActionWithUrl();
        var serialized = JsonConvert.SerializeObject(action, VonageSerialization.SerializerSettings);
        serialized.ShouldMatchExpectedStreamUrlJson();
    }
}