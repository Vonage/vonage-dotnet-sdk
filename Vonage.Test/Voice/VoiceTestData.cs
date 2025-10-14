#region
using System;
using Vonage.Voice;
using Vonage.Voice.Nccos;
using Vonage.Voice.Nccos.Endpoints;
#endregion

namespace Vonage.Test.Voice;

internal static class VoiceTestData
{
    internal static CallCommand CreateCallCommand() =>
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
            Ncco = new Ncco(new TalkAction {Text = "Hello World"},
                new TalkAction {Text = "Hello World", Premium = true}, new TalkAction {Text = "בדיקה בדיקה בדיקה"},
                new MultiInputAction {Mode = MultiInputAction.InputMode.Asynchronous}),
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
            RandomFromNumber = true,
        };

    internal static CallSearchFilter CreateBasicCallSearchFilter() =>
        new CallSearchFilter();

    internal static CallSearchFilter CreateComplexCallSearchFilter() =>
        new CallSearchFilter
        {
            ConversationUuid = "CON-f972836a-550f-45fa-956c-12a2ab5b7d22",
            DateStart = DateTime.Parse("2016-11-14T07:45:14"),
            DateEnd = DateTime.Parse("2016-11-14T07:45:14"),
            PageSize = 10,
            RecordIndex = 0,
            Order = "asc",
            Status = "started",
        };

    internal static DtmfCommand CreateDtmfCommand() =>
        new DtmfCommand {Digits = "1234"};

    internal static StreamCommand CreateStreamCommand() =>
        new StreamCommand
        {
            StreamUrl = new[] {"https://example.com/waiting.mp3"},
            Loop = 0,
            Level = "0.4",
        };

    internal static TalkCommand CreateTalkCommand() =>
        new TalkCommand
        {
            Text = "Hello. How are you today?",
            Language = "en-US",
            Loop = 0,
            Level = "0.4",
            Premium = true,
            Style = 1,
        };

    internal static CallEditCommand CreateCallEditCommand() =>
        new CallEditCommand
        {
            Destination = new Destination
            {
                Type = "ncco",
                Ncco = new Ncco(new TalkAction {Text = "Hello World"}),
                Url = new[] {"https://example.com/ncco.json"},
            },
            Action = CallEditCommand.ActionType.transfer,
        };

    internal static AdvancedMachineDetectionProperties CreateValidAdvancedMachineDetectionProperties(
        int beepTimeout = 45) =>
        new AdvancedMachineDetectionProperties(
            AdvancedMachineDetectionProperties.MachineDetectionBehavior.Continue,
            AdvancedMachineDetectionProperties.MachineDetectionMode.Detect,
            beepTimeout);

    internal static string GetValidRecordingUri() =>
        "https://api.nexmo.com/v1/files/aaaaaaaa-bbbb-cccc-dddd-0123456789ab";

    internal static string GetInvalidDomainUri() =>
        "https://example.com/v1/abc123";

    internal static string GetInvalidUri() =>
        "not a url";

    internal static Uri GetRealTimeDtmfUri() =>
        new Uri("https://example.com/ivr");
}