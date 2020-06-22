using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
namespace Nexmo.Api.Test.Unit.Legacy
{
    public class NumberTest :TestBase
    {
        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void SearchNumbers(bool passCreds)
        {
            //ARRANGE
            var expectedUri = $"{RestUrl}/number/search/?country=US&api_key={ApiKey}&api_secret={ApiSecret}&";            
            var expectedResponseContent = "{\"count\":177,\"numbers\":[{\"country\":\"US\",\"msisdn\":\"15102694548\",\"type\":\"mobile-lvn\",\"features\":[\"SMS\",\"VOICE\"],\"cost\":\"0.67\"},{\"country\":\"US\",\"msisdn\":\"17088568490\",\"type\":\"mobile-lvn\",\"features\":[\"SMS\",\"VOICE\"],\"cost\":\"0.67\"},{\"country\":\"US\",\"msisdn\":\"17088568491\",\"type\":\"mobile-lvn\",\"features\":[\"SMS\",\"VOICE\"],\"cost\":\"0.67\"},{\"country\":\"US\",\"msisdn\":\"17088568492\",\"type\":\"mobile-lvn\",\"features\":[\"SMS\",\"VOICE\"],\"cost\":\"0.67\"},{\"country\":\"US\",\"msisdn\":\"17088568973\",\"type\":\"mobile-lvn\",\"features\":[\"SMS\",\"VOICE\"],\"cost\":\"0.67\"}]}";
            Setup(uri: expectedUri, responseContent: expectedResponseContent);

            //ACT
            var creds = new Request.Credentials() { ApiKey = ApiKey, ApiSecret = ApiSecret };
            var client = new Client(creds);
            Number.SearchResults results;
            if (passCreds)
            {
                results = client.Number.Search(new Number.SearchRequest()
                {
                    country = "US"
                }, creds);
            }
            else
            {
                results = client.Number.Search(new Number.SearchRequest()
                {
                    country = "US"
                });
            }

            //ASSERT
            Assert.Equal(177, results.count);
            Assert.Equal(5, results.numbers.Count());
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void BuyNumber(bool passCreds)
        {
            //ARRANGE
            var expectedUri = $"{RestUrl}/number/buy";
            var expectedResponse = "{\"error-code\":\"200\",\"error-code-label\":\"success\"}";
            var expectedRequestContent = $"country=US&msisdn=17775551212&api_key={ApiKey}&api_secret={ApiSecret}&";

            Setup(uri: expectedUri, responseContent: expectedResponse, requestContent: expectedRequestContent);

            //ACT
            var creds = new Request.Credentials() { ApiKey = ApiKey, ApiSecret = ApiSecret };
            var client = new Client(creds);
            ResponseBase result;
            if (passCreds)
            {
                result = client.Number.Buy("US", "17775551212", creds);
            }
            else
            {
                result = client.Number.Buy("US", "17775551212");
            }

            //ASSERT
            Assert.Equal("200", result.ErrorCode);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void UpdateNumber(bool passCreds)
        {
            //ARRANGE
            var expectedUri = $"{RestUrl}/number/update";
            var expectedResponse = "{\"error-code\":\"200\",\"error-code-label\":\"success\"}";
            var expectedRequestContent = $"country=US&msisdn=17775551212&moHttpUrl=https%3a%2f%2ftest.test.com%2fmo&moSmppSysType=inbound&api_key={ApiKey}&api_secret={ApiSecret}&";
            Setup(uri: expectedUri, responseContent: expectedResponse, requestContent: expectedRequestContent);

            //ACT
            var creds = new Request.Credentials() { ApiKey = ApiKey, ApiSecret = ApiSecret };
            var client = new Client(creds);
            ResponseBase result;
            if (passCreds)
            {
                result = client.Number.Update(new Number.NumberUpdateCommand
                {
                    country = "US",
                    msisdn = "17775551212",
                    moHttpUrl = "https://test.test.com/mo",
                    moSmppSysType = "inbound"
                }, creds);
            }
            else
            {
                result = client.Number.Update(new Number.NumberUpdateCommand
                {
                    country = "US",
                    msisdn = "17775551212",
                    moHttpUrl = "https://test.test.com/mo",
                    moSmppSysType = "inbound"
                });
            }

            //ASSERT
            Assert.Equal("200", result.ErrorCode);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void CancelNumber(bool passCreds)
        {
            //ARRANGE
            var expectedUri = $"{RestUrl}/number/cancel";
            var expectedResponse = "{\"error-code\":\"200\",\"error-code-label\":\"success\"}";
            var expectedRequestContent = $"country=US&msisdn=17775551212&api_key={ApiKey}&api_secret={ApiSecret}&";
            Setup(uri: expectedUri, responseContent: expectedResponse, requestContent: expectedRequestContent);

            //ACT
            var creds = new Request.Credentials() { ApiKey = ApiKey, ApiSecret = ApiSecret };
            var client = new Client(creds);
            ResponseBase result;
            if (passCreds)
            {
                result = client.Number.Cancel("US", "17775551212", creds);
            }
            else
            {
                result = client.Number.Cancel("US", "17775551212");
            }

            Assert.Equal("200", result.ErrorCode);
        }
    }
}
