using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexmo.Api.Test.Unit
{
    [TestClass]
    public class ConversionTest 
    {
        public void Should_submit()
        {
            Conversion.ConversionType = "sms";
            Conversion.SubmitConversion(new Conversion.ConversionRequest
            {
                MessageId = "",
                Delivered = true,
                Timestamp = DateTime.UtcNow
            });


        }
    }
}
