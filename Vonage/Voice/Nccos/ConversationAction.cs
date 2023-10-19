﻿using Newtonsoft.Json;

namespace Vonage.Voice.Nccos;

/// <summary>
///     Represents a NCCO Conversation.
/// </summary>
public class ConversationAction : NccoAction
{
    /// <summary>
    /// Returns the Conversation ActionType.
    /// </summary>
    [JsonProperty("action", Order = 0)]
    public override ActionType Action => ActionType.Conversation;

    /// <summary>
    ///     A list of leg UUIDs that this participant can hear.
    ///     If not provided, the participant can hear everyone.
    ///     If an empty list is provided, the participant will not hear any other participants
    /// </summary>
    [JsonProperty("canHear", Order = 7)]
    public string[] CanHear { get; set; }

    /// <summary>
    ///     A list of leg UUIDs that this participant can be heard by.
    ///     If not provided, the participant can be heard by everyone.
    ///     If an empty list is provided, the participant will not be heard by anyone
    /// </summary>
    [JsonProperty("canSpeak", Order = 6)]
    public string[] CanSpeak { get; set; }

    /// <summary>
    ///     Specifies whether a moderated conversation ends when the moderator hangs up.
    ///     This is set to false by default, which means that the conversation only ends
    ///     when the last remaining participant hangs up, regardless of whether the moderator
    ///     is still on the call. Set endOnExit to true to terminate the conversation when the
    ///     moderator hangs up.
    /// </summary>
    [JsonProperty("endOnExit", Order = 4)]
    public bool EndOnExit { get; set; }

    /// <summary>
    ///     Method to use on the webhooks for the conversation
    /// </summary>
    [JsonProperty("eventMethod")]
    public string EventMethod { get; set; }

    /// <summary>
    ///     Url to receive webhooks at for the conversation
    /// </summary>
    [JsonProperty("eventUrl")]
    public string[] EventUrl { get; set; }

    /// <summary>
    ///     A URL to the mp3 file to stream to participants until the conversation starts.
    ///     By default the conversation starts when the first person calls the virtual number
    ///     associated with your Voice app. To stream this mp3 before the moderator joins the
    ///     conversation, set startOnEnter to false for all users other than the moderator.
    /// </summary>
    [JsonProperty("musicOnHoldUrl", Order = 2)]
    public string[] MusicOnHoldUrl { get; set; }

    /// <summary>
    ///     The name of the Conversation room. Names are namespaced to the application level.
    /// </summary>
    [JsonProperty("name", Order = 1)]
    public string Name { get; set; }

    /// <summary>
    ///     Set to true to record this conversation. For standard conversations,
    ///     recordings start when one or more attendees connects to the conversation.
    ///     For moderated conversations, recordings start when the moderator joins.
    ///     That is, when an NCCO is executed for the named conversation where startOnEnter is set
    ///     to true. When the recording is terminated, the URL you download the recording
    ///     from is sent to the event URL. By default audio is recorded in MP3 format.
    ///     See the recording guide for more details
    /// </summary>
    [JsonProperty("record", Order = 5)]
    public bool Record { get; set; }

    /// <summary>
    ///     The default value of true ensures that the conversation starts when this caller
    ///     joins conversation name. Set to false for attendees in a moderated conversation.
    /// </summary>
    [JsonProperty("startOnEnter", DefaultValueHandling = DefaultValueHandling.Include, Order = 3)]
    public bool StartOnEnter { get; set; }
}