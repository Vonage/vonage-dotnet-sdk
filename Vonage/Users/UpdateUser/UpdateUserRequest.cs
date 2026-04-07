#region
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;
using Vonage.Serialization;
#endregion

namespace Vonage.Users.UpdateUser;

/// <summary>
///     Represents a request to update an existing user's profile information and communication channels in the Vonage platform.
/// </summary>
public readonly struct UpdateUserRequest : IVonageRequest
{
    /// <summary>
    ///     The communication channels configured for the user. This replaces all existing channels with the new configuration.
    /// </summary>
    [JsonPropertyOrder(4)]
    public UserChannels Channels { get; internal init; }

    /// <summary>
    ///     A human-readable display name for the user. Unlike the Name property, this does not need to be unique.
    /// </summary>
    [JsonConverter(typeof(MaybeJsonConverter<string>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonPropertyOrder(1)]
    public Maybe<string> DisplayName { get; internal init; }

    /// <summary>
    ///     The unique identifier of the user to update (e.g., "USR-12345678-1234-1234-1234-123456789012").
    /// </summary>
    [JsonIgnore]
    public string Id { get; internal init; }

    /// <summary>
    ///     A URL pointing to an image associated with the user's profile, such as an avatar or profile picture.
    /// </summary>
    [JsonConverter(typeof(MaybeJsonConverter<Uri>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonPropertyOrder(2)]
    public Maybe<Uri> ImageUrl { get; internal init; }

    /// <summary>
    ///     A unique name for identifying the user within the Vonage platform. Must not be empty if provided.
    /// </summary>
    [JsonConverter(typeof(MaybeJsonConverter<string>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonPropertyOrder(0)]
    public Maybe<string> Name { get; internal init; }

    /// <summary>
    ///     Custom properties associated with the user, stored as key-value pairs for application-specific data.
    /// </summary>
    [JsonPropertyOrder(3)]
    public UserProperty Properties { get; internal init; }

    /// <summary>
    ///     Initializes a builder for creating an UpdateUserRequest. The user ID is required.
    /// </summary>
    /// <returns>A builder instance that requires the user ID to be set first.</returns>
    public static IBuilderForId Build() => new UpdateUserRequestBuilder();

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(new HttpMethod("PATCH"), $"/v1/users/{this.Id}")
        .WithContent(this.GetRequestContent())
        .Build();

    private StringContent GetRequestContent() =>
        new(JsonSerializerBuilder.BuildWithSnakeCase().SerializeObject(this),
            Encoding.UTF8,
            "application/json");
}