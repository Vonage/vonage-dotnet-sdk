using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Vonage.Applications;
using Vonage.Applications.Capabilities;
using Xunit;

namespace Vonage.Test.Unit
{
    public class ApplicationTests : TestBase
    {
        private const string PUBLIC_KEY = "-----BEGIN PUBLIC KEY-----\\nMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAwxyBT5FqzibSYK0vB+Gr\\nP+YlyYqsx4lvAmotTwmObZEhTWNAdU0p9hrnNXWX1Gy5O0NDIue40SUhYhJT5r4x\\nugbpNA/1KJauB8VQjetKr9bu697yskz2+EuKa2D9e6N2EMY6PD1tJWmeMmddM1tW\\n2DAXuYo7/xsDWIIA6egCTzyShNvzlKo5081t41xVVsPjsWN887Xp1KYfE0IMGV2j\\n8Nwdtw/MQfP/7Qz7i9VXb7bgx0LEg84dWsnz8u3VZ3IQHlydzPX/2iw7e4pc+k27\\nOU1SkmPn/2JtfFFS2LJpcO/FmdSyNnyHezNPyzNRLVbE0sJJ1tEhxi9GZc1I+Oc4\\ndwIDAQAB\\n-----END PUBLIC KEY-----\\n";
        private const string PRIVATE_KEY = @"-----BEGIN PRIVATE KEY-----\nMIIEvQIBADANBgkqhkiG9w0BAQEFA\nASCBKcwggSjAgEAAoIBAQDEPpvi+3\nRH1efQ\\nkveWzZDrNNoEXmBw61w+O\n0u/N36tJnN5XnYecU64yHzu2ByEr0\n7iIvYbavFnADwl\\nHMTJwqDQakpa3\n8/SFRnTDq3zronvNZ6nOp7S6K7pcZ\nrw/CvrL6hXT1x7cGBZ4jPx\\nqhjqY\nuJPgZD7OVB69oYOV92vIIJ7JLYwqb\n-----END PRIVATE KEY-----\n";
        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task CreateApplication(bool passCreds)
        {
            //ARRANGE
            var uri = $"{ApiUrl}/v2/applications";
            var expectedResponse = @"{
                  ""id"": ""78d335fa323d01149c3dd6f0d48968cf"",
                  ""name"": ""My Application"",
                  ""capabilities"": {
                                ""voice"": {
                                    ""webhooks"": {
                                        ""answer_url"": {
                                            ""address"": ""https://example.com/webhooks/answer"",
                          ""http_method"": ""GET""
                                        },
                        ""fallback_answer_url"": {
                                            ""address"": ""https://fallback.example.com/webhooks/answer"",
                          ""http_method"": ""GET""
                        },
                        ""event_url"": {
                                            ""address"": ""https://example.com/webhooks/event"",
                          ""http_method"": ""POST""
                        }
                                    }
                                },
                    ""messages"": {
                                    ""webhooks"": {
                                        ""inbound_url"": {
                                            ""address"": ""https://example.com/webhooks/inbound"",
                          ""http_method"": ""POST""
                                        },
                        ""status_url"": {
                                            ""address"": ""https://example.com/webhooks/status"",
                          ""http_method"": ""POST""
                        }
                                    }
                                },
                    ""rtc"": {
                                    ""webhooks"": {
                                        ""event_url"": {
                                            ""address"": ""https://example.com/webhooks/event"",
                          ""http_method"": ""POST""
                                        }
                                    }
                                },
                    ""vbc"": { }
                            },
                  ""keys"": {
                                ""public_key"": ""-----BEGIN PUBLIC KEY-----\nMIIBIjANBgkqhkiG9w0BAQEFAAOCA\nKOxjsU4pf/sMFi9N0jqcSLcjxu33G\nd/vynKnlw9SENi+UZR44GdjGdmfm1\ntL1eA7IBh2HNnkYXnAwYzKJoa4eO3\n0kYWekeIZawIwe/g9faFgkev+1xsO\nOUNhPx2LhuLmgwWSRS4L5W851Xe3f\nUQIDAQAB\n-----END PUBLIC KEY-----\n"",
                    ""private_key"": ""-----BEGIN PRIVATE KEY-----\nMIIEvQIBADANBgkqhkiG9w0BAQEFA\nASCBKcwggSjAgEAAoIBAQDEPpvi+3\nRH1efQ\\nkveWzZDrNNoEXmBw61w+O\n0u/N36tJnN5XnYecU64yHzu2ByEr0\n7iIvYbavFnADwl\\nHMTJwqDQakpa3\n8/SFRnTDq3zronvNZ6nOp7S6K7pcZ\nrw/CvrL6hXT1x7cGBZ4jPx\\nqhjqY\nuJPgZD7OVB69oYOV92vIIJ7JLYwqb\n-----END PRIVATE KEY-----\n""
                  }
                        }";
            var expectedRequestContent = @"{""name"":""My Application"",""capabilities"":{""voice"":{""webhooks"":{""answer_url"":{""http_method"":""GET"",""address"":""https://example.com/webhooks/answer""},""event_url"":{""http_method"":""POST"",""address"":""https://example.com/webhooks/events""},""fallback_answer_url"":{""http_method"":""GET"",""address"":""https://fallback.example.com/webhooks/answer""}}},""rtc"":{""webhooks"":{""event_url"":{""http_method"":""POST"",""address"":""https://example.com/webhooks/events""}}},""vbc"":{},""messages"":{""webhooks"":{""inbound_url"":{""http_method"":""POST"",""address"":""https://example.com/webhooks/inbound""},""status_url"":{""http_method"":""POST"",""address"":""https://example.com/webhooks/status""}}}},""keys"":{""public_key"":""-----BEGIN PUBLIC KEY-----\\nMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAwxyBT5FqzibSYK0vB+Gr\\nP+YlyYqsx4lvAmotTwmObZEhTWNAdU0p9hrnNXWX1Gy5O0NDIue40SUhYhJT5r4x\\nugbpNA/1KJauB8VQjetKr9bu697yskz2+EuKa2D9e6N2EMY6PD1tJWmeMmddM1tW\\n2DAXuYo7/xsDWIIA6egCTzyShNvzlKo5081t41xVVsPjsWN887Xp1KYfE0IMGV2j\\n8Nwdtw/MQfP/7Qz7i9VXb7bgx0LEg84dWsnz8u3VZ3IQHlydzPX/2iw7e4pc+k27\\nOU1SkmPn/2JtfFFS2LJpcO/FmdSyNnyHezNPyzNRLVbE0sJJ1tEhxi9GZc1I+Oc4\\ndwIDAQAB\\n-----END PUBLIC KEY-----\\n""}}";

