using System.ComponentModel;

namespace Vonage.Applications.Capabilities;

/// <summary>
///     Represents the HTTP method used to deliver webhook events.
/// </summary>
public enum WebhookMethod
{
    /// <summary>HTTP GET.</summary>
    [Description("GET")] Get,

    /// <summary>HTTP POST.</summary>
    [Description("POST")] Post
}
