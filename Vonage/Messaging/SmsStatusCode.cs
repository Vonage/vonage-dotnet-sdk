namespace Vonage.Messaging;

/// <summary>
///     Represents the status codes returned by the Vonage SMS API when sending a message.
/// </summary>
public enum SmsStatusCode
{
    /// <summary>
    ///     The message was successfully accepted for delivery.
    /// </summary>
    Success = 0,

    /// <summary>
    ///     The request was rate-limited. Retry after a short delay.
    /// </summary>
    Throttled = 1,

    /// <summary>
    ///     Required parameters are missing from the request.
    /// </summary>
    MissingParameters = 2,

    /// <summary>
    ///     One or more parameters have invalid values.
    /// </summary>
    InvalidParameters = 3,

    /// <summary>
    ///     The API key or secret is invalid.
    /// </summary>
    InvalidCredentials = 4,

    /// <summary>
    ///     An internal error occurred. Retry the request.
    /// </summary>
    InternalError = 5,

    /// <summary>
    ///     The message content is invalid or cannot be sent.
    /// </summary>
    InvalidMessage = 6,

    /// <summary>
    ///     The destination number has been barred from receiving messages.
    /// </summary>
    NumberBarred = 7,

    /// <summary>
    ///     Your account has been suspended.
    /// </summary>
    PartnerAccountBarred = 8,

    /// <summary>
    ///     You have exceeded your account quota.
    /// </summary>
    PartnerQuotaViolation = 9,

    /// <summary>
    ///     Too many simultaneous connections to the API.
    /// </summary>
    TooManyExistingBinds = 10,

    /// <summary>
    ///     Your account is not enabled for HTTP API access.
    /// </summary>
    AccountNotEnabledForHttp = 11,

    /// <summary>
    ///     The message body exceeds the maximum allowed length.
    /// </summary>
    MessageTooLong = 12,

    /// <summary>
    ///     The request signature is invalid.
    /// </summary>
    InvalidSignature = 14,

    /// <summary>
    ///     The sender address (from field) is invalid.
    /// </summary>
    InvalidSenderAddress = 15,

    /// <summary>
    ///     The network code specified is invalid.
    /// </summary>
    InvalidNetworkCode = 22,

    /// <summary>
    ///     The callback URL is invalid or unreachable.
    /// </summary>
    InvalidCallbackUrl = 23,

    /// <summary>
    ///     The destination number is not on the whitelist for this account.
    /// </summary>
    NonWhiteListedDestination = 29,

    /// <summary>
    ///     Using both signature and API secret authentication is not allowed.
    /// </summary>
    SignatureAndApiSeretDisallowed = 32,

    /// <summary>
    ///     The destination number has been deactivated.
    /// </summary>
    NumberDeactivated = 33,
}