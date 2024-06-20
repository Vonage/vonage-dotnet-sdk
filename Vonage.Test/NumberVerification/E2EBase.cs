using Vonage.Serialization;
using Vonage.Test.Common;

namespace Vonage.Test.NumberVerification;

public class E2EBase
{
    internal readonly OidcTestingContext Helper;
    internal readonly SerializationTestHelper Serialization;

    protected E2EBase(string serializationNamespace) : this() => this.Serialization =
        new SerializationTestHelper(serializationNamespace, JsonSerializerBuilder.BuildWithSnakeCase());

    protected E2EBase() => this.Helper = OidcTestingContext.WithBearerCredentials();
}