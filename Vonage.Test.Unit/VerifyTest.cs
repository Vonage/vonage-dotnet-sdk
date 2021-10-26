using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Vonage.Verify;
using Xunit;
namespace Vonage.Test.Unit
{
    public class VerifyTest : TestBase
    {
        [Theory]
        [InlineData(true, true)]
        [InlineData(false, false)]
        public void RequestVerification(bool passCreds, bool kitchenSink)
        {
            var expectedResponse = @"{
              ""request_id"": ""abcdef0123456789abcdef0123456789"",
              ""status"": ""0""
            }";
            var expectedUri = $"{ApiUrl}/verify/json";

            string expectedRequestContent;
            VerifyRequest request = new VerifyRequest { Number= "447700900000", Brand="Acme Inc"};
            if (kitchenSink)
            {
                expectedRequestContent = $"brand={HttpUtility.UrlEncode("Acme Inc")}&sender_id=ACME&workflow_id=1&number=447700900000&country=GB&code_length=4&lg=en-us&pin_expiry=240&next_event_wait=60&api_key={ApiKey}&api_secret={ApiSecret}&";
                request.Country = "GB";
                request.SenderId = "ACME";
                request.CodeLength = 4;
                request.Lg = "en-us";
                request.PinExpiry = 240;
                request.NextEventWait = 60;
                request.WorkflowId = VerifyRequest.Workflow.SMS_TTS_TTS;
            }
            else 
            {
                expectedRequestContent = $"brand={HttpUtility.UrlEncode("Acme Inc")}&number=447700900000&api_key={ApiKey}&api_secret={ApiSecret}&";
            }
            Setup(expectedUri, expectedResponse, expectedRequestContent);
            var creds = Request.Credentials.FromApiKeyAndSecret(ApiKey, ApiSecret);
            var client = new VonageClient(creds);
            VerifyResponse response;
            if (passCreds)
            {
                response = client.VerifyClient.VerifyRequest(request, creds);
            }
            else
            {
                response = client.VerifyClient.VerifyRequest(request);
            }

            Assert.Equal("abcdef0123456789abcdef0123456789", response.RequestId);
            Assert.Equal("0", response.Status);
        }

        [Theory]
        [InlineData(true, true)]
        [InlineData(false, false)]
        public void TestCheckVerification(bool passCreds, bool kitchenSink)
        {
            var expectedResponse = @"{
              ""request_id"": ""abcdef0123456789abcdef0123456789"",
              ""event_id"": ""0A00000012345678"",
              ""status"": ""0"",
              ""price"": ""0.10000000"",
              ""currency"": ""EUR"",
              ""estimated_price_messages_sent"": ""0.03330000""
            }";
            var expectedUri = $"{ApiUrl}/verify/json";

