using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.TestHelpers;

namespace Vonage.Test.Video;

public abstract class E2EBase
{
    internal readonly TestingContext Helper;
    internal readonly SerializationTestHelper Serialization;
    private E2EBase() => this.Helper = TestingContext.WithBearerCredentials();

    protected E2EBase(string serializationNamespace) : this() => this.Serialization =
        new SerializationTestHelper(serializationNamespace,
            JsonSerializerBuilder.BuildWithCamelCase());
}