using Newtonsoft.Json.Converters;

namespace Nexmo.Api.Common
{
    [System.Obsolete("The Nexmo.Api.Common.PageListDateTimeConverter class is obsolete. " +
        "References to it should be switched to the new Vonage.Common.PageListDateTimeConverter class.")]
    public class PageListDateTimeConverter : IsoDateTimeConverter
    {
        public PageListDateTimeConverter()
        {
            base.DateTimeFormat = "yyyy-MM-dd'T'HH:mm:ssZ";            
        }
    }
}
