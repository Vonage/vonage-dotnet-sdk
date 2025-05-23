#region
using Microsoft.CodeAnalysis;
#endregion

namespace Vonage.SourceGenerator.Properties;

internal record OptionalBooleanProperty(
    IPropertySymbol Property,
    bool DefaultValue,
    string MethodName,
    params ValidationRule[] ValidationRules)
    : IOptionalProperty
{
    public const string AttributeName = "OptionalBooleanAttribute";

    public static OptionalBooleanProperty FromMember(IPropertySymbol member)
    {
        var attribute = member.GetAttributes()
            .FirstOrDefault(a => a.AttributeClass?.Name == AttributeName);
        return new OptionalBooleanProperty(member, ExtractDefaultValue(attribute), ExtractMethodName(attribute));
    }

    public string Declaration => @$"
IBuilderForOptional {this.MethodName}();
";

    public string FieldDeclaration =>
        $"private {this.Property.Type.ToDisplayString()} {this.Property.Name.ToLower()};";

    public string DefaultValueAssignment =>
        $"this.{this.Property.Name.ToLower()} = {this.DefaultValue.ToString().ToLowerInvariant()};";

    public string Implementation => @$"
public IBuilderForOptional {this.MethodName}() => this with {{ {this.Property.Name.ToLower()} = {(!this.DefaultValue).ToString().ToLowerInvariant()} }};
";

    private static bool ExtractDefaultValue(AttributeData attribute) =>
        bool.Parse(attribute.ConstructorArguments[0].Value?.ToString());

    private static string ExtractMethodName(AttributeData attribute) =>
        attribute.ConstructorArguments[1].Value?.ToString();
}