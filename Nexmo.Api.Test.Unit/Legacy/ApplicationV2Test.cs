using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Nexmo.Api.Test.Unit.Legacy
{
    public class ApplicationV2Test : TestBase
    {
        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void CreateApplication(bool passCreds)
        {
            //ARRANGE
            var uri = $"{ApiUrl}/v2/applications";
            var expectedResponse = "{\"id\":\"ffffffff-ffff-ffff-ffff-ffffffffffff\",\"name\":\"AppV2Test\",\"capabilities\":{},\"keys\":{\"public_key\":\"-----BEGIN PUBLIC KEY-----\\nMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAwxyBT5FqzibSYK0vB+Gr\\nP+YlyYqsx4lvAmotTwmObZEhTWNAdU0p9hrnNXWX1Gy5O0NDIue40SUhYhJT5r4x\\nugbpNA/1KJauB8VQjetKr9bu697yskz2+EuKa2D9e6N2EMY6PD1tJWmeMmddM1tW\\n2DAXuYo7/xsDWIIA6egCTzyShNvzlKo5081t41xVVsPjsWN887Xp1KYfE0IMGV2j\\n8Nwdtw/MQfP/7Qz7i9VXb7bgx0LEg84dWsnz8u3VZ3IQHlydzPX/2iw7e4pc+k27\\nOU1SkmPn/2JtfFFS2LJpcO/FmdSyNnyHezNPyzNRLVbE0sJJ1tEhxi9GZc1I+Oc4\\ndwIDAQAB\\n-----END PUBLIC KEY-----\\n\",\"private_key\":\"-----BEGIN PRIVATE KEY-----\\nMIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQDDHIFPkWrOJtJg\\nrS8H4as/5iXJiqzHiW8Cai1PCY5tkSFNY0B1TSn2Guc1dZfUbLk7Q0Mi57jRJSFi\\nElPmvjG6Buk0D/Uolq4HxVCN60qv1u7r3vKyTPb4S4prYP17o3YQxjo8PW0laZ4y\\nZ10zW1bYMBe5ijv/GwNYggDp6AJPPJKE2/OUqjnTzW3jXFVWw+OxY3zztenUph8T\\nQgwZXaPw3B23D8xB8//tDPuL1VdvtuDHQsSDzh1ayfPy7dVnchAeXJ3M9f/aLDt7\\nilz6Tbs5TVKSY+f/Ym18UVLYsmlw78WZ1LI2fId7M0/LM1EtVsTSwknW0SHGL0Zl\\nzUj45zh3AgMBAAECggEAFP06iJ7p9fkQKwKbwMXNQIes1fm7QjtDJr0vAvB8vXBe\\nPER/7n6EE+vApqwapBP5eJTGTU7PP382kFB3bScAaMo4iSIUVgRLqXNXtJoKGVDO\\nYq/DvQbxjzQVKnMEdyoBAqdIeYAu1IEAWdzBFnbDfhNCYh0q/MkLpZhVVSm2dzkq\\nl+xrEOTyuE48RQxTQqliUYXXlUd5+bjR+oREuYsjt8j9iiK1u6Gv2ztDPeyzupny\\ntCvSlykvAKn/K6xmeO4hapPKUZ7DRthg3I3uPx+mw48GvO28mzhsqXpbDcYI7q82\\nfZgZJ3JKBTRaNnEE8d5llblC5dksMttLgO2EewdXlQKBgQDscfdVVvLYDW3lsPKl\\nAhEmi3ZcWvzDMCGeGLjJEZPfMnc+7rKbCBADQzNMZI/bsWwMJdQCMy2xMaDXr4Ew\\n4TiBdQ7ogpRex9yuJ3miKs3eey6bvpNxV70lj/xvZoSu7oANSFOCMNFUvSWFTPC5\\nNiNGk35g6xklf3WAYLx4bVJcDQKBgQDTP2qL2zQbU/hmQPnq8x/wgGn8T6zQZXbt\\nojyNPTsnIbQhwQzlFM01SzNsF4hVMB8Zz6r+8XHuo3TsDdg6Orx1auIV5lXCWMj/\\n3MW/jy2JabXJyh7BViHFqQjBPHWDrX17TGGsLK/rfOqMRPPgBGTXfVhINaCo7EAh\\nSTPv2x2RkwKBgBQinGpzDhkiA6LUz8UHiQhcRgcVZIMGvUYmWs4cphgSxx7f2uvi\\n4uI0PdEamzmdQVNDgWtyikiVrlnPw1OzSkmT+2IHhLURlhRqniwWMxPoL47pysqT\\nKzNgsKGX/GKdQuBesWXb3Ge399MDO1i6aIShGNkODEUqNoppMoOa47GdAoGAWn3t\\n/F84YQSFgfgPlu/zHKlFvYm788GjQoSe/7ndHxQ2/8ac6X0RsuS18HXcNvHYQMxO\\n6cswDRQEQCJmH/uNQ5c3pj33OruhzskaBMcmsJiSAREOP6/P48ZXM7/cbz3gZPMB\\nXCoAahYmu1PGTI5VTGIrcTNX0UTy689Z6kOo1PUCgYEAjYPkvv4j286XbGHDQyt7\\ngPvbFUPwtYxwk0u/CuZ1scBkVRCMwc8Gic1hL0yB09nvp86cCjNyYcFqa8fTpjom\\n0C7wjZd6zHR4y23U/jVxhdny6lotpgpWKO7DVprjyHQ90yGu+EDq3jDCOjyhdmqP\\n766dkdpKIYoBJOTH9+3r8gc=\\n-----END PRIVATE KEY-----\\n\"}}";
            var expectedRequestContent = "{\"name\":\"AppV2Test\"}";

            Setup(uri: uri, responseContent: expectedResponse, requestContent: expectedRequestContent);

            //ACT
            var appRequest = new AppRequest
            {
                Name = "AppV2Test"
            };

            var creds = new Request.Credentials() { ApiKey = ApiKey, ApiSecret = ApiSecret };
            var client = new Client(creds);
            AppResponse result;
            if (passCreds)
            {
                result = client.ApplicationV2.Create(appRequest, creds);
            }
            else
            {
                result = client.ApplicationV2.Create(appRequest);
            }

            //ASSERT
            Assert.Equal("ffffffff-ffff-ffff-ffff-ffffffffffff", result.Id);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void ListApplications(bool passCreds)
        {
            //ARRANGE
            var uri = $"{ApiUrl}/v2/applications?page_size=10&page=0&";
            var expected = "{" +
                              "\"page_size\": 10," +
                              "\"page\": 1," +
                              "\"total_items\": 6," +
                              "\"total_pages\": 1," +
                              "\"_embedded\": {" +
                                "\"applications\": [" +
                                  "{" +
                                    "\"id\": \"78d335fa323d01149c3dd6f0d48968cf\"," +
                                    "\"name\": \"My Application\"," +
                                    "\"capabilities\": {" +
                                        "\"voice\": {" +
                                            "\"webhooks\": {" +
                                                "\"answer_url\": {" +
                                                    "\"address\": \"https://example.com/webhooks/answer\"," +
                                                    "\"http_method\": \"POST\"" +
                                                    "}," +
                                                "\"fallback_answer_url\": {" +
                                                    "\"address\": \"https://fallback.example.com/webhooks/answer\"," +
                                                    "\"http_method\": \"POST\"" +
                                                "}," +
                                                "\"event_url\": {" +
                                                    "\"address\": \"https://example.com/webhooks/event\"," +
                                                    "\"http_method\": \"POST\"" +
                                                "}" +
                                            "}" +
                                        "}," +
                                        "\"messages\": {" +
                                            "\"webhooks\": {" +
                                                "\"inbound_url\": {" +
                                                    "\"address\": \"https://example.com/webhooks/inbound\"," +
                                                    "\"http_method\": \"POST\"" +
                                                "}," +
                                                "\"status_url\": {" +
                                                    "\"address\": \"https://example.com/webhooks/status\"," +
                                                    "\"http_method\": \"POST\"" +
                                                "}" +
                                            "}" +
                                        "}," +
                                        "\"rtc\": {" +
                                            "\"webhooks\": {" +
                                                "\"event_url\": {" +
                                                    "\"address\": \"https://example.com/webhooks/event\"," +
                                                    "\"http_method\": \"POST\"" +
                                                "}" +
                                            "}" +
                                        "}," +
                                        "\"vbc\": {}" +
                                    "}" +
                                "}" +
                            "]" +
                        "}" +
                    "}";
            Setup(uri: uri, responseContent: expected);

            //ACT
            var creds = new Request.Credentials() { ApiKey = ApiKey, ApiSecret = ApiSecret };
            var client = new Client(creds);
            List<AppResponse> results;
            if (passCreds)
            {
                results = client.ApplicationV2.List(credentials:creds);
            }
            else
            {
                results = client.ApplicationV2.List();
            }

            //ASSERT
            Assert.Single(results);
            Assert.True(results[0].Id == "78d335fa323d01149c3dd6f0d48968cf");
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void GetApplication(bool passCreds)
        {

            //ARRANGE
            var appId = "ffffffff-ffff-ffff-ffff-ffffffffffff";
            var uri = $"{ApiUrl}/v2/applications/{appId}";
            var expectedResponse = "{  \"id\": \"ffffffff-ffff-ffff-ffff-ffffffffffff\",  \"name\": \"My Application\", \"capabilities\": {\"voice\": {\"webhooks\": {\"answer_url\": {\"address\": \"https://example.com/webhooks/answer\",\"http_method\": \"POST\"},\"fallback_answer_url\": {\"address\": \"https://fallback.example.com/webhooks/answer\",\"http_method\": \"POST\"},\"event_url\": {\"address\": \"https://example.com/webhooks/event\",\"http_method\": \"POST\"}}},\"messages\": {\"webhooks\": {\"inbound_url\": {\"address\": \"https://example.com/webhooks/inbound\",\"http_method\": \"POST\"},\"status_url\": {\"address\": \"https://example.com/webhooks/status\",\"http_method\": \"POST\"}}},\"rtc\": {\"webhooks\": {\"event_url\": {\"address\": \"https://example.com/webhooks/event\",\"http_method\": \"POST\"}}},\"vbc\": {}}}";

            Setup(uri: uri, responseContent: expectedResponse);
            //ACT
            var creds = new Request.Credentials() { ApiKey = ApiKey, ApiSecret = ApiSecret };
            var client = new Client(creds);
            AppResponse result;
            if (passCreds)
            {
                result = client.ApplicationV2.Get(appId,creds);
            }
            else
            {
                result = client.ApplicationV2.Get(appId);
            }

            //ASSERT
            Assert.Equal(result.Id, appId);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void UpdateApplication(bool passCreds)
        {
            //ARRANGE
            var appId = "ffffffff-ffff-ffff-ffff-ffffffffffff";
            var appRequest = new AppRequest
            {
                Id = "ffffffff-ffff-ffff-ffff-ffffffffffff",
                Name = "UpdatedAppTest"
            };
            var uri = $"{ApiUrl}/v2/applications/{appId}";
            var expectedResposne = "{  \"id\": \"ffffffff-ffff-ffff-ffff-ffffffffffff\",  \"name\": \"UpdatedAppTest\", \"capabilities\": {\"voice\": {\"webhooks\": {\"answer_url\": {\"address\": \"https://example.com/webhooks/answer\",\"http_method\": \"POST\"},\"fallback_answer_url\": {\"address\": \"https://fallback.example.com/webhooks/answer\",\"http_method\": \"POST\"},\"event_url\": {\"address\": \"https://example.com/webhooks/event\",\"http_method\": \"POST\"}}},\"messages\": {\"webhooks\": {\"inbound_url\": {\"address\": \"https://example.com/webhooks/inbound\",\"http_method\": \"POST\"},\"status_url\": {\"address\": \"https://example.com/webhooks/status\",\"http_method\": \"POST\"}}},\"rtc\": {\"webhooks\": {\"event_url\": {\"address\": \"https://example.com/webhooks/event\",\"http_method\": \"POST\"}}},\"vbc\": {}}}";
            Setup(uri: uri, responseContent: expectedResposne);

            //ACT
            var creds = new Request.Credentials() { ApiKey = ApiKey, ApiSecret = ApiSecret };
            var client = new Client(creds);
            AppResponse result;
            if (passCreds)
            {
                result = client.ApplicationV2.Update(appRequest,creds);
            }
            else
            {
                result = client.ApplicationV2.Update(appRequest);
            }

            //ASSERT
            Assert.Equal("UpdatedAppTest", result.Name);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void ShouldDeleteApplication(bool passCreds)
        {
            //ARRANGE
            var appId = "ffffffff-ffff-ffff-ffff-ffffffffffff";
            var uri = $"{ApiUrl}/v2/applications/{appId}";
            var response = "";
            Setup(uri: uri, responseContent: response, expectedCode: System.Net.HttpStatusCode.NoContent);

            //ACT
            var creds = new Request.Credentials() { ApiKey = ApiKey, ApiSecret = ApiSecret };
            var client = new Client(creds);
            bool result;
            if (passCreds)
            {
                result = client.ApplicationV2.Delete(appId, creds);
            }
            else
            {
                result = client.ApplicationV2.Delete(appId);
            }

            //ASSERT
            Assert.True(result);
        }
    }
}
