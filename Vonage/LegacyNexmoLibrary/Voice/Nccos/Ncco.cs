using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nexmo.Api.Voice.Nccos
{
    [System.Obsolete("The Nexmo.Api.Voice.Nccos.Ncco class is obsolete. " +
           "References to it should be switched to the new Nexmo.Api.Voice.Nccos.Ncco class.")]
    [JsonConverter(typeof(NccoConverter))]
    public class Ncco
    {
        public List<NccoAction> Actions { get; set; } = new List<NccoAction>();

        public Ncco(params NccoAction[] actions)
        {
            Actions.AddRange(actions);
        }

        public override string ToString()
        {
            var settings = new JsonSerializerSettings()
                {
                    DefaultValueHandling = DefaultValueHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore
                };
            return JsonConvert.SerializeObject(Actions, settings);
        }
    }
}
