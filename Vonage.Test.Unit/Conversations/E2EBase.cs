using Vonage.Serialization;
using Vonage.Test.Unit.Common;
using Vonage.Test.Unit.TestHelpers;

namespace Vonage.Test.Unit.Conversations
{
    public class E2EBase
    {
        protected E2EBase(string serializationNamespace) : this() => this.Serialization =
            new SerializationTestHelper(serializationNamespace, JsonSerializerBuilder.BuildWithSnakeCase());

        protected E2EBase() => this.Helper = TestingContext.WithBearerCredentials("Vonage.Url.Api");
        internal readonly TestingContext Helper;
        internal readonly SerializationTestHelper Serialization;
    }
}