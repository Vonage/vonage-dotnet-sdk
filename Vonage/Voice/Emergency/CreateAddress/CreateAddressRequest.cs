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
    /// </summary>
    [Optional]
    [JsonPropertyName("address_name")]
    [JsonConverter(typeof(MaybeJsonConverter<string>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<string> Name { get; internal init; }

    /// <summary>
    /// </summary>
    [Optional]
    [JsonPropertyName("address_line1")]
    [JsonConverter(typeof(MaybeJsonConverter<string>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<string> FirstAddressLine { get; internal init; }

    /// <summary>
    /// </summary>
    [Optional]
    [JsonPropertyName("address_line2")]
    [JsonConverter(typeof(MaybeJsonConverter<string>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<string> SecondAddressLine { get; internal init; }

    /// <summary>
    /// </summary>
    [Optional]
    [JsonPropertyName("city")]
    [JsonConverter(typeof(MaybeJsonConverter<string>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<string> City { get; internal init; }

    /// <summary>
    /// </summary>
    [Optional]
    [JsonPropertyName("region")]
    [JsonConverter(typeof(MaybeJsonConverter<string>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<string> Region { get; internal init; }

    /// <summary>
    /// </summary>
    [JsonPropertyName("type")]
    public string Type => "emergency";

    /// <summary>
    /// </summary>
    [Optional]
    [JsonPropertyName("address_location_type")]
    [JsonConverter(typeof(MaybeJsonConverter<Address.AddressLocationType>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<Address.AddressLocationType> Location { get; internal init; }

    /// <summary>
    /// </summary>
    [Optional]
    [JsonPropertyName("postal_code")]
    [JsonConverter(typeof(MaybeJsonConverter<string>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<string> PostalCode { get; internal init; }

    /// <summary>
    /// </summary>
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