            Setup(uri: uri, responseContent: expectedResponse, requestContent: expectedRequestContent);

            //ACT
            var messagesWebhooks = new Dictionary<Common.Webhook.Type, Common.Webhook>();
            messagesWebhooks.Add(Common.Webhook.Type.inbound_url , new Common.Webhook { Address = "https://example.com/webhooks/inbound", Method="POST"});
            messagesWebhooks.Add(Common.Webhook.Type.status_url , new Common.Webhook { Address = "https://example.com/webhooks/status", Method="POST" });
            var messagesCapability = new Messages(messagesWebhooks);

            var rtcWebhooks = new Dictionary<Common.Webhook.Type, Common.Webhook>();
            rtcWebhooks.Add(Common.Webhook.Type.event_url, new Common.Webhook { Address = "https://example.com/webhooks/events", Method = "POST" });
            var rtcCapability = new Rtc(rtcWebhooks);

            var voiceWebhooks = new Dictionary<Common.Webhook.Type, Common.Webhook>();
            voiceWebhooks.Add(Common.Webhook.Type.answer_url , new Common.Webhook { Address = "https://example.com/webhooks/answer", Method = "GET" });
            voiceWebhooks.Add(Common.Webhook.Type.event_url, new Common.Webhook { Address = "https://example.com/webhooks/events", Method = "POST" });
            voiceWebhooks.Add(Common.Webhook.Type.fallback_answer_url, new Common.Webhook { Address = "https://fallback.example.com/webhooks/answer", Method = "GET" });
            var voiceCapability = new Applications.Capabilities.Voice(voiceWebhooks);
            var json = JsonConvert.SerializeObject(voiceCapability);
            var vbcCapability = new Vbc();

