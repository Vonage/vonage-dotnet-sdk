#region
using Vonage.Accounts;
#endregion

namespace Vonage.Test.Accounts;

internal static class AccountTestData
{
    internal static CreateSecretRequest CreateBasicSecretRequest() =>
        new CreateSecretRequest
        {
            Secret = "password",
        };

    internal static AccountSettingsRequest CreateBasicSettingsRequest() =>
        new AccountSettingsRequest
        {
            MoCallBackUrl = "https://example.com/webhooks/inbound-sms",
            DrCallBackUrl = "https://example.com/webhooks/delivery-receipt",
        };

    internal static TopUpRequest CreateBasicTopUpRequest() =>
        new TopUpRequest
        {
            Trx = "00X123456Y7890123Z",
        };
}