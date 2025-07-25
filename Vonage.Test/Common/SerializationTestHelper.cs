﻿#region
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Vonage.Common;
#endregion

namespace Vonage.Test.Common;

public class SerializationTestHelper
{
    private const string ExcludeVonageNamespace = "Vonage.Test.";
    private readonly string callerNamespace;

    public SerializationTestHelper(string callerNamespace)
    {
        this.callerNamespace = callerNamespace;
        this.Serializer = new JsonSerializer();
    }

    public SerializationTestHelper(string callerNamespace, JsonSerializer customSerializer)
        : this(callerNamespace) =>
        this.Serializer = customSerializer;

    public JsonSerializer Serializer { get; }

    public string GetRequest(string extension, [CallerMemberName] string name = null) =>
        ReadFile(string.Concat(this.GetUseCaseFolder(), GetRelativeFilePath(name, FileType.Request, extension)));

    public string GetRequestJson([CallerMemberName] string name = null) =>
        ReadFile(string.Concat(this.GetUseCaseFolder(), GetRelativeFilePath(name, FileType.Request, "json")));

    public string GetResponseJson([CallerMemberName] string name = null) =>
        ReadFile(string.Concat(this.GetUseCaseFolder(), GetRelativeFilePath(name, FileType.Response, "json")));

    public string GetResponseJsonForStatusCode(string statusCode, [CallerMemberName] string name = null) =>
        ReadFile(string.Concat(this.GetUseCaseFolder(), GetRelativeFilePath(name, statusCode, FileType.Response)));

    private static string CleanJsonContent(string filePath) =>
        Regex.Replace(filePath, "(\"(?:[^\"\\\\]|\\\\.)*\")|\\s+", "$1");

    private static string GetRelativeFilePath(string caller, FileType type, string extension) =>
        $"/Data/{caller}-{type.ToString().ToLowerInvariant()}.{extension}";

    private static string GetRelativeFilePath(string caller, string statusCode, FileType type) =>
        $"/Data/{caller}{statusCode}-{type.ToString().ToLowerInvariant()}.json";

    private string GetUseCaseFolder() =>
        this.callerNamespace.Replace(ExcludeVonageNamespace, string.Empty).Replace('.', '/');

    private static string ReadFile(string filePath) =>
        File.Exists(filePath)
            ? CleanJsonContent(File.ReadAllText(filePath))
            : string.Empty;

    private enum FileType
    {
        Response,

        Request,
    }
}