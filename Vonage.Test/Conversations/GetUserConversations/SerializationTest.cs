using Vonage.Serialization;
using Vonage.Test.Common;

namespace Vonage.Test.Conversations.GetUserConversations;

public class SerializationTest
{
    private readonly SerializationTestHelper helper = new SerializationTestHelper(
        typeof(SerializationTest).Namespace,
        JsonSerializerBuilder.BuildWithSnakeCase());
}