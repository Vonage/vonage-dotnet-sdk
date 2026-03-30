#region
using Microsoft.CodeAnalysis;
#endregion

namespace Vonage.SourceGenerator.Builders;

internal record MandatoryBuilderInterface(IPropertySymbol Property, string ReturnType, string XmlDocumentation)
    : IBuilderInterface
{
    private string XmlDocPrefix =>
        string.IsNullOrEmpty(this.XmlDocumentation) ? string.Empty : $"{this.XmlDocumentation}\n";

    public string Name => $"IBuilderFor{this.Property.Name}";

    public string BuildDeclaration() => $$"""
                                          public interface {{this.Name}}
                                          {
                                              {{this.XmlDocPrefix}}{{this.ReturnType}} With{{this.Property.Name}}({{this.Property.Type.ToDisplayString()}} value);
                                          }
                                          """;

    public string BuildImplementation() =>
        $"{this.XmlDocPrefix}public {this.ReturnType} With{this.Property.Name}({this.Property.Type.ToDisplayString()} value) => this with {{ {this.Property.Name.ToLower()} = value }};";
}