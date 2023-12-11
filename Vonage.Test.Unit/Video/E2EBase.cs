using System.Text.Json;
using Vonage.Common.Test;
using Vonage.Serialization;
using Vonage.Test.Unit.Video.TestHelpers;

namespace Vonage.Test.Unit.Video
{
    public abstract class E2EBase
    {
        private E2EBase() => this.Helper = E2EHelper.WithBearerCredentials("Vonage.Url.Api.Video");

        protected E2EBase(string serializationNamespace) : this() => this.Serialization =
            new SerializationTestHelper(serializationNamespace,
                JsonSerializerBuilder.Build(JsonNamingPolicy.CamelCase));

        internal readonly E2EHelper Helper;
        internal readonly SerializationTestHelper Serialization;
    }
}