            var capabilities = new ApplicationCapabilities { Messages=messagesCapability, Rtc=rtcCapability, Voice=voiceCapability, Vbc=vbcCapability };            
            var keys = new Applications.Keys
            {
                PublicKey = PUBLIC_KEY
            };
            var request = new CreateApplicationRequest { Capabilities = capabilities, Keys = keys, Name = "My Application" };
            var creds = Request.Credentials.FromApiKeyAndSecret(ApiKey, ApiSecret);
            var client = new VonageClient(creds);
            Application response;
            if (passCreds)
            {
                response = await client.ApplicationClient.CreateApplicaitonAsync(request);
            }
            else
            {
                response = await client.ApplicationClient.CreateApplicaitonAsync(request, creds);
            }
            

            Assert.Equal("78d335fa323d01149c3dd6f0d48968cf", response.Id);
            Assert.Equal("https://example.com/webhooks/answer", response.Capabilities.Voice.Webhooks[Common.Webhook.Type.answer_url].Address);
            Assert.Equal("GET", response.Capabilities.Voice.Webhooks[Common.Webhook.Type.answer_url].Method);

            Assert.Equal("https://fallback.example.com/webhooks/answer",response.Capabilities.Voice.Webhooks[Common.Webhook.Type.fallback_answer_url].Address);
            Assert.Equal("GET", response.Capabilities.Voice.Webhooks[Common.Webhook.Type.fallback_answer_url].Method);

            Assert.Equal("https://example.com/webhooks/event", response.Capabilities.Voice.Webhooks[Common.Webhook.Type.event_url].Address);
            Assert.Equal("POST", response.Capabilities.Voice.Webhooks[Common.Webhook.Type.event_url].Method);

            Assert.Equal("https://example.com/webhooks/inbound", response.Capabilities.Messages.Webhooks[Common.Webhook.Type.inbound_url].Address);
            Assert.Equal("POST", response.Capabilities.Messages.Webhooks[Common.Webhook.Type.inbound_url].Method);

            Assert.Equal("https://example.com/webhooks/status", response.Capabilities.Messages.Webhooks[Common.Webhook.Type.status_url].Address);
            Assert.Equal("POST", response.Capabilities.Messages.Webhooks[Common.Webhook.Type.status_url].Method);

            Assert.Equal("https://example.com/webhooks/event", response.Capabilities.Rtc.Webhooks[Common.Webhook.Type.event_url].Address);
            Assert.Equal("POST", response.Capabilities.Rtc.Webhooks[Common.Webhook.Type.event_url].Method);

