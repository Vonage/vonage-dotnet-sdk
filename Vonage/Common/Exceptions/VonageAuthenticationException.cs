#region
using System;
#endregion

namespace Vonage.Common.Exceptions;

/// <summary>
///     Represents an error when processing authentication.
/// </summary>
/// <example>
///     <code><![CDATA[
/// // Thrown when credentials are missing
/// if (string.IsNullOrEmpty(apiKey))
/// {
///     throw VonageAuthenticationException.FromMissingApiKeyOrSecret();
/// }
/// ]]></code>
/// </example>
public class VonageAuthenticationException : Exception
{
    private VonageAuthenticationException(string message) : base(message)
    {
    }

    /// <summary>
    ///     Creates an exception from a custom error message.
    /// </summary>
    /// <param name="failure">The failure message.</param>
    /// <returns>The exception.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// throw VonageAuthenticationException.FromError("Token has expired");
    /// ]]></code>
    /// </example>
    public static VonageAuthenticationException FromError(string failure) =>
        new(failure);

    /// <summary>
    ///     Creates an exception indicating the ApiKey or ApiSecret are missing.
    /// </summary>
    /// <returns>The exception.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// throw VonageAuthenticationException.FromMissingApiKeyOrSecret();
    /// // Message: "API Key or API Secret missing."
    /// ]]></code>
    /// </example>
    public static VonageAuthenticationException FromMissingApiKeyOrSecret() =>
        new("API Key or API Secret missing.");

    /// <summary>
    ///     Creates an exception indicating the ApplicationId or PrivateKeyPath are missing.
    /// </summary>
    /// <returns>The exception.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// throw VonageAuthenticationException.FromMissingApplicationIdOrPrivateKey();
    /// // Message: "AppId or Private Key Path missing."
    /// ]]></code>
    /// </example>
    public static VonageAuthenticationException FromMissingApplicationIdOrPrivateKey() =>
        new("AppId or Private Key Path missing.");
}