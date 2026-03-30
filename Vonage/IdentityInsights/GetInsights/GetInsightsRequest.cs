#region
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
using Vonage.Serialization;
#endregion

namespace Vonage.IdentityInsights.GetInsights;

/// <summary>
///     Represents a request to retrieve identity insights.
/// </summary>
[Builder]
public readonly partial struct GetInsightsRequest : IVonageRequest
{
    /// <summary>
    ///     Sets the phone number to retrieve insights for. The number should follow E.164 format (e.g., +14155552671).
    ///     You may optionally include a leading +, but do not use 00 at the beginning. The API can extract the phone number
    ///     even if the input contains alphanumeric characters, spaces, or symbols like brackets.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithPhoneNumber("+14155552671")
    /// ]]></code>
    /// </example>
    [MandatoryWithParsing(0, nameof(ParsePhoneNumber))]
    public PhoneNumber PhoneNumber { get; internal init; }

    /// <summary>
    ///     Sets the purpose/reason for the request. Required only for Insights that use the Network Registry.
    ///     For Production Network Registry, the value must match one of the network profile purposes associated with your application.
    ///     For Playground Network Registry, the value must be "FraudPreventionAndDetection".
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithPurpose("FraudPreventionAndDetection")
    /// ]]></code>
    /// </example>
    [Optional]
    public Maybe<string> Purpose { get; internal init; }

    /// <summary>
    ///     Includes phone number format validation in the response. Returns details such as international and national formats.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithFormat()
    /// ]]></code>
    /// </example>
    [OptionalBoolean(false, "WithFormat")]
    public bool Format { get; internal init; }

    /// <summary>
    ///     Includes original carrier information in the response. Returns the carrier that originally owned the phone number.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithOriginalCarrier()
    /// ]]></code>
    /// </example>
    [OptionalBoolean(false, "WithOriginalCarrier")]
    public bool OriginalCarrier { get; internal init; }

    /// <summary>
    ///     Includes current carrier information in the response. Returns the carrier that currently owns the phone number.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithCurrentCarrier()
    /// ]]></code>
    /// </example>
    [OptionalBoolean(false, "WithCurrentCarrier")]
    public bool CurrentCarrier { get; internal init; }

    /// <summary>
    ///     Includes SIM swap detection in the response. Use this to detect if the SIM card has been changed recently.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithSimSwap(new SimSwapRequest(Period: 24))
    /// ]]></code>
    /// </example>
    [Optional]
    public Maybe<SimSwapRequest> SimSwap { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Post, "identity-insights/v1/requests")
        .WithContent(this.GetRequestContent())
        .Build();

    private StringContent GetRequestContent()
    {
        var values = new Dictionary<string, object> {{"phone_number", this.PhoneNumber.Number}};
        this.Purpose.IfSome(some => values.Add("purpose", some));
        values.Add("insights", this.BuildInsights());
        return new StringContent(JsonSerializerBuilder.BuildWithSnakeCase().SerializeObject(values), Encoding.UTF8,
            "application/json");
    }

    private Dictionary<string, object> BuildInsights()
    {
        var insights = new Dictionary<string, object>();
        if (this.Format)
        {
            insights.Add("format", new { });
        }

        this.SimSwap.IfSome(some => insights.Add("sim_swap", new {some.Period}));
        if (this.OriginalCarrier)
        {
            insights.Add("original_carrier", new { });
        }

        if (this.CurrentCarrier)
        {
            insights.Add("current_carrier", new { });
        }

        return insights;
    }

    [ValidationRule]
    internal static Result<GetInsightsRequest> VerifyMinimumSimSwapPeriod(GetInsightsRequest request) =>
        request.SimSwap.Match(
            insights => InputValidation.VerifyHigherOrEqualThan(request, insights.Period, SimSwapRequest.MinimumPeriod,
                nameof(insights.Period)), () => request);

    [ValidationRule]
    internal static Result<GetInsightsRequest> VerifyMaximumSimSwapPeriod(GetInsightsRequest request) =>
        request.SimSwap.Match(
            insights => InputValidation.VerifyLowerOrEqualThan(request, insights.Period, SimSwapRequest.MaximumPeriod,
                nameof(insights.Period)), () => request);

    internal static Result<PhoneNumber> ParsePhoneNumber(string value) => PhoneNumber.Parse(value);
}

/// <summary>
///     Represents SIM swap insight configuration.
/// </summary>
/// <param name="Period">
///     The period in hours to check for SIM swap activity. Must be between 1 and 2400 hours (100 days).
///     For example, a value of 24 checks if the SIM was swapped in the last 24 hours.
/// </param>
public record SimSwapRequest(int Period)
{
    internal const int MinimumPeriod = 1;
    internal const int MaximumPeriod = 2400;
}