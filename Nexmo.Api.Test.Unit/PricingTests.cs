using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
namespace Nexmo.Api.Test.Unit
{
    public class PricingTests : TestBase
    {
        [Fact]
        public void GetPricing()
        {
            //ARRANGE
            var expectedUri = $"{RestUrl}/account/get-pricing/outbound/sms?country=CA&api_key={ApiKey}&api_secret={ApiSecret}&";
            var expectedResponseContent = @"{
              ""countryCode"": ""CA"",
              ""countryName"": ""Canada"",
              ""countryDisplayName"": ""Canada"",
              ""currency"": ""EUR"",
              ""defaultPrice"": ""0.00620000"",
              ""dialingPrefix"": ""1"",
              ""networks"": [
                {
                  ""type"": ""mobile"",
                  ""price"": ""0.00590000"",
                  ""currency"": ""EUR"",
                  ""mcc"": ""302"",
                  ""mnc"": ""530"",
                  ""networkCode"": ""302530"",
                  ""networkName"": ""Keewaytinook Okimakanak""
                }
              ]
            }";
            Setup(uri: expectedUri, responseContent: expectedResponseContent);

            //ACT
            var client = new NexmoClient(Request.Credentials.FromApiKeyAndSecret(ApiKey, ApiSecret));
            var pricing = client.PricingClient.RetrievePricingCountry("sms", new Pricing.PricingCountryRequest { Country = "CA" });

            //ASSERT

            Assert.Equal("302530", pricing.Networks[0].NetworkCode);
            Assert.Equal("Keewaytinook Okimakanak", pricing.Networks[0].NetworkName);
            Assert.Equal("530", pricing.Networks[0].Mnc);
            Assert.Equal("302", pricing.Networks[0].Mcc);
            Assert.Equal("EUR", pricing.Networks[0].Currency);
            Assert.Equal("0.00590000", pricing.Networks[0].Price);
            Assert.Equal("mobile", pricing.Networks[0].Type);
            Assert.Equal("1", pricing.DialingPrefix);
            Assert.Equal("0.00620000", pricing.DefaultPrice);
            Assert.Equal("EUR", pricing.Currency);
            Assert.Equal("Canada", pricing.CountryDisplayName);
            Assert.Equal("Canada", pricing.CountryName);
            Assert.Equal("CA", pricing.CountryCode);
        }
    }
}
