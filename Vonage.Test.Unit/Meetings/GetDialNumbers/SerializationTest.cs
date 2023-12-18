using FluentAssertions;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Meetings.GetDialNumbers;
using Vonage.Serialization;
using Xunit;

namespace Vonage.Test.Unit.Meetings.GetDialNumbers
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