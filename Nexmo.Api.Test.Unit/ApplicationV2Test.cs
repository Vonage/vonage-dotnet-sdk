using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nexmo.Api.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Nexmo.Api.Test.Unit
{
    [TestClass]
    public class ApplicationV2Test : MockedWebTest
    {
        [TestMethod]
        public void ShouldCreateApplication()
        {
            var appRequest = new AppRequest
            {
                Name = "AppV2Test"
            };


            SetExpect($"{ApiUrl}/v2/applications",
"{\"id\":\"ffffffff-ffff-ffff-ffff-ffffffffffff\",\"name\":\"AppV2Test\",\"capabilities\":{},\"keys\":{\"public_key\":\"-----BEGIN PUBLIC KEY-----\\nMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAwxyBT5FqzibSYK0vB+Gr\\nP+YlyYqsx4lvAmotTwmObZEhTWNAdU0p9hrnNXWX1Gy5O0NDIue40SUhYhJT5r4x\\nugbpNA/1KJauB8VQjetKr9bu697yskz2+EuKa2D9e6N2EMY6PD1tJWmeMmddM1tW\\n2DAXuYo7/xsDWIIA6egCTzyShNvzlKo5081t41xVVsPjsWN887Xp1KYfE0IMGV2j\\n8Nwdtw/MQfP/7Qz7i9VXb7bgx0LEg84dWsnz8u3VZ3IQHlydzPX/2iw7e4pc+k27\\nOU1SkmPn/2JtfFFS2LJpcO/FmdSyNnyHezNPyzNRLVbE0sJJ1tEhxi9GZc1I+Oc4\\ndwIDAQAB\\n-----END PUBLIC KEY-----\\n\",\"private_key\":\"-----BEGIN PRIVATE KEY-----\\nMIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQDDHIFPkWrOJtJg\\nrS8H4as/5iXJiqzHiW8Cai1PCY5tkSFNY0B1TSn2Guc1dZfUbLk7Q0Mi57jRJSFi\\nElPmvjG6Buk0D/Uolq4HxVCN60qv1u7r3vKyTPb4S4prYP17o3YQxjo8PW0laZ4y\\nZ10zW1bYMBe5ijv/GwNYggDp6AJPPJKE2/OUqjnTzW3jXFVWw+OxY3zztenUph8T\\nQgwZXaPw3B23D8xB8//tDPuL1VdvtuDHQsSDzh1ayfPy7dVnchAeXJ3M9f/aLDt7\\nilz6Tbs5TVKSY+f/Ym18UVLYsmlw78WZ1LI2fId7M0/LM1EtVsTSwknW0SHGL0Zl\\nzUj45zh3AgMBAAECggEAFP06iJ7p9fkQKwKbwMXNQIes1fm7QjtDJr0vAvB8vXBe\\nPER/7n6EE+vApqwapBP5eJTGTU7PP382kFB3bScAaMo4iSIUVgRLqXNXtJoKGVDO\\nYq/DvQbxjzQVKnMEdyoBAqdIeYAu1IEAWdzBFnbDfhNCYh0q/MkLpZhVVSm2dzkq\\nl+xrEOTyuE48RQxTQqliUYXXlUd5+bjR+oREuYsjt8j9iiK1u6Gv2ztDPeyzupny\\ntCvSlykvAKn/K6xmeO4hapPKUZ7DRthg3I3uPx+mw48GvO28mzhsqXpbDcYI7q82\\nfZgZJ3JKBTRaNnEE8d5llblC5dksMttLgO2EewdXlQKBgQDscfdVVvLYDW3lsPKl\\nAhEmi3ZcWvzDMCGeGLjJEZPfMnc+7rKbCBADQzNMZI/bsWwMJdQCMy2xMaDXr4Ew\\n4TiBdQ7ogpRex9yuJ3miKs3eey6bvpNxV70lj/xvZoSu7oANSFOCMNFUvSWFTPC5\\nNiNGk35g6xklf3WAYLx4bVJcDQKBgQDTP2qL2zQbU/hmQPnq8x/wgGn8T6zQZXbt\\nojyNPTsnIbQhwQzlFM01SzNsF4hVMB8Zz6r+8XHuo3TsDdg6Orx1auIV5lXCWMj/\\n3MW/jy2JabXJyh7BViHFqQjBPHWDrX17TGGsLK/rfOqMRPPgBGTXfVhINaCo7EAh\\nSTPv2x2RkwKBgBQinGpzDhkiA6LUz8UHiQhcRgcVZIMGvUYmWs4cphgSxx7f2uvi\\n4uI0PdEamzmdQVNDgWtyikiVrlnPw1OzSkmT+2IHhLURlhRqniwWMxPoL47pysqT\\nKzNgsKGX/GKdQuBesWXb3Ge399MDO1i6aIShGNkODEUqNoppMoOa47GdAoGAWn3t\\n/F84YQSFgfgPlu/zHKlFvYm788GjQoSe/7ndHxQ2/8ac6X0RsuS18HXcNvHYQMxO\\n6cswDRQEQCJmH/uNQ5c3pj33OruhzskaBMcmsJiSAREOP6/P48ZXM7/cbz3gZPMB\\nXCoAahYmu1PGTI5VTGIrcTNX0UTy689Z6kOo1PUCgYEAjYPkvv4j286XbGHDQyt7\\ngPvbFUPwtYxwk0u/CuZ1scBkVRCMwc8Gic1hL0yB09nvp86cCjNyYcFqa8fTpjom\\n0C7wjZd6zHR4y23U/jVxhdny6lotpgpWKO7DVprjyHQ90yGu+EDq3jDCOjyhdmqP\\n766dkdpKIYoBJOTH9+3r8gc=\\n-----END PRIVATE KEY-----\\n\"}}",
"{\"name\":\"AppV2Test\"}");


            var result = ApplicationV2.Create(appRequest);

            Assert.AreEqual("ffffffff-ffff-ffff-ffff-ffffffffffff", result.Id);
        }

        [TestMethod]
        public void ShouldGetListOfApplications()
        {
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
            SetExpect($"{ApiUrl}/v2/applications?page_size=10&page=0&", expected);

            var results = ApplicationV2.List();

            Assert.AreEqual(1, results.Count);
        }

        [TestMethod]
        public void ShouldGetApplication()
        {
            var appId = "ffffffff-ffff-ffff-ffff-ffffffffffff";

            SetExpect($"{ApiUrl}/v2/applications/{appId}", "{  \"id\": \"ffffffff-ffff-ffff-ffff-ffffffffffff\",  \"name\": \"My Application\", \"capabilities\": {\"voice\": {\"webhooks\": {\"answer_url\": {\"address\": \"https://example.com/webhooks/answer\",\"http_method\": \"POST\"},\"fallback_answer_url\": {\"address\": \"https://fallback.example.com/webhooks/answer\",\"http_method\": \"POST\"},\"event_url\": {\"address\": \"https://example.com/webhooks/event\",\"http_method\": \"POST\"}}},\"messages\": {\"webhooks\": {\"inbound_url\": {\"address\": \"https://example.com/webhooks/inbound\",\"http_method\": \"POST\"},\"status_url\": {\"address\": \"https://example.com/webhooks/status\",\"http_method\": \"POST\"}}},\"rtc\": {\"webhooks\": {\"event_url\": {\"address\": \"https://example.com/webhooks/event\",\"http_method\": \"POST\"}}},\"vbc\": {}}}");

            var results = ApplicationV2.Get(appId);

            Assert.AreEqual(appId, results.Id);
        }

        [TestMethod]
        public void ShouldGetApplicationWhenVoiceFallbackAnswerIsDefined()
        {
            var appId = "ffffffff-ffff-ffff-ffff-ffffffffffff";
            SetExpect(
                $"{ApiUrl}/v2/applications/{appId}",
                @"
                {
                  ""id"": ""ffffffff-ffff-ffff-ffff-ffffffffffff"",
                  ""name"": ""My Application"",
                  ""capabilities"": {
                    ""voice"": {
                      ""webhooks"": {
                        ""answer_url"": { ""address"": ""https://example.com/webhooks/answer"", ""http_method"": ""POST"" },
                        ""fallback_answer_url"": { ""address"": ""https://fallback.example.com/webhooks/answer"", ""http_method"": ""POST"" },
                        ""event_url"": { ""address"": ""https://example.com/webhooks/event"", ""http_method"": ""POST"" }
                      }
                    },
                    ""messages"": {
                      ""webhooks"": {
                        ""inbound_url"": { ""address"": ""https://example.com/webhooks/inbound"", ""http_method"": ""POST"" },
                        ""status_url"": { ""address"": ""https://example.com/webhooks/status"", ""http_method"": ""POST"" }
                      }
                    },
                    ""rtc"": {
                      ""webhooks"": {
                        ""event_url"": { ""address"": ""https://example.com/webhooks/event"", ""http_method"": ""POST"" }
                      }
                    },
                    ""vbc"": {
                    }
                  }
                }"
            );

            var results = ApplicationV2.Get(appId);

            Assert.AreEqual("https://example.com/webhooks/answer",          results.Capabilities.Voice.Hooks.AnswerUrl.Address);
            Assert.AreEqual("https://example.com/webhooks/event",           results.Capabilities.Voice.Hooks.EventUrl.Address);
            Assert.AreEqual("https://fallback.example.com/webhooks/answer", results.Capabilities.Voice.Hooks.FallbackAnswerUrl.Address);
        }

        [TestMethod]
        public void ShouldGetApplicationWhenVoiceFallbackAnswerIsUndefined()
        {
            var appId = "ffffffff-ffff-ffff-ffff-ffffffffffff";
            SetExpect(
                $"{ApiUrl}/v2/applications/{appId}",
                @"
                {
                  ""id"": ""ffffffff-ffff-ffff-ffff-ffffffffffff"",
                  ""name"": ""My Application"",
                  ""capabilities"": {
                    ""voice"": {
                      ""webhooks"": {
                        ""answer_url"": { ""address"": ""https://example.com/webhooks/answer"", ""http_method"": ""POST"" },
                        ""event_url"": { ""address"": ""https://example.com/webhooks/event"", ""http_method"": ""POST"" }
                      }
                    },
                    ""messages"": {
                      ""webhooks"": {
                        ""inbound_url"": { ""address"": ""https://example.com/webhooks/inbound"", ""http_method"": ""POST"" },
                        ""status_url"": { ""address"": ""https://example.com/webhooks/status"", ""http_method"": ""POST"" }
                      }
                    },
                    ""rtc"": {
                      ""webhooks"": {
                        ""event_url"": { ""address"": ""https://example.com/webhooks/event"", ""http_method"": ""POST"" }
                      }
                    },
                    ""vbc"": {
                    }
                  }
                }"
            );

            var results = ApplicationV2.Get(appId);

            Assert.AreEqual("https://example.com/webhooks/answer",  results.Capabilities.Voice.Hooks.AnswerUrl.Address);
            Assert.AreEqual("https://example.com/webhooks/event",   results.Capabilities.Voice.Hooks.EventUrl.Address);
            Assert.AreEqual(null,                                   results.Capabilities.Voice.Hooks.FallbackAnswerUrl);
        }

        [TestMethod]
        public void ShouldUpdateApplication()
        {
            var appId = "ffffffff-ffff-ffff-ffff-ffffffffffff";
            var appRequest = new AppRequest
            {
                Id = "ffffffff-ffff-ffff-ffff-ffffffffffff",
                Name = "UpdatedAppTest"
            };

            // TODO: don't want to introduce UrlEncode dependency, but URLs are hardcoded
            SetExpect($"{ApiUrl}/v2/applications/{appId}",
"{  \"id\": \"ffffffff-ffff-ffff-ffff-ffffffffffff\",  \"name\": \"UpdatedAppTest\", \"capabilities\": {\"voice\": {\"webhooks\": {\"answer_url\": {\"address\": \"https://example.com/webhooks/answer\",\"http_method\": \"POST\"},\"fallback_answer_url\": {\"address\": \"https://fallback.example.com/webhooks/answer\",\"http_method\": \"POST\"},\"event_url\": {\"address\": \"https://example.com/webhooks/event\",\"http_method\": \"POST\"}}},\"messages\": {\"webhooks\": {\"inbound_url\": {\"address\": \"https://example.com/webhooks/inbound\",\"http_method\": \"POST\"},\"status_url\": {\"address\": \"https://example.com/webhooks/status\",\"http_method\": \"POST\"}}},\"rtc\": {\"webhooks\": {\"event_url\": {\"address\": \"https://example.com/webhooks/event\",\"http_method\": \"POST\"}}},\"vbc\": {}}}");

            var result = ApplicationV2.Update(appRequest);

            Assert.AreEqual("UpdatedAppTest", result.Name);
        }

        [TestMethod]
        public void ShouldDeleteApplication()
        {
            var appId = "ffffffff-ffff-ffff-ffff-ffffffffffff";

            SetExpect($"{ApiUrl}/v2/applications/{appId}",
"");
            SetExpectStatus(HttpStatusCode.NoContent);
            var result = ApplicationV2.Delete(appId);

            Assert.AreEqual(true, result);
        }
    }
}
