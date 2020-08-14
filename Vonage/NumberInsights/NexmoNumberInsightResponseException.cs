using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vonage.NumberInsights
{
    public class VonageNumberInsightResponseException : VonageException
    {
        public VonageNumberInsightResponseException(string message) : base(message) { }

        public NumberInsightResponseBase Response { get; set; }
    }
}
