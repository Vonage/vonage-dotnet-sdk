#region
using Microsoft.CodeAnalysis;
#endregion

namespace Vonage.SourceGenerator.Properties;

internal record OptionalWithDefaultProperty(
    IPropertySymbol Property,
    string Type,
    string DefaultValue,
    params ValidationRule[] ValidationRules) : IOptionalProperty
{
    public const string AttributeName = "OptionalWithDefaultAttribute";

    public static OptionalWithDefaultProperty FromMember(IPropertySymbol member)
    {
        var attribute = member.GetAttributes()
            .FirstOrDefault(attribute => attribute.AttributeClass?.Name == AttributeName);
        return HasValidationRules(attribute)
            ? new OptionalWithDefaultProperty(member, ExtractType(attribute), ExtractDefaultValue(attribute),
                ExtractValidationRules(attribute))
            : new OptionalWithDefaultProperty(member, ExtractType(attribute), ExtractDefaultValue(attribute));
    }

    public string Declaration => $"IBuilderForOptional With{this.Property.Name}({this.Property.Type} value);";
    public string FieldDeclaration => $"private {this.Property.Type.ToDisplayString()} {this.Property.Name.ToLower()};";
    public string DefaultValueAssignment => $"this.{this.Property.Name.ToLower()} = {this.ParseDefaultValue()};";

    public string Implementation =>
        $"public IBuilderForOptional With{this.Property.Name}({this.Property.Type} value) => this with {{ {this.Property.Name.ToLower()} = value }};";

    private static string ExtractDefaultValue(AttributeData attribute) =>
        attribute.ConstructorArguments[1].Value?.ToString();

    private static string ExtractType(AttributeData attribute) => attribute.ConstructorArguments[0].Value?.ToString();

    private static ValidationRule[] ExtractValidationRules(AttributeData attribute)
    {
        return attribute.ConstructorArguments[2].Values.Select(v => (string) v.Value)
            .Select(value => new ValidationRule(value)).ToArray();
    }

    private static bool HasValidationRules(AttributeData attribute) => attribute.ConstructorArguments.Length > 2 &&
                                                                       attribute.ConstructorArguments[2].Kind ==
                                                                       TypedConstantKind.Array;

    private string ParseDefaultValue()
    {
        return this.Type switch
        {
            "int" => this.DefaultValue,
            "string" => $"\"{this.DefaultValue}\"",
            _ => "null",
        };
    }
}