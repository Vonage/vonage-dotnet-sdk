using System;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;
using Vonage.Serialization;

namespace Vonage.Users.UpdateUser;

/// <inheritdoc />
public readonly struct UpdateUserRequest : IVonageRequest
{
    /// <summary>
    ///     User channels.
    /// </summary>
    [JsonPropertyOrder(4)]
    public UserChannels Channels { get; internal init; }

    /// <summary>
    ///     A string to be displayed as user name. It does not need to be unique.
    /// </summary>
    [JsonConverter(typeof(MaybeJsonConverter<string>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonPropertyOrder(1)]
    public Maybe<string> DisplayName { get; internal init; }

    /// <summary>
    ///     Unique ID for a user.
    /// </summary>
    [JsonIgnore]
    public string Id { get; internal init; }

    /// <summary>
    ///     An image URL that you associate with the user
    /// </summary>
    [JsonConverter(typeof(MaybeJsonConverter<Uri>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonPropertyOrder(2)]
    public Maybe<Uri> ImageUrl { get; internal init; }

    /// <summary>
    ///     Unique name for a user.
    /// </summary>
    [JsonConverter(typeof(MaybeJsonConverter<string>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonPropertyOrder(0)]
    public Maybe<string> Name { get; internal init; }

    /// <summary>
    ///     User properties.
    /// </summary>
    [JsonPropertyOrder(3)]
    public UserProperty Properties { get; internal init; }

    /// <summary>
    ///     Initializes a builder.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForId Build() => new UpdateUserRequestBuilder();

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(new HttpMethod("PATCH"), this.GetEndpointPath())
        .WithContent(this.GetRequestContent())
        .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/v1/users/{this.Id}";

    private StringContent GetRequestContent() =>
        new(JsonSerializerBuilder.BuildWithSnakeCase().SerializeObject(this),
            Encoding.UTF8,
            "application/json");
}