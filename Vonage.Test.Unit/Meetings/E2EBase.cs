using Vonage.Serialization;
using Vonage.Test.Unit.Common;
using Vonage.Test.Unit.TestHelpers;

namespace Vonage.Test.Unit.Meetings
{
    public abstract class E2EBase
    {
        protected E2EBase(string serializationNamespace)
        {
            this.Helper = TestingContext.WithBearerCredentials("Vonage.Url.Api.Europe");
            this.Serialization =
                new SerializationTestHelper(serializationNamespace, JsonSerializerBuilder.BuildWithSnakeCase());
        }

        internal readonly TestingContext Helper;
        internal readonly SerializationTestHelper Serialization;
    }
}