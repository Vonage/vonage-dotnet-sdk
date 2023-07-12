using Vonage.Common;
using Vonage.Common.Test;
using Vonage.Test.Unit.TestHelpers;

namespace Vonage.Test.Unit.SubAccounts
{
    internal static class SubAccountsHelper
    {
        internal static E2EHelper BuildTestHelper() => E2EHelper.WithBasicCredentials("Vonage.Url.Api");

        internal static SerializationTestHelper BuildSerializationHelper(string useCaseNamespace) =>
            new SerializationTestHelper(useCaseNamespace, JsonSerializer.BuildWithSnakeCase());
    }
}