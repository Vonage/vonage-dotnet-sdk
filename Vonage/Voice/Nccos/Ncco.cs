using System.Collections.Generic;
using Newtonsoft.Json;

namespace Vonage.Voice.Nccos
{
    public class Ncco : List<NccoAction>
    {
        public Ncco(params NccoAction[] actions)
            :base(actions)
        { }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Serialization.VonageSerialization.SerializerSettings);
        }
    }
}
