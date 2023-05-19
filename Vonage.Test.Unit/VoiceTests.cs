﻿using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Web;
using AutoFixture;
using FluentAssertions;
using Vonage.Common;
using Vonage.Common.Exceptions;
using Vonage.Request;
using Vonage.Voice;
using Vonage.Voice.Nccos;
using Vonage.Voice.Nccos.Endpoints;
using Xunit;

namespace Vonage.Test.Unit
{
    public class VoiceTests : TestBase
    {
        private const string BaseUri = "https://api.nexmo.com/v1/calls";
        private readonly Fixture fixture;
        private readonly VonageClient client;

        public VoiceTests()
        {
            this.fixture = new Fixture();
            this.client = new VonageClient(Credentials.FromAppIdAndPrivateKey(this.AppId, this.PrivateKey));
        }

        [Theory]
        [InlineData(45)]
        [InlineData(120)]
        public void AdvancedMachineDetectionProperties_ShouldReturnInstance_GivenBeepTimeoutIsValid(int value)
        {
            var properties = new AdvancedMachineDetectionProperties(
                AdvancedMachineDetectionProperties.MachineDetectionBehavior.Continue,
                AdvancedMachineDetectionProperties.MachineDetectionMode.Detect,
                value);
            properties.Behavior.Should()
                .Be(AdvancedMachineDetectionProperties.MachineDetectionBehavior.Continue);
            properties.Mode.Should().Be(AdvancedMachineDetectionProperties.MachineDetectionMode.Detect);
            properties.BeepTimeout.Should().Be(value);
        }

        [Theory]
        [InlineData(44)]
        [InlineData(121)]
        public void AdvancedMachineDetectionProperties_ShouldThrowException_GivenBeepTimeoutIsInvalid(int value)
        {
            Action act = () => _ = new AdvancedMachineDetectionProperties(
                AdvancedMachineDetectionProperties.MachineDetectionBehavior.Continue,
                AdvancedMachineDetectionProperties.MachineDetectionMode.Detect,
                value);
            act.Should().ThrowExactly<VonageException>()
                .WithMessage("Beep Timeout has a minimal value of 45, and a maximal value of 120.");
        }

        [Fact]
        public void CreateCall()
        {
            var expectedResponse = this.GetResponseJson();
            var expectedRequestContent = this.GetRequestJson();
            this.Setup(BaseUri, expectedResponse, expectedRequestContent);
            var request = new CallCommand
            {
                To = new Endpoint[]
                {
                    new PhoneEndpoint
                    {
                        Number = "14155550100",
                        DtmfAnswer = "p*123#",
                    },
                },
                From = new PhoneEndpoint
                {
                    Number = "14155550100",
                    DtmfAnswer = "p*123#",
                },
                Ncco = new Ncco(new TalkAction {Text = "Hello World"}),
                AnswerUrl = new[] {"https://example.com/answer"},
                AnswerMethod = "GET",
                EventUrl = new[] {"https://example.com/event"},
                EventMethod = "POST",
                MachineDetection = "continue",
                LengthTimer = 1,
                RingingTimer = 1,
                AdvancedMachineDetection = new AdvancedMachineDetectionProperties(
                    AdvancedMachineDetectionProperties.MachineDetectionBehavior.Continue,
                    AdvancedMachineDetectionProperties.MachineDetectionMode.Detect, 45),
            };
            var response = this.client.VoiceClient.CreateCall(request);
            Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", response.Uuid);
            Assert.Equal("CON-f972836a-550f-45fa-956c-12a2ab5b7d22", response.ConversationUuid);
            Assert.Equal("outbound", response.Direction);
            Assert.Equal("started", response.Status);
        }

        [Fact]
        public async Task CreateCallAsync()
        {
            var expectedResponse = this.GetResponseJson(nameof(this.CreateCall));
            var expectedRequestContent = this.GetRequestJson(nameof(this.CreateCall));
            this.Setup(BaseUri, expectedResponse, expectedRequestContent);
            var request = new CallCommand
            {
                To = new Endpoint[]
                {
                    new PhoneEndpoint
                    {
                        Number = "14155550100",
                        DtmfAnswer = "p*123#",
                    },
                },
                From = new PhoneEndpoint
                {
                    Number = "14155550100",
                    DtmfAnswer = "p*123#",
                },
                Ncco = new Ncco(new TalkAction {Text = "Hello World"}),
                AnswerUrl = new[] {"https://example.com/answer"},
                AnswerMethod = "GET",
                EventUrl = new[] {"https://example.com/event"},
                EventMethod = "POST",
                MachineDetection = "continue",
                LengthTimer = 1,
                RingingTimer = 1,
                AdvancedMachineDetection = new AdvancedMachineDetectionProperties(
                    AdvancedMachineDetectionProperties.MachineDetectionBehavior.Continue,
                    AdvancedMachineDetectionProperties.MachineDetectionMode.Detect, 45),
            };
            var response = await this.client.VoiceClient.CreateCallAsync(request);
            Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", response.Uuid);
            Assert.Equal("CON-f972836a-550f-45fa-956c-12a2ab5b7d22", response.ConversationUuid);
            Assert.Equal("outbound", response.Direction);
            Assert.Equal("started", response.Status);
        }

        [Fact]
        public async Task CreateCallAsyncWithCredentials()
        {
            var expectedResponse = this.GetResponseJson(nameof(this.CreateCall));
            var expectedRequestContent = this.GetRequestJson(nameof(this.CreateCall));
            this.Setup(BaseUri, expectedResponse, expectedRequestContent);
            var request = new CallCommand
            {
                To = new Endpoint[]
                {
                    new PhoneEndpoint
                    {
                        Number = "14155550100",
                        DtmfAnswer = "p*123#",
                    },
                },
                From = new PhoneEndpoint
                {
                    Number = "14155550100",
                    DtmfAnswer = "p*123#",
                },
                Ncco = new Ncco(new TalkAction {Text = "Hello World"}),
                AnswerUrl = new[] {"https://example.com/answer"},
                AnswerMethod = "GET",
                EventUrl = new[] {"https://example.com/event"},
                EventMethod = "POST",
                MachineDetection = "continue",
                LengthTimer = 1,
                RingingTimer = 1,
                AdvancedMachineDetection = new AdvancedMachineDetectionProperties(
                    AdvancedMachineDetectionProperties.MachineDetectionBehavior.Continue,
                    AdvancedMachineDetectionProperties.MachineDetectionMode.Detect, 45),
            };
            var creds = Credentials.FromAppIdAndPrivateKey(this.AppId, this.PrivateKey);
            var response = await this.client.VoiceClient.CreateCallAsync(request, creds);
            Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", response.Uuid);
            Assert.Equal("CON-f972836a-550f-45fa-956c-12a2ab5b7d22", response.ConversationUuid);
            Assert.Equal("outbound", response.Direction);
            Assert.Equal("started", response.Status);
        }

