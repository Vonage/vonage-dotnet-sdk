using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Vonage.Video.Beta.Common;

namespace Vonage.Video.Beta.Test.Video
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

        public string GetResponseJson([CallerMemberName] string name = null)
        {
            var filePath = string.Concat(this.GetUseCaseFolder(), GetRelativeFilePath(name));
            return File.Exists(filePath)
                ? CleanJsonContent(File.ReadAllText(filePath))
                : string.Empty;
        }

        private static string CleanJsonContent(string filePath) =>
            Regex.Replace(filePath, "(\"(?:[^\"\\\\]|\\\\.)*\")|\\s+", "$1");

        private string GetUseCaseFolder() =>
            this.callerNamespace
                .Replace(ExcludeNamespace, string.Empty)
                .Replace('.', '/');

        private static string GetRelativeFilePath(string caller) => $"/Data/{caller}-response.json";
    }
}