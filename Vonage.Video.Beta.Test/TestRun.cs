using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Vonage.Request;
using Vonage.Video.Beta.Common.Tokens;
using Vonage.Video.Beta.Video;
using Vonage.Video.Beta.Video.Archives.CreateArchive;
using Vonage.Video.Beta.Video.Archives.DeleteArchive;
using Vonage.Video.Beta.Video.Archives.GetArchive;
using Vonage.Video.Beta.Video.Archives.GetArchives;
using Vonage.Video.Beta.Video.Archives.StopArchive;
using Vonage.Video.Beta.Video.Moderation.DisconnectConnection;
using Vonage.Video.Beta.Video.Moderation.MuteStream;
using Vonage.Video.Beta.Video.Moderation.MuteStreams;
using Vonage.Video.Beta.Video.Sessions;
using Vonage.Video.Beta.Video.Sessions.CreateSession;
using Vonage.Video.Beta.Video.Sessions.GetStream;
using Vonage.Video.Beta.Video.Sessions.GetStreams;
using Vonage.Video.Beta.Video.Signaling;
using Vonage.Video.Beta.Video.Signaling.SendSignal;
using Vonage.Video.Beta.Video.Signaling.SendSignals;
using Xunit;
using Xunit.Abstractions;

namespace Vonage.Video.Beta.Test
{
    public class TestRun
    {
        private readonly ITestOutputHelper testOutputHelper;
        private readonly VideoClient client;
        private readonly Credentials credentials;
        private readonly string sessionId;
        private readonly string connectionId;

        private TestRun(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
            this.credentials =
                Credentials.FromAppIdAndPrivateKeyPath("a98e12ca-f3e5-4df8-bc66-fd4b5f30b9e9", "Key.txt");
            this.client = new VideoClient(this.credentials);
            this.sessionId =
                "2_MX5hOThlMTJjYS1mM2U1LTRkZjgtYmM2Ni1mZDRiNWYzMGI5ZTl-fjE2NzMwMTU1NTU2ODJ-NjFTcWlPeWQvQ0xBc2tOSG9pNUU4TUNlfn5-";
            this.connectionId = "";
        }

        private async Task<string> GetFirstStreamId(string session)
        {
            var response =
                await this.client.SessionClient.GetStreamsAsync(GetStreamsRequest.Parse(
                    this.credentials.ApplicationId, session));
            return response.Match(success => success.Items.First().Id, _ => string.Empty);
        }

        public class CreateSession : TestRun
        {
            public CreateSession(ITestOutputHelper testOutputHelper)
                : base(testOutputHelper)
            {
            }

            [Fact]
            public void CreateSessionAndToken()
            {
                var token = this.client.SessionClient
                    .CreateSessionAsync(CreateSessionRequest.Parse(string.Empty, MediaMode.Routed, ArchiveMode.Manual))
                    .Result
                    .Bind(value => TokenAdditionalClaims.Parse(value.SessionId))
                    .Bind(claims => new VideoTokenGenerator().GenerateToken(this.credentials, claims));
                token.IfSuccess(value =>
                {
                    this.testOutputHelper.WriteLine($"SessionId: {value.SessionId}");
                    this.testOutputHelper.WriteLine($"Token: {value.Token}");
                });
                token.IsSuccess.Should().BeTrue();
            }
        }

        public class Sessions : TestRun
        {
            public Sessions(ITestOutputHelper testOutputHelper)
                : base(testOutputHelper)
            {
            }

            [Fact]
            public async Task GetStreams()
            {
                var response = await this.client.SessionClient.GetStreamsAsync(GetStreamsRequest.Parse(
                    this.credentials.ApplicationId, this.sessionId));
                response.IsSuccess.Should().BeTrue();
            }

            [Fact]
            public async Task GetStream()
            {
                var response = await this.client.SessionClient.GetStreamsAsync(GetStreamsRequest.Parse(
                    this.credentials.ApplicationId, this.sessionId));
                response.IsSuccess.Should().BeTrue();
                var streamId = await this.GetFirstStreamId(this.sessionId);
                var streamResponse = await this.client.SessionClient.GetStreamAsync(GetStreamRequest.Parse(
                    this.credentials.ApplicationId, this.sessionId, streamId));
                streamResponse.IsSuccess.Should().BeTrue();
            }

