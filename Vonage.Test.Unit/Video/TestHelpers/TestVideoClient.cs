using Vonage.Request;
using Vonage.Video;

namespace Vonage.Test.Unit.Video.TestHelpers
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