            string expectedRequestContent;
            VerifyCheckRequest request = new VerifyCheckRequest { Code = "1234", RequestId = "abcdef0123456789abcdef0123456789" };
            if (kitchenSink)
            {
                expectedRequestContent = $"request_id=abcdef0123456789abcdef0123456789&code=1234&ip_address={HttpUtility.UrlEncode("123.0.0.255")}&api_key={ApiKey}&api_secret={ApiSecret}&";
                request.IpAddress = "123.0.0.255";
            }
            else
            {
                expectedRequestContent = $"request_id=abcdef0123456789abcdef0123456789&code=1234&api_key={ApiKey}&api_secret={ApiSecret}&";
            }
            Setup(expectedUri, expectedResponse, expectedRequestContent);
            var creds = Request.Credentials.FromApiKeyAndSecret(ApiKey, ApiSecret);
            var client = new VonageClient(creds);
            VerifyCheckResponse response;
            if (passCreds)
            {
                response = client.VerifyClient.VerifyCheck(request, creds);
            }
            else
            {
                response = client.VerifyClient.VerifyCheck(request);
            }
            Assert.Equal("0.10000000", response.Price);
            Assert.Equal("0.03330000", response.EstimatedPriceMessagesSent);
            Assert.Equal("EUR", response.Currency);
            Assert.Equal("0A00000012345678", response.EventId);
            Assert.Equal("abcdef0123456789abcdef0123456789", response.RequestId);
            Assert.Equal("0", response.Status);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void TestVerifySearch(bool passCreds)
        {
            var expectedResponse = @"{
              ""request_id"": ""abcdef0123456789abcdef0123456789"",
              ""account_id"": ""abcdef01"",
              ""status"": ""IN PROGRESS"",
              ""number"": ""447700900000"",
              ""price"": ""0.10000000"",
              ""currency"": ""EUR"",
              ""sender_id"": ""mySenderId"",
              ""date_submitted"": ""2020-01-01 12:00:00"",
              ""date_finalized"": ""2020-01-01 12:00:00"",
              ""first_event_date"": ""2020-01-01 12:00:00"",
              ""last_event_date"": ""2020-01-01 12:00:00"",
              ""checks"": [
                {
                  ""date_received"": ""2020-01-01 12:00:00"",
                  ""code"": ""987654"",
                  ""status"": ""abc123"",
                  ""ip_address"": ""123.0.0.255""
                }
              ],
              ""events"": [
                {
                  ""type"": ""abc123"",
                  ""id"": ""abc123""
                }
              ],
              ""estimated_price_messages_sent"": ""0.03330000""
            }";
            var expectedUri = $"{ApiUrl}/verify/search/json?request_id=abcdef0123456789abcdef0123456789&api_key={ApiKey}&api_secret={ApiSecret}&";
            Setup(expectedUri, expectedResponse);
            var request = new VerifySearchRequest { RequestId = "abcdef0123456789abcdef0123456789" };
            var creds = Request.Credentials.FromApiKeyAndSecret(ApiKey, ApiSecret);
            var client = new VonageClient(creds);
            VerifySearchResponse response;
            if (passCreds)
            {
                response = client.VerifyClient.VerifySearch(request, creds);
            }
            else
            {
                response = client.VerifyClient.VerifySearch(request);
            }

            var req = response;
            Assert.Equal("abcdef0123456789abcdef0123456789", req.RequestId);
            Assert.Equal("abcdef01", req.AccountId);
            Assert.Equal("IN PROGRESS", req.Status);
            Assert.Equal("447700900000", req.Number);
            Assert.Equal("0.10000000", req.Price);
            Assert.Equal("EUR", req.Currency);
            Assert.Equal("mySenderId", req.SenderId);
            Assert.Equal("2020-01-01 12:00:00", req.DateSubmitted);
            Assert.Equal("2020-01-01 12:00:00", req.DateFinalized);
            Assert.Equal("2020-01-01 12:00:00", req.FirstEventDate);
            Assert.Equal("2020-01-01 12:00:00", req.LastEventDate);
            Assert.Equal("2020-01-01 12:00:00", req.Checks[0].DateReceived);
            Assert.Equal("987654", req.Checks[0].Code);
            Assert.Equal("abc123", req.Checks[0].Status);
            Assert.Equal("123.0.0.255", req.Checks[0].IpAddress);
            Assert.Equal("abc123", req.Events[0].Type);
            Assert.Equal("abc123", req.Events[0].Id);
            Assert.Equal("0.03330000", req.EstimatedPriceMessagesSent);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void TestControlVerify(bool passCreds)
        {
            var expectedResponse = @"{
              ""status"": ""0"",
              ""command"": ""cancel""
            }";

            var expectedUri = $"{ApiUrl}/verify/control/json";
            var requestContent = $"request_id=abcdef0123456789abcdef0123456789&cmd=cancel&api_key={ApiKey}&api_secret={ApiSecret}&";
            Setup(expectedUri, expectedResponse, requestContent);

