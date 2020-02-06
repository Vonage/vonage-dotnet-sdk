using Nexmo.Api.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexmo.Api.ClientMethods
{
    public class Conversion
    {
        public Credentials Credentials { get; set; }
        public Conversion(Credentials creds)
        {
            Credentials = creds;
        }

        /// <summary>
        /// Let's Nexmo know that a 2fa message was received successfully
        /// </summary>
        /// <param name="request"></param>
        /// <param name="creds"></param>
        /// <exception cref="NexmoHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        public void SubmitConversion (Api.Conversion.ConversionRequest request, Credentials creds = null)
        {
            Api.Conversion.SubmitConversion(request, creds);
        }
    }
}
