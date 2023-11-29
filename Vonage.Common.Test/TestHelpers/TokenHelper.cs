namespace Vonage.Common.Test.TestHelpers;

public static class TokenHelper
{
    private const string FilePath = "./../../../../key.txt";

    /// <summary>
    ///     Loads the key from the text file.
    /// </summary>
    /// <returns>The key.</returns>
    public static string GetKey() => File.ReadAllText(FilePath);
}