using System.IO;
using System.Runtime.CompilerServices;
using Vonage.Video.Beta.Common;

namespace Vonage.Video.Beta.Test.Common
{
    public class SerializationTestHelper
    {
        private const string ExcludeNamespace = "Vonage.Video.Beta.Test.";
        private readonly string callerNamespace;

        public JsonSerializer Serializer { get; }

        public SerializationTestHelper(string callerNamespace)
        {
            this.callerNamespace = callerNamespace;
            this.Serializer = new JsonSerializer();
        }

        public string GetResponseJson([CallerMemberName] string name = null) =>
            ReadFile(string.Concat(this.GetUseCaseFolder(), GetRelativeFilePath(name)));

        public string GetResponseJsonForStatusCode(string statusCode, [CallerMemberName] string name = null) =>
            ReadFile(string.Concat(this.GetUseCaseFolder(), GetRelativeFilePath(name, statusCode)));

        private static string ReadFile(string filePath) =>
            File.Exists(filePath)
                ? File.ReadAllText(filePath)
                : string.Empty;

        private string GetUseCaseFolder() =>
            this.callerNamespace
                .Replace(ExcludeNamespace, string.Empty)
                .Replace('.', '/');

        private static string GetRelativeFilePath(string caller) => $"/Data/{caller}-response.json";

        private static string GetRelativeFilePath(string caller, string statusCode) =>
            $"/Data/{caller}{statusCode}-response.json";
    }
}