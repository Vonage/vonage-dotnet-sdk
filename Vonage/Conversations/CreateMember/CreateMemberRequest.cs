using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;
using Vonage.Serialization;

namespace Vonage.Conversations.CreateMember;

/// <inheritdoc />
public readonly struct CreateMemberRequest : IVonageRequest
{
    /// <summary>
    /// 
    /// </summary>
    public enum AvailableStates
    {
        /// <summary>
        /// 
        /// </summary>
        [Description("invited")] Invited,
        
        /// <summary>
        /// 
        /// </summary>
        [Description("joined")] Joined,
    };
    
    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Post, this.GetEndpointPath())
        .WithContent(this.GetRequestContent())
        .Build();
    
    /// <inheritdoc />
    public string GetEndpointPath() => $"/v1/conversations/{this.ConversationId}/members";
    
    /// <summary>
    ///     Initializes a builder for CreateMemberRequest.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForConversationId Build() => new CreateMemberRequestBuilder();
    
    [JsonConverter(typeof(EnumDescriptionJsonConverter<AvailableStates>))]
    [JsonPropertyOrder(0)]
    public AvailableStates State { get; internal init; }
    
    [JsonIgnore] public string ConversationId { get; internal init; }
    
    [JsonPropertyOrder(1)] public MemberUser User { get; internal init; }
    
    [JsonPropertyOrder(2)] public MemberChannelV2 Channel { get; internal init; }
    
    [JsonConverter(typeof(MaybeJsonConverter<string>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<string> KnockingId { get; internal init; }
    
    [JsonConverter(typeof(MaybeJsonConverter<string>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<string> InvitingMemberId { get; internal init; }
    
    [JsonConverter(typeof(MaybeJsonConverter<string>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<string> From { get; internal init; }
    
    [JsonConverter(typeof(MaybeJsonConverter<MemberMedia>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<MemberMedia> Media { get; internal init; }
    
    private StringContent GetRequestContent() =>
        new StringContent(JsonSerializerBuilder.BuildWithSnakeCase().SerializeObject(this), Encoding.UTF8,
            "application/json");
}

public record MemberUser(string Id, string Name);