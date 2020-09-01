using Newtonsoft.Json;
using System.Collections.Generic;

namespace Vonage.Voice.Nccos
{
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
