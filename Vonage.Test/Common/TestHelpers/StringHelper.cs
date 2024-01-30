using System.Linq;

namespace Vonage.Test.Common.TestHelpers;

public static class StringHelper
{
    public static string GenerateString(int length) =>
        string.Join(string.Empty, Enumerable.Range(0, length).Select(_ => 'a').ToArray());
}