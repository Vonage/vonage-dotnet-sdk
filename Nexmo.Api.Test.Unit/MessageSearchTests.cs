using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Nexmo.Api.Test.Unit
{
    public class MessageSearchTests : TestBase
    {
        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void SearchMessage(bool passCreds)
        {
            var id = "0A00000000ABCD00";
            var expectedResponse = @" {""type"": ""MT"",
              ""message-id"": ""0A00000000ABCD00"",
              ""account-id"": ""API_KEY"",
              ""network"": ""012345"",
              ""from"": ""Nexmo"",
              ""to"": ""447700900000"",
              ""body"": ""A text message sent using the Nexmo SMS API"",
              ""date-received"": ""2020-01-01 12:00:00"",
              ""price"": 0.0333,
              ""date-closed"": ""2020-01-01 12:00:00"",
              ""latency"": 3000,
              ""client-ref"": ""my-personal-reference"",
              ""status"": ""abc123"",
              ""final-status"": ""DELIVRD"",
              ""error-code"": ""0"",
              ""error-code-label"": ""Delivered""}";

            var expectedUri = $"{RestUrl}/search/message?id={id}&api_key={ApiKey}&api_secret={ApiSecret}&";
            Setup(expectedUri, expectedResponse);
            var creds = Request.Credentials.FromApiKeyAndSecret(ApiKey, ApiSecret);
            var client = new NexmoClient(creds);
            Common.Message message;
            if (passCreds)
            {
                message = client.MessageSearchClient.SearchMessage(new MessageSearch.MessageSearchRequest { Id = id }, creds);
            }
            else{
                message = client.MessageSearchClient.SearchMessage(new MessageSearch.MessageSearchRequest { Id = id });
            }
            Assert.Equal("MT", message.Type);
            Assert.Equal("0A00000000ABCD00", message.MessageId);
            Assert.Equal("API_KEY", message.AccountId);
            Assert.Equal("012345", message.Network);
            Assert.Equal("Nexmo", message.From);
            Assert.Equal("447700900000", message.To);
            Assert.Equal("A text message sent using the Nexmo SMS API", message.Body);
            Assert.Equal("2020-01-01 12:00:00", message.DateReceived);
            Assert.True((decimal)0.0333 == message.Price);
            Assert.Equal("2020-01-01 12:00:00", message.DateClosed);
            Assert.True((decimal)3000 == message.Latency);
            Assert.Equal("my-personal-reference", message.ClientRef);
            Assert.Equal("abc123", message.Status);
            Assert.Equal("DELIVRD", message.FinalStatus);
            Assert.Equal("0", message.ErrorCode);
            Assert.Equal("Delivered", message.ErrorCodeLabel);
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void SearchMessages(bool passCreds, bool passParameters)
        {
            //ARRANGE
            var expectedResponse = @"{
              ""count"": 2,
              ""items"": [
                {
                  ""type"": ""MO"",
                  ""message-id"": ""0A00000000ABCD00"",
                  ""account-id"": ""API_KEY"",
                  ""date-received"": ""2020-01-01 12:00:00"",
                  ""network"": ""012345"",
                  ""from"": ""Nexmo"",
                  ""to"": ""447700900000"",
                  ""body"": ""A text message sent using the Nexmo SMS API""
                },
                {
                  ""type"": ""MT"",
                  ""message-id"": ""0A00000000ABCD00"",
                  ""account-id"": ""API_KEY"",
                  ""network"": ""012345"",
                  ""from"": ""Nexmo"",
                  ""to"": ""447700900000"",
                  ""body"": ""A text message sent using the Nexmo SMS API"",
                  ""date-received"": ""2020-01-01 12:00:00"",
                  ""price"": 0.0333,
                  ""date-closed"": ""2020-01-01 12:00:00"",
                  ""latency"": 3000,
                  ""client-ref"": ""my-personal-reference"",
                  ""status"": ""abc123"",
                  ""final-status"": ""DELIVRD"",
                  ""error-code"": ""0"",
                  ""error-code-label"": ""Delivered""
                }
              ]
            }";
            string expectedUri;
            MessageSearch.MessagesSearchResponse response;
            MessageSearch.MessagesSearchRequest request;
            var creds = Request.Credentials.FromApiKeyAndSecret(ApiKey, ApiSecret);
            var client = new NexmoClient(creds);
            if (passParameters)
            {
                expectedUri = $"{RestUrl}/search/messages?ids=123456789&ids=987654321&date=2020-01-01&to=447700900000&api_key={ApiKey}&api_secret={ApiSecret}&";
                var ids = new string[] { "123456789", "987654321" };
                request = new MessageSearch.MessagesSearchRequest { Date = "2020-01-01", Ids = ids, To = "447700900000" };
                Assert.Equal("123456789",request.Ids[0]);
            }
            else
            {
                expectedUri = $"{RestUrl}/search/messages?api_key={ApiKey}&api_secret={ApiSecret}&";
                request = new MessageSearch.MessagesSearchRequest();
            }
            Setup(expectedUri, expectedResponse);

            //ACT
            if (passCreds)
            {
                response = client.MessageSearchClient.SearchMessages(request, creds);
            }
            else
            {
                response = client.MessageSearchClient.SearchMessages(request);
            }

            //ASSERT
            var message = response.Items[1];

            Assert.Equal("MT", message.Type);
            Assert.Equal("0A00000000ABCD00", message.MessageId);
            Assert.Equal("API_KEY", message.AccountId);
            Assert.Equal("012345", message.Network);
            Assert.Equal("Nexmo", message.From);
            Assert.Equal("447700900000", message.To);
            Assert.Equal("A text message sent using the Nexmo SMS API", message.Body);
            Assert.Equal("2020-01-01 12:00:00", message.DateReceived);
            Assert.True((decimal)0.0333 == message.Price);
            Assert.Equal("2020-01-01 12:00:00", message.DateClosed);
            Assert.True((decimal)3000 == message.Latency);
            Assert.Equal("my-personal-reference", message.ClientRef);
            Assert.Equal("abc123", message.Status);
            Assert.Equal("DELIVRD", message.FinalStatus);
            Assert.Equal("0", message.ErrorCode);
            Assert.Equal("Delivered", message.ErrorCodeLabel);

            Assert.True(2 == response.Count);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SearchRejections(bool passCreds)
        {
            var expectedResult = @"{
              ""count"": 1,
              ""items"": [
                {
                  ""account-id"": ""API_KEY"",
                  ""from"": ""Nexmo"",
                  ""to"": ""447700900000"",
                  ""date-received"": ""2020-01-01 12:00:00"",
                  ""error-code"": ""0"",
                  ""error-code-label"": ""Delivered""
                }
              ]
            }";
            var expectedUri = $"{RestUrl}/search/rejections?to=447700900000&date=2020-01-01&api_key={ApiKey}&api_secret={ApiSecret}&";

            Setup(expectedUri, expectedResult);
            var creds = Request.Credentials.FromApiKeyAndSecret(ApiKey, ApiSecret);
            var client = new NexmoClient(creds);
            var rejectionRequest = new MessageSearch.RejectionSearchRequest { Date = "2020-01-01", To = "447700900000" };
            MessageSearch.RejectionSearchResponse response;
            if (passCreds)
            {
                response = client.MessageSearchClient.SearchRejections(rejectionRequest, creds);
            }
            else
            {
                response = client.MessageSearchClient.SearchRejections(rejectionRequest);
            }

            var rejection = response.Items[0];
            Assert.Equal("API_KEY", rejection.AccountId);
            Assert.Equal("Nexmo", rejection.From);
            Assert.Equal("447700900000", rejection.To);
            Assert.Equal("2020-01-01 12:00:00", rejection.DateReceived);
            Assert.Equal("0", rejection.ErrorCode);
            Assert.Equal("Delivered", rejection.ErrorCodeLabel);
            Assert.True(1 == response.Count);
        }
    }
}
