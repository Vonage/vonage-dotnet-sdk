using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using Vonage.Applications;
using Vonage.Applications.Capabilities;
using Vonage.Common;
using Vonage.Request;
using Xunit;

namespace Vonage.Test.Unit
{
    public class ApplicationTests : TestBase
    {
        private const string PublicKey = "some public key";

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void CreateApplication(bool passCreds)
        {
            var expectedResponseContent = this.GetResponseJson();
            var expectedRequestContent = this.GetRequestJson();
            this.Setup($"{this.ApiUrl}/v2/applications", expectedResponseContent, expectedRequestContent);
            var messagesWebhooks = new Dictionary<Webhook.Type, Webhook>
            {
                {
                    Webhook.Type.InboundUrl,
                    new Webhook {Address = "https://example.com/webhooks/inbound", Method = "POST"}
                },
                {
                    Webhook.Type.StatusUrl,
                    new Webhook {Address = "https://example.com/webhooks/status", Method = "POST"}
                },
            };
            var messagesCapability = new Applications.Capabilities.Messages(messagesWebhooks);
            var rtcWebhooks = new Dictionary<Webhook.Type, Webhook>
            {
                {Webhook.Type.EventUrl, new Webhook {Address = "https://example.com/webhooks/events", Method = "POST"}},
            };
            var rtcCapability = new Rtc(rtcWebhooks);
            var voiceWebhooks = new Dictionary<Webhook.Type, Applications.Capabilities.Voice.VoiceWebhook>
            {
                {
                    Webhook.Type.AnswerUrl,
                    new Applications.Capabilities.Voice.VoiceWebhook(new Uri("https://example.com/webhooks/answer"),
                        HttpMethod.Get)
                },
                {
                    Webhook.Type.EventUrl,
                    new Applications.Capabilities.Voice.VoiceWebhook(new Uri("https://example.com/webhooks/events"),
                        HttpMethod.Post)
                },
                {
                    Webhook.Type.FallbackAnswerUrl,
                    new Applications.Capabilities.Voice.VoiceWebhook(
                        new Uri("https://fallback.example.com/webhooks/answer"), HttpMethod.Get)
                },
            };
            var voiceCapability = new Applications.Capabilities.Voice(voiceWebhooks)
            {
                Region = "eu-west",
                SignedCallbacks = true,
                ConversationsTimeToLive = 12,
            };
            var vbcCapability = new Vbc();
            var capabilities = new ApplicationCapabilities
            {
                Messages = messagesCapability, Rtc = rtcCapability, Voice = voiceCapability, Vbc = vbcCapability,
            };
            var keys = new Keys
            {
                PublicKey = PublicKey,
            };
            var request = new CreateApplicationRequest
            {
                Capabilities = capabilities,
                Keys = keys,
                Name = "My Application",
            };
            var credentials = Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);
            var client = this.BuildVonageClient(credentials);
            var response = passCreds
                ? client.ApplicationClient.CreateApplicaiton(request)
                : client.ApplicationClient.CreateApplicaiton(request, credentials);
            Assert.Equal("78d335fa323d01149c3dd6f0d48968cf", response.Id);
            Assert.True(response.Capabilities.Voice.SignedCallbacks);
            Assert.Equal(12, response.Capabilities.Voice.ConversationsTimeToLive);
            Assert.Equal("eu-west", response.Capabilities.Voice.Region);
            Assert.Equal(new Uri("https://example.com/webhooks/answer"),
                response.Capabilities.Voice.Webhooks[Webhook.Type.AnswerUrl].Address);
            Assert.Equal(HttpMethod.Get, response.Capabilities.Voice.Webhooks[Webhook.Type.AnswerUrl].Method);
            Assert.Equal(new Uri("https://fallback.example.com/webhooks/answer"),
                response.Capabilities.Voice.Webhooks[Webhook.Type.FallbackAnswerUrl].Address);
            Assert.Equal(HttpMethod.Get, response.Capabilities.Voice.Webhooks[Webhook.Type.FallbackAnswerUrl].Method);
            Assert.Equal(new Uri("https://example.com/webhooks/event"),
                response.Capabilities.Voice.Webhooks[Webhook.Type.EventUrl].Address);
            Assert.Equal(HttpMethod.Post, response.Capabilities.Voice.Webhooks[Webhook.Type.EventUrl].Method);
            Assert.Equal("https://example.com/webhooks/inbound",
                response.Capabilities.Messages.Webhooks[Webhook.Type.InboundUrl].Address);
            Assert.Equal("POST", response.Capabilities.Messages.Webhooks[Webhook.Type.InboundUrl].Method);
            Assert.Equal("https://example.com/webhooks/status",
                response.Capabilities.Messages.Webhooks[Webhook.Type.StatusUrl].Address);
            Assert.Equal("POST", response.Capabilities.Messages.Webhooks[Webhook.Type.StatusUrl].Method);
            Assert.Equal("https://example.com/webhooks/event",
                response.Capabilities.Rtc.Webhooks[Webhook.Type.EventUrl].Address);
            Assert.Equal("POST", response.Capabilities.Rtc.Webhooks[Webhook.Type.EventUrl].Method);
            Assert.Equal("My Application", response.Name);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task CreateApplicationAsync(bool passCreds)
        {
            //ARRANGE
            var uri = $"{this.ApiUrl}/v2/applications";
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
                                ""public_key"": ""some public key"",
                    ""private_key"": ""some private key""
                  }
                        }";
            var expectedRequestContent =
                @"{""name"":""My Application"",""capabilities"":{""voice"":{""webhooks"":{""answer_url"":{""http_method"":""GET"",""address"":""https://example.com/webhooks/answer""},""event_url"":{""http_method"":""POST"",""address"":""https://example.com/webhooks/events""},""fallback_answer_url"":{""http_method"":""GET"",""address"":""https://fallback.example.com/webhooks/answer""}}},""rtc"":{""webhooks"":{""event_url"":{""http_method"":""POST"",""address"":""https://example.com/webhooks/events""}}},""vbc"":{},""messages"":{""webhooks"":{""inbound_url"":{""http_method"":""POST"",""address"":""https://example.com/webhooks/inbound""},""status_url"":{""http_method"":""POST"",""address"":""https://example.com/webhooks/status""}}}},""keys"":{""public_key"":""some public key""}}";
            this.Setup(uri, expectedResponse, expectedRequestContent);

