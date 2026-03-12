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
    ///     A single phone number you want insights on, starting with the country code. You may optionally include a leading +,
    ///     but do not use 00 at the beginning. Ideally, the number should follow the E.164 format. However, the API is
    ///     designed to extract the phone number even if the input string contains alphanumeric characters, spaces, or symbols
    ///     like brackets.
    /// </summary>
    [MandatoryWithParsing(0, nameof(ParsePhoneNumber))]
    public PhoneNumber PhoneNumber { get; internal init; }

    /// <summary>
    ///     Specifies the reason for the request. This property is required only for Insights that use the Network Registry.
    ///     For a Network Registry of type Production, the value must match one of the network profile purposes associated with
    ///     your application. For a Network Registry of type Playground, the value must be "FraudPreventionAndDetection".
    /// </summary>
    [Optional]
    public Maybe<string> Purpose { get; internal init; }

    /// <summary>
    ///     Request the format insight.
    /// </summary>
    [OptionalBoolean(false, "WithFormat")]
    public bool Format { get; internal init; }

    /// <summary>
    ///     Request the original_carrier insight.
    /// </summary>
    [OptionalBoolean(false, "WithOriginalCarrier")]
    public bool OriginalCarrier { get; internal init; }

    /// <summary>
    ///     Request the current_carrier insight.
    /// </summary>
    [OptionalBoolean(false, "WithCurrentCarrier")]
    public bool CurrentCarrier { get; internal init; }

    /// <summary>
    ///     Request the sim_swap insight.
    /// </summary>
    [Optional]
    public Maybe<SimSwapRequest> SimSwap { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Post, "/identity-insights/v1/requests")
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
///     Represents Sim Swap insights.
/// </summary>
/// <param name="Period">Period in hours to be checked for SIM swap.</param>
public record SimSwapRequest(int Period)
{
    internal const int MinimumPeriod = 1;
    internal const int MaximumPeriod = 2400;
}