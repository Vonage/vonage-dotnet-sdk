using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexmo.Api.Voice
{
    public class DtmfCommand
    {
        /// <summary>
        /// The array of digits to send to the call
        /// </summary>
        public string digits { get; set; }
    }
}
