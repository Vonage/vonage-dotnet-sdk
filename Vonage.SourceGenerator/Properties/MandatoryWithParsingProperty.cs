#region
using Microsoft.CodeAnalysis;
#endregion

namespace Vonage.SourceGenerator.Properties;

internal record MandatoryWithParsingProperty(
    IPropertySymbol Property,
    int Order,
    string ParserMethodName,
    string InputType,
    string XmlDocumentation) : IProperty
{
    public const string AttributeName = "MandatoryWithParsingAttribute";

    public string FieldDeclaration =>
        $"private Result<{this.Property.Type.ToDisplayString()}> {this.Property.Name.ToLower()};";

    public string DefaultValueAssignment => $"this.{this.Property.Name.ToLower()} = default;";

    public static MandatoryWithParsingProperty FromMember(IPropertySymbol member, INamedTypeSymbol containingType)
    {
        var attribute = member.GetAttributes()
            .FirstOrDefault(attr => attr.AttributeClass?.Name == AttributeName);
        var order = ExtractOrder(attribute);
        var parserMethodName = ExtractParserMethodName(attribute);
        var inputType = GetInputTypeFromParser(containingType, parserMethodName);
        var xmlDoc = XmlDocumentationHelper.GetXmlDocumentation(member);
        return new MandatoryWithParsingProperty(member, order, parserMethodName, inputType, xmlDoc);
    }

    private static int ExtractOrder(AttributeData attribute) =>
        int.Parse(attribute.ConstructorArguments[0].Value?.ToString() ?? "0");

    private static string ExtractParserMethodName(AttributeData attribute) =>
        attribute.ConstructorArguments[1].Value?.ToString() ?? "";

    private static string GetInputTypeFromParser(INamedTypeSymbol containingType, string parserMethodName) =>
        containingType.GetMembers()
            .OfType<IMethodSymbol>()
            .First(m => m.Name == parserMethodName)
            .Parameters[0]
            .Type
            .ToDisplayString();
}