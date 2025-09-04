#region
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
#endregion

namespace Vonage.Test.Applications;

[Trait("Category", "Legacy")]
public class ApplicationTests : TestBase
{
    private const string PublicKey = "some public key";

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task CreateApplicationAsync(bool passCreds)
    {
        //ARRANGE
        var uri = $"{this.ApiUrl}/v2/applications";
        var expectedResponse = this.GetResponseJson();
        var expectedRequestContent = this.GetRequestJson();
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
        var messagesCapability = new Vonage.Applications.Capabilities.Messages(messagesWebhooks);
        var rtcWebhooks = new Dictionary<Webhook.Type, Webhook>
        {
            {Webhook.Type.EventUrl, new Webhook {Address = "https://example.com/webhooks/events", Method = "POST"}},
        };
        var rtcCapability = new Rtc(rtcWebhooks);
        var voiceWebhooks = new Dictionary<VoiceWebhookType, Vonage.Applications.Capabilities.Voice.VoiceWebhook>
        {
            {
                VoiceWebhookType.AnswerUrl,
                new Vonage.Applications.Capabilities.Voice.VoiceWebhook(new Uri("https://example.com/webhooks/answer"),
                    HttpMethod.Get)
            },
            {
                VoiceWebhookType.EventUrl,
                new Vonage.Applications.Capabilities.Voice.VoiceWebhook(new Uri("https://example.com/webhooks/events"),
                    HttpMethod.Post)
            },
            {
                VoiceWebhookType.FallbackAnswerUrl,
                new Vonage.Applications.Capabilities.Voice.VoiceWebhook(
                    new Uri("https://fallback.example.com/webhooks/answer"), HttpMethod.Get)
            },
        };
        var voiceCapability = new Vonage.Applications.Capabilities.Voice
        {
            Webhooks = voiceWebhooks,
        };
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
            response = await client.ApplicationClient.CreateApplicationAsync(request);
        }
        else
        {
            response = await client.ApplicationClient.CreateApplicationAsync(request, creds);
        }

