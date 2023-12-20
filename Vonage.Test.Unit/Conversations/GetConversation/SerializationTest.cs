using Vonage.Conversations;
using Vonage.Serialization;
using Vonage.Test.Unit.Common;
using Vonage.Test.Unit.Common.Extensions;
using Xunit;

namespace Vonage.Test.Unit.Conversations.GetConversation
{
    public class SerializationTest
    {
        private readonly SerializationTestHelper helper = new SerializationTestHelper(
            typeof(SerializationTest).Namespace,
            JsonSerializerBuilder.BuildWithSnakeCase());

        [Fact]
        public void ShouldDeserialize200() => this.helper.Serializer
            .DeserializeObject<Conversation>(this.helper.GetResponseJson())
            .Should()
            .BeSuccess(ConversationTests.VerifyExpectedResponse);

        [Fact]
        public void ShouldDeserialize200Minimal() => this.helper.Serializer
            .DeserializeObject<Conversation>(this.helper.GetResponseJson())
            .Should()
            .BeSuccess(ConversationTests.VerifyMinimalResponse);
    }
}