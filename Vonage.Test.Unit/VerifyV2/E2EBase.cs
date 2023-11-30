using Vonage.Common;
using Vonage.Common.Test;
using Vonage.Test.Unit.TestHelpers;

namespace Vonage.Test.Unit.VerifyV2
{
    public class E2EBase
    {
        protected E2EBase(string serializationNamespace) : this() => this.Serialization =
            new SerializationTestHelper(serializationNamespace, JsonSerializer.BuildWithSnakeCase());

        protected E2EBase() => this.Helper = TestingContext.WithBearerCredentials("Vonage.Url.Api");
        internal readonly TestingContext Helper;
        internal readonly SerializationTestHelper Serialization;
    }
}