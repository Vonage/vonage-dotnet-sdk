using Vonage.Request;
using Vonage.Server.Video;

namespace Vonage.Server.Test.TestHelpers
{
    internal class TestVideoClient : VideoClient
    {
        private TestVideoClient(Credentials credentials) : base(credentials)
        {
        }

        public TestVideoClient(Credentials credentials, Configuration configuration)
            : this(credentials)
        {
            this.Configuration = configuration;
        }
    }
}