#region
using System;
using Vonage.ShortCodes;
#endregion

namespace Vonage.Test.ShortCodes;

internal static class ShortCodeTestData
{
    internal static OptInManageRequest CreateOptInManageRequest() =>
        new OptInManageRequest {Msisdn = "15559301529"};

    internal static OptInQueryRequest CreateBasicOptInQueryRequest() =>
        new OptInQueryRequest();

    internal static AlertRequest CreateBasicAlertRequest() =>
        new AlertRequest {To = "16365553226"};

    internal static AlertRequest CreateAlertRequestWithAllParameters() =>
        new AlertRequest
        {
            To = "16365553226",
            StatusReportReq = "1",
            ClientRef = Guid.NewGuid().ToString(),
            Template = "Test Template",
            Type = "text",
        };

    internal static TwoFactorAuthRequest CreateTwoFactorAuthRequest() =>
        new TwoFactorAuthRequest();
}