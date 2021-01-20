using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
namespace Vonage.Test.Unit
{
    public class PricingTests : TestBase
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetPricingForCountry(bool passCreds)
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
            var creds = Request.Credentials.FromApiKeyAndSecret(ApiKey, ApiSecret);
            var client = new VonageClient(creds);
            Pricing.Country country;

            if (passCreds)
            {
                country = client.PricingClient.RetrievePricingCountry("sms", new Pricing.PricingCountryRequest { Country = "CA" }, creds);
            }
            else
            {
                country = client.PricingClient.RetrievePricingCountry("sms", new Pricing.PricingCountryRequest { Country = "CA" });
            }

            //ASSERT

            Assert.Equal("302530", country.Networks[0].NetworkCode);
            Assert.Equal("Keewaytinook Okimakanak", country.Networks[0].NetworkName);
            Assert.Equal("530", country.Networks[0].Mnc);
            Assert.Equal("302", country.Networks[0].Mcc);
            Assert.Equal("EUR", country.Networks[0].Currency);
            Assert.Equal("0.00590000", country.Networks[0].Price);
            Assert.Equal("mobile", country.Networks[0].Type);
            Assert.Equal("1", country.DialingPrefix);
            Assert.Equal("0.00620000", country.DefaultPrice);
            Assert.Equal("EUR", country.Currency);
            Assert.Equal("Canada", country.CountryDisplayName);
            Assert.Equal("Canada", country.CountryName);
            Assert.Equal("CA", country.CountryCode);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async void GetPricingForCountryAsync(bool passCreds)
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
            var creds = Request.Credentials.FromApiKeyAndSecret(ApiKey, ApiSecret);
            var client = new VonageClient(creds);
            Pricing.Country country;

            if (passCreds)
            {
                country = await client.PricingClient.RetrievePricingCountryAsync("sms", new Pricing.PricingCountryRequest { Country = "CA" }, creds);
            }
            else
            {
                country = await client.PricingClient.RetrievePricingCountryAsync("sms", new Pricing.PricingCountryRequest { Country = "CA" });
            }

            //ASSERT

            Assert.Equal("302530", country.Networks[0].NetworkCode);
            Assert.Equal("Keewaytinook Okimakanak", country.Networks[0].NetworkName);
            Assert.Equal("530", country.Networks[0].Mnc);
            Assert.Equal("302", country.Networks[0].Mcc);
            Assert.Equal("EUR", country.Networks[0].Currency);
            Assert.Equal("0.00590000", country.Networks[0].Price);
            Assert.Equal("mobile", country.Networks[0].Type);
            Assert.Equal("1", country.DialingPrefix);
            Assert.Equal("0.00620000", country.DefaultPrice);
            Assert.Equal("EUR", country.Currency);
            Assert.Equal("Canada", country.CountryDisplayName);
            Assert.Equal("Canada", country.CountryName);
            Assert.Equal("CA", country.CountryCode);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetPricingForPrefix(bool passCreds)
        {
            //ARRANGE
            var expectedUri = $"{RestUrl}/account/get-prefix-pricing/outbound/sms?prefix=1&api_key={ApiKey}&api_secret={ApiSecret}&";
            var expectedResponse = @"{
                  ""count"": ""243"",
                  ""countries"": [
                    {
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
                    }
                  ]
                }";
            Setup(uri: expectedUri, responseContent: expectedResponse);

            //ACT
            var creds = Request.Credentials.FromApiKeyAndSecret(ApiKey, ApiSecret);
            var client = new VonageClient(creds);
            Pricing.PricingResult pricing;
            if (passCreds)
            {
                pricing = client.PricingClient.RetrievePrefixPricing("sms", new Pricing.PricingPrefixRequest { Prefix = "1" }, creds);
            }
            else
            {
                pricing = client.PricingClient.RetrievePrefixPricing("sms", new Pricing.PricingPrefixRequest { Prefix = "1" });
            }

            //ASSERT
            Assert.Equal("302530", pricing.Countries[0].Networks[0].NetworkCode);
            Assert.Equal("Keewaytinook Okimakanak", pricing.Countries[0].Networks[0].NetworkName);
            Assert.Equal("530", pricing.Countries[0].Networks[0].Mnc);
            Assert.Equal("302", pricing.Countries[0].Networks[0].Mcc);
            Assert.Equal("EUR", pricing.Countries[0].Networks[0].Currency);
            Assert.Equal("0.00590000", pricing.Countries[0].Networks[0].Price);
            Assert.Equal("mobile", pricing.Countries[0].Networks[0].Type);
            Assert.Equal("1", pricing.Countries[0].DialingPrefix);
            Assert.Equal("0.00620000", pricing.Countries[0].DefaultPrice);
            Assert.Equal("EUR", pricing.Countries[0].Currency);
            Assert.Equal("Canada", pricing.Countries[0].CountryDisplayName);
            Assert.Equal("Canada", pricing.Countries[0].CountryName);            
            Assert.Equal("243", pricing.Count);            
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async void GetPricingForPrefixAsync(bool passCreds)
        {
            //ARRANGE
            var expectedUri = $"{RestUrl}/account/get-prefix-pricing/outbound/sms?prefix=1&api_key={ApiKey}&api_secret={ApiSecret}&";
            var expectedResponse = @"{
                  ""count"": ""243"",
                  ""countries"": [
                    {
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
                    }
                  ]
                }";
            Setup(uri: expectedUri, responseContent: expectedResponse);

