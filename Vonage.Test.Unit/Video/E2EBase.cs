using Vonage.Common.Test;
using Vonage.Serialization;
using Vonage.Test.Unit.TestHelpers;

namespace Vonage.Test.Unit.Video
{
    public abstract class E2EBase
    {
        private E2EBase() => this.Helper = TestingContext.WithBearerCredentials("Vonage.Url.Api.Video");

        protected E2EBase(string serializationNamespace) : this() => this.Serialization =
            new SerializationTestHelper(serializationNamespace,
                JsonSerializerBuilder.BuildWithCamelCase());

        internal readonly TestingContext Helper;
        internal readonly SerializationTestHelper Serialization;
    }
}