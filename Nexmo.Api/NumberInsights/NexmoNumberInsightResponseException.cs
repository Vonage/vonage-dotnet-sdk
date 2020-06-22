using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexmo.Api.NumberInsights
{
    public class NexmoNumberInsightResponseException : NexmoException
    {
        public NexmoNumberInsightResponseException(string message) : base(message) { }

        public NumberInsightResponseBase Response { get; set; }
    }
}