            [Fact]
            public async Task ChangeLayout()
            {
                var streamId = await this.GetFirstStreamId(
                    this.sessionId);
                var streamResponse = await this.client.SessionClient.GetStreamAsync(GetStreamRequest.Parse(
                    this.credentials.ApplicationId, this.sessionId, streamId));
                streamResponse.IsSuccess.Should().BeTrue();
            }
        }

        public class Signaling : TestRun
        {
            public Signaling(ITestOutputHelper testOutputHelper)
                : base(testOutputHelper)
            {
            }

            [Fact]
            public async Task SendSignal()
            {
                var response = await this.client.SignalingClient.SendSignalAsync(SendSignalRequest.Parse(
                    this.credentials.ApplicationId, this.sessionId, connectionId,
                    BuildSignalContent()));
                response.IsSuccess.Should().BeTrue();
            }

            [Fact]
            public async Task SendSignals()
            {
                var response = await this.client.SignalingClient.SendSignalsAsync(SendSignalsRequest.Parse(
                    this.credentials.ApplicationId, this.sessionId,
                    BuildSignalContent()));
                response.IsSuccess.Should().BeTrue();
            }

            private static SignalContent BuildSignalContent() => new("chat", "Hello world");
        }

        public class Moderation : TestRun
        {
            public Moderation(ITestOutputHelper testOutputHelper)
                : base(testOutputHelper)
            {
            }

            [Fact]
            public async Task DisconnectConnection()
            {
                var response = await this.client.ModerationClient.DisconnectConnectionAsync(
                    DisconnectConnectionRequest.Parse(this.credentials.ApplicationId, this.sessionId, connectionId));
                response.IsSuccess.Should().BeTrue();
            }

            [Fact]
            public async Task MuteStream()
            {
                var streamId = await this.GetFirstStreamId(this.sessionId);
                var response = await this.client.ModerationClient.MuteStreamAsync(
                    MuteStreamRequest.Parse(this.credentials.ApplicationId, this.sessionId, streamId));
                response.IsSuccess.Should().BeTrue();
            }

            [Fact]
            public async Task MuteStreams()
            {
                var response = await this.client.ModerationClient.MuteStreamsAsync(
                    MuteStreamsRequest.Parse(this.credentials.ApplicationId, this.sessionId,
                        new MuteStreamsRequest.MuteStreamsConfiguration(true, Array.Empty<string>())));
                response.IsSuccess.Should().BeTrue();
            }
        }

        public class Archiving : TestRun
        {
            public Archiving(ITestOutputHelper testOutputHelper)
                : base(testOutputHelper)
            {
            }

            [Fact]
            public async Task GetArchives()
            {
                var response = await this.client.ArchiveClient.GetArchivesAsync(
                    GetArchivesRequest.Parse(this.credentials.ApplicationId));
                response.IsSuccess.Should().BeTrue();
            }

            [Fact]
            public async Task GetArchive()
            {
                var archiveId = await this.GetFirstArchiveId();
                var response = await this.client.ArchiveClient.GetArchiveAsync(
                    GetArchiveRequest.Parse(this.credentials.ApplicationId, archiveId));
                response.IsSuccess.Should().BeTrue();
            }

            [Fact]
            public async Task CreateArchive()
            {
                var response = await this.client.ArchiveClient.CreateArchiveAsync(
                    CreateArchiveRequest.Parse(this.credentials.ApplicationId, this.sessionId, true, true, "test"));
                response.IsSuccess.Should().BeTrue();
            }

            [Fact]
            public async Task StopArchive()
            {
                var archiveId = await this.GetFirstArchiveId();
                var response = await this.client.ArchiveClient.StopArchiveAsync(
                    StopArchiveRequest.Parse(this.credentials.ApplicationId, archiveId));
                response.IsSuccess.Should().BeTrue();
            }

            [Fact]
            public async Task DeleteArchive()
            {
                var archiveId = await this.GetFirstArchiveId();
                var response = await this.client.ArchiveClient.DeleteArchiveAsync(
                    DeleteArchiveRequest.Parse(this.credentials.ApplicationId, archiveId));
                response.IsSuccess.Should().BeTrue();
            }

            private async Task<string> GetFirstArchiveId()
            {
                var response = await this.client.ArchiveClient.GetArchivesAsync(
                    GetArchivesRequest.Parse(this.credentials.ApplicationId));
                return response.Match(success => success.Items.First().Id, _ => string.Empty);
            }
        }
    }
}