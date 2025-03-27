#region
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
#endregion

namespace Vonage.SourceGenerator;

internal class CodeGenerator(
    INamedTypeSymbol type,
    MandatoryProperty[] mandatoryProperties,
    OptionalProperty[] optionalProperties,
    List<ValidationRule> validationRules)
{
    private string BuilderName => $"{this.TypeName}Builder";
    private string TypeName => type.Name;

    private MandatoryProperty[] OrderedMandatoryProperties =>
        mandatoryProperties.OrderBy(property => property.Order).ToArray();

    private IPropertySymbol[] AllProperties =>
        this.OrderedMandatoryProperties.Select(property => property.Property)
            .Concat(optionalProperties.Select(property => property.Property))
            .ToArray();

    public string GenerateCode()
    {
        var code = string.Concat(GenerateUsingStatements(),
            this.GenerateNamespace(),
            this.GenerateInterfaceDeclarations(),
            this.ExtendTypeWithPartial(),
            this.GenerateBuilder()
        );
        return FormatCode(code);
    }

    private static string FormatCode(string code)
    {
        var syntaxTree = CSharpSyntaxTree.ParseText(code);
        var root = syntaxTree.GetRoot().NormalizeWhitespace();
        return root.ToFullString();
    }

    private string GenerateBuilder()
    {
        var fields = this.AllProperties
            .Select(property => $"private {GetPropertyType(property)} {property.Name.ToLower()};")
            .ToArray();
        var propertyAssignments = this.AllProperties
            .Select(property => $"{property.Name} = this.{property.Name.ToLower()},")
            .ToArray();
        return $$"""
                 internal struct {{this.BuilderName}} : {{string.Join(", ", this.GetAllInterfaces())}}
                 {
                    {{string.Join("\n", fields)}}
                    {{string.Join("\n", this.GetBuilderInterfaces().Select(i => i.BuildImplementation()))}}
                    public Result<{{this.TypeName}}> Create() => Result<{{this.TypeName}}>.FromSuccess(
                         new {{this.TypeName}}
                         {
                             {{string.Join("\n", propertyAssignments)}}
                         })
                         .Map(InputEvaluation<{{this.TypeName}}>.Evaluate)
                         .Bind(evaluation => evaluation.WithRules({{string.Join(",", this.FormatValidationRules())}}));
                 }
                 """;
    }

    private string ExtendTypeWithPartial() => $$"""
                                                public partial struct {{this.TypeName}}
                                                {
                                                    public static {{this.GetFirstInterface()}} Build() => new {{this.BuilderName}}();
                                                }
                                                """;

    private string GenerateInterfaceDeclarations() =>
        string.Concat(this.GetBuilderInterfaces().Select(builderInterface => builderInterface.BuildDeclaration()));

    private IBuilderInterface[] GetBuilderInterfaces()
    {
        var interfaces = this.OrderedMandatoryProperties
            .Select((property, index) =>
                new MandatoryBuilderInterface(
                    property.Property,
                    index == this.OrderedMandatoryProperties.Length - 1
                        ? $"IBuilderForOptional<{this.TypeName}>"
                        : $"IBuilderFor{this.OrderedMandatoryProperties[index + 1].Property.Name}"
                ))
            .Cast<IBuilderInterface>()
            .ToList();
        interfaces.Add(new OptionalBuilderInterface(optionalProperties, this.TypeName));
        return interfaces.ToArray();
    }

    private string GenerateNamespace() =>
        string.IsNullOrEmpty(type.ContainingNamespace?.ToDisplayString())
            ? string.Empty
            : $"namespace {type.ContainingNamespace.ToDisplayString()};\n\n";

    private static string GenerateUsingStatements() => """
                                                       using Vonage.Common.Monads;
                                                       using Vonage.Common.Validation;
                                                       """;

    private IEnumerable<string> FormatValidationRules() =>
        validationRules.Select(r => $"{this.TypeName}.{r.MethodName}");

    private string GetFirstInterface() =>
        this.OrderedMandatoryProperties.Length > 0
            ? $"IBuilderFor{this.OrderedMandatoryProperties[0].Property.Name}"
            : $"IBuilderForOptional<{this.TypeName}>";

    private string[] GetAllInterfaces() =>
        this.OrderedMandatoryProperties.Select(p => $"IBuilderFor{p.Property.Name}")
            .Append($"IBuilderForOptional<{this.TypeName}>")
            .ToArray();

    private static string GetPropertyType(IPropertySymbol prop) => prop.Type.ToDisplayString();
}

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

internal record OptionalBuilderInterface(OptionalProperty[] Properties, string GenericType) : IBuilderInterface
{
    public string Name => "IBuilderForOptional";

    public string BuildDeclaration() => $$"""
                                          public interface IBuilderForOptional<{{this.GenericType}}>
                                          {
                                                {{string.Join("\n", this.Properties.Select(p => $"    IBuilderForOptional<{this.GenericType}> With{p.Property.Name}({p.Property.Type.ToDisplayString()} value);"))}}
                                                Result<{{this.GenericType}}> Create();
                                          }
                                          """;

    public string BuildImplementation() =>
        string.Join("\n", this.Properties.Select(p =>
            $"    public IBuilderForOptional<{this.GenericType}> With{p.Property.Name}({p.Property.Type.ToDisplayString()} value) => this with {{ {p.Property.Name.ToLower()} = value }};"
        ));
}

internal interface IBuilderInterface
{
    string Name { get; }
    string BuildDeclaration();
    string BuildImplementation();
}