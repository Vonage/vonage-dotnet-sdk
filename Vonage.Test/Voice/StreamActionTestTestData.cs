#region
using Vonage.Voice.Nccos;
#endregion

namespace Vonage.Test.Voice;

internal static class StreamActionTestTestData
{
    internal static StreamAction CreateStreamActionWithUrl() =>
        new StreamAction {StreamUrl = new[] {"https://www.example.com/waiting.mp3"}};
}