#region
using System.Collections.Generic;
using Newtonsoft.Json;
using Vonage.Serialization;
#endregion

namespace Vonage.Voice.Nccos;

/// <summary>
/// </summary>
public class Ncco : List<NccoAction>
{
    /// <summary>
    /// </summary>
    /// <param name="actions"></param>
    public Ncco(params NccoAction[] actions)
        : base(actions)
    {
    }

    /// <summary>
    ///     Converts the NCCO to JSON
    /// </summary>
    /// <returns></returns>
    public override string ToString() => JsonConvert.SerializeObject(this, VonageSerialization.SerializerSettings);
}