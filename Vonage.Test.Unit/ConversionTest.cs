using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Xunit;
namespace Vonage.Test.Unit
{
    public class ConversionTest : TestBase
    {
        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void SmsConversion(bool passCreds)
        {
            var expectedUri = $"{this.ApiUrl}/conversions/sms";
            var expectedContent = "message-id=00A0B0C0&delivered=true&timestamp=2020-01-01+12%3A00%3A00&api_key=testkey&api_secret=testSecret&";
            var expectedResponse = "";
            this.Setup(expectedUri, expectedResponse, expectedContent);
            var request = new Conversions.ConversionRequest { Delivered = true, MessageId = "00A0B0C0", TimeStamp = "2020-01-01 12:00:00" };
            var credentials = Request.Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);
            var client = new VonageClient(credentials);
            bool response;
            if (passCreds)
            {
                response = client.ConversionClient.SmsConversion(request, credentials);
            }
            else
            {
                response = client.ConversionClient.SmsConversion(request);
            }
            Assert.True(response);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void VoiceConversion(bool passCreds)
        {
            var expectedUri = $"{this.ApiUrl}/conversions/voice";
            var expectedContent = "message-id=00A0B0C0&delivered=true&timestamp=2020-01-01+12%3A00%3A00&api_key=testkey&api_secret=testSecret&";
            var expectedResponse = "";
            this.Setup(expectedUri, expectedResponse, expectedContent);
            var request = new Conversions.ConversionRequest { Delivered = true, MessageId = "00A0B0C0", TimeStamp = "2020-01-01 12:00:00" };
            var creds = Request.Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);
            var client = new VonageClient(creds);
            bool response;
            if (passCreds)
            {
                response = client.ConversionClient.VoiceConversion(request, creds);
            }
            else
            {
                response = client.ConversionClient.VoiceConversion(request);
            }
            Assert.True(response);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async void SmsConversionAsync(bool passCreds)
        {
            var expectedUri = $"{this.ApiUrl}/conversions/sms";
            var expectedContent = "message-id=00A0B0C0&delivered=true&timestamp=2020-01-01+12%3A00%3A00&api_key=testkey&api_secret=testSecret&";
            var expectedResponse = "";
            this.Setup(expectedUri, expectedResponse, expectedContent);
            var request = new Conversions.ConversionRequest { Delivered = true, MessageId = "00A0B0C0", TimeStamp = "2020-01-01 12:00:00" };
            var creds = Request.Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);
            var client = new VonageClient(creds);
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
        public async void VoiceConversionAsync(bool passCreds)
        {
            var expectedUri = $"{this.ApiUrl}/conversions/voice";
            var expectedContent = "message-id=00A0B0C0&delivered=true&timestamp=2020-01-01+12%3A00%3A00&api_key=testkey&api_secret=testSecret&";
            var expectedResponse = "";
            this.Setup(expectedUri, expectedResponse, expectedContent);
            var request = new Conversions.ConversionRequest { Delivered = true, MessageId = "00A0B0C0", TimeStamp = "2020-01-01 12:00:00" };
            var creds = Request.Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);
            var client = new VonageClient(creds);
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
