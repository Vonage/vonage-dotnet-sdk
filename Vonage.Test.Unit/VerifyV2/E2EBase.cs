using Vonage.Common;
using Vonage.Common.Test;
using Vonage.Test.Unit.TestHelpers;

namespace Vonage.Test.Unit.VerifyV2
{
    public class E2EBase
    {
        protected E2EBase(string serializationNamespace) : this() => this.Serialization =
            new SerializationTestHelper(serializationNamespace, JsonSerializer.BuildWithSnakeCase());

        protected E2EBase() => this.Helper = E2EHelper.WithBearerCredentials("Vonage.Url.Api");
        internal readonly E2EHelper Helper;
        internal readonly SerializationTestHelper Serialization;
    }
}