using System.Web;
using System.Collections.Generic;
using Xunit;
using Newtonsoft.Json;
using Nexmo.Api.Cryptography;

namespace Nexmo.Api.UnitTest
{
    public class SMS_test : TestBase
    {      

        [Fact]
        public void TestSmsRequest()
        {

            // ARRANGE
            var restUrl = "https://rest.nexmo.com";
            var expectedUri = $"{restUrl}/sms/json";            
            var responseContent = "{\"message-count\": \"1\",\"messages\": [{\"to\": \"17775551212\",\"message-id\": \"02000000A3AF32FA\",\"status\": \"0\",\"remaining-balance\": \"7.55560000\",\"message-price\": \"0.00480000\",\"network\": \"310004\"}]}";
            var from = "98975";
            var to = "17775551212";
            var text = "this is a test";
            var requestContent = $"from={from}&to={to}&text={HttpUtility.UrlEncode(text)}&api_key={ApiKey}&api_secret={ApiSecret}&";
            Setup(uri: expectedUri, responseContent: responseContent, requestContent: requestContent);

            // ACT
            var client = new Client(new Request.Credentials() { ApiKey = ApiKey, ApiSecret = ApiSecret });
            var response = client.SMS.Send(new SMS.SMSRequest() { to = to, from = from, text = text });

            //ASSERT
            Assert.True(response.messages.Count == 1);
        }

        [Fact]
        public void TestSmsWithoutFrom()
        {
            // ARRANGE
            var restUrl = "https://rest.nexmo.com";
            var expectedUri = $"{restUrl}/sms/json";
            var responseContent = "{\"message-count\": \"1\",\"messages\": [{\"to\": \"17775551212\",\"message-id\": \"02000000A3AF32FA\",\"status\": \"0\",\"remaining-balance\": \"7.55560000\",\"message-price\": \"0.00480000\",\"network\": \"310004\"}]}";
          
            var to = "17775551212";
            var text = "this is a test";
            var requestContent = $"to={to}&text={HttpUtility.UrlEncode(text)}&api_key={ApiKey}&api_secret={ApiSecret}&";
            Setup(uri: expectedUri, responseContent: responseContent, requestContent: requestContent);

            // ACT
            var client = new Client(new Request.Credentials() { ApiKey = ApiKey, ApiSecret = ApiSecret });
            var response = client.SMS.Send(new SMS.SMSRequest() { to = to, text = text });

            //ASSERT
            Assert.True(response.messages.Count == 1);
        }

        [Fact]
        public void TestGenerateSignature()
        {
            var signingKey = "2zzXDyLUAEdT8rTcKqJuOwgPmRYBDAu4jXDi0GmoARevPdOZ1R";
            var expectedSig = "666c2c1a3fe7d621ad10456c4531e702";
            var request = @"{
              ""msisdn"": ""447700900001"",
              ""to"": ""447700900000"",
              ""messageId"": ""0A0000000123ABCD1"",
              ""text"": ""Hello world"",
              ""type"": ""text"",
              ""keyword"": ""HELLO"",
              ""message-timestamp"": ""2020-01-01T12:00:00.000+00:00"",
              ""timestamp"": ""1578787200"",
              ""nonce"": ""aaaaaaaa-bbbb-cccc-dddd-0123456789ab"",
              ""concat"": ""true"",
              ""concat-ref"": ""1"",
              ""concat-total"": ""3"",
              ""concat-part"": ""2"",
              ""data"": ""abc123"",
              ""udh"": ""abc123""
            }";
            var message = JsonConvert.DeserializeObject<SMS.SMSInbound>(request);
            var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(request);
            var signatureString = SMS.SMSInbound.ConstructSignatureStringFromDictionary(dict);
            var method = SmsSignatureGenerator.Method.md5hash;
            var testSig = SmsSignatureGenerator.GenerateSignature(signatureString, signingKey, method);
            Assert.Equal(testSig, expectedSig);

        }
    }
}
