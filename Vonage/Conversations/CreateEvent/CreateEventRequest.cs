#region
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;
using Vonage.Serialization;
#endregion

namespace Vonage.Conversations.CreateEvent;

/// <inheritdoc />
public readonly struct CreateEventRequest : IVonageRequest
{
    /// <summary>
    ///     The conversation Id.
    /// </summary>
    [JsonIgnore]
    public string ConversationId { get; internal init; }

    /// <summary>
    ///     The type of event.
    /// </summary>
    [JsonPropertyOrder(0)]
    public string Type { get; internal init; }

    /// <summary>
    ///     The member Id.
    /// </summary>
    [JsonPropertyOrder(1)]
    [JsonConverter(typeof(MaybeJsonConverter<string>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<string> From { get; internal init; }

    /// <summary>
    ///     The event body.
    /// </summary>
    [JsonPropertyOrder(2)]
    public JsonElement Body { get; internal init; }

    /// <summary>
    ///     Initializes a builder.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForConversationId Build() => new CreateEventRequestBuilder();

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Post, $"/v1/conversations/{this.ConversationId}/events")
        .WithContent(this.GetRequestContent())
        .Build();

    private StringContent GetRequestContent() =>
        new StringContent(JsonSerializerBuilder.BuildWithSnakeCase().SerializeObject(this), Encoding.UTF8,
            "application/json");
}