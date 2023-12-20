using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Serialization;

namespace Vonage.Conversations.UpdateConversation;

/// <inheritdoc />
public readonly struct UpdateConversationRequest : IVonageRequest
{
    /// <summary>
    /// </summary>
    public Maybe<Callback> Callback { get; internal init; }

    /// <summary>
    /// </summary>
    public string ConversationId { get; internal init; }

    /// <summary>
    /// </summary>
    public Maybe<string> DisplayName { get; internal init; }

    /// <summary>
    /// </summary>
    public Maybe<Uri> ImageUrl { get; internal init; }

    /// <summary>
    /// </summary>
    public Maybe<string> Name { get; internal init; }

    /// <summary>
    /// </summary>
    public Maybe<IEnumerable<INumber>> Numbers { get; internal init; }

    /// <summary>
    /// </summary>
    public Maybe<Properties> Properties { get; internal init; }

    /// <summary>
    ///     Initializes a builder for UpdateConversationRequest.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForConversationId Build() => new UpdateConversationRequestBuilder();

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Put, this.GetEndpointPath())
        .WithContent(this.GetRequestContent())
        .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/v1/conversations/{this.ConversationId}";

    private StringContent GetRequestContent()
    {
        var serializer = JsonSerializerBuilder.BuildWithSnakeCase();
        var values = new Dictionary<string, object>();
        this.Name.IfSome(value => values.Add("name", value));
        this.DisplayName.IfSome(value => values.Add("display_name", value));
        this.ImageUrl.IfSome(value => values.Add("image_url", value));
        this.Properties.IfSome(value => values.Add("properties", value));
        this.Numbers.IfSome(value => values.Add("numbers", value
            .Select(workflow => workflow.Serialize(serializer))
            .Select(serializedString => serializer.DeserializeObject<dynamic>(serializedString))
            .Select(result => result.IfFailure(default))));
        this.Callback.IfSome(value => values.Add("callback", value));
        return new StringContent(serializer.SerializeObject(values), Encoding.UTF8, "application/json");
    }
}