#region
using Vonage.Conversions;
#endregion

namespace Vonage.Test.Conversions;

internal static class ConversionTestData
{
    internal static ConversionRequest CreateBasicRequest() =>
        new ConversionRequest
        {
            Delivered = true,
            MessageId = "00A0B0C0",
            TimeStamp = "2020-01-01 12:00:00",
        };
}