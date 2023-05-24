using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Web;
using AutoFixture;
using FluentAssertions;
using Vonage.Common.Exceptions;
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
            this.client = new VonageClient(this.BuildCredentialsForBearerAuthentication());
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
            this.Setup(BaseUri, this.GetResponseJson(), this.GetRequestJson());
            var request = BuildCreateCallCommand();
            var response = this.client.VoiceClient.CreateCall(request);
            Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", response.Uuid);
            Assert.Equal("CON-f972836a-550f-45fa-956c-12a2ab5b7d22", response.ConversationUuid);
            Assert.Equal("outbound", response.Direction);
            Assert.Equal("started", response.Status);
        }

        [Fact]
        public async Task CreateCallAsync()
        {
            this.Setup(BaseUri, this.GetResponseJson(nameof(this.CreateCall)),
                this.GetRequestJson(nameof(this.CreateCall)));
            var request = BuildCreateCallCommand();
            var response = await this.client.VoiceClient.CreateCallAsync(request);
            Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", response.Uuid);
            Assert.Equal("CON-f972836a-550f-45fa-956c-12a2ab5b7d22", response.ConversationUuid);
            Assert.Equal("outbound", response.Direction);
            Assert.Equal("started", response.Status);
        }

        [Fact]
        public async Task CreateCallAsyncWithCredentials()
        {
            this.Setup(BaseUri, this.GetResponseJson(nameof(this.CreateCall)),
                this.GetRequestJson(nameof(this.CreateCall)));
            var response = await this.client.VoiceClient.CreateCallAsync(BuildCreateCallCommand(),
                this.BuildCredentialsForBearerAuthentication());
            Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", response.Uuid);
            Assert.Equal("CON-f972836a-550f-45fa-956c-12a2ab5b7d22", response.ConversationUuid);
            Assert.Equal("outbound", response.Direction);
            Assert.Equal("started", response.Status);
        }

        [Fact]
        public async Task CreateCallAsyncWithRandomFromNumber()
        {
            this.Setup(BaseUri, this.GetResponseJson(nameof(this.CreateCallWithRandomFromNumber)),
                this.GetRequestJson(nameof(this.CreateCallWithRandomFromNumber)));
            var request = BuildCreateCallCommand();
            request.From = null;
            request.AdvancedMachineDetection = null;
            request.RandomFromNumber = true;
            this.BuildCredentialsForBearerAuthentication();
            var response = await this.client.VoiceClient.CreateCallAsync(request);
            Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", response.Uuid);
            Assert.Equal("CON-f972836a-550f-45fa-956c-12a2ab5b7d22", response.ConversationUuid);
            Assert.Equal("outbound", response.Direction);
            Assert.Equal("started", response.Status);
        }

        [Fact]
        public async Task CreateCallAsyncWithWrongCredsThrowsAuthException()
        {
            Func<Task> act = async () =>
                await this.BuildClientWithBasicAuthentication().VoiceClient.CreateCallAsync(new CallCommand());
            await act.Should().ThrowExactlyAsync<VonageAuthenticationException>()
                .WithMessage("AppId or Private Key Path missing.");
        }

        [Fact]
        public void CreateCallWithCredentials()
        {
            this.Setup(BaseUri, this.GetResponseJson(nameof(this.CreateCall)),
                this.GetRequestJson(nameof(this.CreateCall)));
            var response = this.client.VoiceClient.CreateCall(BuildCreateCallCommand(),
                this.BuildCredentialsForBearerAuthentication());
            Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", response.Uuid);
            Assert.Equal("CON-f972836a-550f-45fa-956c-12a2ab5b7d22", response.ConversationUuid);
            Assert.Equal("outbound", response.Direction);
            Assert.Equal("started", response.Status);
        }

        [Fact]
        public void CreateCallWithEndpointAndNcco()
        {
            this.Setup(BaseUri, this.GetResponseJson(), this.GetRequestJson());
            var response = this.client.VoiceClient.CreateCall(
                new PhoneEndpoint {Number = "14155550100"}, "14155550100",
                new Ncco(new TalkAction {Text = "Hello World"}));
            Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", response.Uuid);
            Assert.Equal("CON-f972836a-550f-45fa-956c-12a2ab5b7d22", response.ConversationUuid);
            Assert.Equal("outbound", response.Direction);
            Assert.Equal("started", response.Status);
        }

        [Fact]
        public async Task CreateCallWithEndpointAndNccoAsync()
        {
            this.Setup(BaseUri, this.GetResponseJson(nameof(this.CreateCallWithEndpointAndNcco)),
                this.GetRequestJson(nameof(this.CreateCallWithEndpointAndNcco)));
            var response = await this.client.VoiceClient.CreateCallAsync(
                new PhoneEndpoint {Number = "14155550100"}, "14155550100",
                new Ncco(new TalkAction {Text = "Hello World"}));
            Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", response.Uuid);
            Assert.Equal("CON-f972836a-550f-45fa-956c-12a2ab5b7d22", response.ConversationUuid);
            Assert.Equal("outbound", response.Direction);
            Assert.Equal("started", response.Status);
        }

        [Fact]
        public void CreateCallWithRandomFromNumber()
        {
            this.Setup(BaseUri, this.GetResponseJson(), this.GetRequestJson());
            var request = BuildCreateCallCommand();
            request.From = null;
            request.AdvancedMachineDetection = null;
            request.RandomFromNumber = true;
            this.BuildCredentialsForBearerAuthentication();
            var response = this.client.VoiceClient.CreateCall(request);
            Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", response.Uuid);
            Assert.Equal("CON-f972836a-550f-45fa-956c-12a2ab5b7d22", response.ConversationUuid);
            Assert.Equal("outbound", response.Direction);
            Assert.Equal("started", response.Status);
        }

        [Fact]
        public void CreateCallWithStringParameters()
        {
            this.Setup(BaseUri, this.GetResponseJson(), this.GetRequestJson());
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
            this.Setup(BaseUri, this.GetResponseJson(nameof(this.CreateCallWithStringParameters)),
                this.GetRequestJson(nameof(this.CreateCallWithStringParameters)));
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
            this.Setup(BaseUri, this.GetResponseJson(), this.GetRequestJson());
            var request = BuildCreateCallCommand();
            request.Ncco = new Ncco(new TalkAction {Text = "בדיקה בדיקה בדיקה"});
            var response = this.client.VoiceClient.CreateCall(request);
            Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", response.Uuid);
            Assert.Equal("CON-f972836a-550f-45fa-956c-12a2ab5b7d22", response.ConversationUuid);
            Assert.Equal("outbound", response.Direction);
            Assert.Equal("started", response.Status);
        }

        [Fact]
        public void CreateCallWithWrongCredsThrowsAuthException()
        {
            Action act = () => this.BuildClientWithBasicAuthentication()
                .VoiceClient.CreateCall(new CallCommand());
            act.Should().ThrowExactly<VonageAuthenticationException>()
                .WithMessage("AppId or Private Key Path missing.");
        }

        [Fact]
        public void GetRecording()
        {
            var expectedUri = this.fixture.Create<Uri>().ToString();
            var expectedResponse = new byte[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
            this.Setup(expectedUri, expectedResponse);
            var response = this.client.VoiceClient.GetRecording(expectedUri);
            Assert.Equal(expectedResponse, response.ResultStream);
        }

        [Fact]
        public async Task GetRecordingsAsync()
        {
            var expectedUri = this.fixture.Create<Uri>().ToString();
            var expectedResponse = new byte[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
            this.Setup(expectedUri, expectedResponse);
            var response = await this.client.VoiceClient.GetRecordingAsync(expectedUri);
            Assert.Equal(expectedResponse, response.ResultStream);
        }

        [Fact]
        public async Task GetRecordingsAsyncWithCredentials()
        {
            var expectedUri = this.fixture.Create<Uri>().ToString();
            var expectedResponse = new byte[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
            this.Setup(expectedUri, expectedResponse);
            var response =
                await this.client.VoiceClient.GetRecordingAsync(expectedUri,
                    this.BuildCredentialsForBearerAuthentication());
            Assert.Equal(expectedResponse, response.ResultStream);
        }

        [Fact]
        public void GetRecordingWithCredentials()
        {
            var expectedUri = this.fixture.Create<Uri>().ToString();
            var expectedResponse = new byte[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
            this.Setup(expectedUri, expectedResponse);
            var response =
                this.client.VoiceClient.GetRecording(expectedUri, this.BuildCredentialsForBearerAuthentication());
            Assert.Equal(expectedResponse, response.ResultStream);
        }

        [Fact]
        public void GetSpecificCall()
        {
            var uuid = this.fixture.Create<string>();
            this.Setup($"{BaseUri}/{uuid}", this.GetResponseJson());
            var callRecord = this.client.VoiceClient.GetCall(uuid);
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

        [Fact]
        public async Task GetSpecificCallAsync()
        {
            var uuid = this.fixture.Create<Guid>().ToString();
            this.Setup($"{BaseUri}/{uuid}", this.GetResponseJson(nameof(this.GetSpecificCall)));
            var callRecord = await this.client.VoiceClient.GetCallAsync(uuid);
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

        [Fact]
        public async Task GetSpecificCallAsyncWithCredentials()
        {
            var uuid = this.fixture.Create<Guid>().ToString();
            this.Setup($"{BaseUri}/{uuid}", this.GetResponseJson(nameof(this.GetSpecificCall)));
            var callRecord =
                await this.client.VoiceClient.GetCallAsync(uuid, this.BuildCredentialsForBearerAuthentication());
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

        [Fact]
        public void GetSpecificCallWithCredentials()
        {
            var uuid = this.fixture.Create<string>();
            this.Setup($"{BaseUri}/{uuid}", this.GetResponseJson(nameof(this.GetSpecificCall)));
            var callRecord = this.client.VoiceClient.GetCall(uuid, this.BuildCredentialsForBearerAuthentication());
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

        [Fact]
        public void ListCalls()
        {
            var filter = new CallSearchFilter();
            this.Setup($"{this.ApiUrl}/v1/calls", this.GetResponseJson());
            var callList = this.client.VoiceClient.GetCalls(filter);
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

        [Fact]
        public async Task ListCallsAsync()
        {
            var filter = new CallSearchFilter();
            this.Setup($"{this.ApiUrl}/v1/calls", this.GetResponseJson(nameof(this.ListCalls)));
            var callList = await this.client.VoiceClient.GetCallsAsync(filter);
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

        [Fact]
        public async Task ListCallsAsyncWithCredentials()
        {
            var filter = new CallSearchFilter
            {
                ConversationUuid = "CON-f972836a-550f-45fa-956c-12a2ab5b7d22",
                DateStart = DateTime.Parse("2016-11-14T07:45:14"),
                DateEnd = DateTime.Parse("2016-11-14T07:45:14"),
                PageSize = 10,
                RecordIndex = 0,
                Order = "asc",
                Status = "started",
            };
            this.Setup(
                $"{BaseUri}?status=started&date_start={HttpUtility.UrlEncode("2016-11-14T07:45:14Z").ToUpper()}&date_end={HttpUtility.UrlEncode("2016-11-14T07:45:14Z").ToUpper()}&page_size=10&record_index=0&order=asc&conversation_uuid=CON-f972836a-550f-45fa-956c-12a2ab5b7d22&",
                this.GetResponseJson(nameof(this.ListCalls)));
            var callList =
                await this.client.VoiceClient.GetCallsAsync(filter, this.BuildCredentialsForBearerAuthentication());
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

        [Fact]
        public void ListCallsWithCredentials()
        {
            var filter = new CallSearchFilter
            {
                ConversationUuid = "CON-f972836a-550f-45fa-956c-12a2ab5b7d22",
                DateStart = DateTime.Parse("2016-11-14T07:45:14"),
                DateEnd = DateTime.Parse("2016-11-14T07:45:14"),
                PageSize = 10,
                RecordIndex = 0,
                Order = "asc",
                Status = "started",
            };
            this.Setup(
                $"{BaseUri}?status=started&date_start={HttpUtility.UrlEncode("2016-11-14T07:45:14Z").ToUpper()}&date_end={HttpUtility.UrlEncode("2016-11-14T07:45:14Z").ToUpper()}&page_size=10&record_index=0&order=asc&conversation_uuid=CON-f972836a-550f-45fa-956c-12a2ab5b7d22&",
                this.GetResponseJson(nameof(this.ListCalls)));
            var callList = this.client.VoiceClient.GetCalls(filter, this.BuildCredentialsForBearerAuthentication());
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

        [Fact]
        public void StartDtmf()
        {
            var uuid = this.fixture.Create<string>();
            var command = new DtmfCommand {Digits = "1234"};
            this.Setup($"{BaseUri}/{uuid}/dtmf", this.GetResponseJson(), this.GetRequestJson());
            var response = this.client.VoiceClient.StartDtmf(uuid, command);
            Assert.Equal("DTMF sent", response.Message);
            Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", response.Uuid);
        }

        [Fact]
        public async Task StartDtmfAsync()
        {
            var uuid = this.fixture.Create<string>();
            var command = new DtmfCommand {Digits = "1234"};
            this.Setup($"{BaseUri}/{uuid}/dtmf", this.GetResponseJson(nameof(this.StartDtmf)),
                this.GetRequestJson(nameof(this.StartDtmf)));
            var response = await this.client.VoiceClient.StartDtmfAsync(uuid, command);
            Assert.Equal("DTMF sent", response.Message);
            Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", response.Uuid);
        }

        [Fact]
        public async Task StartDtmfAsyncWithCredentials()
        {
            var uuid = this.fixture.Create<string>();
            var command = new DtmfCommand {Digits = "1234"};
            this.Setup($"{BaseUri}/{uuid}/dtmf", this.GetResponseJson(nameof(this.StartDtmf)),
                this.GetRequestJson(nameof(this.StartDtmf)));
            var response =
                await this.client.VoiceClient.StartDtmfAsync(uuid, command,
                    this.BuildCredentialsForBearerAuthentication());
            Assert.Equal("DTMF sent", response.Message);
            Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", response.Uuid);
        }

        [Fact]
        public void StartDtmfWithCredentials()
        {
            var uuid = this.fixture.Create<string>();
            var command = new DtmfCommand {Digits = "1234"};
            this.Setup($"{BaseUri}/{uuid}/dtmf", this.GetResponseJson(nameof(this.StartDtmf)),
                this.GetRequestJson(nameof(this.StartDtmf)));
            var response =
                this.client.VoiceClient.StartDtmf(uuid, command, this.BuildCredentialsForBearerAuthentication());
            Assert.Equal("DTMF sent", response.Message);
            Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", response.Uuid);
        }

        [Fact]
        public void StartStream()
        {
            var uuid = this.fixture.Create<string>();
            var command = new StreamCommand
            {
                StreamUrl = new[] {"https://example.com/waiting.mp3"},
            };
            this.Setup($"{BaseUri}/{uuid}/stream", this.GetResponseJson(), this.GetRequestJson());
            var response = this.client.VoiceClient.StartStream(uuid, command);
            Assert.Equal("Stream started", response.Message);
            Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", response.Uuid);
        }

        [Fact]
        public async Task StartStreamAsync()
        {
            var uuid = this.fixture.Create<string>();
            var command = new StreamCommand
            {
                StreamUrl = new[] {"https://example.com/waiting.mp3"},
            };
            this.Setup($"{BaseUri}/{uuid}/stream", this.GetResponseJson(nameof(this.StartStream)),
                this.GetRequestJson(nameof(this.StartStream)));
            var response = await this.client.VoiceClient.StartStreamAsync(uuid, command);
            Assert.Equal("Stream started", response.Message);
            Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", response.Uuid);
        }

        [Fact]
        public async Task StartStreamAsyncWithCredentials()
        {
            var uuid = this.fixture.Create<string>();
            var command = new StreamCommand
            {
                StreamUrl = new[] {"https://example.com/waiting.mp3"},
                Loop = 0,
                Level = "0.4",
            };
            this.Setup($"{BaseUri}/{uuid}/stream", this.GetResponseJson(nameof(this.StartStream)),
                this.GetRequestJson(nameof(this.StartStreamWithCredentials)));
            var response =
                await this.client.VoiceClient.StartStreamAsync(uuid, command,
                    this.BuildCredentialsForBearerAuthentication());
            Assert.Equal("Stream started", response.Message);
            Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", response.Uuid);
        }

        [Fact]
        public void StartStreamWithCredentials()
        {
            var uuid = this.fixture.Create<string>();
            var command = new StreamCommand
            {
                StreamUrl = new[] {"https://example.com/waiting.mp3"},
                Loop = 0,
                Level = "0.4",
            };
            this.Setup($"{BaseUri}/{uuid}/stream", this.GetResponseJson(nameof(this.StartStream)),
                this.GetRequestJson());
            var response =
                this.client.VoiceClient.StartStream(uuid, command, this.BuildCredentialsForBearerAuthentication());
            Assert.Equal("Stream started", response.Message);
            Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", response.Uuid);
        }

        [Fact]
        public void StartTalk()
        {
            var uuid = this.fixture.Create<string>();
            this.Setup($"{BaseUri}/{uuid}/talk", this.GetResponseJson(), this.GetRequestJson());
            var response = this.client.VoiceClient.StartTalk(uuid, new TalkCommand
            {
                Text = "Hello. How are you today?",
            });
            Assert.Equal("Talk started", response.Message);
            Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", response.Uuid);
        }

        [Fact]
        public async Task StartTalkAsync()
        {
            var uuid = this.fixture.Create<string>();
            this.Setup($"{BaseUri}/{uuid}/talk", this.GetResponseJson(nameof(this.StartTalk)),
                this.GetRequestJson(nameof(this.StartTalk)));
            var response = await this.client.VoiceClient.StartTalkAsync(uuid, new TalkCommand
            {
                Text = "Hello. How are you today?",
            });
            Assert.Equal("Talk started", response.Message);
            Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", response.Uuid);
        }

        [Fact]
        public async Task StartTalkAsyncWithCredentials()
        {
            var uuid = this.fixture.Create<string>();
            this.Setup($"{BaseUri}/{uuid}/talk", this.GetResponseJson(nameof(this.StartTalk)),
                this.GetRequestJson(nameof(this.StartTalk)));
            var response = await this.client.VoiceClient.StartTalkAsync(uuid, new TalkCommand
            {
                Text = "Hello. How are you today?",
            }, this.BuildCredentialsForBearerAuthentication());
            Assert.Equal("Talk started", response.Message);
            Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", response.Uuid);
        }

        [Fact]
        public void StartTalkWithCredentials()
        {
            var uuid = this.fixture.Create<string>();
            var command = new TalkCommand
            {
                Text = "Hello. How are you today?",
                Loop = 0,
                Level = "0.4",
                VoiceName = "salli",
                Language = "en-US",
                Style = 1,
                Premium = true,
            };
            this.Setup($"{BaseUri}/{uuid}/talk", this.GetResponseJson(nameof(this.StartTalk)), this.GetRequestJson());
            var response =
                this.client.VoiceClient.StartTalk(uuid, command, this.BuildCredentialsForBearerAuthentication());
            Assert.Equal("Talk started", response.Message);
            Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", response.Uuid);
        }

        [Fact]
        public void StopStream()
        {
            var uuid = this.fixture.Create<string>();
            this.Setup($"{BaseUri}/{uuid}/stream", this.GetResponseJson());
            var response = this.client.VoiceClient.StopStream(uuid);
            Assert.Equal("Stream stopped", response.Message);
            Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", response.Uuid);
        }

        [Fact]
        public async Task StopStreamAsync()
        {
            var uuid = this.fixture.Create<string>();
            this.Setup($"{BaseUri}/{uuid}/stream", this.GetResponseJson(nameof(this.StopStream)));
            var response = await this.client.VoiceClient.StopStreamAsync(uuid);
            Assert.Equal("Stream stopped", response.Message);
            Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", response.Uuid);
        }

        [Fact]
        public async Task StopStreamAsyncWithCredentials()
        {
            var uuid = this.fixture.Create<string>();
            this.Setup($"{BaseUri}/{uuid}/stream", this.GetResponseJson(nameof(this.StopStream)));
            var response =
                await this.client.VoiceClient.StopStreamAsync(uuid, this.BuildCredentialsForBearerAuthentication());
            Assert.Equal("Stream stopped", response.Message);
            Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", response.Uuid);
        }

        [Fact]
        public void StopStreamWithCredentials()
        {
            var uuid = this.fixture.Create<string>();
            this.Setup($"{BaseUri}/{uuid}/stream", this.GetResponseJson(nameof(this.StopStream)));
            var response = this.client.VoiceClient.StopStream(uuid, this.BuildCredentialsForBearerAuthentication());
            Assert.Equal("Stream stopped", response.Message);
            Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", response.Uuid);
        }

        [Fact]
        public void StopTalk()
        {
            var uuid = this.fixture.Create<string>();
            this.Setup($"{BaseUri}/{uuid}/stream", this.GetResponseJson());
            var response = this.client.VoiceClient.StopStream(uuid);
            Assert.Equal("Talk stopped", response.Message);
            Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", response.Uuid);
        }

        [Fact]
        public async Task StopTalkAsync()
        {
            var uuid = this.fixture.Create<string>();
            this.Setup($"{BaseUri}/{uuid}/stream", this.GetResponseJson(nameof(this.StopTalk)));
            var response = await this.client.VoiceClient.StopStreamAsync(uuid);
            Assert.Equal("Talk stopped", response.Message);
            Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", response.Uuid);
        }

        [Fact]
        public async Task StopTalkAsyncWithCredentials()
        {
            var uuid = this.fixture.Create<string>();
            this.Setup($"{BaseUri}/{uuid}/stream", this.GetResponseJson(nameof(this.StopTalk)));
            var response =
                await this.client.VoiceClient.StopStreamAsync(uuid, this.BuildCredentialsForBearerAuthentication());
            Assert.Equal("Talk stopped", response.Message);
            Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", response.Uuid);
        }

        [Fact]
        public void StopTalkWithCredentials()
        {
            var uuid = this.fixture.Create<string>();
            this.Setup($"{BaseUri}/{uuid}/stream", this.GetResponseJson(nameof(this.StopTalk)));
            var response = this.client.VoiceClient.StopStream(uuid, this.BuildCredentialsForBearerAuthentication());
            Assert.Equal("Talk stopped", response.Message);
            Assert.Equal("63f61863-4a51-4f6b-86e1-46edebcf9356", response.Uuid);
        }

        [Fact]
        public async Task UpdateCallAsync()
        {
            var uuid = this.fixture.Create<Guid>().ToString();
            var request = new CallEditCommand {Action = CallEditCommand.ActionType.earmuff};
            this.Setup($"{BaseUri}/{uuid}", string.Empty, this.GetRequestJson());
            var response = await this.client.VoiceClient.UpdateCallAsync(uuid, request);
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
            var uuid = this.fixture.Create<Guid>().ToString();
            var expectedRequestContent = this.GetRequestJson().Replace("$action", actionType.ToString().ToLower());
            var request = new CallEditCommand
            {
                Destination = new Destination {Type = "ncco", Url = new[] {"https://example.com/ncco.json"}},
                Action = actionType,
            };
            this.Setup($"{BaseUri}/{uuid}", string.Empty, expectedRequestContent);
            Assert.True(this.client.VoiceClient.UpdateCall(uuid, request));
        }

        [Fact]
        public void UpdateCallWithCredentials()
        {
            var uuid = this.fixture.Create<Guid>().ToString();
            var request = new CallEditCommand
            {
                Destination = new Destination {Type = "ncco", Url = new[] {"https://example.com/ncco.json"}},
                Action = CallEditCommand.ActionType.transfer,
            };
            this.Setup($"{BaseUri}/{uuid}", string.Empty, this.GetRequestJson());
            var response =
                this.client.VoiceClient.UpdateCall(uuid, request, this.BuildCredentialsForBearerAuthentication());
            Assert.True(response);
        }

        [Fact]
        public void UpdateCallWithInlineNcco()
        {
            var uuid = this.fixture.Create<Guid>().ToString();
            var request = new CallEditCommand
            {
                Destination = new Destination {Type = "ncco", Ncco = new Ncco(new TalkAction {Text = "hello world"})},
                Action = CallEditCommand.ActionType.transfer,
            };
            this.Setup($"{BaseUri}/{uuid}", string.Empty, this.GetRequestJson());
            Assert.True(this.client.VoiceClient.UpdateCall(uuid, request));
        }

        private VonageClient BuildClientWithBasicAuthentication() =>
            new VonageClient(this.BuildCredentialsForBasicAuthentication());

        private static CallCommand BuildCreateCallCommand() =>
            new CallCommand
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
    }
}