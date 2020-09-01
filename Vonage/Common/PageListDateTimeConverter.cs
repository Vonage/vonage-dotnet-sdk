using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vonage.Common
{
    public class PageListDateTimeConverter : IsoDateTimeConverter
    {
        public PageListDateTimeConverter()
        {
            base.DateTimeFormat = "yyyy-MM-dd'T'HH:mm:ssZ";            
        }
    }
}
