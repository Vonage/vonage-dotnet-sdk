#region
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Vonage.SourceGenerator.Builders;
using Vonage.SourceGenerator.Properties;
#endregion

namespace Vonage.SourceGenerator;

internal class CodeGenerator(
    INamedTypeSymbol type,
    MandatoryProperty[] mandatoryProperties,
    IOptionalProperty[] optionalProperties,
    ValidationRuleMethod[] validationMethods)
{
    private IProperty[] AllProperties =>
        this.OrderedMandatoryProperties.Concat<IProperty>(optionalProperties).ToArray();

    private MandatoryProperty[] OrderedMandatoryProperties =>
        mandatoryProperties.OrderBy(property => property.Order).ToArray();

    private string BuilderName => $"{this.TypeName}Builder";
    private string TypeName => type.Name;

    public string GenerateCode()
    {
        var code = string.Concat(this.GenerateUsingStatements(),
            this.GenerateNamespace(),
            this.GenerateInterfaceDeclarations(),
            this.ExtendTypeWithPartial(),
            this.GenerateBuilder()
        );
        return FormatCode(code);
    }

    private string ExtendTypeWithPartial() => $$"""
                                                public partial struct {{this.TypeName}}
                                                {
                                                    public static {{this.GetFirstInterface()}} Build() => new {{this.BuilderName}}();
                                                }
                                                """;

    private static string FormatCode(string code)
    {
        var syntaxTree = CSharpSyntaxTree.ParseText(code);
        var root = syntaxTree.GetRoot().NormalizeWhitespace();
        return root.ToFullString();
    }

    private IEnumerable<string> FormatValidationRules() =>
        validationMethods.Select(rule => $"{this.TypeName}.{rule.Name}");

    private string GenerateBuilder()
    {
        var propertyAssignments = this.AllProperties
            .Select(property => property.Property.Name)
            .Select(name => $"{name} = this.{name.ToLower()},")
            .ToArray();
        return $$"""
                 internal struct {{this.BuilderName}} : {{string.Join(", ", this.GetAllInterfaces())}}
                 {
                    {{string.Join("\n", this.AllProperties.Select(property => property.FieldDeclaration).ToArray())}}
                    
                    public {{this.BuilderName}}()
                    {
                        {{string.Join("\n", this.AllProperties.Select(property => property.DefaultValueAssignment).ToArray())}}
                    }
                    
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

    private string GenerateInterfaceDeclarations() =>
        string.Concat(this.GetBuilderInterfaces().Select(builderInterface => builderInterface.BuildDeclaration()));

    private string GenerateNamespace() =>
        string.IsNullOrEmpty(type.ContainingNamespace?.ToDisplayString())
            ? string.Empty
            : $"namespace {type.ContainingNamespace.ToDisplayString()};\n\n";

    private string GenerateUsingStatements()
    {
        var builder = new StringBuilder();
        builder.AppendLine("using Vonage.Common.Client;");
        builder.AppendLine("using Vonage.Common.Monads;");
        builder.AppendLine("using Vonage.Common.Validation;");
        var parameters = type.GetAttributes()
            .First(attr => attr.AttributeClass!.Name == "BuilderAttribute")
            .ConstructorArguments;
        if (parameters.Any())
        {
            parameters.First().Values.Select(value => value.Value?.ToString()).ToList()
                .ForEach(value => builder.AppendLine($"using {value};"));
        }

        return builder.ToString();
    }

    private string[] GetAllInterfaces() =>
        this.OrderedMandatoryProperties.Select(p => $"IBuilderFor{p.Property.Name}")
            .Append("IBuilderForOptional")
            .ToArray();

    private IBuilderInterface[] GetBuilderInterfaces()
    {
        var interfaces = this.OrderedMandatoryProperties
            .Select((property, index) =>
                new MandatoryBuilderInterface(
                    property.Property,
                    index == this.OrderedMandatoryProperties.Length - 1
                        ? "IBuilderForOptional"
                        : $"IBuilderFor{this.OrderedMandatoryProperties[index + 1].Property.Name}"
                ))
            .Cast<IBuilderInterface>()
            .ToList();
        interfaces.Add(new OptionalBuilderInterface(optionalProperties, this.TypeName));
        return interfaces.ToArray();
    }

    private string GetFirstInterface() =>
        this.OrderedMandatoryProperties.Length > 0
            ? $"IBuilderFor{this.OrderedMandatoryProperties[0].Property.Name}"
            : "IBuilderForOptional";
}