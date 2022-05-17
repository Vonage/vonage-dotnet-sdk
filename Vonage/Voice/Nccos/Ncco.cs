using System.Collections.Generic;

namespace Vonage.Voice.Nccos
{
    public class Ncco : List<NccoAction>
    {
        public Ncco(params NccoAction[] actions)
            :base(actions)
        { }
    }
}
