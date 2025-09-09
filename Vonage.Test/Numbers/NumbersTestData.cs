#region
using Vonage.Numbers;
#endregion

namespace Vonage.Test.Numbers;

internal static class NumbersTestData
{
    internal static NumberTransactionRequest CreateBasicTransactionRequest() =>
        new NumberTransactionRequest {Country = "GB", Msisdn = "447700900000"};

    internal static NumberTransactionRequest CreateTransactionRequestWithCredentials() =>
        new NumberTransactionRequest {Country = "GB", Msisdn = "447700900000", TargetApiKey = "12345"};

    internal static NumberSearchRequest CreateBasicSearchRequest() =>
        new NumberSearchRequest {Country = "GB"};

    internal static NumberSearchRequest CreateDetailedSearchRequest() =>
        new NumberSearchRequest
        {
            Country = "GB",
            Type = "mobile-lvn",
            Pattern = "12345",
            SearchPattern = SearchPattern.Contains,
            Features = "SMS",
            Size = 10,
            Index = 1,
        };

    internal static NumberSearchRequest CreateSearchRequestWithApplication() =>
        new NumberSearchRequest
        {
            Country = "GB",
            Type = "mobile-lvn",
            Pattern = "12345",
            SearchPattern = SearchPattern.Contains,
            Features = "SMS",
            Size = 10,
            Index = 1,
            ApplicationId = "testApp",
        };

    internal static UpdateNumberRequest CreateBasicUpdateRequest() =>
        new UpdateNumberRequest {Country = "GB", Msisdn = "447700900000"};

    internal static UpdateNumberRequest CreateDetailedUpdateRequest() =>
        new UpdateNumberRequest
        {
            Country = "GB",
            Msisdn = "447700900000",
            AppId = "aaaaaaaa-bbbb-cccc-dddd-0123456789abc",
            MoHttpUrl = "https://example.com/webhooks/inbound-sms",
            MoSmppSysType = "inbound",
            VoiceCallbackType = "tel",
            VoiceCallbackValue = "447700900000",
            VoiceStatusCallback = "https://example.com/webhooks/status",
        };
}