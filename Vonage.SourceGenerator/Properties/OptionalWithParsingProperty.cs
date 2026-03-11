#region
using Microsoft.CodeAnalysis;
#endregion

namespace Vonage.SourceGenerator.Properties;

internal record OptionalWithParsingProperty(
    IPropertySymbol Property,
    string ParserMethodName,
    string InputType,
    string InnerType) : IOptionalProperty
{
    public const string AttributeName = "OptionalWithParsingAttribute";

    public string FieldDeclaration =>
        $"private Result<{this.Property.Type.ToDisplayString()}> {this.Property.Name.ToLower()};";

    public string DefaultValueAssignment =>
        $"this.{this.Property.Name.ToLower()} = Result<{this.Property.Type.ToDisplayString()}>.FromSuccess({this.Property.Type.ToDisplayString()}.None);";

    public string Declaration =>
        $"IBuilderForOptional With{this.Property.Name}({this.InputType} value);";

    public string Implementation =>
        $"public IBuilderForOptional With{this.Property.Name}({this.InputType} value) => this with {{ {this.Property.Name.ToLower()} = {this.Property.ContainingType.Name}.{this.ParserMethodName}(value).Map(value => {this.Property.Type.ToDisplayString()}.Some(value)) }};";

    public static OptionalWithParsingProperty FromMember(IPropertySymbol member, INamedTypeSymbol containingType)
    {
        var attribute = member.GetAttributes()
            .First(attr => attr.AttributeClass?.Name == AttributeName);
        var parserMethodName = ExtractParserMethodName(attribute);
        var inputType = GetInputTypeFromParser(containingType, parserMethodName);
        var innerType = GetInnerType(member);
        return new OptionalWithParsingProperty(member, parserMethodName, inputType, innerType);
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