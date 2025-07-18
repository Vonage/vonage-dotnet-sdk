﻿#region
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Serialization;
#endregion

namespace Vonage.Conversations.CreateConversation;

/// <inheritdoc />
public readonly struct CreateConversationRequest : IVonageRequest
{
    /// <summary>
    /// Conversation callback
    /// </summary>
    public Maybe<Callback> Callback { get; internal init; }

    /// <summary>
    /// The public facing name of the conversation
    /// </summary>
    public Maybe<string> DisplayName { get; internal init; }

    /// <summary>
    /// An image URL that you associate with the conversation
    /// </summary>
    public Maybe<Uri> ImageUrl { get; internal init; }

    /// <summary>
    /// Your internal conversation name. Must be unique
    /// </summary>
    public Maybe<string> Name { get; internal init; }

    /// <summary>
    /// Conversation numbers
    /// </summary>
    public Maybe<IEnumerable<INumber>> Numbers { get; internal init; }

    /// <summary>
    /// Conversation properties
    /// </summary>
    public Maybe<Properties> Properties { get; internal init; }

    /// <summary>
    ///     Initializes a builder for CreateConversationRequest.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForOptional Build() => new CreateConversationRequestBuilder();

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Post, "/v1/conversations")
        .WithContent(this.GetRequestContent())
        .Build();

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