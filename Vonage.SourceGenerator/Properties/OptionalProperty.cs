#region
using Microsoft.CodeAnalysis;
#endregion

namespace Vonage.SourceGenerator.Properties;

internal record OptionalProperty(IPropertySymbol Property, string XmlDocumentation) : IOptionalProperty
{
    public OptionalProperty(IPropertySymbol property) : this(property,
        XmlDocumentationHelper.GetXmlDocumentation(property))
    {
    }

    private string InnerType =>
        this.Property.Type is INamedTypeSymbol namedType &&
        namedType.OriginalDefinition.ToDisplayString().Contains("Maybe<")
            ? namedType.TypeArguments[0].ToDisplayString()
            : this.Property.Type.ToString();

    private string XmlDocPrefix =>
        string.IsNullOrEmpty(this.XmlDocumentation) ? string.Empty : $"{this.XmlDocumentation}\n";

    public string Declaration =>
        $"{this.XmlDocPrefix}IBuilderForOptional With{this.Property.Name}({this.InnerType} value);";

    public string FieldDeclaration => $"private {this.Property.Type.ToDisplayString()} {this.Property.Name.ToLower()};";
    public string DefaultValueAssignment => $"this.{this.Property.Name.ToLower()} = Maybe<{this.InnerType}>.None;";

    public string Implementation =>
        $"{this.XmlDocPrefix}public IBuilderForOptional With{this.Property.Name}({this.InnerType} value) => this with {{ {this.Property.Name.ToLower()} = value }};";
}