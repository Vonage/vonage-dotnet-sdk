using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.TestHelpers;

namespace Vonage.Test.AccountsNew;

public abstract class E2EBase
{
    internal readonly TestingContext Helper = TestingContext.WithBasicCredentials();

    internal readonly SerializationTestHelper Serialization =
        new SerializationTestHelper(typeof(SecretInfoSerializationTest).Namespace,
            JsonSerializerBuilder.BuildWithSnakeCase());
}
