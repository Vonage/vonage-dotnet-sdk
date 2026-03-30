#region
using Microsoft.CodeAnalysis;
#endregion

namespace Vonage.SourceGenerator.Properties;

internal record OptionalWithParsingProperty(
    IPropertySymbol Property,
    string ParserMethodName,
    string InputType,
    string InnerType,
    string XmlDocumentation) : IOptionalProperty
{
    public const string AttributeName = "OptionalWithParsingAttribute";

    private string XmlDocPrefix =>
        string.IsNullOrEmpty(this.XmlDocumentation) ? string.Empty : $"{this.XmlDocumentation}\n";

    public string FieldDeclaration =>
        $"private Result<{this.Property.Type.ToDisplayString()}> {this.Property.Name.ToLower()};";

    public string DefaultValueAssignment =>
        $"this.{this.Property.Name.ToLower()} = Result<{this.Property.Type.ToDisplayString()}>.FromSuccess({this.Property.Type.ToDisplayString()}.None);";

    public string Declaration =>
        $"{this.XmlDocPrefix}IBuilderForOptional With{this.Property.Name}({this.InputType} value);";

    public string Implementation =>
        $"{this.XmlDocPrefix}public IBuilderForOptional With{this.Property.Name}({this.InputType} value) => this with {{ {this.Property.Name.ToLower()} = {this.Property.ContainingType.Name}.{this.ParserMethodName}(value).Map(value => {this.Property.Type.ToDisplayString()}.Some(value)) }};";

    public static OptionalWithParsingProperty FromMember(IPropertySymbol member, INamedTypeSymbol containingType)
    {
        var attribute = member.GetAttributes()
            .First(attr => attr.AttributeClass?.Name == AttributeName);
        var parserMethodName = ExtractParserMethodName(attribute);
        var inputType = GetInputTypeFromParser(containingType, parserMethodName);
        var innerType = GetInnerType(member);
        var xmlDoc = XmlDocumentationHelper.GetXmlDocumentation(member);
        return new OptionalWithParsingProperty(member, parserMethodName, inputType, innerType, xmlDoc);
    }

    private static string ExtractParserMethodName(AttributeData attribute) =>
        attribute.ConstructorArguments[0].Value?.ToString() ?? "";

    private static string GetInputTypeFromParser(INamedTypeSymbol containingType, string parserMethodName) =>
        containingType.GetMembers()
            .OfType<IMethodSymbol>()
            .First(m => m.Name == parserMethodName)
            .Parameters[0]
            .Type
            .ToDisplayString();

    private static string GetInnerType(IPropertySymbol property) =>
        property.Type is INamedTypeSymbol namedType &&
        namedType.OriginalDefinition.ToDisplayString().Contains("Maybe<")
            ? namedType.TypeArguments[0].ToDisplayString()
            : property.Type.ToString();
}