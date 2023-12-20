using FluentAssertions;
using Vonage.Meetings.GetDialNumbers;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.Meetings.GetDialNumbers
{
    public class SerializationTest
    {
        private readonly SerializationTestHelper helper;

        public SerializationTest() =>
            this.helper = new SerializationTestHelper(typeof(SerializationTest).Namespace,
                JsonSerializerBuilder.BuildWithSnakeCase());

        [Fact]
        public void ShouldDeserialize200() =>
            this.helper.Serializer
                .DeserializeObject<GetDialNumbersResponse[]>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(VerifyNumbers);

        internal static void VerifyNumbers(GetDialNumbersResponse[] success)
        {
            success.Should().HaveCount(1);
            success[0].DisplayName.Should().Be("United States");
            success[0].Locale.Should().Be("en-US");
            success[0].Number.Should().Be("17323338801");
        }
    }
}