            Assert.Equal("My Application", response.Name);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task UpdateApplication(bool passCredentials)
        {
            var id = "78d335fa323d01149c3dd6f0d48968cf";
            var uri = $"{ApiUrl}/v2/applications/{id}";
            var expectedResponse = @"{
                  ""id"": ""78d335fa323d01149c3dd6f0d48968cf"",
                  ""name"": ""My Application"",
                  ""capabilities"": {
                                ""voice"": {
                                    ""webhooks"": {
                                        ""answer_url"": {
                                            ""address"": ""https://example.com/webhooks/answer"",
                          ""http_method"": ""GET""
                                        },
                        ""fallback_answer_url"": {
                                            ""address"": ""https://fallback.example.com/webhooks/answer"",
                          ""http_method"": ""GET""
                        },
                        ""event_url"": {
                                            ""address"": ""https://example.com/webhooks/event"",
                          ""http_method"": ""POST""
                        }
                                    }
                                },
                    ""messages"": {
                                    ""webhooks"": {
                                        ""inbound_url"": {
                                            ""address"": ""https://example.com/webhooks/inbound"",
                          ""http_method"": ""POST""
                                        },
                        ""status_url"": {
                                            ""address"": ""https://example.com/webhooks/status"",
                          ""http_method"": ""POST""
                        }
                                    }
                                },
                    ""rtc"": {
                                    ""webhooks"": {
                                        ""event_url"": {
                                            ""address"": ""https://example.com/webhooks/event"",
                          ""http_method"": ""POST""
                                        }
                                    }
                                },
                    ""vbc"": { }
                            },
                  ""keys"": {
                                ""public_key"": ""-----BEGIN PUBLIC KEY-----\nMIIBIjANBgkqhkiG9w0BAQEFAAOCA\nKOxjsU4pf/sMFi9N0jqcSLcjxu33G\nd/vynKnlw9SENi+UZR44GdjGdmfm1\ntL1eA7IBh2HNnkYXnAwYzKJoa4eO3\n0kYWekeIZawIwe/g9faFgkev+1xsO\nOUNhPx2LhuLmgwWSRS4L5W851Xe3f\nUQIDAQAB\n-----END PUBLIC KEY-----\n"",
                    ""private_key"": ""-----BEGIN PRIVATE KEY-----\nMIIEvQIBADANBgkqhkiG9w0BAQEFA\nASCBKcwggSjAgEAAoIBAQDEPpvi+3\nRH1efQ\\nkveWzZDrNNoEXmBw61w+O\n0u/N36tJnN5XnYecU64yHzu2ByEr0\n7iIvYbavFnADwl\\nHMTJwqDQakpa3\n8/SFRnTDq3zronvNZ6nOp7S6K7pcZ\nrw/CvrL6hXT1x7cGBZ4jPx\\nqhjqY\nuJPgZD7OVB69oYOV92vIIJ7JLYwqb\n-----END PRIVATE KEY-----\n""
                  }
                        }";
            var expectedRequestContent = @"{""name"":""My Application"",""capabilities"":{""voice"":{""webhooks"":{""answer_url"":{""http_method"":""GET"",""address"":""https://example.com/webhooks/answer""},""event_url"":{""http_method"":""POST"",""address"":""https://example.com/webhooks/events""},""fallback_answer_url"":{""http_method"":""GET"",""address"":""https://fallback.example.com/webhooks/answer""}}},""rtc"":{""webhooks"":{""event_url"":{""http_method"":""POST"",""address"":""https://example.com/webhooks/events""}}},""vbc"":{},""messages"":{""webhooks"":{""inbound_url"":{""http_method"":""POST"",""address"":""https://example.com/webhooks/inbound""},""status_url"":{""http_method"":""POST"",""address"":""https://example.com/webhooks/status""}}}},""keys"":{""public_key"":""-----BEGIN PUBLIC KEY-----\\nMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAwxyBT5FqzibSYK0vB+Gr\\nP+YlyYqsx4lvAmotTwmObZEhTWNAdU0p9hrnNXWX1Gy5O0NDIue40SUhYhJT5r4x\\nugbpNA/1KJauB8VQjetKr9bu697yskz2+EuKa2D9e6N2EMY6PD1tJWmeMmddM1tW\\n2DAXuYo7/xsDWIIA6egCTzyShNvzlKo5081t41xVVsPjsWN887Xp1KYfE0IMGV2j\\n8Nwdtw/MQfP/7Qz7i9VXb7bgx0LEg84dWsnz8u3VZ3IQHlydzPX/2iw7e4pc+k27\\nOU1SkmPn/2JtfFFS2LJpcO/FmdSyNnyHezNPyzNRLVbE0sJJ1tEhxi9GZc1I+Oc4\\ndwIDAQAB\\n-----END PUBLIC KEY-----\\n""}}";
            Setup(uri: uri, responseContent: expectedResponse, requestContent: expectedRequestContent);

            //ACT
            var messagesWebhooks = new Dictionary<Common.Webhook.Type, Common.Webhook>();
            messagesWebhooks.Add(Common.Webhook.Type.inbound_url, new Common.Webhook { Address = "https://example.com/webhooks/inbound", Method = "POST" });
            messagesWebhooks.Add(Common.Webhook.Type.status_url, new Common.Webhook { Address = "https://example.com/webhooks/status", Method = "POST" });
            var messagesCapability = new Messages(messagesWebhooks);

            var rtcWebhooks = new Dictionary<Common.Webhook.Type, Common.Webhook>();
            rtcWebhooks.Add(Common.Webhook.Type.event_url, new Common.Webhook { Address = "https://example.com/webhooks/events", Method = "POST" });
            var rtcCapability = new Rtc(rtcWebhooks);

            var voiceWebhooks = new Dictionary<Common.Webhook.Type, Common.Webhook>();
            voiceWebhooks.Add(Common.Webhook.Type.answer_url, new Common.Webhook { Address = "https://example.com/webhooks/answer", Method = "GET" });
            voiceWebhooks.Add(Common.Webhook.Type.event_url, new Common.Webhook { Address = "https://example.com/webhooks/events", Method = "POST" });
            voiceWebhooks.Add(Common.Webhook.Type.fallback_answer_url, new Common.Webhook { Address = "https://fallback.example.com/webhooks/answer", Method = "GET" });
            var voiceCapability = new Applications.Capabilities.Voice(voiceWebhooks);
            var json = JsonConvert.SerializeObject(voiceCapability);
            var vbcCapability = new Vbc();

