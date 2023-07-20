using Vonage.Common;
using Vonage.Common.Test;
using Vonage.Test.Unit.TestHelpers;

namespace Vonage.Test.Unit.Meetings
{
    public abstract class E2EBase
    {
        protected E2EBase(string serializationNamespace)
        {
            this.Helper = E2EHelper.WithBearerCredentials("Vonage.Url.Api.Europe");
            this.Serialization =
                new SerializationTestHelper(serializationNamespace, JsonSerializer.BuildWithSnakeCase());
        }

        internal readonly E2EHelper Helper;
        internal readonly SerializationTestHelper Serialization;
    }
}