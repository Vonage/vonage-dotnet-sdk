#region
using System.Text;
using Microsoft.CodeAnalysis;
#endregion

namespace Vonage.SourceGenerator;

internal class CodeGenerator(
    INamedTypeSymbol type,
    MandatoryProperty[] mandatoryProperties,
    OptionalProperty[] optionalProperties,
    List<ValidationRule> validationRules)
{
    private string BuilderName => string.Concat(this.TypeName, "Builder");
    private string TypeName => type.Name;

    private MandatoryProperty[] OrderedMandatoryProperties =>
        mandatoryProperties.OrderBy(property => property.Order).ToArray();

    private IPropertySymbol[] AllProperties =>
        this.OrderedMandatoryProperties.Select(property => property.Property)
            .Concat(optionalProperties.Select(property => property.Property))
            .ToArray();

    public string GenerateCode() =>
        new StringBuilder().Append(this.GenerateUsingStatements())
            .Append(this.GenerateNamespace())
            .Append(this.GenerateInterfaceDeclarations())
            .Append(this.ExtendTypeWithPartial())
            .Append(this.GenerateBuilder())
            .ToString();

    private StringBuilder GenerateBuilder()
    {
        var builder = new StringBuilder();
        builder.AppendLine($"internal struct {this.BuilderName} : {string.Join(",", this.GetAllInterfaces())}");
        builder.AppendLine("{");
        foreach (var prop in this.AllProperties)
        {
            builder.AppendLine($"    private {GetPropertyType(prop)} {prop.Name.ToLower()};");
        }

        this.GetBuilderInterfaces().ToList()
            .ForEach(builderInterface => builder.Append(builderInterface.BuildImplementation()));
        builder.AppendLine($"    public Result<{this.TypeName}> Create() => Result<{this.TypeName}>.FromSuccess(");
        builder.AppendLine($"        new {this.TypeName}");
        builder.AppendLine("            {");
        foreach (var prop in this.AllProperties)
        {
            builder.AppendLine($"               {prop.Name} = this.{prop.Name.ToLower()},");
        }

        builder.AppendLine("            })");
        builder.AppendLine($"         .Map(InputEvaluation<{this.TypeName}>.Evaluate)");
        builder.AppendLine(
            $"         .Bind(evaluation => evaluation.WithRules({string.Join(",\n", this.GetValidationRules())}));");
        builder.AppendLine("}");
        return builder;
    }

    private StringBuilder ExtendTypeWithPartial()
    {
        var builder = new StringBuilder();
        builder.AppendLine($"public partial struct {this.TypeName}");
        builder.AppendLine("{");
        builder.AppendLine($"    public static {this.GetFirstInterface()} Build() => new {this.BuilderName}();");
        builder.AppendLine("}");
        return builder;
    }

    private StringBuilder GenerateInterfaceDeclarations() =>
        this.GetBuilderInterfaces()
            .Select(builderInterface => builderInterface.BuildDeclaration())
            .Aggregate(new StringBuilder(), (builder, declaration) => builder.Append(declaration));

    private IBuilderInterface[] GetBuilderInterfaces()
    {
        var interfaces = new List<IBuilderInterface>();
        for (var index = 0; index < this.OrderedMandatoryProperties.Count(); index++)
        {
            var returnType = index == this.OrderedMandatoryProperties.Length - 1
                ? $"IBuilderForOptional<{this.TypeName}>"
                : $"IBuilderFor{this.OrderedMandatoryProperties[index + 1].Property.Name}";
            interfaces.Add(new MandatoryBuilderInterface(this.OrderedMandatoryProperties[index].Property, returnType));
        }

        interfaces.Add(new OptionalBuilderInterface(optionalProperties, this.TypeName));
        return interfaces.ToArray();
    }

    private StringBuilder GenerateNamespace()
    {
        var namespaceName = type.ContainingNamespace?.ToDisplayString() ?? "";
        var builder = new StringBuilder();
        return !string.IsNullOrEmpty(namespaceName)
            ? builder.AppendLine($"namespace {namespaceName};").AppendLine()
            : builder;
    }

    private StringBuilder GenerateUsingStatements()
    {
        var builder = new StringBuilder();
        builder.AppendLine("using Vonage.Common.Monads;");
        builder.AppendLine("using Vonage.Common.Validation;");
        builder.AppendLine();
        return builder;
    }

    private IEnumerable<string> GetValidationRules() =>
        validationRules.Select(rule => $"{this.TypeName}.{rule.MethodName}");

    private string GetFirstInterface() =>
        this.OrderedMandatoryProperties.Length > 0
            ? $"IBuilderFor{this.OrderedMandatoryProperties[0].Property.Name}"
            : $"IBuilderForOptional<{this.TypeName}>";

    private string[] GetAllInterfaces() =>
        this.OrderedMandatoryProperties.Select(property => string.Concat("IBuilderFor", property.Property.Name))
            .Append($"IBuilderForOptional<{this.TypeName}>").ToArray();

    private static string GetPropertyType(IPropertySymbol propertySymbol) => propertySymbol.Type.ToDisplayString();
}

internal record MandatoryBuilderInterface(IPropertySymbol Property, string ReturnType) : IBuilderInterface
{
    public string Name => $"IBuilderFor{this.Property.Name}";

    public StringBuilder BuildDeclaration()
    {
        var builder = new StringBuilder();
        builder.AppendLine($"public interface {this.Name}");
        builder.AppendLine("{");
        builder.AppendLine(
            $"   {this.ReturnType} With{this.Property.Name}({this.Property.Type.ToDisplayString()} value);");
        builder.AppendLine("}");
        return builder;
    }

    public StringBuilder BuildImplementation()
    {
        var builder = new StringBuilder();
        builder.AppendLine(
            $"    public {this.ReturnType} With{this.Property.Name}({this.Property.Type.ToDisplayString()} value) => this with {{ {this.Property.Name.ToLower()} = value }};");
        return builder;
    }
}

internal record OptionalBuilderInterface(OptionalProperty[] Properties, string GenericType) : IBuilderInterface
{
    public string Name => "IBuilderForOptional";

    public StringBuilder BuildDeclaration()
    {
        var builder = new StringBuilder();
        builder.AppendLine($"public interface IBuilderForOptional<{this.GenericType}>");
        builder.AppendLine("{");
        for (var index = 0; index < this.Properties.Count(); index++)
        {
            builder.AppendLine(
                $"   public IBuilderForOptional<{this.GenericType}> With{this.Properties[index].Property.Name}({this.Properties[index].Property.Type.ToDisplayString()} value);");
        }

        builder.AppendLine($"   Result<{this.GenericType}> Create();");
        builder.AppendLine("}");
        builder.AppendLine();
        return builder;
    }

    public StringBuilder BuildImplementation()
    {
        var builder = new StringBuilder();
        for (var index = 0; index < this.Properties.Count(); index++)
        {
            builder.AppendLine(
                $"    public IBuilderForOptional<{this.GenericType}> With{this.Properties[index].Property.Name}({this.Properties[index].Property.Type.ToDisplayString()} value) => this with {{ {this.Properties[index].Property.Name.ToLower()} = value }};");
        }

        return builder;
    }
}

internal interface IBuilderInterface
{
    public string Name { get; }
    public StringBuilder BuildDeclaration();
    public StringBuilder BuildImplementation();
}