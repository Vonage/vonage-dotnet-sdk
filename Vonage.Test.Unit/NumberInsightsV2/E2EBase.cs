using Vonage.Common;
using Vonage.Common.Test;
using Vonage.Test.Unit.TestHelpers;

namespace Vonage.Test.Unit.NumberInsightsV2
{
    public abstract class E2EBase
    {
        protected E2EBase(string serializationNamespace)
        {
            this.Helper = E2EHelper.WithBasicCredentials("Vonage.Url.Api");
            this.Serialization =
                new SerializationTestHelper(serializationNamespace, JsonSerializer.BuildWithSnakeCase());
        }

        internal readonly E2EHelper Helper;
        internal readonly SerializationTestHelper Serialization;
    }
}