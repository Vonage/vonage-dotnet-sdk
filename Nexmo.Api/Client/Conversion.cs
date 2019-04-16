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

        public void SubmitConversion (Api.Conversion.ConversionRequest request, Credentials creds = null)
        {
            Api.Conversion.SubmitConversion(request, creds);
        }
    }
}