        [Fact]
        public async Task CreateCallAsyncWithWrongCredsThrowsAuthException()
        {
            var expectedResponse = @"{
              ""uuid"": ""63f61863-4a51-4f6b-86e1-46edebcf9356"",
              ""status"": ""started"",
              ""direction"": ""outbound"",
              ""conversation_uuid"": ""CON-f972836a-550f-45fa-956c-12a2ab5b7d22""
            }";
            var expectedRequestContent =
                @"{""to"":[{""number"":""14155550100"",""type"":""phone""}],""from"":{""number"":""14155550100"",""type"":""phone""},""ncco"":[{""text"":""Hello World"",""action"":""talk""}]}";
            this.Setup(BaseUri, expectedResponse, expectedRequestContent);
            var toEndpoint = new PhoneEndpoint {Number = "14155550100"};
            var exception = await Assert.ThrowsAsync<VonageAuthenticationException>(async () =>
                await this.BuildClientWithWrongCredentials().VoiceClient.CreateCallAsync(
                    toEndpoint, "14155550100", new Ncco(new TalkAction {Text = "Hello World"})));
            Assert.NotNull(exception);
            Assert.Equal("AppId or Private Key Path missing.", exception.Message);
        }

        [Fact]
        public void CreateCallWithCredentials()
        {
            var expectedResponse = this.GetResponseJson(nameof(this.CreateCall));
            var expectedRequestContent = this.GetRequestJson(nameof(this.CreateCall));
            this.Setup(BaseUri, expectedResponse, expectedRequestContent);
            var request = new CallCommand
            {
                To = new Endpoint[]
                {
                    new PhoneEndpoint
                    {
                        Number = "14155550100",
                        DtmfAnswer = "p*123#",
                    },
                },
                From = new PhoneEndpoint
                {
                    Number = "14155550100",
                    DtmfAnswer = "p*123#",
                },
                Ncco = new Ncco(new TalkAction {Text = "Hello World"}),
                AnswerUrl = new[] {"https://example.com/answer"},
                AnswerMethod = "GET",
                EventUrl = new[] {"https://example.com/event"},
                EventMethod = "POST",
                MachineDetection = "continue",
                AdvancedMachineDetection = new AdvancedMachineDetectionProperties(
                    AdvancedMachineDetectionProperties.MachineDetectionBehavior.Continue,
                    AdvancedMachineDetectionProperties.MachineDetectionMode.Detect, 45),
                LengthTimer = 1,
                RingingTimer = 1,
            };
            var response = this.client.VoiceClient.CreateCall(request,
                Credentials.FromAppIdAndPrivateKey(this.AppId, this.PrivateKey));
            Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", response.Uuid);
            Assert.Equal("CON-f972836a-550f-45fa-956c-12a2ab5b7d22", response.ConversationUuid);
            Assert.Equal("outbound", response.Direction);
            Assert.Equal("started", response.Status);
        }

        [Fact]
        public void CreateCallWithEndpointAndNcco()
        {
            var expectedResponse = this.GetResponseJson();
            var expectedRequestContent = this.GetRequestJson();
            this.Setup(BaseUri, expectedResponse, expectedRequestContent);
            var toEndpoint = new PhoneEndpoint {Number = "14155550100"};
            var response = this.client.VoiceClient.CreateCall(
                toEndpoint, "14155550100", new Ncco(new TalkAction {Text = "Hello World"}));
            Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", response.Uuid);
            Assert.Equal("CON-f972836a-550f-45fa-956c-12a2ab5b7d22", response.ConversationUuid);
            Assert.Equal("outbound", response.Direction);
            Assert.Equal("started", response.Status);
        }

        [Fact]
        public async Task CreateCallWithEndpointAndNccoAsync()
        {
            var expectedResponse = this.GetResponseJson(nameof(this.CreateCallWithEndpointAndNcco));
            var expectedRequestContent = this.GetRequestJson(nameof(this.CreateCallWithEndpointAndNcco));
            this.Setup(BaseUri, expectedResponse, expectedRequestContent);
            var toEndpoint = new PhoneEndpoint {Number = "14155550100"};
            var response = await this.client.VoiceClient.CreateCallAsync(
                toEndpoint, "14155550100", new Ncco(new TalkAction {Text = "Hello World"}));
            Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", response.Uuid);
            Assert.Equal("CON-f972836a-550f-45fa-956c-12a2ab5b7d22", response.ConversationUuid);
            Assert.Equal("outbound", response.Direction);
            Assert.Equal("started", response.Status);
        }

