using System.Threading.Tasks;
using Vonage.Conversions;
using Vonage.Request;
using Xunit;

namespace Vonage.Test.Unit
{
    public class ConversionTest : TestBase
    {
        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task SmsConversionAsync(bool passCreds)
        {
            var expectedUri = $"{this.ApiUrl}/conversions/sms";
            var expectedContent =
                "message-id=00A0B0C0&delivered=true&timestamp=2020-01-01+12%3A00%3A00&api_key=testkey&api_secret=testSecret&";
            var expectedResponse = "";
            this.Setup(expectedUri, expectedResponse, expectedContent);
            var request = new ConversionRequest
                {Delivered = true, MessageId = "00A0B0C0", TimeStamp = "2020-01-01 12:00:00"};
            var creds = Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);
            var client = this.BuildVonageClient(creds);
            bool response;
            if (passCreds)
            {
                response = await client.ConversionClient.SmsConversionAsync(request, creds);
            }
            else
            {
                response = await client.ConversionClient.SmsConversionAsync(request);
            }

            Assert.True(response);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task VoiceConversionAsync(bool passCreds)
        {
            var expectedUri = $"{this.ApiUrl}/conversions/voice";
            var expectedContent =
                "message-id=00A0B0C0&delivered=true&timestamp=2020-01-01+12%3A00%3A00&api_key=testkey&api_secret=testSecret&";
            var expectedResponse = "";
            this.Setup(expectedUri, expectedResponse, expectedContent);
            var request = new ConversionRequest
                {Delivered = true, MessageId = "00A0B0C0", TimeStamp = "2020-01-01 12:00:00"};
            var creds = Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);
            var client = this.BuildVonageClient(creds);
            bool response;
            if (passCreds)
            {
                response = await client.ConversionClient.VoiceConversionAsync(request, creds);
            }
            else
            {
                response = await client.ConversionClient.VoiceConversionAsync(request);
            }

            Assert.True(response);
        }
    }
}