            var capabilities = new ApplicationCapabilities { Messages = messagesCapability, Rtc = rtcCapability, Voice = voiceCapability, Vbc = vbcCapability };
            var keys = new Applications.Keys
            {
                PublicKey = PUBLIC_KEY
            };
            var application = new CreateApplicationRequest { Capabilities = capabilities, Keys = keys, Name = "My Application" };
            var creds = Request.Credentials.FromApiKeyAndSecret(ApiKey, ApiSecret);
            var client = new VonageClient(creds);
            Application response;
            if (passCredentials)
            {
                response = await client.ApplicationClient.UpdateApplicationAsync(id, application);
            }
            else
            {
                response = await client.ApplicationClient.UpdateApplicationAsync(id, application,creds);
            }
            

            Assert.Equal("78d335fa323d01149c3dd6f0d48968cf", response.Id);
            Assert.Equal("https://example.com/webhooks/answer", response.Capabilities.Voice.Webhooks[Common.Webhook.Type.answer_url].Address);
            Assert.Equal("GET", response.Capabilities.Voice.Webhooks[Common.Webhook.Type.answer_url].Method);

            Assert.Equal("https://fallback.example.com/webhooks/answer", response.Capabilities.Voice.Webhooks[Common.Webhook.Type.fallback_answer_url].Address);
            Assert.Equal("GET", response.Capabilities.Voice.Webhooks[Common.Webhook.Type.fallback_answer_url].Method);

            Assert.Equal("https://example.com/webhooks/event", response.Capabilities.Voice.Webhooks[Common.Webhook.Type.event_url].Address);
            Assert.Equal("POST", response.Capabilities.Voice.Webhooks[Common.Webhook.Type.event_url].Method);

            Assert.Equal("https://example.com/webhooks/inbound", response.Capabilities.Messages.Webhooks[Common.Webhook.Type.inbound_url].Address);
            Assert.Equal("POST", response.Capabilities.Messages.Webhooks[Common.Webhook.Type.inbound_url].Method);

            Assert.Equal("https://example.com/webhooks/status", response.Capabilities.Messages.Webhooks[Common.Webhook.Type.status_url].Address);
            Assert.Equal("POST", response.Capabilities.Messages.Webhooks[Common.Webhook.Type.status_url].Method);

            Assert.Equal("https://example.com/webhooks/event", response.Capabilities.Rtc.Webhooks[Common.Webhook.Type.event_url].Address);
            Assert.Equal("POST", response.Capabilities.Rtc.Webhooks[Common.Webhook.Type.event_url].Method);

            Assert.Equal("My Application", response.Name);
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public async Task ListApplications(bool passCreds, bool passParameters)
        {
            var expectedResult = @"{
                  ""page_size"": 10,
                  ""page"": 1,
                  ""total_items"": 6,
                  ""total_pages"": 1,
                  ""_embedded"": {
                                ""applications"": [
                                  {
                        ""id"": ""78d335fa323d01149c3dd6f0d48968cf"",
                                    ""name"": ""My Application"",
                                    ""capabilities"": {
                          ""voice"": {
                            ""webhooks"": {
                              ""answer_url"": {
                                ""address"": ""https://example.com/webhooks/answer"",
                                            ""http_method"": ""GET""
                              },
                              ""fallback_answer_url"": {
                                ""address"": ""https://fallback.example.com/webhooks/answer"",
                                ""http_method"": ""GET""
                              },
                              ""event_url"": {
                                ""address"": ""https://example.com/webhooks/event"",
                                ""http_method"": ""POST""
                              }
                            }
                          },
                          ""messages"": {
                            ""webhooks"": {
                              ""inbound_url"": {
                                ""address"": ""https://example.com/webhooks/inbound"",
                                ""http_method"": ""POST""
                              },
                              ""status_url"": {
                                ""address"": ""https://example.com/webhooks/status"",
                                ""http_method"": ""POST""
                              }
                            }
                          },
                          ""rtc"": {
                            ""webhooks"": {
                              ""event_url"": {
                                ""address"": ""https://example.com/webhooks/event"",
                                ""http_method"": ""POST""
                              }
                            }
                          },
                          ""vbc"": {}
                        }
                      }
                    ]
                  }
                }";
            string expectedUri;
            ListApplicationsRequest request;
            if (passParameters)
            {
                expectedUri = $"{ApiUrl}/v2/applications?page_size=10&page=1&";
                request = new ListApplicationsRequest { Page = 1, PageSize = 10 };
            }
            else
            {
                expectedUri = $"{ApiUrl}/v2/applications";
                request = new ListApplicationsRequest();
            }
            Setup(expectedUri, expectedResult);