        [Fact]
        public void CreateCallWithStringParameters()
        {
            var expectedResponse = @"{
              ""uuid"": ""63f61863-4a51-4f6b-86e1-46edebcf9356"",
              ""status"": ""started"",
              ""direction"": ""outbound"",
              ""conversation_uuid"": ""CON-f972836a-550f-45fa-956c-12a2ab5b7d22""
            }";
            var expectedRequestContent =
                @"{""to"":[{""number"":""14155550100"",""type"":""phone""}],""from"":{""number"":""14155550100"",""type"":""phone""},""ncco"":[{""action"":""talk"",""text"":""Hello World""}]}";
            this.Setup(BaseUri, expectedResponse, expectedRequestContent);
            var response = this.client.VoiceClient.CreateCall("14155550100", "14155550100",
                new Ncco(new TalkAction {Text = "Hello World"}));
            Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", response.Uuid);
            Assert.Equal("CON-f972836a-550f-45fa-956c-12a2ab5b7d22", response.ConversationUuid);
            Assert.Equal("outbound", response.Direction);
            Assert.Equal("started", response.Status);
        }

        [Fact]
        public async Task CreateCallWithStringParametersAsync()
        {
            var expectedResponse = @"{
              ""uuid"": ""63f61863-4a51-4f6b-86e1-46edebcf9356"",
              ""status"": ""started"",
              ""direction"": ""outbound"",
              ""conversation_uuid"": ""CON-f972836a-550f-45fa-956c-12a2ab5b7d22""
            }";
            var expectedRequestContent =
                @"{""to"":[{""number"":""14155550100"",""type"":""phone""}],""from"":{""number"":""14155550100"",""type"":""phone""},""ncco"":[{""action"":""talk"",""text"":""Hello World""}]}";
            this.Setup(BaseUri, expectedResponse, expectedRequestContent);
            Credentials.FromAppIdAndPrivateKey(this.AppId, this.PrivateKey);
            var response = await this.client.VoiceClient.CreateCallAsync("14155550100", "14155550100",
                new Ncco(new TalkAction {Text = "Hello World"}));
            Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", response.Uuid);
            Assert.Equal("CON-f972836a-550f-45fa-956c-12a2ab5b7d22", response.ConversationUuid);
            Assert.Equal("outbound", response.Direction);
            Assert.Equal("started", response.Status);
        }

        [Fact]
        public void CreateCallWithUnicodeCharacters()
        {
            var expectedResponse = @"{
              ""uuid"": ""63f61863-4a51-4f6b-86e1-46edebcf9356"",
              ""status"": ""started"",
              ""direction"": ""outbound"",
              ""conversation_uuid"": ""CON-f972836a-550f-45fa-956c-12a2ab5b7d22""
            }";
            var expectedRequestContent =
                @"{""to"":[{""number"":""14155550100"",""dtmfAnswer"":""p*123#"",""type"":""phone""}],""from"":{""number"":""14155550100"",""dtmfAnswer"":""p*123#"",""type"":""phone""},""ncco"":[{""action"":""talk"",""text"":""בדיקה בדיקה בדיקה""}],""answer_url"":[""https://example.com/answer""],""answer_method"":""GET"",""event_url"":[""https://example.com/event""],""event_method"":""POST"",""machine_detection"":""continue"",""length_timer"":1,""ringing_timer"":1}";
            this.Setup(BaseUri, expectedResponse, expectedRequestContent);
            var request = new CallCommand
            {
                To = new Endpoint[]
                {
                    new PhoneEndpoint
                    {
                        Number = "14155550100",
                        DtmfAnswer = "p*123#",
                    },
                },
                From = new PhoneEndpoint
                {
                    Number = "14155550100",
                    DtmfAnswer = "p*123#",
                },
                Ncco = new Ncco(new TalkAction {Text = "בדיקה בדיקה בדיקה"}),
                AnswerUrl = new[] {"https://example.com/answer"},
                AnswerMethod = "GET",
                EventUrl = new[] {"https://example.com/event"},
                EventMethod = "POST",
                MachineDetection = "continue",
                LengthTimer = 1,
                RingingTimer = 1,
            };
            Credentials.FromAppIdAndPrivateKey(this.AppId, this.PrivateKey);
            var response = this.client.VoiceClient.CreateCall(request);
            Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", response.Uuid);
            Assert.Equal("CON-f972836a-550f-45fa-956c-12a2ab5b7d22", response.ConversationUuid);
            Assert.Equal("outbound", response.Direction);
            Assert.Equal("started", response.Status);
        }

        [Fact]
        public void CreateCallWithWrongCredsThrowsAuthException()
        {
            var expectedResponse = @"{
              ""uuid"": ""63f61863-4a51-4f6b-86e1-46edebcf9356"",
              ""status"": ""started"",
              ""direction"": ""outbound"",
              ""conversation_uuid"": ""CON-f972836a-550f-45fa-956c-12a2ab5b7d22""
            }";
            var expectedRequestContent =
                @"{""to"":[{""number"":""14155550100"",""type"":""phone""}],""from"":{""number"":""14155550100"",""type"":""phone""},""ncco"":[{""text"":""Hello World"",""action"":""talk""}]}";
            this.Setup(BaseUri, expectedResponse, expectedRequestContent);
            var toEndpoint = new PhoneEndpoint {Number = "14155550100"};
            var exception = Assert.Throws<VonageAuthenticationException>(() => this.BuildClientWithWrongCredentials()
                .VoiceClient.CreateCall(
                    toEndpoint, "14155550100", new Ncco(new TalkAction {Text = "Hello World"})));
            Assert.NotNull(exception);
            Assert.Equal("AppId or Private Key Path missing.", exception.Message);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void StopStream(bool passCreds)
        {
            var uuid = "63f61863-4a51-4f6b-86e1-46edebcf9356";
            var expectedUri = $"{BaseUri}/{uuid}/stream";
            var expectedResponse = @"{
                  ""message"": ""Stream stopped"",
                  ""uuid"": ""63f61863-4a51-4f6b-86e1-46edebcf9356""
                }";
            this.Setup(expectedUri, expectedResponse, "{}");
            var creds = Credentials.FromAppIdAndPrivateKey(this.AppId, this.PrivateKey);
            CallCommandResponse response;
            if (passCreds)
            {
                response = this.client.VoiceClient.StopStream(uuid, creds);
            }
            else
            {
                response = this.client.VoiceClient.StopStream(uuid);
            }

            Assert.Equal("Stream stopped", response.Message);
            Assert.Equal(uuid, response.Uuid);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task StopStreamAsync(bool passCreds)
        {
            var uuid = "63f61863-4a51-4f6b-86e1-46edebcf9356";
            var expectedUri = $"{BaseUri}/{uuid}/stream";
            var expectedResponse = @"{
                  ""message"": ""Stream stopped"",
                  ""uuid"": ""63f61863-4a51-4f6b-86e1-46edebcf9356""
                }";
            this.Setup(expectedUri, expectedResponse, "{}");
            var creds = Credentials.FromAppIdAndPrivateKey(this.AppId, this.PrivateKey);
            CallCommandResponse response;
            if (passCreds)
            {
                response = await this.client.VoiceClient.StopStreamAsync(uuid, creds);
            }
            else
            {
                response = await this.client.VoiceClient.StopStreamAsync(uuid);
            }

            Assert.Equal("Stream stopped", response.Message);
            Assert.Equal(uuid, response.Uuid);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void StopTalk(bool passCreds)
        {
            var uuid = "63f61863-4a51-4f6b-86e1-46edebcf9356";
            var expectedUri = $"{BaseUri}/{uuid}/stream";
            var expectedResponse = @"{
                  ""message"": ""Talk stopped"",
                  ""uuid"": ""63f61863-4a51-4f6b-86e1-46edebcf9356""
                }";
            this.Setup(expectedUri, expectedResponse, "{}");
            var creds = Credentials.FromAppIdAndPrivateKey(this.AppId, this.PrivateKey);
            CallCommandResponse response;
            if (passCreds)
            {
                response = this.client.VoiceClient.StopStream(uuid, creds);
            }
            else
            {
                response = this.client.VoiceClient.StopStream(uuid);
            }

            Assert.Equal("Talk stopped", response.Message);
            Assert.Equal(uuid, response.Uuid);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task StopTalkAsync(bool passCreds)
        {
            var uuid = "63f61863-4a51-4f6b-86e1-46edebcf9356";
            var expectedUri = $"{BaseUri}/{uuid}/stream";
            var expectedResponse = @"{
                  ""message"": ""Talk stopped"",
                  ""uuid"": ""63f61863-4a51-4f6b-86e1-46edebcf9356""
                }";
            this.Setup(expectedUri, expectedResponse, "{}");
            var creds = Credentials.FromAppIdAndPrivateKey(this.AppId, this.PrivateKey);
            CallCommandResponse response;
            if (passCreds)
            {
                response = await this.client.VoiceClient.StopStreamAsync(uuid, creds);
            }
            else
            {
                response = await this.client.VoiceClient.StopStreamAsync(uuid);
            }

            Assert.Equal("Talk stopped", response.Message);
            Assert.Equal(uuid, response.Uuid);
        }

        [Fact]
        public void TestCreateCallWithRandomFromNumber()
        {
            var expectedResponse = @"{
              ""uuid"": ""63f61863-4a51-4f6b-86e1-46edebcf9356"",
              ""status"": ""started"",
              ""direction"": ""outbound"",
              ""conversation_uuid"": ""CON-f972836a-550f-45fa-956c-12a2ab5b7d22""
            }";
            var expectedRequestContent = this.GetExpectedJson();
            this.Setup(BaseUri, expectedResponse, expectedRequestContent);
            var request = new CallCommand
            {
                To = new Endpoint[]
                {
                    new PhoneEndpoint
                    {
                        Number = "14155550100",
                        DtmfAnswer = "p*123#",
                    },
                },
                RandomFromNumber = true,
                Ncco = new Ncco(new TalkAction {Text = "Hello World"}),
                AnswerUrl = new[] {"https://example.com/answer"},
                AnswerMethod = "GET",
                EventUrl = new[] {"https://example.com/event"},
                EventMethod = "POST",
                MachineDetection = "continue",
                LengthTimer = 1,
                RingingTimer = 1,
            };
            Credentials.FromAppIdAndPrivateKey(this.AppId, this.PrivateKey);
            var response = this.client.VoiceClient.CreateCall(request);
            Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", response.Uuid);
            Assert.Equal("CON-f972836a-550f-45fa-956c-12a2ab5b7d22", response.ConversationUuid);
            Assert.Equal("outbound", response.Direction);
            Assert.Equal("started", response.Status);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void TestGetRecordings(bool passCreds)
        {
            var expectedUri = this.fixture.Create<Uri>().ToString();
            var creds = Credentials.FromAppIdAndPrivateKey(this.AppId, this.PrivateKey);
            var expectedResponse = new byte[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
            this.Setup(expectedUri, expectedResponse);
            GetRecordingResponse response;
            if (passCreds)
            {
                response = this.client.VoiceClient.GetRecording(expectedUri, creds);
            }
            else
            {
                response = this.client.VoiceClient.GetRecording(expectedUri);
            }

            Assert.Equal(expectedResponse, response.ResultStream);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task TestGetRecordingsAsync(bool passCreds)
        {
            var expectedUri = this.fixture.Create<Uri>().ToString();
            var creds = Credentials.FromAppIdAndPrivateKey(this.AppId, this.PrivateKey);
            var expectedResponse = new byte[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
            this.Setup(expectedUri, expectedResponse);
            GetRecordingResponse response;
            if (passCreds)
            {
                response = await this.client.VoiceClient.GetRecordingAsync(expectedUri, creds);
            }
            else
            {
                response = await this.client.VoiceClient.GetRecordingAsync(expectedUri);
            }

            Assert.Equal(expectedResponse, response.ResultStream);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void TestGetSpecificCall(bool passCreds)
        {
            var uuid = "63f61863-4a51-4f6b-86e1-46edebcf9356";
            var expectedResponse = @"{
                        ""_links"": {
                          ""self"": {
                            ""href"": ""/calls/63f61863-4a51-4f6b-86e1-46edebcf9356""
                          }
                        },
                        ""uuid"": ""63f61863-4a51-4f6b-86e1-46edebcf9356"",
                        ""conversation_uuid"": ""CON-f972836a-550f-45fa-956c-12a2ab5b7d22"",
                        ""to"":
                          {
                            ""type"": ""phone"",
                            ""number"": ""447700900000""
                          }
                        ,
                        ""from"":
                          {
                            ""type"": ""phone"",
                            ""number"": ""447700900001""
                          }
                        ,
                        ""status"": ""started"",
                        ""direction"": ""outbound"",
                        ""rate"": ""0.39"",
                        ""price"": ""23.40"",
                        ""duration"": ""60"",
                        ""start_time"": ""2020-01-01 12:00:00"",
                        ""end_time"": ""2020-01-01 12:00:00"",
                        ""network"": ""65512""
                      }";
            var expectedUri = $"{BaseUri}/{uuid}";
            this.Setup(expectedUri, expectedResponse);
            var creds = Credentials.FromAppIdAndPrivateKey(this.AppId, this.PrivateKey);
            CallRecord callRecord;
            if (passCreds)
            {
                callRecord = this.client.VoiceClient.GetCall(uuid, creds);
            }
            else
            {
                callRecord = this.client.VoiceClient.GetCall(uuid);
            }

            Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", callRecord.Uuid);
            Assert.Equal("CON-f972836a-550f-45fa-956c-12a2ab5b7d22", callRecord.ConversationUuid);
            Assert.Equal("447700900000", callRecord.To.Number);
            Assert.Equal("phone", callRecord.To.Type);
            Assert.Equal("phone", callRecord.From.Type);
            Assert.Equal("447700900001", callRecord.From.Number);
            Assert.Equal("started", callRecord.Status);
            Assert.Equal("outbound", callRecord.Direction);
            Assert.Equal("0.39", callRecord.Rate);
            Assert.Equal("23.40", callRecord.Price);
            Assert.Equal("60", callRecord.Duration);
            Assert.Equal(DateTime.ParseExact("2020-01-01T12:00:00.000Z", "yyyy-MM-dd'T'HH:mm:ss.fff'Z'",
                CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal |
                                              DateTimeStyles.AdjustToUniversal), callRecord.StartTime);
            Assert.Equal(DateTime.ParseExact("2020-01-01T12:00:00.000Z", "yyyy-MM-dd'T'HH:mm:ss.fff'Z'",
                CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal |
                                              DateTimeStyles.AdjustToUniversal), callRecord.EndTime);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task TestGetSpecificCallAsync(bool passCreds)
        {
            var uuid = "63f61863-4a51-4f6b-86e1-46edebcf9356";
            var expectedResponse = @"{
                        ""_links"": {
                          ""self"": {
                            ""href"": ""/calls/63f61863-4a51-4f6b-86e1-46edebcf9356""
                          }
                        },
                        ""uuid"": ""63f61863-4a51-4f6b-86e1-46edebcf9356"",
                        ""conversation_uuid"": ""CON-f972836a-550f-45fa-956c-12a2ab5b7d22"",
                        ""to"":
                          {
                            ""type"": ""phone"",
                            ""number"": ""447700900000""
                          }
                        ,
                        ""from"":
                          {
                            ""type"": ""phone"",
                            ""number"": ""447700900001""
                          }
                        ,
                        ""status"": ""started"",
                        ""direction"": ""outbound"",
                        ""rate"": ""0.39"",
                        ""price"": ""23.40"",
                        ""duration"": ""60"",
                        ""start_time"": ""2020-01-01 12:00:00"",
                        ""end_time"": ""2020-01-01 12:00:00"",
                        ""network"": ""65512""
                      }";
            var expectedUri = $"{BaseUri}/{uuid}";
            this.Setup(expectedUri, expectedResponse);
            var creds = Credentials.FromAppIdAndPrivateKey(this.AppId, this.PrivateKey);
            CallRecord callRecord;
            if (passCreds)
            {
                callRecord = await this.client.VoiceClient.GetCallAsync(uuid, creds);
            }
            else
            {
                callRecord = await this.client.VoiceClient.GetCallAsync(uuid);
            }

            Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", callRecord.Uuid);
            Assert.Equal("CON-f972836a-550f-45fa-956c-12a2ab5b7d22", callRecord.ConversationUuid);
            Assert.Equal("447700900000", callRecord.To.Number);
            Assert.Equal("phone", callRecord.To.Type);
            Assert.Equal("phone", callRecord.From.Type);
            Assert.Equal("447700900001", callRecord.From.Number);
            Assert.Equal("started", callRecord.Status);
            Assert.Equal("outbound", callRecord.Direction);
            Assert.Equal("0.39", callRecord.Rate);
            Assert.Equal("23.40", callRecord.Price);
            Assert.Equal("60", callRecord.Duration);
            Assert.Equal(DateTime.ParseExact("2020-01-01T12:00:00.000Z", "yyyy-MM-dd'T'HH:mm:ss.fff'Z'",
                CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal |
                                              DateTimeStyles.AdjustToUniversal), callRecord.StartTime);
            Assert.Equal(DateTime.ParseExact("2020-01-01T12:00:00.000Z", "yyyy-MM-dd'T'HH:mm:ss.fff'Z'",
                CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal |
                                              DateTimeStyles.AdjustToUniversal), callRecord.EndTime);
        }

        [Theory]
        [InlineData(true, true)]
        [InlineData(false, false)]
        public void TestListCalls(bool passCreds, bool kitchenSink)
        {
            var expectedResponse = @"{
                  ""count"": 100,
                  ""page_size"": 10,
                  ""record_index"": 0,
                  ""_links"": {
                                ""self"": {
                                    ""href"": ""/calls?page_size=10&record_index=20&order=asc""
                                }
                            },
                  ""_embedded"": {
                                ""calls"": [
                                  {
                        ""_links"": {
                          ""self"": {
                            ""href"": ""/calls/63f61863-4a51-4f6b-86e1-46edebcf9356""
                          }
                        },
                        ""uuid"": ""63f61863-4a51-4f6b-86e1-46edebcf9356"",
                        ""conversation_uuid"": ""CON-f972836a-550f-45fa-956c-12a2ab5b7d22"",
                        ""to"":
                          {
                            ""type"": ""phone"",
                            ""number"": ""447700900000""
                          }
                        ,
                        ""from"": 
                          {
                            ""type"": ""phone"",
                            ""number"": ""447700900001""
                          }
                        ,
                        ""status"": ""started"",
                        ""direction"": ""outbound"",
                        ""rate"": ""0.39"",
                        ""price"": ""23.40"",
                        ""duration"": ""60"",
                        ""start_time"": ""2020-01-01 12:00:00"",
                        ""end_time"": ""2020-01-01 12:00:00"",
                        ""network"": ""65512""
                      }
                    ]
                  }
                }";
            CallSearchFilter filter;
            string expectedUri;
            if (kitchenSink)
            {
                expectedUri =
                    $"{BaseUri}?status=started&date_start={HttpUtility.UrlEncode("2016-11-14T07:45:14Z").ToUpper()}&date_end={HttpUtility.UrlEncode("2016-11-14T07:45:14Z").ToUpper()}&page_size=10&record_index=0&order=asc&conversation_uuid=CON-f972836a-550f-45fa-956c-12a2ab5b7d22&";
                filter = new CallSearchFilter
                {
                    ConversationUuid = "CON-f972836a-550f-45fa-956c-12a2ab5b7d22",
                    DateStart = DateTime.Parse("2016-11-14T07:45:14"),
                    DateEnd = DateTime.Parse("2016-11-14T07:45:14"),
                    PageSize = 10,
                    RecordIndex = 0,
                    Order = "asc",
                    Status = "started",
                };
            }
            else
            {
                expectedUri = $"{this.ApiUrl}/v1/calls";
                filter = new CallSearchFilter();
            }

            this.Setup(expectedUri, expectedResponse);
            var creds = Credentials.FromAppIdAndPrivateKey(this.AppId, this.PrivateKey);
            PageResponse<CallList> callList;
            if (passCreds)
            {
                callList = this.client.VoiceClient.GetCalls(filter, creds);
            }
            else
            {
                callList = this.client.VoiceClient.GetCalls(filter);
            }

            var callRecord = callList.Embedded.Calls[0];
            Assert.True(100 == callList.Count);
            Assert.True(10 == callList.PageSize);
            Assert.True(0 == callList.PageIndex);
            Assert.Equal("/calls?page_size=10&record_index=20&order=asc", callList.Links.Self.Href);
            Assert.Equal("/calls/63f61863-4a51-4f6b-86e1-46edebcf9356", callRecord.Links.Self.Href);
            Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", callRecord.Uuid);
            Assert.Equal("CON-f972836a-550f-45fa-956c-12a2ab5b7d22", callRecord.ConversationUuid);
            Assert.Equal("447700900000", callRecord.To.Number);
            Assert.Equal("phone", callRecord.To.Type);
            Assert.Equal("phone", callRecord.From.Type);
            Assert.Equal("447700900001", callRecord.From.Number);
            Assert.Equal("started", callRecord.Status);
            Assert.Equal("outbound", callRecord.Direction);
            Assert.Equal("0.39", callRecord.Rate);
            Assert.Equal("23.40", callRecord.Price);
            Assert.Equal("60", callRecord.Duration);
            Assert.Equal(DateTime.ParseExact("2020-01-01T12:00:00.000Z", "yyyy-MM-dd'T'HH:mm:ss.fff'Z'",
                CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal |
                                              DateTimeStyles.AdjustToUniversal), callRecord.StartTime);
            Assert.Equal(DateTime.ParseExact("2020-01-01T12:00:00.000Z", "yyyy-MM-dd'T'HH:mm:ss.fff'Z'",
                CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal |
                                              DateTimeStyles.AdjustToUniversal), callRecord.EndTime);
        }

        [Theory]
        [InlineData(true, true)]
        [InlineData(false, false)]
        public async Task TestListCallsAsync(bool passCreds, bool kitchenSink)
        {
            var expectedResponse = @"{
                  ""count"": 100,
                  ""page_size"": 10,
                  ""record_index"": 0,
                  ""_links"": {
                                ""self"": {
                                    ""href"": ""/calls?page_size=10&record_index=20&order=asc""
                                }
                            },
                  ""_embedded"": {
                                ""calls"": [
                                  {
                        ""_links"": {
                          ""self"": {
                            ""href"": ""/calls/63f61863-4a51-4f6b-86e1-46edebcf9356""
                          }
                        },
                        ""uuid"": ""63f61863-4a51-4f6b-86e1-46edebcf9356"",
                        ""conversation_uuid"": ""CON-f972836a-550f-45fa-956c-12a2ab5b7d22"",
                        ""to"":
                          {
                            ""type"": ""phone"",
                            ""number"": ""447700900000""
                          }
                        ,
                        ""from"": 
                          {
                            ""type"": ""phone"",
                            ""number"": ""447700900001""
                          }
                        ,
                        ""status"": ""started"",
                        ""direction"": ""outbound"",
                        ""rate"": ""0.39"",
                        ""price"": ""23.40"",
                        ""duration"": ""60"",
                        ""start_time"": ""2020-01-01 12:00:00"",
                        ""end_time"": ""2020-01-01 12:00:00"",
                        ""network"": ""65512""
                      }
                    ]
                  }
                }";
            CallSearchFilter filter;
            string expectedUri;
            if (kitchenSink)
            {
                expectedUri =
                    $"{BaseUri}?status=started&date_start={HttpUtility.UrlEncode("2016-11-14T07:45:14Z").ToUpper()}&date_end={HttpUtility.UrlEncode("2016-11-14T07:45:14Z").ToUpper()}&page_size=10&record_index=0&order=asc&conversation_uuid=CON-f972836a-550f-45fa-956c-12a2ab5b7d22&";
                filter = new CallSearchFilter
                {
                    ConversationUuid = "CON-f972836a-550f-45fa-956c-12a2ab5b7d22",
                    DateStart = DateTime.Parse("2016-11-14T07:45:14"),
                    DateEnd = DateTime.Parse("2016-11-14T07:45:14"),
                    PageSize = 10,
                    RecordIndex = 0,
                    Order = "asc",
                    Status = "started",
                };
            }
            else
            {
                expectedUri = $"{this.ApiUrl}/v1/calls";
                filter = new CallSearchFilter();
            }

            this.Setup(expectedUri, expectedResponse);
            var creds = Credentials.FromAppIdAndPrivateKey(this.AppId, this.PrivateKey);
            PageResponse<CallList> callList;
            if (passCreds)
            {
                callList = await this.client.VoiceClient.GetCallsAsync(filter, creds);
            }
            else
            {
                callList = await this.client.VoiceClient.GetCallsAsync(filter);
            }

            var callRecord = callList.Embedded.Calls[0];
            Assert.True(100 == callList.Count);
            Assert.True(10 == callList.PageSize);
            Assert.True(0 == callList.PageIndex);
            Assert.Equal("/calls?page_size=10&record_index=20&order=asc", callList.Links.Self.Href);
            Assert.Equal("/calls/63f61863-4a51-4f6b-86e1-46edebcf9356", callRecord.Links.Self.Href);
            Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", callRecord.Uuid);
            Assert.Equal("CON-f972836a-550f-45fa-956c-12a2ab5b7d22", callRecord.ConversationUuid);
            Assert.Equal("447700900000", callRecord.To.Number);
            Assert.Equal("phone", callRecord.To.Type);
            Assert.Equal("phone", callRecord.From.Type);
            Assert.Equal("447700900001", callRecord.From.Number);
            Assert.Equal("started", callRecord.Status);
            Assert.Equal("outbound", callRecord.Direction);
            Assert.Equal("0.39", callRecord.Rate);
            Assert.Equal("23.40", callRecord.Price);
            Assert.Equal("60", callRecord.Duration);
            Assert.Equal(DateTime.ParseExact("2020-01-01T12:00:00.000Z", "yyyy-MM-dd'T'HH:mm:ss.fff'Z'",
                CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal |
                                              DateTimeStyles.AdjustToUniversal), callRecord.StartTime);
            Assert.Equal(DateTime.ParseExact("2020-01-01T12:00:00.000Z", "yyyy-MM-dd'T'HH:mm:ss.fff'Z'",
                CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal |
                                              DateTimeStyles.AdjustToUniversal), callRecord.EndTime);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void TestStartDtmf(bool passCreds)
        {
            var uuid = "63f61863-4a51-4f6b-86e1-46edebcf9356";
            var expectedUri = $"{BaseUri}/{uuid}/dtmf";
            var expectedResponse = @"{
                  ""message"": ""DTMF sent"",
                  ""uuid"": ""63f61863-4a51-4f6b-86e1-46edebcf9356""
                }";
            var expectedRequestContent = @"{""digits"":""1234""}";
            var command = new DtmfCommand {Digits = "1234"};
            this.Setup(expectedUri, expectedResponse, expectedRequestContent);
            var creds = Credentials.FromAppIdAndPrivateKey(this.AppId, this.PrivateKey);
            CallCommandResponse response;
            if (passCreds)
            {
                response = this.client.VoiceClient.StartDtmf(uuid, command, creds);
            }
            else
            {
                response = this.client.VoiceClient.StartDtmf(uuid, command);
            }

            Assert.Equal("DTMF sent", response.Message);
            Assert.Equal(uuid, response.Uuid);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task TestStartDtmfAsync(bool passCreds)
        {
            var uuid = "63f61863-4a51-4f6b-86e1-46edebcf9356";
            var expectedUri = $"{BaseUri}/{uuid}/dtmf";
            var expectedResponse = @"{
                  ""message"": ""DTMF sent"",
                  ""uuid"": ""63f61863-4a51-4f6b-86e1-46edebcf9356""
                }";
            var expectedRequestContent = @"{""digits"":""1234""}";
            var command = new DtmfCommand {Digits = "1234"};
            this.Setup(expectedUri, expectedResponse, expectedRequestContent);
            var creds = Credentials.FromAppIdAndPrivateKey(this.AppId, this.PrivateKey);
            CallCommandResponse response;
            if (passCreds)
            {
                response = await this.client.VoiceClient.StartDtmfAsync(uuid, command, creds);
            }
            else
            {
                response = await this.client.VoiceClient.StartDtmfAsync(uuid, command);
            }

            Assert.Equal("DTMF sent", response.Message);
            Assert.Equal(uuid, response.Uuid);
        }

        [Theory]
        [InlineData(true, true)]
        [InlineData(false, false)]
        public void TestStartStream(bool passCreds, bool kitchenSink)
        {
            var uuid = "63f61863-4a51-4f6b-86e1-46edebcf9356";
            var expectedUri = $"{BaseUri}/{uuid}/stream";
            var expectedResponse = @"{
                  ""message"": ""Stream started"",
                  ""uuid"": ""63f61863-4a51-4f6b-86e1-46edebcf9356""
                }";
            string expectedRequestContent;
            StreamCommand command;
            if (kitchenSink)
            {
                expectedRequestContent =
                    @"{""stream_url"":[""https://example.com/waiting.mp3""],""loop"":0,""level"":""0.4""}";
                command = new StreamCommand
                {
                    StreamUrl = new[] {"https://example.com/waiting.mp3"},
                    Loop = 0,
                    Level = "0.4",
                };
            }
            else
            {
                expectedRequestContent = @"{""stream_url"":[""https://example.com/waiting.mp3""]}";
                command = new StreamCommand
                {
                    StreamUrl = new[] {"https://example.com/waiting.mp3"},
                };
            }

            this.Setup(expectedUri, expectedResponse, expectedRequestContent);
            var creds = Credentials.FromAppIdAndPrivateKey(this.AppId, this.PrivateKey);
            CallCommandResponse response;
            if (passCreds)
            {
                response = this.client.VoiceClient.StartStream(uuid, command, creds);
            }
            else
            {
                response = this.client.VoiceClient.StartStream(uuid, command);
            }

            Assert.Equal("Stream started", response.Message);
            Assert.Equal(uuid, response.Uuid);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task TestStartStreamAsync(bool passCreds)
        {
            var uuid = "63f61863-4a51-4f6b-86e1-46edebcf9356";
            var expectedUri = $"{BaseUri}/{uuid}/stream";
            var expectedResponse = @"{
                  ""message"": ""Stream started"",
                  ""uuid"": ""63f61863-4a51-4f6b-86e1-46edebcf9356""
                }";
            var expectedRequestContent = @"{""stream_url"":[""https://example.com/waiting.mp3""]}";
            var command = new StreamCommand
            {
                StreamUrl = new[] {"https://example.com/waiting.mp3"},
            };
            this.Setup(expectedUri, expectedResponse, expectedRequestContent);
            var creds = Credentials.FromAppIdAndPrivateKey(this.AppId, this.PrivateKey);
            CallCommandResponse response;
            if (passCreds)
            {
                response = await this.client.VoiceClient.StartStreamAsync(uuid, command, creds);
            }
            else
            {
                response = await this.client.VoiceClient.StartStreamAsync(uuid, command);
            }

            Assert.Equal("Stream started", response.Message);
            Assert.Equal(uuid, response.Uuid);
        }

        [Theory]
        [InlineData(true, true)]
        [InlineData(false, false)]
        public void TestStartTalk(bool passCreds, bool kitchenSink)
        {
            var uuid = "63f61863-4a51-4f6b-86e1-46edebcf9356";
            var expectedUri = $"{BaseUri}/{uuid}/talk";
            var expectedResponse = @"{
                  ""message"": ""Talk started"",
                  ""uuid"": ""63f61863-4a51-4f6b-86e1-46edebcf9356""
                }";
            string expectedRequestContent;
            TalkCommand command;
            if (kitchenSink)
            {
                expectedRequestContent =
                    @"{""text"":""Hello. How are you today?"",""voice_name"":""salli"",""loop"":0,""level"":""0.4"",""language"":""en-US"",""style"":1,""premium"":true}";
                command = new TalkCommand
                {
                    Text = "Hello. How are you today?",
                    Loop = 0,
                    Level = "0.4",
                    VoiceName = "salli",
                    Language = "en-US",
                    Style = 1,
                    Premium = true,
                };
            }
            else
            {
                expectedRequestContent = @"{""text"":""Hello. How are you today?""}";
                command = new TalkCommand
                {
                    Text = "Hello. How are you today?",
                };
            }

            this.Setup(expectedUri, expectedResponse, expectedRequestContent);
            var creds = Credentials.FromAppIdAndPrivateKey(this.AppId, this.PrivateKey);
            CallCommandResponse response;
            if (passCreds)
            {
                response = this.client.VoiceClient.StartTalk(uuid, command, creds);
            }
            else
            {
                response = this.client.VoiceClient.StartTalk(uuid, command);
            }

            Assert.Equal("Talk started", response.Message);
            Assert.Equal(uuid, response.Uuid);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task TestStartTalkAsync(bool passCreds)
        {
            var uuid = "63f61863-4a51-4f6b-86e1-46edebcf9356";
            var expectedUri = $"{BaseUri}/{uuid}/talk";
            var expectedResponse = @"{
                  ""message"": ""Talk started"",
                  ""uuid"": ""63f61863-4a51-4f6b-86e1-46edebcf9356""
                }";
            var expectedRequestContent = @"{""text"":""Hello. How are you today?""}";
            var command = new TalkCommand
            {
                Text = "Hello. How are you today?",
            };
            this.Setup(expectedUri, expectedResponse, expectedRequestContent);
            var creds = Credentials.FromAppIdAndPrivateKey(this.AppId, this.PrivateKey);
            CallCommandResponse response;
            if (passCreds)
            {
                response = await this.client.VoiceClient.StartTalkAsync(uuid, command, creds);
            }
            else
            {
                response = await this.client.VoiceClient.StartTalkAsync(uuid, command);
            }

            Assert.Equal("Talk started", response.Message);
            Assert.Equal(uuid, response.Uuid);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task TestUpdateCallAsync(bool passCreds)
        {
            var uuid = "63f61863-4a51-4f6b-86e1-46edebcf9356";
            var expectedUri = $"{BaseUri}/{uuid}";
            var expectedResponse = "";
            var expectedRequestContent = @"{""action"":""earmuff""}";
            var request = new CallEditCommand {Action = CallEditCommand.ActionType.earmuff};
            this.Setup(expectedUri, expectedResponse, expectedRequestContent);
            bool response;
            var creds = Credentials.FromAppIdAndPrivateKey(this.AppId, this.PrivateKey);
            if (passCreds)
            {
                response = await this.client.VoiceClient.UpdateCallAsync(uuid, request, creds);
            }
            else
            {
                response = await this.client.VoiceClient.UpdateCallAsync(uuid, request);
            }

            Assert.True(response);
        }

        [Fact]
        public void TestUpdateCallWithCredentials()
        {
            var uuid = "63f61863-4a51-4f6b-86e1-46edebcf9356";
            var expectedUri = $"{BaseUri}/{uuid}";
            var expectedResponse = "";
            var expectedRequestContent =
                @"{""action"":""transfer"",""destination"":{""type"":""ncco"",""url"":[""https://example.com/ncco.json""]}}";
            var destination = new Destination {Type = "ncco", Url = new[] {"https://example.com/ncco.json"}};
            var request = new CallEditCommand {Destination = destination, Action = CallEditCommand.ActionType.transfer};
            this.Setup(expectedUri, expectedResponse, expectedRequestContent);
            var creds = Credentials.FromAppIdAndPrivateKey(this.AppId, this.PrivateKey);
            var response = this.client.VoiceClient.UpdateCall(uuid, request, creds);
            Assert.True(response);
        }

        [Fact]
        public void TestUpdateCallWithInlineNcco()
        {
            var uuid = "63f61863-4a51-4f6b-86e1-46edebcf9356";
            var expectedUri = $"{BaseUri}/{uuid}";
            var expectedResponse = "";
            var expectedRequestContent =
                @"{""action"":""transfer"",""destination"":{""type"":""ncco"",""ncco"":[{""action"":""talk"",""text"":""Hello World""}]}}";
            var destination = new Destination {Type = "ncco", Ncco = new Ncco(new TalkAction {Text = "hello world"})};
            var request = new CallEditCommand {Destination = destination, Action = CallEditCommand.ActionType.transfer};
            this.Setup(expectedUri, expectedResponse, expectedRequestContent);
            Credentials.FromAppIdAndPrivateKey(this.AppId, this.PrivateKey);
            var response = this.client.VoiceClient.UpdateCall(uuid, request);
            Assert.True(response);
        }

        [Theory]
        [InlineData(CallEditCommand.ActionType.hangup)]
        [InlineData(CallEditCommand.ActionType.mute)]
        [InlineData(CallEditCommand.ActionType.unmute)]
        [InlineData(CallEditCommand.ActionType.earmuff)]
        [InlineData(CallEditCommand.ActionType.unearmuff)]
        [InlineData(CallEditCommand.ActionType.transfer)]
        public void UpdateCallWithActionsType(CallEditCommand.ActionType actionType)
        {
            var uuid = "63f61863-4a51-4f6b-86e1-46edebcf9356";
            var expectedUri = $"{BaseUri}/{uuid}";
            var expectedActionType = actionType.ToString().ToLower();
            var expectedRequestContent = @"{""action"":""" + expectedActionType +
                                         @""",""destination"":{""type"":""ncco"",""url"":[""https://example.com/ncco.json""]}}";
            var destination = new Destination {Type = "ncco", Url = new[] {"https://example.com/ncco.json"}};
            var request = new CallEditCommand {Destination = destination, Action = actionType};
            this.Setup(expectedUri, string.Empty, expectedRequestContent);
            Credentials.FromAppIdAndPrivateKey(this.AppId, this.PrivateKey);
            var response = this.client.VoiceClient.UpdateCall(uuid, request);
            Assert.True(response);
        }

        private VonageClient BuildClientWithWrongCredentials() =>
            new VonageClient(Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret));
    }
}