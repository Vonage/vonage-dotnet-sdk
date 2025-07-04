#region
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Vonage.Common.Serialization;
using Vonage.Serialization;
#endregion

namespace Vonage.Messages;

/// <inheritdoc />
public abstract class MessageRequestBase : IMessage
{
    /// <inheritdoc />
    [JsonPropertyOrder(0)]
    [JsonConverter(typeof(EnumDescriptionJsonConverter<MessagesChannel>))]
    public abstract MessagesChannel Channel { get; }

    /// <inheritdoc />
    [JsonPropertyOrder(5)]
    public string ClientRef { get; set; }

    /// <inheritdoc />
    [JsonPropertyOrder(4)]
    public string From { get; set; }

    /// <inheritdoc />
    [JsonPropertyOrder(1)]
    [JsonConverter(typeof(EnumDescriptionJsonConverter<MessagesMessageType>))]
    public abstract MessagesMessageType MessageType { get; }

    /// <inheritdoc />
    [JsonPropertyOrder(3)]
    public string To { get; set; }

    /// <inheritdoc />
    [JsonPropertyOrder(6)]
    public string WebhookVersion { get; set; }

    /// <inheritdoc />
    [JsonPropertyOrder(7)]
    public Uri WebhookUrl { get; set; }

    /// <inheritdoc />
    [JsonPropertyOrder(99)]
    public List<IMessage> Failover { get; set; }

    /// <inheritdoc />
    public string Serialize()
    {
        var settings = JsonSerializerBuilder.BuildWithSnakeCase().Settings;
        var jsonElement = JsonSerializer.SerializeToElement(this, this.GetType(), settings);
        var dictionary = new Dictionary<string, JsonElement>();
        foreach (var prop in jsonElement.EnumerateObject())
        {
            dictionary[prop.Name] = prop.Value;
        }

        if (this.Failover != null)
        {
            var messages = this.Failover
                .Select(message => JsonSerializer.SerializeToElement(message, message.GetType(), settings))
                .ToArray();
            dictionary["failover"] = JsonSerializer.SerializeToElement(messages);
        }

        return JsonSerializerBuilder.BuildWithSnakeCase().SerializeObject(dictionary);
    }
}