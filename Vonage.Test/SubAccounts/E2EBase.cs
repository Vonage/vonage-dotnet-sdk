#region
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.TestHelpers;
#endregion

namespace Vonage.Test.SubAccounts;

public abstract class E2EBase(string serializationNamespace)
{
    internal readonly TestingContext Helper = TestingContext.WithBasicCredentials();

    internal readonly SerializationTestHelper Serialization =
        new SerializationTestHelper(serializationNamespace, JsonSerializerBuilder.BuildWithSnakeCase());
}