        Assert.Equal("78d335fa323d01149c3dd6f0d48968cf", response.Id);
        Assert.Equal(
            new Vonage.Applications.Capabilities.Voice.VoiceWebhook(new Uri("https://example.com/webhooks/answer"),
                HttpMethod.Get), response.Capabilities.Voice.Webhooks[VoiceWebhookType.AnswerUrl]);
        Assert.Equal(
            new Vonage.Applications.Capabilities.Voice.VoiceWebhook(
                new Uri("https://fallback.example.com/webhooks/answer"), HttpMethod.Get),
            response.Capabilities.Voice.Webhooks[VoiceWebhookType.FallbackAnswerUrl]);
        Assert.Equal(
            new Vonage.Applications.Capabilities.Voice.VoiceWebhook(new Uri("https://example.com/webhooks/event"),
                HttpMethod.Post), response.Capabilities.Voice.Webhooks[VoiceWebhookType.EventUrl]);
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
    public async Task CreateApplicationAsyncWithMeetingsCapabilities()
    {
        var expectedResponseContent = this.GetResponseJson();
        var expectedRequestContent = this.GetRequestJson();
        this.Setup($"{this.ApiUrl}/v2/applications", expectedResponseContent, expectedRequestContent);
        var request = new CreateApplicationRequest
        {
            Capabilities = new ApplicationCapabilities
            {
                Meetings = new Meetings(new Dictionary<Webhook.Type, Webhook>
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
        var response = await client.ApplicationClient.CreateApplicationAsync(request);
        response.Capabilities.Meetings.Webhooks.Should().BeEquivalentTo(response.Capabilities.Meetings.Webhooks);
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
        var response = await client.ApplicationClient.CreateApplicationAsync(request);
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
        var answerWebhook = new Vonage.Applications.Capabilities.Voice.VoiceWebhook(
            new Uri("https://example.com/webhooks/answer"),
            HttpMethod.Get, 300, 2000);
        var eventWebhook = new Vonage.Applications.Capabilities.Voice.VoiceWebhook(
            new Uri("https://example.com/webhooks/events"),
            HttpMethod.Post, 500, 3000);
        var fallbackWebhook = new Vonage.Applications.Capabilities.Voice.VoiceWebhook(
            new Uri("https://fallback.example.com/webhooks/answer"), HttpMethod.Get, 800, 4000);
        var request = new CreateApplicationRequest
        {
            Capabilities = new ApplicationCapabilities
            {
                Voice = new Vonage.Applications.Capabilities.Voice
                {
                    Webhooks = new Dictionary<VoiceWebhookType, Vonage.Applications.Capabilities.Voice.VoiceWebhook>
                    {
                        {VoiceWebhookType.AnswerUrl, answerWebhook},
                        {VoiceWebhookType.EventUrl, eventWebhook},
                        {VoiceWebhookType.FallbackAnswerUrl, fallbackWebhook},
                    },
                },
            },
        };
        var credentials = Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);
        var client = this.BuildVonageClient(credentials);
        var response = await client.ApplicationClient.CreateApplicationAsync(request);
        response.Capabilities.Voice.Webhooks[VoiceWebhookType.AnswerUrl].Should().Be(answerWebhook);
        response.Capabilities.Voice.Webhooks[VoiceWebhookType.EventUrl].Should().Be(eventWebhook);
        response.Capabilities.Voice.Webhooks[VoiceWebhookType.FallbackAnswerUrl].Should().Be(fallbackWebhook);
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
    public async Task GetApplicationAsync(bool passCreds)
    {
        var id = "78d335fa323d01149c3dd6f0d48968cf";
        var expectedResponse = this.GetResponseJson();
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
        Assert.Equal(
            new Vonage.Applications.Capabilities.Voice.VoiceWebhook(new Uri("https://example.com/webhooks/answer"),
                HttpMethod.Get), application.Capabilities.Voice.Webhooks[VoiceWebhookType.AnswerUrl]);
        Assert.Equal(
            new Vonage.Applications.Capabilities.Voice.VoiceWebhook(
                new Uri("https://fallback.example.com/webhooks/answer"), HttpMethod.Get),
            application.Capabilities.Voice.Webhooks[VoiceWebhookType.FallbackAnswerUrl]);
        Assert.Equal(
            new Vonage.Applications.Capabilities.Voice.VoiceWebhook(new Uri("https://example.com/webhooks/event"),
                HttpMethod.Post), application.Capabilities.Voice.Webhooks[VoiceWebhookType.EventUrl]);
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
    public async Task ListApplicationsAsync(bool passCreds, bool passParameters)
    {
        var expectedResult = this.GetResponseJson();
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
        Assert.Equal(
            new Vonage.Applications.Capabilities.Voice.VoiceWebhook(new Uri("https://example.com/webhooks/answer"),
                HttpMethod.Get), application.Capabilities.Voice.Webhooks[VoiceWebhookType.AnswerUrl]);
        Assert.Equal(
            new Vonage.Applications.Capabilities.Voice.VoiceWebhook(
                new Uri("https://fallback.example.com/webhooks/answer"), HttpMethod.Get),
            application.Capabilities.Voice.Webhooks[VoiceWebhookType.FallbackAnswerUrl]);
        Assert.Equal(
            new Vonage.Applications.Capabilities.Voice.VoiceWebhook(new Uri("https://example.com/webhooks/event"),
                HttpMethod.Post), application.Capabilities.Voice.Webhooks[VoiceWebhookType.EventUrl]);
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
    public async Task UpdateApplicationAsync(bool passCredentials)
    {
        var id = "78d335fa323d01149c3dd6f0d48968cf";
        var uri = $"{this.ApiUrl}/v2/applications/{id}";
        var expectedResponse = this.GetResponseJson();
        var expectedRequestContent = this.GetRequestJson();
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
        var messagesCapability = new Vonage.Applications.Capabilities.Messages(messagesWebhooks);
        var rtcWebhooks = new Dictionary<Webhook.Type, Webhook>
        {
            {Webhook.Type.EventUrl, new Webhook {Address = "https://example.com/webhooks/events", Method = "POST"}},
        };
        var rtcCapability = new Rtc(rtcWebhooks);
        var voiceWebhooks = new Dictionary<VoiceWebhookType, Vonage.Applications.Capabilities.Voice.VoiceWebhook>
        {
            {
                VoiceWebhookType.AnswerUrl,
                new Vonage.Applications.Capabilities.Voice.VoiceWebhook(new Uri("https://example.com/webhooks/answer"),
                    HttpMethod.Get)
            },
            {
                VoiceWebhookType.EventUrl,
                new Vonage.Applications.Capabilities.Voice.VoiceWebhook(new Uri("https://example.com/webhooks/events"),
                    HttpMethod.Post)
            },
            {
                VoiceWebhookType.FallbackAnswerUrl,
                new Vonage.Applications.Capabilities.Voice.VoiceWebhook(
                    new Uri("https://fallback.example.com/webhooks/answer"), HttpMethod.Get)
            },
        };
        var voiceCapability = new Vonage.Applications.Capabilities.Voice
        {
            Webhooks = voiceWebhooks,
        };
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
        Assert.Equal(
            new Vonage.Applications.Capabilities.Voice.VoiceWebhook(new Uri("https://example.com/webhooks/answer"),
                HttpMethod.Get), response.Capabilities.Voice.Webhooks[VoiceWebhookType.AnswerUrl]);
        Assert.Equal(
            new Vonage.Applications.Capabilities.Voice.VoiceWebhook(
                new Uri("https://fallback.example.com/webhooks/answer"), HttpMethod.Get),
            response.Capabilities.Voice.Webhooks[VoiceWebhookType.FallbackAnswerUrl]);
        Assert.Equal(
            new Vonage.Applications.Capabilities.Voice.VoiceWebhook(new Uri("https://example.com/webhooks/event"),
                HttpMethod.Post), response.Capabilities.Voice.Webhooks[VoiceWebhookType.EventUrl]);
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