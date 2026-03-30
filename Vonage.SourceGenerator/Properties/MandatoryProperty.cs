#region
using Microsoft.CodeAnalysis;
#endregion

namespace Vonage.SourceGenerator.Properties;

internal record MandatoryProperty(IPropertySymbol Property, int Order, string XmlDocumentation)
    : IProperty
{
    public const string AttributeName = "MandatoryAttribute";

    public string FieldDeclaration => $"private {this.Property.Type.ToDisplayString()} {this.Property.Name.ToLower()};";
    public string DefaultValueAssignment => $"this.{this.Property.Name.ToLower()} = default;";

    public static MandatoryProperty FromMember(IPropertySymbol member)
    {
        var attribute = member.GetAttributes()
            .FirstOrDefault(attribute => attribute.AttributeClass?.Name == AttributeName);
        var xmlDoc = XmlDocumentationHelper.GetXmlDocumentation(member);
        return new MandatoryProperty(member, ExtractOrder(attribute), xmlDoc);
    }

    private static int ExtractOrder(AttributeData attribute) =>
        int.Parse(attribute.ConstructorArguments[0].Value?.ToString() ?? "");
}