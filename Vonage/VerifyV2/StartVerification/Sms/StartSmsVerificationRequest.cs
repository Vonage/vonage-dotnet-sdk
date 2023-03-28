using System;
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Monads;

namespace Vonage.VerifyV2.StartVerification.Sms;

/// <inheritdoc />
public struct StartSmsVerificationRequest : IVonageRequest
{
    /// <summary>
    ///     Gets the brand that is sending the verification request.
    /// </summary>
    public string Brand { get; internal init; }

    /// <summary>
    ///     Gets the wait time in seconds between attempts to delivery the verification code.
    /// </summary>
    public int ChannelTimeout { get; internal init; }

    /// <summary>
    ///     Gets the client reference.
    /// </summary>
    public Maybe<string> ClientReference { get; internal init; }

    /// <summary>
    ///     Gets the length of the code to send to the user
    /// </summary>
    public int CodeLength { get; internal init; }

    /// <summary>
    ///     Gets the request language.
    /// </summary>
    public Locale Locale { get; internal init; }

    /// <summary>
    ///     Gets verification workflows.
    /// </summary>
    public Workflow[] Workflows { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => throw new NotImplementedException();

    /// <inheritdoc />
    public string GetEndpointPath() => "/verify";
}

/// <summary>
///     Represents a verification workflow.
/// </summary>
/// <param name="Channel">The channel name.</param>
/// <param name="To">
///     The phone number to contact, in the E.164 format. Don't use a leading + or 00 when entering a phone
///     number, start with the country code, for example, 447700900000.
/// </param>
/// <param name="Hash">Optional Android Application Hash Key for automatic code detection on a user's device.</param>
public record Workflow(string Channel, string To, Maybe<string> Hash)
{
    /// <inheritdoc />
    public Workflow(string channel, string to)
        : this(channel, to, Maybe<string>.None)
    {
    }
}