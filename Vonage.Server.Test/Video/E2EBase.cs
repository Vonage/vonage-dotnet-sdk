using Vonage.Common;
using Vonage.Common.Test;
using Vonage.Server.Test.TestHelpers;

namespace Vonage.Server.Test.Video
{
    public abstract class E2EBase
    {
        private E2EBase() => this.Helper = E2EHelper.WithBearerCredentials("Vonage.Url.Api.Video");

        protected E2EBase(string serializationNamespace) : this() => this.Serialization =
            new SerializationTestHelper(serializationNamespace, JsonSerializer.BuildWithCamelCase());

        internal readonly E2EHelper Helper;
        internal readonly SerializationTestHelper Serialization;
    }
}