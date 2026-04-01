#region
using System.Reflection;
using System.Runtime.InteropServices;
#endregion

namespace Vonage.Common.Client;

/// <summary>
///     Provides methods to generate formatted User-Agent strings for HTTP requests to Vonage APIs.
/// </summary>
/// <remarks>
///     The User-Agent includes the SDK version, .NET runtime version, and any custom application identifier.
/// </remarks>
public static class UserAgentProvider
{
    /// <summary>
    ///     Generates a formatted User-Agent string for Vonage API requests.
    /// </summary>
    /// <param name="userAgent">Optional custom application identifier to append to the User-Agent string.</param>
    /// <returns>
    ///     A formatted User-Agent string in the format: "vonage-dotnet/{version} dotnet/{runtime} {userAgent}".
    /// </returns>
    /// <example>
    ///     <code><![CDATA[
    /// var agent = UserAgentProvider.GetFormattedUserAgent("my-app/1.0");
    /// // Returns: "vonage-dotnet/9.0.0 dotnet/8.0.0 my-app/1.0"
    /// ]]></code>
    /// </example>
    public static string GetFormattedUserAgent(string userAgent)
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
        var libraryVersion = typeof(VonageHttpClient<StandardApiError>)
            .GetTypeInfo()
            .Assembly
            .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
            .InformationalVersion;
        return $"vonage-dotnet/{libraryVersion} dotnet/{languageVersion} {userAgent}".Trim();
    }
}