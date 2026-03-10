#region
using Vonage.SourceGenerator.Properties;
#endregion

namespace Vonage.SourceGenerator.Builders;

internal record MandatoryWithParsingBuilderInterface(
    MandatoryWithParsingProperty Property,
    string ReturnType,
    string TypeName) : IBuilderInterface
{
    public string Name => $"IBuilderFor{this.Property.Property.Name}";

    public string BuildDeclaration() => $$"""
                                          public interface {{this.Name}}
                                          {
                                              {{this.ReturnType}} With{{this.Property.Property.Name}}({{this.Property.InputType}} value);
                                          }
                                          """;

    public string BuildImplementation() =>
        $"    public {this.ReturnType} With{this.Property.Property.Name}({this.Property.InputType} value) => this with {{ {this.Property.Property.Name.ToLower()} = {this.TypeName}.{this.Property.ParserMethodName}(value) }};";
}