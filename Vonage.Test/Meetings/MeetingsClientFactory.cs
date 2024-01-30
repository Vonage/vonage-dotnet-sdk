using System.IO.Abstractions.TestingHelpers;
using Vonage.Common.Client;
using Vonage.Meetings;

namespace Vonage.Test.Meetings;

public static class MeetingsClientFactory
{
    public static MeetingsClient Create(VonageHttpClientConfiguration configuration) =>
        new MeetingsClient(configuration, new MockFileSystem());

    public static MeetingsClient Create(VonageHttpClientConfiguration configuration,
        MockFileSystem mockFileSystem) => new MeetingsClient(configuration, mockFileSystem);
}