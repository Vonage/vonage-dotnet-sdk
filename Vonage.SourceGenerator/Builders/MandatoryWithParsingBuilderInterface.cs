#region
using Vonage.SourceGenerator.Properties;
#endregion

namespace Vonage.SourceGenerator.Builders;

internal record MandatoryWithParsingBuilderInterface(
    MandatoryWithParsingProperty Property,
    string ReturnType,
    string TypeName) : IBuilderInterface
{
    private string XmlDocPrefix => string.IsNullOrEmpty(this.Property.XmlDocumentation)
        ? string.Empty
        : $"{this.Property.XmlDocumentation}\n";

    public string Name => $"IBuilderFor{this.Property.Property.Name}";

    public string BuildDeclaration() => $$"""
                                          public interface {{this.Name}}
                                          {
                                              {{this.XmlDocPrefix}}{{this.ReturnType}} With{{this.Property.Property.Name}}({{this.Property.InputType}} value);
                                          }
                                          """;

    public string BuildImplementation() =>
        $"{this.XmlDocPrefix}public {this.ReturnType} With{this.Property.Property.Name}({this.Property.InputType} value) => this with {{ {this.Property.Property.Name.ToLower()} = {this.TypeName}.{this.Property.ParserMethodName}(value) }};";
}