            var request = new VerifyControlRequest { Cmd = "cancel", RequestId = "abcdef0123456789abcdef0123456789" };
            var creds = Request.Credentials.FromApiKeyAndSecret(ApiKey, ApiSecret);
            var client = new VonageClient(creds);
            VerifyControlResponse response;
            if (passCreds)
            {
                response = client.VerifyClient.VerifyControl(request, creds);
            }
            else
            {
                response = client.VerifyClient.VerifyControl(request, creds);
            }
            Assert.Equal("0", response.Status);
            Assert.Equal("cancel", response.Command);
        }

        [Fact]
        public void TestControlVerifyInvalidCredentials()
        {
            var expectedResponse = @"{
              ""status"": ""4"",
              ""error_text"": ""invalid credentials""
            }";

            var expectedUri = $"{ApiUrl}/verify/control/json";
            var requestContent = $"request_id=abcdef0123456789abcdef0123456789&cmd=cancel&api_key={ApiKey}&api_secret={ApiSecret}&";
            Setup(expectedUri, expectedResponse, requestContent);

            var request = new VerifyControlRequest { Cmd = "cancel", RequestId = "abcdef0123456789abcdef0123456789" };
            var creds = Request.Credentials.FromApiKeyAndSecret(ApiKey, ApiSecret);
            var client = new VonageClient(creds);
            try
            {
                var response = client.VerifyClient.VerifyControl(request, creds);
                Assert.True(false, "Automatically failing because exception wasn't thrown");
            }            
            catch(VonageVerifyResponseException ex)
            {
                Assert.Equal("4", ex.Response.Status);
                Assert.Equal("invalid credentials", ex.Response.ErrorText);
            }            
        }

        [Theory]
        [InlineData(true, true)]
        [InlineData(false, false)]
        public void Psd2Verification(bool passCreds, bool kitchenSink)
        {
            var expectedResponse = @"{
              ""request_id"": ""abcdef0123456789abcdef0123456789"",
              ""status"": ""0""
            }";
            var expectedUri = $"{ApiUrl}/verify/psd2/json";

            string expectedRequestContent;
            Psd2Request request = new Psd2Request { Number = "447700900000", Payee = "Acme Inc", Amount = 4.8 };
            if (kitchenSink)
            {
                expectedRequestContent = $"payee={HttpUtility.UrlEncode("Acme Inc")}&amount=4.8&workflow_id=1&number=447700900000&country=GB&code_length=4&lg=en-us&pin_expiry=240&next_event_wait=60&api_key={ApiKey}&api_secret={ApiSecret}&";
                request.Country = "GB";
                request.CodeLength = 4;
                request.Lg = "en-us";
                request.PinExpiry = 240;
                request.NextEventWait = 60;
                request.WorkflowId = Psd2Request.Workflow.SMS_TTS_TTS;
            }
            else
            {
                expectedRequestContent = $"payee={HttpUtility.UrlEncode("Acme Inc")}&amount=4.8&number=447700900000&api_key={ApiKey}&api_secret={ApiSecret}&";
            }

            Setup(expectedUri, expectedResponse, expectedRequestContent);
            var creds = Request.Credentials.FromApiKeyAndSecret(ApiKey, ApiSecret);
            var client = new VonageClient(creds);
            VerifyResponse response;
            if (passCreds)
            {
                response = client.VerifyClient.VerifyRequestWithPSD2(request, creds);
            }
            else
            {
                response = client.VerifyClient.VerifyRequestWithPSD2(request);
            }

            Assert.Equal("abcdef0123456789abcdef0123456789", response.RequestId);
            Assert.Equal("0", response.Status);
        }

        [Theory]
        [InlineData(true, true)]
        [InlineData(false, false)]
        public async void RequestVerificationAsync(bool passCreds, bool kitchenSink)
        {
            var expectedResponse = @"{
              ""request_id"": ""abcdef0123456789abcdef0123456789"",
              ""status"": ""0""
            }";
            var expectedUri = $"{ApiUrl}/verify/json";

            string expectedRequestContent;
            VerifyRequest request = new VerifyRequest { Number = "447700900000", Brand = "Acme Inc" };
            if (kitchenSink)
            {
                expectedRequestContent = $"brand={HttpUtility.UrlEncode("Acme Inc")}&sender_id=ACME&workflow_id=1&number=447700900000&country=GB&code_length=4&lg=en-us&pin_expiry=240&next_event_wait=60&api_key={ApiKey}&api_secret={ApiSecret}&";
                request.Country = "GB";
                request.SenderId = "ACME";
                request.CodeLength = 4;
                request.Lg = "en-us";
                request.PinExpiry = 240;
                request.NextEventWait = 60;
                request.WorkflowId = VerifyRequest.Workflow.SMS_TTS_TTS;
            }
            else
            {
                expectedRequestContent = $"brand={HttpUtility.UrlEncode("Acme Inc")}&number=447700900000&api_key={ApiKey}&api_secret={ApiSecret}&";
            }
            Setup(expectedUri, expectedResponse, expectedRequestContent);
            var creds = Request.Credentials.FromApiKeyAndSecret(ApiKey, ApiSecret);
            var client = new VonageClient(creds);
            VerifyResponse response;
            if (passCreds)
            {
                response = await client.VerifyClient.VerifyRequestAsync(request, creds);
            }
            else
            {
                response = await client.VerifyClient.VerifyRequestAsync(request);
            }

            Assert.Equal("abcdef0123456789abcdef0123456789", response.RequestId);
            Assert.Equal("0", response.Status);
        }

        [Theory]
        [InlineData(true, true)]
        [InlineData(false, false)]
        public async void TestCheckVerificationAsync(bool passCreds, bool kitchenSink)
        {
            var expectedResponse = @"{
              ""request_id"": ""abcdef0123456789abcdef0123456789"",
              ""event_id"": ""0A00000012345678"",
              ""status"": ""0"",
              ""price"": ""0.10000000"",
              ""currency"": ""EUR"",
              ""estimated_price_messages_sent"": ""0.03330000""
            }";
            var expectedUri = $"{ApiUrl}/verify/json";

            string expectedRequestContent;
            VerifyCheckRequest request = new VerifyCheckRequest { Code = "1234", RequestId = "abcdef0123456789abcdef0123456789" };
            if (kitchenSink)
            {
                expectedRequestContent = $"request_id=abcdef0123456789abcdef0123456789&code=1234&ip_address={HttpUtility.UrlEncode("123.0.0.255")}&api_key={ApiKey}&api_secret={ApiSecret}&";
                request.IpAddress = "123.0.0.255";
            }
            else
            {
                expectedRequestContent = $"request_id=abcdef0123456789abcdef0123456789&code=1234&api_key={ApiKey}&api_secret={ApiSecret}&";
            }
            Setup(expectedUri, expectedResponse, expectedRequestContent);
            var creds = Request.Credentials.FromApiKeyAndSecret(ApiKey, ApiSecret);
            var client = new VonageClient(creds);
            VerifyCheckResponse response;
            if (passCreds)
            {
                response = await client.VerifyClient.VerifyCheckAsync(request, creds);
            }
            else
            {
                response = await client.VerifyClient.VerifyCheckAsync(request);
            }
            Assert.Equal("0.10000000", response.Price);
            Assert.Equal("0.03330000", response.EstimatedPriceMessagesSent);
            Assert.Equal("EUR", response.Currency);
            Assert.Equal("0A00000012345678", response.EventId);
            Assert.Equal("abcdef0123456789abcdef0123456789", response.RequestId);
            Assert.Equal("0", response.Status);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async void TestVerifySearchAsync(bool passCreds)
        {
            var expectedResponse = @"{
              ""request_id"": ""abcdef0123456789abcdef0123456789"",
              ""account_id"": ""abcdef01"",
              ""status"": ""IN PROGRESS"",
              ""number"": ""447700900000"",
              ""price"": ""0.10000000"",
              ""currency"": ""EUR"",
              ""sender_id"": ""mySenderId"",
              ""date_submitted"": ""2020-01-01 12:00:00"",
              ""date_finalized"": ""2020-01-01 12:00:00"",
              ""first_event_date"": ""2020-01-01 12:00:00"",
              ""last_event_date"": ""2020-01-01 12:00:00"",
              ""checks"": [
                {
                  ""date_received"": ""2020-01-01 12:00:00"",
                  ""code"": ""987654"",
                  ""status"": ""abc123"",
                  ""ip_address"": ""123.0.0.255""
                }
              ],
              ""events"": [
                {
                  ""type"": ""abc123"",
                  ""id"": ""abc123""
                }
              ],
              ""estimated_price_messages_sent"": ""0.03330000""
            }";
            var expectedUri = $"{ApiUrl}/verify/search/json?request_id=abcdef0123456789abcdef0123456789&api_key={ApiKey}&api_secret={ApiSecret}&";
            Setup(expectedUri, expectedResponse);
            var request = new VerifySearchRequest { RequestId = "abcdef0123456789abcdef0123456789" };
            var creds = Request.Credentials.FromApiKeyAndSecret(ApiKey, ApiSecret);
            var client = new VonageClient(creds);
            VerifySearchResponse response;
            if (passCreds)
            {
                response = await client.VerifyClient.VerifySearchAsync(request, creds);
            }
            else
            {
                response = await client.VerifyClient.VerifySearchAsync(request);
            }

            var req = response;
            Assert.Equal("abcdef0123456789abcdef0123456789", req.RequestId);
            Assert.Equal("abcdef01", req.AccountId);
            Assert.Equal("IN PROGRESS", req.Status);
            Assert.Equal("447700900000", req.Number);
            Assert.Equal("0.10000000", req.Price);
            Assert.Equal("EUR", req.Currency);
            Assert.Equal("mySenderId", req.SenderId);
            Assert.Equal("2020-01-01 12:00:00", req.DateSubmitted);
            Assert.Equal("2020-01-01 12:00:00", req.DateFinalized);
            Assert.Equal("2020-01-01 12:00:00", req.FirstEventDate);
            Assert.Equal("2020-01-01 12:00:00", req.LastEventDate);
            Assert.Equal("2020-01-01 12:00:00", req.Checks[0].DateReceived);
            Assert.Equal("987654", req.Checks[0].Code);
            Assert.Equal("abc123", req.Checks[0].Status);
            Assert.Equal("123.0.0.255", req.Checks[0].IpAddress);
            Assert.Equal("abc123", req.Events[0].Type);
            Assert.Equal("abc123", req.Events[0].Id);
            Assert.Equal("0.03330000", req.EstimatedPriceMessagesSent);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async void TestControlVerifyAsync(bool passCreds)
        {
            var expectedResponse = @"{
              ""status"": ""0"",
              ""command"": ""cancel""
            }";

            var expectedUri = $"{ApiUrl}/verify/control/json";
            var requestContent = $"request_id=abcdef0123456789abcdef0123456789&cmd=cancel&api_key={ApiKey}&api_secret={ApiSecret}&";
            Setup(expectedUri, expectedResponse, requestContent);

            var request = new VerifyControlRequest { Cmd = "cancel", RequestId = "abcdef0123456789abcdef0123456789" };
            var creds = Request.Credentials.FromApiKeyAndSecret(ApiKey, ApiSecret);
            var client = new VonageClient(creds);
            VerifyControlResponse response;
            if (passCreds)
            {
                response = await client.VerifyClient.VerifyControlAsync(request, creds);
            }
            else
            {
                response = await client.VerifyClient.VerifyControlAsync(request, creds);
            }
            Assert.Equal("0", response.Status);
            Assert.Equal("cancel", response.Command);
        }
        
        [Theory]
        [InlineData(true, true)]
        [InlineData(false, false)]
        public void Psd2VerificationAsync(bool passCreds, bool kitchenSink)
        {
            var expectedResponse = @"{
              ""request_id"": ""abcdef0123456789abcdef0123456789"",
              ""status"": ""0""
            }";
            var expectedUri = $"{ApiUrl}/verify/psd2/json";

            string expectedRequestContent;
            Psd2Request request = new Psd2Request { Number = "447700900000", Payee = "Acme Inc", Amount = 4.8 };
            if (kitchenSink)
            {
                expectedRequestContent = $"payee={HttpUtility.UrlEncode("Acme Inc")}&amount=4.8&workflow_id=1&number=447700900000&country=GB&code_length=4&lg=en-us&pin_expiry=240&next_event_wait=60&api_key={ApiKey}&api_secret={ApiSecret}&";
                request.Country = "GB";
                request.CodeLength = 4;
                request.Lg = "en-us";
                request.PinExpiry = 240;
                request.NextEventWait = 60;
                request.WorkflowId = Psd2Request.Workflow.SMS_TTS_TTS;
            }
            else
            {
                expectedRequestContent = $"payee={HttpUtility.UrlEncode("Acme Inc")}&amount=4.8&number=447700900000&api_key={ApiKey}&api_secret={ApiSecret}&";
            }

            Setup(expectedUri, expectedResponse, expectedRequestContent);
            var creds = Request.Credentials.FromApiKeyAndSecret(ApiKey, ApiSecret);
            var client = new VonageClient(creds);
            VerifyResponse response;
            if (passCreds)
            {
                response = client.VerifyClient.VerifyRequestWithPSD2(request, creds);
            }
            else
            {
                response = client.VerifyClient.VerifyRequestWithPSD2(request);
            }

            Assert.Equal("abcdef0123456789abcdef0123456789", response.RequestId);
            Assert.Equal("0", response.Status);
        }
    }
}
