using System.Threading.Tasks;
using Vonage.Request;
using Vonage.Video.Beta.Common;
using Vonage.Video.Beta.Video;
using Vonage.Video.Beta.Video.Sessions;
using Vonage.Video.Beta.Video.Sessions.CreateSession;
using Xunit;

namespace Vonage.Video.Beta.Test
{
    public class SampleTest
    {
        [Fact]
        public async Task DoSomething()
        {
            var videoClient =
                new VideoClient(
                    Credentials.FromAppIdAndPrivateKeyPath("a98e12ca-f3e5-4df8-bc66-fd4b5f30b9e9", "key.txt"));
            var sessionClient = videoClient.SessionClient;
            const MediaMode mediaMode = MediaMode.Routed;
            const ArchiveMode archiveMode = ArchiveMode.Always;
            var location = IpAddress.Empty;
            var result = await CreateSessionRequest
                .Parse(location, mediaMode, archiveMode)
                .BindAsync(createSession => sessionClient.CreateSessionAsync(createSession));
        }
    }
}