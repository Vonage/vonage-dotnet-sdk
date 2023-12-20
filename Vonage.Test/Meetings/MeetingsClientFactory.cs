using System.IO.Abstractions.TestingHelpers;
using Vonage.Common.Client;
using Vonage.Meetings;

namespace Vonage.Test.Meetings
{
    public static class MeetingsClientFactory
    {
        public static MeetingsClient Create(VonageHttpClientConfiguration configuration) =>
            new(configuration, new MockFileSystem());

        public static MeetingsClient Create(VonageHttpClientConfiguration configuration,
            MockFileSystem mockFileSystem) =>
            new(configuration, mockFileSystem);
    }
}