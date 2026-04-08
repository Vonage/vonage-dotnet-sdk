#region
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;
using Vonage.Common.Validation;
using Vonage.Serialization;
#endregion

namespace Vonage.Voice.Emergency.CreateAddress;

/// <inheritdoc />
[Builder]
public readonly partial struct CreateAddressRequest : IVonageRequest
{
    private const int MaxNameLength = 32;
    private const int MinNameLength = 2;
    private const int CountryLength = 2;

    /// <summary>
    ///     Sets a friendly name to identify the address (2-32 characters, e.g., "Office HQ").
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithName("Office HQ")
    /// ]]></code>
    /// </example>
    [Optional]
    [JsonPropertyName("address_name")]
    [JsonConverter(typeof(MaybeJsonConverter<string>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<string> Name { get; internal init; }

    /// <summary>
    ///     Sets the first line of the street address.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithFirstAddressLine("123 Main Street")
    /// ]]></code>
    /// </example>
    [Optional]
    [JsonPropertyName("address_line1")]
    [JsonConverter(typeof(MaybeJsonConverter<string>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<string> FirstAddressLine { get; internal init; }

    /// <summary>
    ///     Sets the second line of the street address (e.g., suite or apartment number).
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithSecondAddressLine("Suite 400")
    /// ]]></code>
    /// </example>
    [Optional]
    [JsonPropertyName("address_line2")]
    [JsonConverter(typeof(MaybeJsonConverter<string>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<string> SecondAddressLine { get; internal init; }

    /// <summary>
    ///     Sets the city of the address.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithCity("San Francisco")
    /// ]]></code>
    /// </example>
    [Optional]
    [JsonPropertyName("city")]
    [JsonConverter(typeof(MaybeJsonConverter<string>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<string> City { get; internal init; }

    /// <summary>
    ///     Sets the state, province, or region of the address.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithRegion("CA")
    /// ]]></code>
    /// </example>
    [Optional]
    [JsonPropertyName("region")]
    [JsonConverter(typeof(MaybeJsonConverter<string>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<string> Region { get; internal init; }

    /// <summary>
    ///     The address type. Always "emergency".
    /// </summary>
    [JsonPropertyName("type")]
    public string Type => "emergency";

    /// <summary>
    ///     Sets whether the address is a business or residential location.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithLocation(Address.AddressLocationType.Business)
    /// ]]></code>
    /// </example>
    [Optional]
    [JsonPropertyName("address_location_type")]
    [JsonConverter(typeof(MaybeJsonConverter<Address.AddressLocationType>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<Address.AddressLocationType> Location { get; internal init; }

    /// <summary>
    ///     Sets the postal or ZIP code of the address.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithPostalCode("94105")
    /// ]]></code>
    /// </example>
    [Optional]
    [JsonPropertyName("postal_code")]
    [JsonConverter(typeof(MaybeJsonConverter<string>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<string> PostalCode { get; internal init; }

    /// <summary>
    ///     Sets the two-character country code in ISO 3166-1 alpha-2 format (e.g., "US", "GB"). Must be exactly 2 characters.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithCountry("US")
    /// ]]></code>
    /// </example>
    [Optional]
    [JsonPropertyName("country")]
    [JsonConverter(typeof(MaybeJsonConverter<string>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<string> Country { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Post, "/v1/addresses")
            .WithContent(this.GetRequestContent())
            .Build();

    private StringContent GetRequestContent() =>
        new StringContent(JsonSerializerBuilder.BuildWithCamelCase().SerializeObject(this), Encoding.UTF8,
            "application/json");

    [ValidationRule]
    internal static Result<CreateAddressRequest> VerifyNameMaxLength(CreateAddressRequest request) =>
        request.Name.Match(
            some => InputValidation.VerifyLengthLowerOrEqualThan(request, some, MaxNameLength, nameof(request.Name)),
            () => request);

    [ValidationRule]
    internal static Result<CreateAddressRequest> VerifyNameMinLength(CreateAddressRequest request) =>
        request.Name.Match(
            some => InputValidation.VerifyLengthHigherOrEqualThan(request, some, MinNameLength, nameof(request.Name)),
            () => request);

    [ValidationRule]
    internal static Result<CreateAddressRequest> VerifyCountryLength(CreateAddressRequest request) =>
        request.Country.Match(
            some => InputValidation.VerifyLength(request, some, CountryLength, nameof(request.Country)),
            () => request);
}