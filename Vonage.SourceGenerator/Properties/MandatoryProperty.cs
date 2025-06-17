#region
using Microsoft.CodeAnalysis;
#endregion

namespace Vonage.SourceGenerator.Properties;

internal record MandatoryProperty(IPropertySymbol Property, int Order)
    : IProperty
{
    public const string AttributeName = "MandatoryAttribute";

    public static MandatoryProperty FromMember(IPropertySymbol member)
    {
        var attribute = member.GetAttributes()
            .FirstOrDefault(attribute => attribute.AttributeClass?.Name == AttributeName);
        return new MandatoryProperty(member, ExtractOrder(attribute));
    }

    public string FieldDeclaration => $"private {this.Property.Type.ToDisplayString()} {this.Property.Name.ToLower()};";
    public string DefaultValueAssignment => $"this.{this.Property.Name.ToLower()} = default;";

    private static int ExtractOrder(AttributeData attribute) =>
        int.Parse(attribute.ConstructorArguments[0].Value?.ToString() ?? "");
}