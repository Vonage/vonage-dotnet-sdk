#region
using Microsoft.CodeAnalysis;
#endregion

namespace Vonage.SourceGenerator.Properties;

internal record OptionalWithDefaultProperty(
    IPropertySymbol Property,
    string Type,
    string DefaultValue,
    string XmlDocumentation) : IOptionalProperty
{
    public const string AttributeName = "OptionalWithDefaultAttribute";

    private string XmlDocPrefix =>
        string.IsNullOrEmpty(this.XmlDocumentation) ? string.Empty : $"{this.XmlDocumentation}\n";

    public string Declaration =>
        $"{this.XmlDocPrefix}IBuilderForOptional With{this.Property.Name}({this.Property.Type} value);";

    public string FieldDeclaration => $"private {this.Property.Type.ToDisplayString()} {this.Property.Name.ToLower()};";
    public string DefaultValueAssignment => $"this.{this.Property.Name.ToLower()} = {this.ParseDefaultValue()};";

    public string Implementation =>
        $"{this.XmlDocPrefix}public IBuilderForOptional With{this.Property.Name}({this.Property.Type} value) => this with {{ {this.Property.Name.ToLower()} = value }};";

    public static OptionalWithDefaultProperty FromMember(IPropertySymbol member)
    {
        var attribute = member.GetAttributes()
            .FirstOrDefault(attribute => attribute.AttributeClass?.Name == AttributeName);
        var xmlDoc = XmlDocumentationHelper.GetXmlDocumentation(member);
        return new OptionalWithDefaultProperty(member, ExtractType(attribute), ExtractDefaultValue(attribute), xmlDoc);
    }

    private static string ExtractDefaultValue(AttributeData attribute) =>
        attribute.ConstructorArguments[1].Value?.ToString();

    private static string ExtractType(AttributeData attribute) => attribute.ConstructorArguments[0].Value?.ToString();

    private string ParseDefaultValue() =>
        this.Type switch
        {
            "string" => $"\"{this.DefaultValue}\"",
            _ => this.DefaultValue,
        };
}