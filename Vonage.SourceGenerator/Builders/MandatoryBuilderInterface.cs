#region
using Microsoft.CodeAnalysis;
#endregion

namespace Vonage.SourceGenerator.Builders;

internal record MandatoryBuilderInterface(IPropertySymbol Property, string ReturnType) : IBuilderInterface
{
    public string Name => $"IBuilderFor{this.Property.Name}";

    public string BuildDeclaration() => $$"""
                                          public interface {{this.Name}}
                                          {
                                              {{this.ReturnType}} With{{this.Property.Name}}({{this.Property.Type.ToDisplayString()}} value);
                                          }
                                          """;

    public string BuildImplementation() =>
        $"    public {this.ReturnType} With{this.Property.Name}({this.Property.Type.ToDisplayString()} value) => this with {{ {this.Property.Name.ToLower()} = value }};";
}