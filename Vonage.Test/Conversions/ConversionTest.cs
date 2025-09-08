#region
using System.Threading.Tasks;
using Vonage.Conversions;
using Vonage.Request;
using Vonage.Serialization;
using Vonage.Test.Common;
using Xunit;
#endregion

namespace Vonage.Test.Conversions;

[Trait("Category", "Legacy")]
public class ConversionTest : TestBase
{
    private readonly SerializationTestHelper helper = new SerializationTestHelper(typeof(ConversionTest).Namespace,
        JsonSerializerBuilder.BuildWithCamelCase());

    [Fact]
    public async Task SmsConversion()
    {
        this.Setup($"{this.ApiUrl}/conversions/sms", this.helper.GetResponseJson(),
            "message-id=00A0B0C0&delivered=true&timestamp=2020-01-01+12%3A00%3A00&api_key=testkey&api_secret=testSecret&");
        var response = await this.BuildConversionClient().SmsConversionAsync(ConversionTestData.CreateBasicRequest());
        response.ShouldBeSuccessfulConversion();
    }

    [Fact]
    public async Task VoiceConversion()
    {
        this.Setup($"{this.ApiUrl}/conversions/voice", this.helper.GetResponseJson(),
            "message-id=00A0B0C0&delivered=true&timestamp=2020-01-01+12%3A00%3A00&api_key=testkey&api_secret=testSecret&");
        var response = await this.BuildConversionClient().VoiceConversionAsync(ConversionTestData.CreateBasicRequest());
        response.ShouldBeSuccessfulConversion();
    }

    private IConversionClient BuildConversionClient() =>
        this.BuildVonageClient(Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret)).ConversionClient;
}