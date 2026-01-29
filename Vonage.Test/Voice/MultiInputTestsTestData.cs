#region
using Vonage.Voice.Nccos;
#endregion

namespace Vonage.Test.Voice;

internal static class MultiInputTestsTestData
{
    internal static SpeechSettings CreateSpeechSettingsWithAllProperties() =>
        new SpeechSettings
        {
            Uuid = new[] {"aaaaaaaa-bbbb-cccc-dddd-0123456789ab"},
            EndOnSilence = 1,
            Language = "en-US",
            Context = new[] {"dog", "cat"},
            StartTimeout = 5,
            MaxDuration = 30,
            Provider = SpeechProvider.Deepgram,
            ProviderOptions = new SpeechProviderOptions
            {
                Model = "nova-3",
                Dictation = true,
                FillerWords = true,
                InterimResults = true,
                Keywords = ["first", "second", "next"],
                Language = "bg",
                Numerals = true,
                ProfanityFilter = true,
                Punctuate = true,
                Redact = true,
                SmartFormat = true,
                Endpointing = 10,
                UtteranceEnd = 100,
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