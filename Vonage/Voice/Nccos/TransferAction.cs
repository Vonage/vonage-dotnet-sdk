#region
using System.Collections.Generic;
using Newtonsoft.Json;
#endregion

namespace Vonage.Voice.Nccos;

/// <summary>
///     Moves the call legs from a current conversation into another existing conversation.
/// </summary>
/// <param name="conversationId">Target conversation ID, defined as string.</param>
public class TransferAction(string conversationId) : NccoAction
{
    private List<string> canHear;
    private List<string> canSpeak;

    /// <summary>
    ///     Target conversation ID, defined as string.
    /// </summary>
    [JsonProperty("conversationId", Order = 1)]
    public string ConversationId { get; } = conversationId;

    /// <inheritdoc />
    public override ActionType Action => ActionType.Transfer;

    /// <summary>
    ///     Set to true to mute the participant. The audio from the participant will not be played to the conversation and will
    ///     not be recorded. When using canSpeak, the mute parameter is not supported.
    /// </summary>
    [JsonProperty("mute", Order = 4)]
    public bool Muted { get; private set; }

    /// <summary>
    ///     A list of leg UUIDs that this participant can hear, defined as an array of strings. If not provided, the
    ///     participant can hear everyone. If an empty list is provided, the participant will not hear any other participants.
    /// </summary>
    [JsonProperty("canHear", Order = 2)]
    public IEnumerable<string> CanHear => this.canHear;

    /// <summary>
    ///     A list of leg UUIDs that this participant can be heard by, defined as an array of strings. If not provided, the
    ///     participant can be heard by everyone. If an empty list is provided, the participant will not be heard by anyone.
    /// </summary>
    [JsonProperty("canSpeak", Order = 3)]
    public IEnumerable<string> CanSpeak => this.canSpeak;

    /// <summary>
    ///     The participant will not hear any other participants.
    /// </summary>
    /// <returns>The transfer action.</returns>
    public TransferAction CanHearNoParticipants()
    {
        this.canHear = [];
        return this;
    }

    /// <summary>
    ///     Adds a specific participant that this participant can hear.
    /// </summary>
    /// <param name="legId">The leg UUID.</param>
    /// <returns>The transfer action.</returns>
    public TransferAction CanHearParticipant(string legId)
    {
        this.canHear ??= [];
        this.canHear.Add(legId);
        return this;
    }

    /// <summary>
    ///     The participant will not be heard by any other participants.
    /// </summary>
    /// <returns>The transfer action.</returns>
    public TransferAction CanSpeakToNoParticipants()
    {
        this.canSpeak = [];
        return this;
    }

    /// <summary>
    ///     Adds a specific participant that this participant can speak to.
    /// </summary>
    /// <param name="legId">The leg UUID.</param>
    /// <returns>The transfer action.</returns>
    public TransferAction CanSpeakToParticipant(string legId)
    {
        this.canSpeak ??= [];
        this.canSpeak.Add(legId);
        return this;
    }

    /// <summary>
    ///     Mute the participant.
    /// </summary>
    /// <returns>The transfer action.</returns>
    public TransferAction Mute()
    {
        this.Muted = true;
        return this;
    }
}