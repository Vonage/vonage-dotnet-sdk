using Vonage.Common.Test;
using Vonage.Serialization;
using Vonage.Test.Unit.TestHelpers;

namespace Vonage.Test.Unit.SubAccounts
{
    public abstract class E2EBase
    {
        protected E2EBase(string serializationNamespace)
        {
            this.Helper = TestingContext.WithBasicCredentials("Vonage.Url.Api");
            this.Serialization =
                new SerializationTestHelper(serializationNamespace, JsonSerializerBuilder.BuildWithSnakeCase());
        }

        internal readonly TestingContext Helper;
        internal readonly SerializationTestHelper Serialization;
    }
}