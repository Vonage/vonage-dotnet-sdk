using FluentAssertions;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Meetings.Common;
using Vonage.Meetings.GetDialNumbers;
using Xunit;

namespace Vonage.Test.Unit.Meetings.GetDialNumbers
{
    public class GetDialNumbersDeserializationTest
    {
        private readonly SerializationTestHelper helper;

        public GetDialNumbersDeserializationTest() =>
            this.helper = new SerializationTestHelper(typeof(GetDialNumbersDeserializationTest).Namespace,
                JsonSerializerBuilder.Build());

        [Fact]
        public void ShouldDeserialize200() =>
            this.helper.Serializer
                .DeserializeObject<GetDialNumbersResponse[]>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(success =>
                {
                    success.Should().HaveCount(1);
                    success[0].DisplayName.Should().Be("United States");
                    success[0].Locale.Should().Be("en-US");
                    success[0].Number.Should().Be("17323338801");
                });
    }
}