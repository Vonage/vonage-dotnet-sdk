using Newtonsoft.Json.Converters;

namespace Vonage.Common;

public class PageListDateTimeConverter : IsoDateTimeConverter
{
    public PageListDateTimeConverter()
    {
        this.DateTimeFormat = "yyyy-MM-dd'T'HH:mm:ssZ";
    }
}