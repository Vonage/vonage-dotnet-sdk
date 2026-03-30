#region
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
#endregion

namespace Vonage.SourceGenerator.Properties;

internal interface IProperty
{
    string FieldDeclaration { get; }
    IPropertySymbol Property { get; }
    string DefaultValueAssignment { get; }
}

internal interface IOptionalProperty : IProperty
{
    string Declaration { get; }
    string Implementation { get; }
}

internal static class XmlDocumentationHelper
{
    /// <summary>
    ///     Extracts XML documentation content from an IPropertySymbol.
    ///     Returns the inner content of the member tag (summary, example, etc.).
    /// </summary>
    public static string GetXmlDocumentation(IPropertySymbol property)
    {
        var xml = property.GetDocumentationCommentXml();
        if (string.IsNullOrWhiteSpace(xml))
        {
            return string.Empty;
        }

        // Extract content between <member> tags
        var memberMatch = Regex.Match(xml, @"<member[^>]*>(.*?)</member>", RegexOptions.Singleline);
        if (!memberMatch.Success)
        {
            return string.Empty;
        }

        var content = memberMatch.Groups[1].Value.Trim();
        if (string.IsNullOrWhiteSpace(content))
        {
            return string.Empty;
        }

        // Convert to XML doc comments format
        var lines = content.Split('\n')
            .Select(line => line.Trim())
            .Where(line => !string.IsNullOrEmpty(line))
            .Select(line => $"/// {line}");
        return string.Join("\n", lines);
    }
}