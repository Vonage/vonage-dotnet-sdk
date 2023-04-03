using System.Reflection;
using System.Runtime.InteropServices;

namespace Vonage.Common.Client;

public class UserAgentProvider
{
    internal static string GetFormattedUserAgent(string userAgent)
    {
#if NETSTANDARD1_6 || NETSTANDARD2_0 || NETSTANDARD2_1
        var languageVersion = RuntimeInformation.FrameworkDescription
            .Replace(" ", string.Empty)
            .Replace("/", string.Empty)
            .Replace(":", string.Empty)
            .Replace(";", string.Empty)
            .Replace("_", string.Empty)
            .Replace("(", string.Empty)
            .Replace(")", string.Empty);
#else
        var languageVersion = System.Diagnostics.FileVersionInfo
            .GetVersionInfo(typeof(int).Assembly.Location)
            .ProductVersion;
#endif
        var libraryVersion = typeof(VonageHttpClient)
            .GetTypeInfo()
            .Assembly
            .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
            .InformationalVersion;
        return $"vonage-dotnet/{libraryVersion} dotnet/{languageVersion} {userAgent}".Trim();
    }
}