using Vonage.Common;
using Vonage.Common.Test;
using Vonage.Test.Unit.TestHelpers;

namespace Vonage.Test.Unit.SubAccounts
{
    public abstract class E2EBase
    {
        protected E2EBase(string serializationNamespace)
        {
            this.helper = E2EHelper.WithBasicCredentials("Vonage.Url.Api");
            this.serialization =
                new SerializationTestHelper(serializationNamespace, JsonSerializer.BuildWithSnakeCase());
        }

        internal readonly E2EHelper helper;
        internal readonly SerializationTestHelper serialization;
    }
}