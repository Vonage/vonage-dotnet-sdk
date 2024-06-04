using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;
using Vonage.Serialization;

namespace Vonage.Conversations.UpdateMember;

/// <inheritdoc />
public readonly struct UpdateMemberRequest : IVonageRequest
{
    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(new HttpMethod("PATCH"), this.GetEndpointPath())
        .WithContent(this.GetRequestContent())
        .Build();
    
    /// <summary>
    ///     Initializes a builder for UpdateMemberRequest.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForConversationId Build() => new UpdateMemberRequestBuilder();
    
    /// <inheritdoc />
    public string GetEndpointPath() => $"/v1/conversations/{this.ConversationId}/members/{this.MemberId}";
    
    private StringContent GetRequestContent() =>
        new StringContent(JsonSerializerBuilder.BuildWithSnakeCase().SerializeObject(this), Encoding.UTF8,
            "application/json");
    
    /// <summary>
    /// </summary>
    [JsonIgnore]
    public string ConversationId { get; internal init; }
    
    /// <summary>
    /// </summary>
    [JsonIgnore]
    public string MemberId { get; internal init; }
    
    /// <summary>
    ///     Invite or join a member to a conversation
    /// </summary>
    [JsonConverter(typeof(EnumDescriptionJsonConverter<AvailableStates>))]
    [JsonPropertyOrder(0)]
    public AvailableStates State { get; internal init; }
    
    /// <summary>
    /// </summary>
    [JsonPropertyOrder(1)]
    [JsonConverter(typeof(MaybeJsonConverter<string>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<string> From { get; internal init; }
    
    /// <summary>
    /// </summary>
    [JsonConverter(typeof(MaybeJsonConverter<Reason>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<Reason> Reason { get; internal init; }
    
    /// <summary>
    /// </summary>
    public enum AvailableStates
    {
        /// <summary>
        /// </summary>
        [Description("left")] Left,
        
        /// <summary>
        /// </summary>
        [Description("joined")] Joined,
    }
}

/// <summary>
/// </summary>
/// <param name="Code"></param>
/// <param name="Text"></param>
public record Reason(string Code, string Text);