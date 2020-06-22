using System;
using System.Collections.Generic;
using System.Text;
using Nexmo.Api;
using Xunit;

namespace Nexmo.Api.Test.Unit.Legacy
{
    public class AccountTest : TestBase
    {
        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void GetAccountBalance(bool passCreds)
        {
            //ARRANGE
            var expectedUri = $"{RestUrl}/account/get-balance?api_key={ApiKey}&api_secret={ApiSecret}&";
            var expectedResponseContent = @"{""value"": 3.14159, ""autoReload"": false }";
            Setup(uri: expectedUri, responseContent: expectedResponseContent);

            //ACT
            var creds = new Request.Credentials() { ApiKey = ApiKey, ApiSecret = ApiSecret };
            var client = new Client(creds);
            Account.Balance balance;
            if (passCreds)
            {
                balance = client.Account.GetBalance(creds);
            }
            else
            {
                balance = client.Account.GetBalance();
            }
            

            //ASSERT
            Assert.Equal(3.14159m, balance.value);
            Assert.False(balance.autoReload);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void GetPricing(bool passCreds)
        {
            //ARRANGE
            var expectedUri = $"{RestUrl}/account/get-pricing/outbound/?country=US&api_key={ApiKey}&api_secret={ApiSecret}&";
            var expectedResponseContent = @"{""country"":""US"",""name"":""United States"",""countryDisplayName"":""United States"",""prefix"":""1"",""mt"":""0.00480000"",""networks"":[{""code"":""US-FIXED"",""network"":""United States of America Landline"",""mtPrice"":""0.00480000"",""ranges"":""""},{""code"":""311340"",""network"":""Illinois Valley Cellular RSA 2-I Partnership"",""mtPrice"":""0.00480000""},{""code"":""311740"",""network"":""TelAlaska Cellular, Inc."",""mtPrice"":""0.00480000""},{""code"":""311910"",""network"":""SI WIRELESS, LLC"",""mtPrice"":""0.00480000""},{""code"":""310860"",""network"":""Five Star Wireless"",""mtPrice"":""0.00480000""},{""code"":""310760"",""network"":""Panhandle Telecommunications Systems, Inc."",""mtPrice"":""0.00480000""},{""code"":""310011"",""network"":""Northstar Technology, LLC"",""mtPrice"":""0.00480000""},{""code"":""311887"",""network"":""XO California, Inc."",""mtPrice"":""0.00480000""},{""code"":""311380"",""network"":""NEW DIMENSION WIRELESS LTD."",""mtPrice"":""0.00480000""},{""code"":""310300"",""network"":""Smart Call, LLC"",""mtPrice"":""0.00480000""},{""code"":""310004"",""network"":""Verizon Wireless"",""mtPrice"":""0.00480000""},{""code"":""US-VOIP"",""network"":""United States of America VoIP"",""mtPrice"":""0.00480000""},{""code"":""311580"",""network"":""United States Cellular Corp. - Maine"",""mtPrice"":""0.00480000""},{""code"":""311230"",""network"":""Cellular South, Inc."",""mtPrice"":""0.00480000""},{""code"":""310610"",""network"":""Epic Touch Co."",""mtPrice"":""0.00480000""},{""code"":""310060"",""network"":""New Cell, Inc. dba CeLLCom"",""mtPrice"":""0.00480000""},{""code"":""310870"",""network"":""Kaplan Telephone Company"",""mtPrice"":""0.00480000""},{""code"":""310750"",""network"":""East Kentucky Netwrk, LLC dba Appalachian Wireless"",""mtPrice"":""0.00480000""},{""code"":""311290"",""network"":""Pinpoint Wireless, Inc."",""mtPrice"":""0.00480000""},{""code"":""311330"",""network"":""Michigan Wireless, LLC dba Bug Tussel Wireless"",""mtPrice"":""0.00480000""},{""code"":""310710"",""network"":""ASTAC Wireless LLC"",""mtPrice"":""0.00480000""},{""code"":""311050"",""network"":""Wilkes Cellular, Inc."",""mtPrice"":""0.00480000""},{""code"":""310770"",""network"":""Iowa Wireless Services, Lp"",""mtPrice"":""0.00480000""},{""code"":""310100"",""network"":""Plateau Telecommunications, Inc."",""mtPrice"":""0.00480000""},{""code"":""311090"",""network"":""Flat Wireless, LLC"",""mtPrice"":""0.00480000""},{""code"":""311190"",""network"":""Cellular Properties, Inc."",""mtPrice"":""0.00480000""},{""code"":""311710"",""network"":""NORTHEAST WIRELESS NETWORKS, LLC"",""mtPrice"":""0.00480000""},{""code"":""311370"",""network"":""NACS Wireless, Inc."",""mtPrice"":""0.00480000""},{""code"":""311430"",""network"":""RSA 1 Limited Partnership dba Chat Mobility"",""mtPrice"":""0.00480000""},{""code"":""311100"",""network"":""Nex-Tech Wireless, LLC"",""mtPrice"":""0.00480000""},{""code"":""311240"",""network"":""Cordova Wireless Communications, Inc."",""mtPrice"":""0.00480000""},{""code"":""316011"",""network"":""Southern Communications Services"",""mtPrice"":""0.00480000""},{""code"":""310340"",""network"":""Westlink Communications, LLC"",""mtPrice"":""0.00480000""},{""code"":""311860"",""network"":""Uintah Basin Electronic Telecommunications"",""mtPrice"":""0.00480000""},{""code"":""311670"",""network"":""Pine Belt Cellular, Inc."",""mtPrice"":""0.00480000""},{""code"":""US-PREMIUM"",""network"":""United States Premium"",""mtPrice"":""0.00480000""},{""code"":""310570"",""network"":""MTPCS, LLC"",""mtPrice"":""0.00480000""},{""code"":""310180"",""network"":""CT Cube, L.P. dba West Central Cellular"",""mtPrice"":""0.00480000""},{""code"":""311080"",""network"":""Pine Telephone Co."",""mtPrice"":""0.00480000""},{""code"":""310130"",""network"":""Carolina West"",""mtPrice"":""0.00480000""},{""code"":""310580"",""network"":""Inland Cellular"",""mtPrice"":""0.00480000""},{""code"":""311040"",""network"":""Commnet Wireless, LLC"",""mtPrice"":""0.00480000""},{""code"":""316993"",""network"":""Cablevision Lightpath, Inc. - NY"",""mtPrice"":""0.00480000""},{""code"":""316995"",""network"":""Coral Wireless, LLC"",""mtPrice"":""0.00480000""},{""code"":""311410"",""network"":""Iowa RSA 2 Limited Partnership dba Chat Mobility"",""mtPrice"":""0.00480000""},{""code"":""310630"",""network"":""Choice Wireless LC"",""mtPrice"":""0.00480000""},{""code"":""311420"",""network"":""Northwest Missouri Cellular Limited Partnership"",""mtPrice"":""0.00480000""},{""code"":""310270"",""network"":""POWERTEL MEMPHIS LICENSES, INC."",""mtPrice"":""0.00480000""},{""code"":""31100"",""network"":""Mid-Tex Cellular Ltd."",""mtPrice"":""0.00480000""},{""code"":""311610"",""network"":""North Dakota Network Co dba SRT Wireless"",""mtPrice"":""0.00480000""},{""code"":""316884"",""network"":""Kentucky RSA 4 Cellular General Partnership"",""mtPrice"":""0.00480000""},{""code"":""310540"",""network"":""Oklahoma Western Telephone Company"",""mtPrice"":""0.00480000""},{""code"":""316885"",""network"":""Sagebrush Cellular, Inc."",""mtPrice"":""0.00480000""},{""code"":""316883"",""network"":""Virginia PCS Alliance, L.c."",""mtPrice"":""0.00480000""},{""code"":""312040"",""network"":""Custer Telephone Cooperative, Inc."",""mtPrice"":""0.00480000""},{""code"":""311650"",""network"":""United Wireless Communications, Inc."",""mtPrice"":""0.00480000""},{""code"":""310690"",""network"":""Keystone Wireless, LLC"",""mtPrice"":""0.00480000""},{""code"":""310120"",""network"":""SPRINT Spectrum L.P."",""mtPrice"":""0.00480000""},{""code"":""310020"",""network"":""Union Telephone Company"",""mtPrice"":""0.00480000""},{""code"":""311020"",""network"":""Chariton Valley Cellular"",""mtPrice"":""0.00480000""},{""code"":""311030"",""network"":""Indigo Wireless, Inc."",""mtPrice"":""0.00480000""},{""code"":""311310"",""network"":""New Mexico RSA 6-III Partnership dba Leaco Rural"",""mtPrice"":""0.00480000""},{""code"":""310023"",""network"":""Voicestream GSM I, LLC"",""mtPrice"":""0.00480000""},{""code"":""310320"",""network"":""Smith Bagley Inc. dba Cellular One of Ne Arizona"",""mtPrice"":""0.00480000""},{""code"":""311730"",""network"":""Proximity Mobility, LLC"",""mtPrice"":""0.00480000""},{""code"":""310260"",""network"":""T-mobile USA, Inc."",""mtPrice"":""0.00480000""},{""code"":""310450"",""network"":""N.E. Colorado Cellular, Inc."",""mtPrice"":""0.00480000""},{""code"":""310090"",""network"":""AT&T Mobility"",""mtPrice"":""0.00480000""},{""code"":""310740"",""network"":""Tracy Corporation Ii"",""mtPrice"":""0.00480000""},{""code"":""311690"",""network"":""Telebeeper of New Mexico"",""mtPrice"":""0.00480000""},{""code"":""310840"",""network"":""Telecom North America Mobile Inc"",""mtPrice"":""0.00480000""},{""code"":""310880"",""network"":""Advantage Cellular Systems, Inc."",""mtPrice"":""0.00480000""}]}";
            Setup(uri: expectedUri, responseContent: expectedResponseContent);

            //ACT
            var creds = new Request.Credentials() { ApiKey = ApiKey, ApiSecret = ApiSecret };
            var client = new Client(creds);
            Account.Pricing pricing;
            if (passCreds)
            {
                pricing = client.Account.GetPricing("US",creds:creds);
            }
            else
            {
                pricing = client.Account.GetPricing("US");
            }

            //ASSERT
            Assert.Equal("US-FIXED", pricing.networks[0].code);
            Assert.Equal("United States of America Landline", pricing.networks[0].network);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void SetSettings(bool passCreds)
        {
            //ARRANGE
            var expectedUri = $"{RestUrl}/account/settings";
            var expectedRequestContents = $"newSecret=newSecret1&moCallBackUrl=http%3a%2f%2fmo.callbackurl.com&drCallBackUrl=http%3a%2f%2fdr.callbackurl.com&api_key={ApiKey}&api_secret={ApiSecret}&";
            var expectedResponseContent = @"{""api-secret"": ""newSecret1"",""mo-callback-url"": ""http://mo.callbackurl.com"",""dr-callback-url"": ""http://dr.callbackurl.com"", ""max-outbound-request"":30, ""max-inbound-request"":15}";
            Setup(uri: expectedUri, responseContent: expectedResponseContent, requestContent: expectedRequestContents);

            //ACT
            var creds = new Request.Credentials() { ApiKey = ApiKey, ApiSecret = ApiSecret };
            var client = new Client(creds);
            Account.Settings result;

            if (passCreds)
            {
                result = client.Account.SetSettings("newSecret1", "http://mo.callbackurl.com", "http://dr.callbackurl.com",creds);
            }
            else
            {
                result = client.Account.SetSettings("newSecret1", "http://mo.callbackurl.com", "http://dr.callbackurl.com");
            }
            

            Assert.Equal("newSecret1", result.apiSecret);
            Assert.Equal("http://mo.callbackurl.com", result.moCallbackUrl);
            Assert.Equal("http://dr.callbackurl.com", result.drCallbackUrl);
            Assert.Equal(15, result.maxInboundRequest);
            Assert.Equal(30, result.maxOutboundRequest);

        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void TopUp(bool passCreds)
        {
            //ARRANGE
            var expectedUri = $"{RestUrl}/account/top-up?trx=00X123456Y7890123Z&api_key={ApiKey}&api_secret={ApiSecret}&";
            var expectedResponseContent = @"{}";
            Setup(uri: expectedUri, responseContent: expectedResponseContent);

            //ACT
            var creds = new Request.Credentials() { ApiKey = ApiKey, ApiSecret = ApiSecret };
            var client = new Client(creds);
            if (passCreds)
            {
                client.Account.TopUp("00X123456Y7890123Z",creds);
            }
            else
            {
                client.Account.TopUp("00X123456Y7890123Z");
            }
            

            //ASSERT
            //nothing to assert as nothing is returned
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void GetNumbers(bool passCreds)
        {
            //ARRANGE
            var expectedUri = $"{RestUrl}/account/numbers?api_key={ApiKey}&api_secret={ApiSecret}&";
            var expectedResponseContent = @"{""count"":1,""numbers"":[{""country"":""US"",""msisdn"":""17775551212"",""type"":""mobile-lvn"",""features"":[""VOICE"",""SMS""]}]}";
            Setup(uri: expectedUri, responseContent: expectedResponseContent);

            //ACT
            var creds = new Request.Credentials() { ApiKey = ApiKey, ApiSecret = ApiSecret };
            var client = new Client(creds);
            Account.NumbersResponse numbers;
            if (passCreds)
            {
                numbers = client.Account.GetNumbers(creds);
            }
            else
            {
                numbers = client.Account.GetNumbers();
            }
            

            Assert.Equal(1, numbers.count);
            Assert.Equal("17775551212", numbers.numbers[0].msisdn);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void GetPrefixPricing(bool passCreds)
        {
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
            var expectedUri = $"{RestUrl}/account/get-pricing/outbound/?country=CA&type=sms&api_key={ApiKey}&api_secret={ApiSecret}&";
            Setup(expectedUri, expectedResponse);

            var creds = new Request.Credentials() { ApiKey = ApiKey, ApiSecret = ApiSecret };
            var client = new Client(creds);
            Account.Pricing response;
            if (passCreds)
            {
                response = client.Account.GetPrefixPricing("CA", "sms", creds);
            }
            else
            {
                response = client.Account.GetPrefixPricing("CA", "sms");
            }
            

            Assert.NotNull(response);
        }
    }
}
