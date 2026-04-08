#region
using System.Collections.Generic;
using Newtonsoft.Json;
using Vonage.Serialization;
#endregion

namespace Vonage.Voice.Nccos;

/// <summary>
///     Represents a Nexmo Call Control Object (NCCO) — an ordered sequence of actions that control the flow of a voice call. Actions are executed in order and can include talk, stream, record, connect, input, notify, wait, and transfer.
/// </summary>
public class Ncco : List<NccoAction>
{
    /// <summary>
    ///     Creates a new NCCO with the specified sequence of actions.
    /// </summary>
    /// <param name="actions">The ordered NCCO actions to execute during the call.</param>
    public Ncco(params NccoAction[] actions)
        : base(actions)
    {
    }

    /// <summary>
    ///     Serializes the NCCO to its JSON representation for use in API requests or answer webhooks.
    /// </summary>
    /// <returns>The JSON string representing this NCCO.</returns>
    public override string ToString() => JsonConvert.SerializeObject(this, VonageSerialization.SerializerSettings);
}