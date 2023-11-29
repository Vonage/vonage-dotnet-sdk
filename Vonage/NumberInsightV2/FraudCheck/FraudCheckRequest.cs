using System;
using System.Collections.Generic;
using System.Net.Http;
using Vonage.Common.Client;

namespace Vonage.NumberInsightV2.FraudCheck;

/// <inheritdoc />
public readonly struct FraudCheckRequest : IVonageRequest
{
    /// <summary>
    ///     The insight(s) you need, at least one of: fraud_score and sim_swap.
    /// </summary>
    public IEnumerable<string> Insights { get; internal init; }

    /// <summary>
    ///     A single phone number that you need insight about in the E.164 format. Don't use a leading + or 00 when entering a
    ///     phone number, start with the country code, e.g. 447700900000.
    /// </summary>
    public string Phone { get; internal init; }

    /// <summary>
    ///     Accepted value is “phone” when a phone number is provided.
    /// </summary>
    public string Type => "phone";

    /// <summary>
    ///     Initializes a builder for FraudCheckRequest.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForPhone Build() => new FraudCheckRequestBuilder();

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => throw new NotImplementedException();

    /// <inheritdoc />
    public string GetEndpointPath() => throw new NotImplementedException();
}