            //Act
            var creds = Request.Credentials.FromApiKeyAndSecret(ApiKey, ApiSecret);
            var client = new VonageClient(creds);
            
            
            ApplicationPage applications;
            if (passCreds)
            {
                applications = await client.ApplicationClient.ListApplicationsAsync(request,creds);
            }
            else
            {
                applications = await client.ApplicationClient.ListApplicationsAsync(request);
            }

            Application application = applications.Embedded.Applications[0];

            Assert.Equal("78d335fa323d01149c3dd6f0d48968cf", application.Id);
            Assert.Equal("https://example.com/webhooks/answer", application.Capabilities.Voice.Webhooks[Common.Webhook.Type.answer_url].Address);
            Assert.Equal("GET", application.Capabilities.Voice.Webhooks[Common.Webhook.Type.answer_url].Method);

            Assert.Equal("https://fallback.example.com/webhooks/answer", application.Capabilities.Voice.Webhooks[Common.Webhook.Type.fallback_answer_url].Address);
            Assert.Equal("GET", application.Capabilities.Voice.Webhooks[Common.Webhook.Type.fallback_answer_url].Method);

            Assert.Equal("https://example.com/webhooks/event", application.Capabilities.Voice.Webhooks[Common.Webhook.Type.event_url].Address);
            Assert.Equal("POST", application.Capabilities.Voice.Webhooks[Common.Webhook.Type.event_url].Method);

            Assert.Equal("https://example.com/webhooks/inbound", application.Capabilities.Messages.Webhooks[Common.Webhook.Type.inbound_url].Address);
            Assert.Equal("POST", application.Capabilities.Messages.Webhooks[Common.Webhook.Type.inbound_url].Method);

            Assert.Equal("https://example.com/webhooks/status", application.Capabilities.Messages.Webhooks[Common.Webhook.Type.status_url].Address);
            Assert.Equal("POST", application.Capabilities.Messages.Webhooks[Common.Webhook.Type.status_url].Method);

            Assert.Equal("https://example.com/webhooks/event", application.Capabilities.Rtc.Webhooks[Common.Webhook.Type.event_url].Address);
            Assert.Equal("POST", application.Capabilities.Rtc.Webhooks[Common.Webhook.Type.event_url].Method);

            Assert.Equal("My Application", application.Name);

            Assert.Equal(6, applications.TotalItems);
            Assert.Equal(1, applications.TotalPages);
            Assert.Equal(10, applications.PageSize);
            Assert.Equal(1, applications.Page);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task GetApplication(bool passCreds)
        {
            var id = "78d335fa323d01149c3dd6f0d48968cf";
            var uri = $"{ApiUrl}/v2/applications/{id}";
            var expectedResponse = @"{
                  ""id"": ""78d335fa323d01149c3dd6f0d48968cf"",
                  ""name"": ""My Application"",
                  ""capabilities"": {
                                ""voice"": {
                                    ""webhooks"": {
                                        ""answer_url"": {
                                            ""address"": ""https://example.com/webhooks/answer"",
                          ""http_method"": ""GET""
                                        },
                        ""fallback_answer_url"": {
                                            ""address"": ""https://fallback.example.com/webhooks/answer"",
                          ""http_method"": ""GET""
                        },
                        ""event_url"": {
                                            ""address"": ""https://example.com/webhooks/event"",
                          ""http_method"": ""POST""
                        }
                                    }
                                },
                    ""messages"": {
                                    ""webhooks"": {
                                        ""inbound_url"": {
                                            ""address"": ""https://example.com/webhooks/inbound"",
                          ""http_method"": ""POST""
                                        },
                        ""status_url"": {
                                            ""address"": ""https://example.com/webhooks/status"",
                          ""http_method"": ""POST""
                        }
                                    }
                                },
                    ""rtc"": {
                                    ""webhooks"": {
                                        ""event_url"": {
                                            ""address"": ""https://example.com/webhooks/event"",
                          ""http_method"": ""POST""
                                        }
                                    }
                                },
                    ""vbc"": { }
                            },
                  ""keys"": {
                                ""public_key"": ""-----BEGIN PUBLIC KEY-----\nMIIBIjANBgkqhkiG9w0BAQEFAAOCA\nKOxjsU4pf/sMFi9N0jqcSLcjxu33G\nd/vynKnlw9SENi+UZR44GdjGdmfm1\ntL1eA7IBh2HNnkYXnAwYzKJoa4eO3\n0kYWekeIZawIwe/g9faFgkev+1xsO\nOUNhPx2LhuLmgwWSRS4L5W851Xe3f\nUQIDAQAB\n-----END PUBLIC KEY-----\n"",
                    ""private_key"": ""-----BEGIN PRIVATE KEY-----\nMIIEvQIBADANBgkqhkiG9w0BAQEFA\nASCBKcwggSjAgEAAoIBAQDEPpvi+3\nRH1efQ\\nkveWzZDrNNoEXmBw61w+O\n0u/N36tJnN5XnYecU64yHzu2ByEr0\n7iIvYbavFnADwl\\nHMTJwqDQakpa3\n8/SFRnTDq3zronvNZ6nOp7S6K7pcZ\nrw/CvrL6hXT1x7cGBZ4jPx\\nqhjqY\nuJPgZD7OVB69oYOV92vIIJ7JLYwqb\n-----END PRIVATE KEY-----\n""
                  }
                        }";
            var expectedUri = $"{ApiUrl}/v2/applications/{id}";
            Setup(expectedUri, expectedResponse);

