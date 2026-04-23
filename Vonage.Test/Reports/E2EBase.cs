#region
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.TestHelpers;
#endregion

namespace Vonage.Test.Reports;

public abstract class E2EBase
{
    internal readonly TestingContext Helper = TestingContext.WithBasicCredentials();

    internal readonly SerializationTestHelper Serialization =
        new SerializationTestHelper(typeof(ReportResponseSerializationTest).Namespace,
            JsonSerializerBuilder.BuildWithSnakeCase());
}