            //ACT
            var creds = Request.Credentials.FromApiKeyAndSecret(ApiKey, ApiSecret);
            var client = new VonageClient(creds);
            Pricing.PricingResult pricing;
            if (passCreds)
            {
                pricing = await client.PricingClient.RetrievePrefixPricingAsync("sms", new Pricing.PricingPrefixRequest { Prefix = "1" }, creds);
            }
            else
            {
                pricing = await client.PricingClient.RetrievePrefixPricingAsync("sms", new Pricing.PricingPrefixRequest { Prefix = "1" });
            }

            //ASSERT
            Assert.Equal("302530", pricing.Countries[0].Networks[0].NetworkCode);
            Assert.Equal("Keewaytinook Okimakanak", pricing.Countries[0].Networks[0].NetworkName);
            Assert.Equal("530", pricing.Countries[0].Networks[0].Mnc);
            Assert.Equal("302", pricing.Countries[0].Networks[0].Mcc);
            Assert.Equal("EUR", pricing.Countries[0].Networks[0].Currency);
            Assert.Equal("0.00590000", pricing.Countries[0].Networks[0].Price);
            Assert.Equal("mobile", pricing.Countries[0].Networks[0].Type);
            Assert.Equal("1", pricing.Countries[0].DialingPrefix);
            Assert.Equal("0.00620000", pricing.Countries[0].DefaultPrice);
            Assert.Equal("EUR", pricing.Countries[0].Currency);
            Assert.Equal("Canada", pricing.Countries[0].CountryDisplayName);
            Assert.Equal("Canada", pricing.Countries[0].CountryName);
            Assert.Equal("243", pricing.Count);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetPricingAllCountries(bool passCreds)
        {
            //ARRANGE
            var expectedUri = $"{RestUrl}/account/get-pricing/outbound/sms?api_key={ApiKey}&api_secret={ApiSecret}&";
            var expectedResponse = @"{
                  ""count"": ""243"",
                  ""countries"": [
                    {
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
                    }
                  ]
                }";
            Setup(uri: expectedUri, responseContent: expectedResponse);

            //ACT
            var creds = Request.Credentials.FromApiKeyAndSecret(ApiKey, ApiSecret);
            var client = new VonageClient(creds);
            Pricing.PricingResult pricing;
            if (passCreds)
            {
                pricing = client.PricingClient.RetrievePricingAllCountries("sms",creds);
            }
            else
            {
                pricing = client.PricingClient.RetrievePricingAllCountries("sms");
            }

            //ASSERT
            Assert.Equal("302530", pricing.Countries[0].Networks[0].NetworkCode);
            Assert.Equal("Keewaytinook Okimakanak", pricing.Countries[0].Networks[0].NetworkName);
            Assert.Equal("530", pricing.Countries[0].Networks[0].Mnc);
            Assert.Equal("302", pricing.Countries[0].Networks[0].Mcc);
            Assert.Equal("EUR", pricing.Countries[0].Networks[0].Currency);
            Assert.Equal("0.00590000", pricing.Countries[0].Networks[0].Price);
            Assert.Equal("mobile", pricing.Countries[0].Networks[0].Type);
            Assert.Equal("1", pricing.Countries[0].DialingPrefix);
            Assert.Equal("0.00620000", pricing.Countries[0].DefaultPrice);
            Assert.Equal("EUR", pricing.Countries[0].Currency);
            Assert.Equal("Canada", pricing.Countries[0].CountryDisplayName);
            Assert.Equal("Canada", pricing.Countries[0].CountryName);
            Assert.Equal("243", pricing.Count);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async void GetPricingAllCountriesAsync(bool passCreds)
        {
            //ARRANGE
            var expectedUri = $"{RestUrl}/account/get-pricing/outbound/sms?api_key={ApiKey}&api_secret={ApiSecret}&";
            var expectedResponse = @"{
                  ""count"": ""243"",
                  ""countries"": [
                    {
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
                    }
                  ]
                }";
            Setup(uri: expectedUri, responseContent: expectedResponse);

            //ACT
            var creds = Request.Credentials.FromApiKeyAndSecret(ApiKey, ApiSecret);
            var client = new VonageClient(creds);
            Pricing.PricingResult pricing;
            if (passCreds)
            {
                pricing = await client.PricingClient.RetrievePricingAllCountriesAsync("sms", creds);
            }
            else
            {
                pricing = await client.PricingClient.RetrievePricingAllCountriesAsync("sms");
            }

            //ASSERT
            Assert.Equal("302530", pricing.Countries[0].Networks[0].NetworkCode);
            Assert.Equal("Keewaytinook Okimakanak", pricing.Countries[0].Networks[0].NetworkName);
            Assert.Equal("530", pricing.Countries[0].Networks[0].Mnc);
            Assert.Equal("302", pricing.Countries[0].Networks[0].Mcc);
            Assert.Equal("EUR", pricing.Countries[0].Networks[0].Currency);
            Assert.Equal("0.00590000", pricing.Countries[0].Networks[0].Price);
            Assert.Equal("mobile", pricing.Countries[0].Networks[0].Type);
            Assert.Equal("1", pricing.Countries[0].DialingPrefix);
            Assert.Equal("0.00620000", pricing.Countries[0].DefaultPrice);
            Assert.Equal("EUR", pricing.Countries[0].Currency);
            Assert.Equal("Canada", pricing.Countries[0].CountryDisplayName);
            Assert.Equal("Canada", pricing.Countries[0].CountryName);
            Assert.Equal("243", pricing.Count);
        }
    }
}
