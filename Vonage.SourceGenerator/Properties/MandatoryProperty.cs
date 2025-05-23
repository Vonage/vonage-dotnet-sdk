#region
using Microsoft.CodeAnalysis;
#endregion

namespace Vonage.SourceGenerator.Properties;

internal record MandatoryProperty(IPropertySymbol Property, int Order, params ValidationRule[] ValidationRules)
    : IProperty
{
    public const string AttributeName = "MandatoryAttribute";

    public static MandatoryProperty FromMember(IPropertySymbol member)
    {
        var attribute = member.GetAttributes()
            .FirstOrDefault(attribute => attribute.AttributeClass?.Name == AttributeName);
        return HasValidationRules(attribute)
            ? new MandatoryProperty(member, ExtractOrder(attribute), ExtractValidationRules(attribute))
            : new MandatoryProperty(member, ExtractOrder(attribute));
    }

    public string FieldDeclaration => $"private {this.Property.Type.ToDisplayString()} {this.Property.Name.ToLower()};";
    public string DefaultValueAssignment => $"this.{this.Property.Name.ToLower()} = default;";

    private static int ExtractOrder(AttributeData attribute) =>
        int.Parse(attribute.ConstructorArguments[0].Value?.ToString() ?? "");

    private static ValidationRule[] ExtractValidationRules(AttributeData attribute)
    {
        return attribute.ConstructorArguments[1].Values.Select(value => (string) value.Value)
            .Select(value => new ValidationRule(value)).ToArray();
    }

    private static bool HasValidationRules(AttributeData attribute) => attribute.ConstructorArguments.Length > 1 &&
                                                                       attribute.ConstructorArguments[1].Kind ==
                                                                       TypedConstantKind.Array;
}