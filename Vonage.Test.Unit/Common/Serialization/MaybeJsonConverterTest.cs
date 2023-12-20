using System;
using System.Text;
using System.Text.Json;
using Vonage.Common.Serialization;
using Vonage.Test.Unit.Common.Extensions;
using Xunit;

namespace Vonage.Test.Unit.Common.Serialization
{
    public class MaybeJsonConverterTest
    {
        [Fact]
        public void Read_ShouldDeserializeUri_GivenJsonIsUriString()
        {
            var reader =
                new Utf8JsonReader(Encoding.Default.GetBytes(
                    "\"https://api.nexmo.com/v2/verify/c11236f4-00bf-4b89-84ba-88b25df97315/silent-auth/redirect\""));
            reader.Read();
            new MaybeJsonConverter<Uri>().Read(ref reader, typeof(Uri), new JsonSerializerOptions())
                .Should()
                .BeSome(new Uri(
                    "https://api.nexmo.com/v2/verify/c11236f4-00bf-4b89-84ba-88b25df97315/silent-auth/redirect"));
        }
    }
}