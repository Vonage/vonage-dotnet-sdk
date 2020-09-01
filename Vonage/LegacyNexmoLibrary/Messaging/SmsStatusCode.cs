namespace Nexmo.Api.Messaging
{
    [System.Obsolete("The Nexmo.Api.Messaging.SmsStatusCode enum is obsolete. " +
        "References to it should be switched to the new Vonage.Messaging.SmsStatusCode enum.")]
    public enum SmsStatusCode
    {
        Success = 0,
        Throttled = 1,
        MissingParameters = 2,
        InvalidParameters = 3,
        InvalidCredentials = 4,
        InternalError = 5,
        InvalidMessage = 6,
        NumberBarred = 7,
        PartnerAccountBarred = 8,
        PartnerQuotaViolation = 9,
        TooManyExistingBinds = 10,
        AccountNotEnabledForHttp = 11,
        MessageTooLong = 12,
        InvalidSignature = 14,
        InvalidSenderAddress = 15,
        InvalidNetworkCode = 22,
        InvalidCallbackUrl = 23,
        NonWhiteListedDestination = 29,
        SignatureAndApiSeretDisallowed = 32,
        NumberDeactivated = 33
    }
}