            var creds = Request.Credentials.FromApiKeyAndSecret(ApiKey, ApiSecret);
            var client = new VonageClient(creds);
            Application application;
            if (passCreds)
            {
                application = await client.ApplicationClient.GetApplicationAsync(id, creds);
            }
            else
            { 
                application = await client.ApplicationClient.GetApplicationAsync(id); 
            }

            Assert.Equal("78d335fa323d01149c3dd6f0d48968cf", application.Id);
            Assert.Equal("https://example.com/webhooks/answer", application.Capabilities.Voice.Webhooks[Common.Webhook.Type.answer_url].Address);
            Assert.Equal("GET", application.Capabilities.Voice.Webhooks[Common.Webhook.Type.answer_url].Method);

            Assert.Equal("https://fallback.example.com/webhooks/answer", application.Capabilities.Voice.Webhooks[Common.Webhook.Type.fallback_answer_url].Address);
            Assert.Equal("GET", application.Capabilities.Voice.Webhooks[Common.Webhook.Type.fallback_answer_url].Method);

            Assert.Equal("https://example.com/webhooks/event", application.Capabilities.Voice.Webhooks[Common.Webhook.Type.event_url].Address);
            Assert.Equal("POST", application.Capabilities.Voice.Webhooks[Common.Webhook.Type.event_url].Method);

            Assert.Equal("https://example.com/webhooks/inbound", application.Capabilities.Messages.Webhooks[Common.Webhook.Type.inbound_url].Address);
            Assert.Equal("POST", application.Capabilities.Messages.Webhooks[Common.Webhook.Type.inbound_url].Method);

            Assert.Equal("https://example.com/webhooks/status", application.Capabilities.Messages.Webhooks[Common.Webhook.Type.status_url].Address);
            Assert.Equal("POST", application.Capabilities.Messages.Webhooks[Common.Webhook.Type.status_url].Method);

            Assert.Equal("https://example.com/webhooks/event", application.Capabilities.Rtc.Webhooks[Common.Webhook.Type.event_url].Address);
            Assert.Equal("POST", application.Capabilities.Rtc.Webhooks[Common.Webhook.Type.event_url].Method);

            Assert.Equal("My Application", application.Name);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task DeleteApplication(bool passCreds)
        {
            var id = "78d335fa323d01149c3dd6f0d48968cf";
            var uri = $"{ApiUrl}/v2/applications/{id}";
            string expectedResponse = "";
            Setup(uri, expectedResponse);
            var creds = Request.Credentials.FromApiKeyAndSecret(ApiKey, ApiSecret);
            var client = new VonageClient(creds);
            bool result;
            if(passCreds)
            {
                result = await client.ApplicationClient.DeleteApplicationAsync(id, creds);
            }
            else
            {
                result = await client.ApplicationClient.DeleteApplicationAsync(id);
            }
            Assert.True(result);
        }

    }
}
