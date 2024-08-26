namespace Vonage.Messaging;

/// <summary>
///     Indicates that the class can be signed.
/// </summary>
public interface ISignable
{
    /// <summary>
    ///     The signature to enable verification of the source of this webhook.
    ///     Please see the developer documentation for validating signatures for more information,
    ///     or use one of our published SDKs. Only included if you have signatures enabled
    /// </summary>
    string Sig { get; set; }
}