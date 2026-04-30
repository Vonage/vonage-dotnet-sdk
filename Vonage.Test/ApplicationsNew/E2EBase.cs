using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.TestHelpers;

namespace Vonage.Test.ApplicationsNew;

public abstract class E2EBase
{
    internal readonly TestingContext Helper = TestingContext.WithBasicCredentials();

    internal readonly SerializationTestHelper Serialization =
        new SerializationTestHelper(typeof(ApplicationResponseSerializationTest).Namespace,
            JsonSerializerBuilder.BuildWithSnakeCase());
}
