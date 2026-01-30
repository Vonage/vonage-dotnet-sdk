#region
using System.Collections.Generic;
using Vonage.Voice.Nccos;
#endregion

namespace Vonage.Test.Voice;

internal static class MultiInputTestsTestData
{
    internal static SpeechSettings CreateSpeechSettingsWithAllProperties() =>
        new SpeechSettings
        {
            Uuid = ["aaaaaaaa-bbbb-cccc-dddd-0123456789ab"],
            EndOnSilence = 1,
            Language = "en-US",
            Context = new[] {"dog", "cat"},
            StartTimeout = 5,
            MaxDuration = 30,
            Provider = SpeechProvider.Deepgram,
            ProviderOptions = new Dictionary<string, string>
            {
                {"test", "value"},
                {"foo", "bar"},
            },
        };

    internal static DtmfSettings CreateDtmfSettingsWithAllProperties() =>
        new DtmfSettings {MaxDigits = 1, TimeOut = 3, SubmitOnHash = true};

    internal static MultiInputAction CreateMultiInputActionWithAllProperties() =>
        new MultiInputAction
        {
            Speech = CreateSpeechSettingsWithAllProperties(),
            Dtmf = CreateDtmfSettingsWithAllProperties(),
            Type = new[]
            {
                NccoInputType.DTMF,
                NccoInputType.Speech,
            },
        };

    internal static MultiInputAction CreateEmptyMultiInputAction() =>
        new MultiInputAction();
}