            //ACT
            var messagesWebhooks = new Dictionary<Webhook.Type, Webhook>
            {
                {
                    Webhook.Type.InboundUrl,
                    new Webhook {Address = "https://example.com/webhooks/inbound", Method = "POST"}
                },
                {
                    Webhook.Type.StatusUrl,
                    new Webhook {Address = "https://example.com/webhooks/status", Method = "POST"}
                },
            };
            var messagesCapability = new Applications.Capabilities.Messages(messagesWebhooks);
            var rtcWebhooks = new Dictionary<Webhook.Type, Webhook>
            {
                {Webhook.Type.EventUrl, new Webhook {Address = "https://example.com/webhooks/events", Method = "POST"}},
            };
            var rtcCapability = new Rtc(rtcWebhooks);
            var voiceWebhooks = new Dictionary<Webhook.Type, Applications.Capabilities.Voice.VoiceWebhook>
            {
                {
                    Webhook.Type.AnswerUrl,
                    new Applications.Capabilities.Voice.VoiceWebhook(new Uri("https://example.com/webhooks/answer"),
                        HttpMethod.Get)
                },
                {
                    Webhook.Type.EventUrl,
                    new Applications.Capabilities.Voice.VoiceWebhook(new Uri("https://example.com/webhooks/events"),
                        HttpMethod.Post)
                },
                {
                    Webhook.Type.FallbackAnswerUrl,
                    new Applications.Capabilities.Voice.VoiceWebhook(
                        new Uri("https://fallback.example.com/webhooks/answer"), HttpMethod.Get)
                },
            };
            var voiceCapability = new Applications.Capabilities.Voice(voiceWebhooks);
            JsonConvert.SerializeObject(voiceCapability);
            var vbcCapability = new Vbc();
            var capabilities = new ApplicationCapabilities
                {Messages = messagesCapability, Rtc = rtcCapability, Voice = voiceCapability, Vbc = vbcCapability};
            var keys = new Keys
            {
                PublicKey = PublicKey,
            };
            var request = new CreateApplicationRequest
                {Capabilities = capabilities, Keys = keys, Name = "My Application"};
            var creds = Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);
            var client = this.BuildVonageClient(creds);
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
            Assert.Equal(new Uri("https://example.com/webhooks/answer"),
                response.Capabilities.Voice.Webhooks[Webhook.Type.AnswerUrl].Address);
            Assert.Equal(HttpMethod.Get, response.Capabilities.Voice.Webhooks[Webhook.Type.AnswerUrl].Method);
            Assert.Equal(new Uri("https://fallback.example.com/webhooks/answer"),
                response.Capabilities.Voice.Webhooks[Webhook.Type.FallbackAnswerUrl].Address);
            Assert.Equal(HttpMethod.Get, response.Capabilities.Voice.Webhooks[Webhook.Type.FallbackAnswerUrl].Method);
            Assert.Equal(new Uri("https://example.com/webhooks/event"),
                response.Capabilities.Voice.Webhooks[Webhook.Type.EventUrl].Address);
            Assert.Equal(HttpMethod.Post, response.Capabilities.Voice.Webhooks[Webhook.Type.EventUrl].Method);
            Assert.Equal("https://example.com/webhooks/inbound",
                response.Capabilities.Messages.Webhooks[Webhook.Type.InboundUrl].Address);
            Assert.Equal("POST", response.Capabilities.Messages.Webhooks[Webhook.Type.InboundUrl].Method);
            Assert.Equal("https://example.com/webhooks/status",
                response.Capabilities.Messages.Webhooks[Webhook.Type.StatusUrl].Address);
            Assert.Equal("POST", response.Capabilities.Messages.Webhooks[Webhook.Type.StatusUrl].Method);
            Assert.Equal("https://example.com/webhooks/event",
                response.Capabilities.Rtc.Webhooks[Webhook.Type.EventUrl].Address);
            Assert.Equal("POST", response.Capabilities.Rtc.Webhooks[Webhook.Type.EventUrl].Method);
            Assert.Equal("My Application", response.Name);
        }

        [Fact]
        public async Task CreateApplicationAsyncWithPrivacySettings()
        {
            var expectedResponseContent = this.GetResponseJson();
            var expectedRequestContent = this.GetRequestJson();
            this.Setup($"{this.ApiUrl}/v2/applications", expectedResponseContent, expectedRequestContent);
            var request = new CreateApplicationRequest
            {
                Capabilities = new ApplicationCapabilities(),
                Name = "My Application",
                Keys = new Keys
                {
                    PublicKey = PublicKey,
                },
                Privacy = new PrivacySettings(true),
            };
            var credentials = Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);
            var client = this.BuildVonageClient(credentials);
            var response = await client.ApplicationClient.CreateApplicaitonAsync(request);
            response.Privacy.Should().Be(new PrivacySettings(true));
            response.Capabilities.Should().BeEquivalentTo(new ApplicationCapabilities());
            response.Keys.Should().BeEquivalentTo(new Keys
            {
                PublicKey = PublicKey,
            });
            response.Name.Should().Be("My Application");
        }

        [Fact]
        public async Task CreateApplicationAsyncWithVoiceTimeouts()
        {
            var expectedResponseContent = this.GetResponseJson();
            var expectedRequestContent = this.GetRequestJson();
            this.Setup($"{this.ApiUrl}/v2/applications", expectedResponseContent, expectedRequestContent);
            var answerWebhook = new Applications.Capabilities.Voice.VoiceWebhook(
                new Uri("https://example.com/webhooks/answer"),
                HttpMethod.Get, 300, 2000);
            var eventWebhook = new Applications.Capabilities.Voice.VoiceWebhook(
                new Uri("https://example.com/webhooks/events"),
                HttpMethod.Post, 500, 3000);
            var fallbackWebhook = new Applications.Capabilities.Voice.VoiceWebhook(
                new Uri("https://fallback.example.com/webhooks/answer"), HttpMethod.Get, 800, 4000);
            var request = new CreateApplicationRequest
            {
                Capabilities = new ApplicationCapabilities
                {
                    Voice = new Applications.Capabilities.Voice(
                        new Dictionary<Webhook.Type, Applications.Capabilities.Voice.VoiceWebhook>
                        {
                            {Webhook.Type.AnswerUrl, answerWebhook},
                            {Webhook.Type.EventUrl, eventWebhook},
                            {Webhook.Type.FallbackAnswerUrl, fallbackWebhook},
                        }),
                },
            };
            var credentials = Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);
            var client = this.BuildVonageClient(credentials);
            var response = await client.ApplicationClient.CreateApplicaitonAsync(request);
            response.Capabilities.Voice.Webhooks[Webhook.Type.AnswerUrl].Should().Be(answerWebhook);
            response.Capabilities.Voice.Webhooks[Webhook.Type.EventUrl].Should().Be(eventWebhook);
            response.Capabilities.Voice.Webhooks[Webhook.Type.FallbackAnswerUrl].Should().Be(fallbackWebhook);
        }

        [Fact]
        public async Task CreateApplicationWithMeetingsCapabilities()
        {
            var expectedResponseContent = this.GetResponseJson();
            var expectedRequestContent = this.GetRequestJson();
            this.Setup($"{this.ApiUrl}/v2/applications", expectedResponseContent, expectedRequestContent);
            var request = new CreateApplicationRequest
            {
                Capabilities = new ApplicationCapabilities
                {
                    Meetings = new Applications.Capabilities.Meetings(new Dictionary<Webhook.Type, Webhook>
                    {
                        {Webhook.Type.RoomChanged, new Webhook {Address = "http://example.com", Method = "POST"}},
                        {Webhook.Type.SessionChanged, new Webhook {Address = "http://example.com", Method = "POST"}},
                        {
                            Webhook.Type.RecordingChanged,
                            new Webhook {Address = "https://54eba990d025.ngrok.app/recordings", Method = "POST"}
                        },
                    }),
                },
            };
            var credentials = Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);
            var client = this.BuildVonageClient(credentials);
            var response = await client.ApplicationClient.CreateApplicaitonAsync(request);
            response.Capabilities.Meetings.Webhooks.Should().BeEquivalentTo(response.Capabilities.Meetings.Webhooks);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void DeleteApplication(bool passCreds)
        {
            var id = "78d335fa323d01149c3dd6f0d48968cf";
            var uri = $"{this.ApiUrl}/v2/applications/{id}";
            var expectedResponse = "";
            this.Setup(uri, expectedResponse);
            var creds = Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);
            var client = this.BuildVonageClient(creds);
            var result = passCreds
                ? client.ApplicationClient.DeleteApplication(id, creds)
                : client.ApplicationClient.DeleteApplication(id);
            Assert.True(result);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task DeleteApplicationAsync(bool passCreds)
        {
            var id = "78d335fa323d01149c3dd6f0d48968cf";
            var uri = $"{this.ApiUrl}/v2/applications/{id}";
            var expectedResponse = "";
            this.Setup(uri, expectedResponse);
            var creds = Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);
            var client = this.BuildVonageClient(creds);
            bool result;
            if (passCreds)
            {
                result = await client.ApplicationClient.DeleteApplicationAsync(id, creds);
            }
            else
            {
                result = await client.ApplicationClient.DeleteApplicationAsync(id);
            }

            Assert.True(result);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void GetApplication(bool passCreds)
        {
            var id = "78d335fa323d01149c3dd6f0d48968cf";
            var expectedResponse = this.GetResponseJson();
            var expectedUri = $"{this.ApiUrl}/v2/applications/{id}";
            this.Setup(expectedUri, expectedResponse);
            var creds = Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);
            var client = this.BuildVonageClient(creds);
            var application = passCreds
                ? client.ApplicationClient.GetApplication(id, creds)
                : client.ApplicationClient.GetApplication(id);
            Assert.Equal("78d335fa323d01149c3dd6f0d48968cf", application.Id);
            Assert.Equal(new Uri("https://example.com/webhooks/answer"),
                application.Capabilities.Voice.Webhooks[Webhook.Type.AnswerUrl].Address);
            Assert.Equal(HttpMethod.Get, application.Capabilities.Voice.Webhooks[Webhook.Type.AnswerUrl].Method);
            Assert.Equal(new Uri("https://fallback.example.com/webhooks/answer"),
                application.Capabilities.Voice.Webhooks[Webhook.Type.FallbackAnswerUrl].Address);
            Assert.Equal(HttpMethod.Get,
                application.Capabilities.Voice.Webhooks[Webhook.Type.FallbackAnswerUrl].Method);
            Assert.Equal(new Uri("https://example.com/webhooks/event"),
                application.Capabilities.Voice.Webhooks[Webhook.Type.EventUrl].Address);
            Assert.Equal(HttpMethod.Post, application.Capabilities.Voice.Webhooks[Webhook.Type.EventUrl].Method);
            Assert.Equal("https://example.com/webhooks/inbound",
                application.Capabilities.Messages.Webhooks[Webhook.Type.InboundUrl].Address);
            Assert.Equal("POST", application.Capabilities.Messages.Webhooks[Webhook.Type.InboundUrl].Method);
            Assert.Equal("https://example.com/webhooks/status",
                application.Capabilities.Messages.Webhooks[Webhook.Type.StatusUrl].Address);
            Assert.Equal("POST", application.Capabilities.Messages.Webhooks[Webhook.Type.StatusUrl].Method);
            Assert.Equal("https://example.com/webhooks/event",
                application.Capabilities.Rtc.Webhooks[Webhook.Type.EventUrl].Address);
            Assert.Equal("POST", application.Capabilities.Rtc.Webhooks[Webhook.Type.EventUrl].Method);
            Assert.Equal("My Application", application.Name);
            Assert.Equal("http://example.com",
                application.Capabilities.Meetings.Webhooks[Webhook.Type.RoomChanged].Address);
            Assert.Equal("POST", application.Capabilities.Meetings.Webhooks[Webhook.Type.RoomChanged].Method);
            Assert.Equal("http://example.com",
                application.Capabilities.Meetings.Webhooks[Webhook.Type.SessionChanged].Address);
            Assert.Equal("POST", application.Capabilities.Meetings.Webhooks[Webhook.Type.SessionChanged].Method);
            Assert.Equal("https://54eba990d025.ngrok.app/recordings",
                application.Capabilities.Meetings.Webhooks[Webhook.Type.RecordingChanged].Address);
            Assert.Equal("POST", application.Capabilities.Meetings.Webhooks[Webhook.Type.RecordingChanged].Method);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task GetApplicationAsync(bool passCreds)
        {
            var id = "78d335fa323d01149c3dd6f0d48968cf";
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
                                ""public_key"": ""some public key"",
                    ""private_key"": ""some private key""
                  }
                        }";
            var expectedUri = $"{this.ApiUrl}/v2/applications/{id}";
            this.Setup(expectedUri, expectedResponse);
            var creds = Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);
            var client = this.BuildVonageClient(creds);
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
            Assert.Equal(new Uri("https://example.com/webhooks/answer"),
                application.Capabilities.Voice.Webhooks[Webhook.Type.AnswerUrl].Address);
            Assert.Equal(HttpMethod.Get, application.Capabilities.Voice.Webhooks[Webhook.Type.AnswerUrl].Method);
            Assert.Equal(new Uri("https://fallback.example.com/webhooks/answer"),
                application.Capabilities.Voice.Webhooks[Webhook.Type.FallbackAnswerUrl].Address);
            Assert.Equal(HttpMethod.Get,
                application.Capabilities.Voice.Webhooks[Webhook.Type.FallbackAnswerUrl].Method);
            Assert.Equal(new Uri("https://example.com/webhooks/event"),
                application.Capabilities.Voice.Webhooks[Webhook.Type.EventUrl].Address);
            Assert.Equal(HttpMethod.Post, application.Capabilities.Voice.Webhooks[Webhook.Type.EventUrl].Method);
            Assert.Equal("https://example.com/webhooks/inbound",
                application.Capabilities.Messages.Webhooks[Webhook.Type.InboundUrl].Address);
            Assert.Equal("POST", application.Capabilities.Messages.Webhooks[Webhook.Type.InboundUrl].Method);
            Assert.Equal("https://example.com/webhooks/status",
                application.Capabilities.Messages.Webhooks[Webhook.Type.StatusUrl].Address);
            Assert.Equal("POST", application.Capabilities.Messages.Webhooks[Webhook.Type.StatusUrl].Method);
            Assert.Equal("https://example.com/webhooks/event",
                application.Capabilities.Rtc.Webhooks[Webhook.Type.EventUrl].Address);
            Assert.Equal("POST", application.Capabilities.Rtc.Webhooks[Webhook.Type.EventUrl].Method);
            Assert.Equal("My Application", application.Name);
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void ListApplications(bool passCreds, bool passParameters)
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
                expectedUri = $"{this.ApiUrl}/v2/applications?page_size=10&page=1&";
                request = new ListApplicationsRequest {Page = 1, PageSize = 10};
            }
            else
            {
                expectedUri = $"{this.ApiUrl}/v2/applications";
                request = new ListApplicationsRequest();
            }

            this.Setup(expectedUri, expectedResult);

            //Act
            var creds = Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);
            var client = this.BuildVonageClient(creds);
            var applications = passCreds
                ? client.ApplicationClient.ListApplications(request, creds)
                : client.ApplicationClient.ListApplications(request);
            var application = applications.Embedded.Applications[0];
            Assert.Equal("78d335fa323d01149c3dd6f0d48968cf", application.Id);
            Assert.Equal(new Uri("https://example.com/webhooks/answer"),
                application.Capabilities.Voice.Webhooks[Webhook.Type.AnswerUrl].Address);
            Assert.Equal(HttpMethod.Get, application.Capabilities.Voice.Webhooks[Webhook.Type.AnswerUrl].Method);
            Assert.Equal(new Uri("https://fallback.example.com/webhooks/answer"),
                application.Capabilities.Voice.Webhooks[Webhook.Type.FallbackAnswerUrl].Address);
            Assert.Equal(HttpMethod.Get,
                application.Capabilities.Voice.Webhooks[Webhook.Type.FallbackAnswerUrl].Method);
            Assert.Equal(new Uri("https://example.com/webhooks/event"),
                application.Capabilities.Voice.Webhooks[Webhook.Type.EventUrl].Address);
            Assert.Equal(HttpMethod.Post, application.Capabilities.Voice.Webhooks[Webhook.Type.EventUrl].Method);
            Assert.Equal("https://example.com/webhooks/inbound",
                application.Capabilities.Messages.Webhooks[Webhook.Type.InboundUrl].Address);
            Assert.Equal("POST", application.Capabilities.Messages.Webhooks[Webhook.Type.InboundUrl].Method);
            Assert.Equal("https://example.com/webhooks/status",
                application.Capabilities.Messages.Webhooks[Webhook.Type.StatusUrl].Address);
            Assert.Equal("POST", application.Capabilities.Messages.Webhooks[Webhook.Type.StatusUrl].Method);
            Assert.Equal("https://example.com/webhooks/event",
                application.Capabilities.Rtc.Webhooks[Webhook.Type.EventUrl].Address);
            Assert.Equal("POST", application.Capabilities.Rtc.Webhooks[Webhook.Type.EventUrl].Method);
            Assert.Equal("My Application", application.Name);
            Assert.Equal(6, applications.TotalItems);
            Assert.Equal(1, applications.TotalPages);
            Assert.Equal(10, applications.PageSize);
            Assert.Equal(1, applications.Page);
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public async Task ListApplicationsAsync(bool passCreds, bool passParameters)
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
                expectedUri = $"{this.ApiUrl}/v2/applications?page_size=10&page=1&";
                request = new ListApplicationsRequest {Page = 1, PageSize = 10};
            }
            else
            {
                expectedUri = $"{this.ApiUrl}/v2/applications";
                request = new ListApplicationsRequest();
            }

            this.Setup(expectedUri, expectedResult);

            //Act
            var creds = Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);
            var client = this.BuildVonageClient(creds);
            ApplicationPage applications;
            if (passCreds)
            {
                applications = await client.ApplicationClient.ListApplicationsAsync(request, creds);
            }
            else
            {
                applications = await client.ApplicationClient.ListApplicationsAsync(request);
            }

            var application = applications.Embedded.Applications[0];
            Assert.Equal("78d335fa323d01149c3dd6f0d48968cf", application.Id);
            Assert.Equal(new Uri("https://example.com/webhooks/answer"),
                application.Capabilities.Voice.Webhooks[Webhook.Type.AnswerUrl].Address);
            Assert.Equal(HttpMethod.Get, application.Capabilities.Voice.Webhooks[Webhook.Type.AnswerUrl].Method);
            Assert.Equal(new Uri("https://fallback.example.com/webhooks/answer"),
                application.Capabilities.Voice.Webhooks[Webhook.Type.FallbackAnswerUrl].Address);
            Assert.Equal(HttpMethod.Get,
                application.Capabilities.Voice.Webhooks[Webhook.Type.FallbackAnswerUrl].Method);
            Assert.Equal(new Uri("https://example.com/webhooks/event"),
                application.Capabilities.Voice.Webhooks[Webhook.Type.EventUrl].Address);
            Assert.Equal(HttpMethod.Post, application.Capabilities.Voice.Webhooks[Webhook.Type.EventUrl].Method);
            Assert.Equal("https://example.com/webhooks/inbound",
                application.Capabilities.Messages.Webhooks[Webhook.Type.InboundUrl].Address);
            Assert.Equal("POST", application.Capabilities.Messages.Webhooks[Webhook.Type.InboundUrl].Method);
            Assert.Equal("https://example.com/webhooks/status",
                application.Capabilities.Messages.Webhooks[Webhook.Type.StatusUrl].Address);
            Assert.Equal("POST", application.Capabilities.Messages.Webhooks[Webhook.Type.StatusUrl].Method);
            Assert.Equal("https://example.com/webhooks/event",
                application.Capabilities.Rtc.Webhooks[Webhook.Type.EventUrl].Address);
            Assert.Equal("POST", application.Capabilities.Rtc.Webhooks[Webhook.Type.EventUrl].Method);
            Assert.Equal("My Application", application.Name);
            Assert.Equal(6, applications.TotalItems);
            Assert.Equal(1, applications.TotalPages);
            Assert.Equal(10, applications.PageSize);
            Assert.Equal(1, applications.Page);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void UpdateApplication(bool passCredentials)
        {
            var id = "78d335fa323d01149c3dd6f0d48968cf";
            var uri = $"{this.ApiUrl}/v2/applications/{id}";
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
                                ""public_key"": ""some public key"",
                    ""private_key"": ""some private key""
                  }
                        }";
            var expectedRequestContent =
                @"{""name"":""My Application"",""capabilities"":{""voice"":{""webhooks"":{""answer_url"":{""http_method"":""GET"",""address"":""https://example.com/webhooks/answer""},""event_url"":{""http_method"":""POST"",""address"":""https://example.com/webhooks/events""},""fallback_answer_url"":{""http_method"":""GET"",""address"":""https://fallback.example.com/webhooks/answer""}}},""rtc"":{""webhooks"":{""event_url"":{""http_method"":""POST"",""address"":""https://example.com/webhooks/events""}}},""vbc"":{},""messages"":{""webhooks"":{""inbound_url"":{""http_method"":""POST"",""address"":""https://example.com/webhooks/inbound""},""status_url"":{""http_method"":""POST"",""address"":""https://example.com/webhooks/status""}}}},""keys"":{""public_key"":""some public key""}}";
            this.Setup(uri, expectedResponse, expectedRequestContent);

            //ACT
            var messagesWebhooks = new Dictionary<Webhook.Type, Webhook>
            {
                {
                    Webhook.Type.InboundUrl,
                    new Webhook {Address = "https://example.com/webhooks/inbound", Method = "POST"}
                },
                {
                    Webhook.Type.StatusUrl,
                    new Webhook {Address = "https://example.com/webhooks/status", Method = "POST"}
                },
            };
            var messagesCapability = new Applications.Capabilities.Messages(messagesWebhooks);
            var rtcWebhooks = new Dictionary<Webhook.Type, Webhook>
            {
                {Webhook.Type.EventUrl, new Webhook {Address = "https://example.com/webhooks/events", Method = "POST"}},
            };
            var rtcCapability = new Rtc(rtcWebhooks);
            var voiceWebhooks = new Dictionary<Webhook.Type, Applications.Capabilities.Voice.VoiceWebhook>
            {
                {
                    Webhook.Type.AnswerUrl,
                    new Applications.Capabilities.Voice.VoiceWebhook(new Uri("https://example.com/webhooks/answer"),
                        HttpMethod.Get)
                },
                {
                    Webhook.Type.EventUrl,
                    new Applications.Capabilities.Voice.VoiceWebhook(new Uri("https://example.com/webhooks/events"),
                        HttpMethod.Post)
                },
                {
                    Webhook.Type.FallbackAnswerUrl,
                    new Applications.Capabilities.Voice.VoiceWebhook(
                        new Uri("https://fallback.example.com/webhooks/answer"), HttpMethod.Get)
                },
            };
            var voiceCapability = new Applications.Capabilities.Voice(voiceWebhooks);
            JsonConvert.SerializeObject(voiceCapability);
            var vbcCapability = new Vbc();
            var capabilities = new ApplicationCapabilities
                {Messages = messagesCapability, Rtc = rtcCapability, Voice = voiceCapability, Vbc = vbcCapability};
            var keys = new Keys
            {
                PublicKey = PublicKey,
            };
            var application = new CreateApplicationRequest
                {Capabilities = capabilities, Keys = keys, Name = "My Application"};
            var creds = Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);
            var client = this.BuildVonageClient(creds);
            var response = passCredentials
                ? client.ApplicationClient.UpdateApplication(id, application)
                : client.ApplicationClient.UpdateApplication(id, application, creds);
            Assert.Equal("78d335fa323d01149c3dd6f0d48968cf", response.Id);
            Assert.Equal(new Uri("https://example.com/webhooks/answer"),
                response.Capabilities.Voice.Webhooks[Webhook.Type.AnswerUrl].Address);
            Assert.Equal(HttpMethod.Get, response.Capabilities.Voice.Webhooks[Webhook.Type.AnswerUrl].Method);
            Assert.Equal(new Uri("https://fallback.example.com/webhooks/answer"),
                response.Capabilities.Voice.Webhooks[Webhook.Type.FallbackAnswerUrl].Address);
            Assert.Equal(HttpMethod.Get, response.Capabilities.Voice.Webhooks[Webhook.Type.FallbackAnswerUrl].Method);
            Assert.Equal(new Uri("https://example.com/webhooks/event"),
                response.Capabilities.Voice.Webhooks[Webhook.Type.EventUrl].Address);
            Assert.Equal(HttpMethod.Post, response.Capabilities.Voice.Webhooks[Webhook.Type.EventUrl].Method);
            Assert.Equal("https://example.com/webhooks/inbound",
                response.Capabilities.Messages.Webhooks[Webhook.Type.InboundUrl].Address);
            Assert.Equal("POST", response.Capabilities.Messages.Webhooks[Webhook.Type.InboundUrl].Method);
            Assert.Equal("https://example.com/webhooks/status",
                response.Capabilities.Messages.Webhooks[Webhook.Type.StatusUrl].Address);
            Assert.Equal("POST", response.Capabilities.Messages.Webhooks[Webhook.Type.StatusUrl].Method);
            Assert.Equal("https://example.com/webhooks/event",
                response.Capabilities.Rtc.Webhooks[Webhook.Type.EventUrl].Address);
            Assert.Equal("POST", response.Capabilities.Rtc.Webhooks[Webhook.Type.EventUrl].Method);
            Assert.Equal("My Application", response.Name);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task UpdateApplicationAsync(bool passCredentials)
        {
            var id = "78d335fa323d01149c3dd6f0d48968cf";
            var uri = $"{this.ApiUrl}/v2/applications/{id}";
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
                                ""public_key"": ""some public key"",
                    ""private_key"": ""some private key""
                  }
                        }";
            var expectedRequestContent =
                @"{""name"":""My Application"",""capabilities"":{""voice"":{""webhooks"":{""answer_url"":{""http_method"":""GET"",""address"":""https://example.com/webhooks/answer""},""event_url"":{""http_method"":""POST"",""address"":""https://example.com/webhooks/events""},""fallback_answer_url"":{""http_method"":""GET"",""address"":""https://fallback.example.com/webhooks/answer""}}},""rtc"":{""webhooks"":{""event_url"":{""http_method"":""POST"",""address"":""https://example.com/webhooks/events""}}},""vbc"":{},""messages"":{""webhooks"":{""inbound_url"":{""http_method"":""POST"",""address"":""https://example.com/webhooks/inbound""},""status_url"":{""http_method"":""POST"",""address"":""https://example.com/webhooks/status""}}}},""keys"":{""public_key"":""some public key""}}";
            this.Setup(uri, expectedResponse, expectedRequestContent);

            //ACT
            var messagesWebhooks = new Dictionary<Webhook.Type, Webhook>
            {
                {
                    Webhook.Type.InboundUrl,
                    new Webhook {Address = "https://example.com/webhooks/inbound", Method = "POST"}
                },
                {
                    Webhook.Type.StatusUrl,
                    new Webhook {Address = "https://example.com/webhooks/status", Method = "POST"}
                },
            };
            var messagesCapability = new Applications.Capabilities.Messages(messagesWebhooks);
            var rtcWebhooks = new Dictionary<Webhook.Type, Webhook>
            {
                {Webhook.Type.EventUrl, new Webhook {Address = "https://example.com/webhooks/events", Method = "POST"}},
            };
            var rtcCapability = new Rtc(rtcWebhooks);
            var voiceWebhooks = new Dictionary<Webhook.Type, Applications.Capabilities.Voice.VoiceWebhook>
            {
                {
                    Webhook.Type.AnswerUrl,
                    new Applications.Capabilities.Voice.VoiceWebhook(new Uri("https://example.com/webhooks/answer"),
                        HttpMethod.Get)
                },
                {
                    Webhook.Type.EventUrl,
                    new Applications.Capabilities.Voice.VoiceWebhook(new Uri("https://example.com/webhooks/events"),
                        HttpMethod.Post)
                },
                {
                    Webhook.Type.FallbackAnswerUrl,
                    new Applications.Capabilities.Voice.VoiceWebhook(
                        new Uri("https://fallback.example.com/webhooks/answer"), HttpMethod.Get)
                },
            };
            var voiceCapability = new Applications.Capabilities.Voice(voiceWebhooks);
            JsonConvert.SerializeObject(voiceCapability);
            var vbcCapability = new Vbc();
            var capabilities = new ApplicationCapabilities
                {Messages = messagesCapability, Rtc = rtcCapability, Voice = voiceCapability, Vbc = vbcCapability};
            var keys = new Keys
            {
                PublicKey = PublicKey,
            };
            var application = new CreateApplicationRequest
                {Capabilities = capabilities, Keys = keys, Name = "My Application"};
            var creds = Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);
            var client = this.BuildVonageClient(creds);
            Application response;
            if (passCredentials)
            {
                response = await client.ApplicationClient.UpdateApplicationAsync(id, application);
            }
            else
            {
                response = await client.ApplicationClient.UpdateApplicationAsync(id, application, creds);
            }

            Assert.Equal("78d335fa323d01149c3dd6f0d48968cf", response.Id);
            Assert.Equal(new Uri("https://example.com/webhooks/answer"),
                response.Capabilities.Voice.Webhooks[Webhook.Type.AnswerUrl].Address);
            Assert.Equal(HttpMethod.Get, response.Capabilities.Voice.Webhooks[Webhook.Type.AnswerUrl].Method);
            Assert.Equal(new Uri("https://fallback.example.com/webhooks/answer"),
                response.Capabilities.Voice.Webhooks[Webhook.Type.FallbackAnswerUrl].Address);
            Assert.Equal(HttpMethod.Get, response.Capabilities.Voice.Webhooks[Webhook.Type.FallbackAnswerUrl].Method);
            Assert.Equal(new Uri("https://example.com/webhooks/event"),
                response.Capabilities.Voice.Webhooks[Webhook.Type.EventUrl].Address);
            Assert.Equal(HttpMethod.Post, response.Capabilities.Voice.Webhooks[Webhook.Type.EventUrl].Method);
            Assert.Equal("https://example.com/webhooks/inbound",
                response.Capabilities.Messages.Webhooks[Webhook.Type.InboundUrl].Address);
            Assert.Equal("POST", response.Capabilities.Messages.Webhooks[Webhook.Type.InboundUrl].Method);
            Assert.Equal("https://example.com/webhooks/status",
                response.Capabilities.Messages.Webhooks[Webhook.Type.StatusUrl].Address);
            Assert.Equal("POST", response.Capabilities.Messages.Webhooks[Webhook.Type.StatusUrl].Method);
            Assert.Equal("https://example.com/webhooks/event",
                response.Capabilities.Rtc.Webhooks[Webhook.Type.EventUrl].Address);
            Assert.Equal("POST", response.Capabilities.Rtc.Webhooks[Webhook.Type.EventUrl].Method);
            Assert.Equal("My Application", response.Name);
        }
    }
}