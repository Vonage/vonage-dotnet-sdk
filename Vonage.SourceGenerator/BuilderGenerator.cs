#region
using System.Collections.Immutable;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
#endregion

namespace Vonage.SourceGenerator;

[Generator]
public class BuilderGenerator : IIncrementalGenerator
{
    private const string BuilderAttribute = "Builder";

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var structDeclarations = IdentifyBuilders(context);
        GenerateCode(context, structDeclarations);
    }

    private static string GenerateBuilder(INamedTypeSymbol structSymbol) =>
        new CodeGenerator(structSymbol,
                GetMandatoryProperties(structSymbol).ToArray(),
                GetOptionalProperties(structSymbol)
                    .Concat(GetOptionalBooleanProperties(structSymbol))
                    .Concat(GetOptionalWithDefaultProperties(structSymbol))
                    .ToArray())
            .GenerateCode();

    private static void GenerateCode(IncrementalGeneratorInitializationContext context,
        IncrementalValueProvider<ImmutableArray<INamedTypeSymbol>> structDeclarations)
    {
        context.RegisterSourceOutput(structDeclarations,
            (productionContext, structSymbols) =>
            {
                structSymbols.ToList().ForEach(symbol => GenerateSourceCode(symbol, productionContext));
            });
    }

    private static void GenerateSourceCode(INamedTypeSymbol structSymbol, SourceProductionContext productionContext)
    {
        var generatedCode = GenerateBuilder(structSymbol);
        var fileName = $"{structSymbol.Name}Builder.g.cs";
        productionContext.AddSource(fileName, SourceText.From(generatedCode, Encoding.UTF8));
    }

    private static IEnumerable<IPropertySymbol> GetMandatoryMembers(INamedTypeSymbol structSymbol) =>
        structSymbol.GetMembers().OfType<IPropertySymbol>()
            .Where(member =>
                member.GetAttributes().Any(attribute => attribute.AttributeClass?.Name == "MandatoryAttribute"));

    private static IEnumerable<MandatoryProperty> GetMandatoryProperties(INamedTypeSymbol structSymbol)
    {
        foreach (var member in GetMandatoryMembers(structSymbol))
        {
            var mandatoryAttr = member.GetAttributes()
                .FirstOrDefault(a => a.AttributeClass?.Name == "MandatoryAttribute");
            var order = int.Parse(mandatoryAttr.ConstructorArguments[0].Value?.ToString() ?? "");
            var property = new MandatoryProperty(member, order);
            if (mandatoryAttr.ConstructorArguments.Length > 1)
            {
                if (mandatoryAttr.ConstructorArguments[1].Kind == TypedConstantKind.Array)
                {
                    property = property with
                    {
                        ValidationRules = mandatoryAttr.ConstructorArguments[1].Values.Select(v => (string?) v.Value)
                            .Select(value => new ValidationRule(value)).ToArray(),
                    };
                }
            }

            yield return property;
        }
    }

    private static IEnumerable<IPropertySymbol> GetOptionalBooleanMembers(INamedTypeSymbol structSymbol) =>
        structSymbol.GetMembers().OfType<IPropertySymbol>()
            .Where(member =>
                member.GetAttributes().Any(attribute => attribute.AttributeClass?.Name == "OptionalBooleanAttribute"));

    private static IEnumerable<IOptionalProperty> GetOptionalBooleanProperties(INamedTypeSymbol structSymbol) =>
        from member in GetOptionalBooleanMembers(structSymbol)
        let attribute = member.GetAttributes()
            .FirstOrDefault(a => a.AttributeClass?.Name == "OptionalBooleanAttribute")
        select new OptionalBooleanProperty(
            member,
            bool.Parse(attribute.ConstructorArguments[0].Value?.ToString()),
            attribute.ConstructorArguments[1].Value?.ToString());

    private static IEnumerable<IPropertySymbol> GetOptionalMembers(INamedTypeSymbol structSymbol) =>
        structSymbol.GetMembers().OfType<IPropertySymbol>()
            .Where(member =>
                member.GetAttributes().Any(attribute => attribute.AttributeClass?.Name == "OptionalAttribute"));

    private static IEnumerable<IOptionalProperty> GetOptionalProperties(INamedTypeSymbol structSymbol) =>
        GetOptionalMembers(structSymbol).Select(member => new OptionalProperty(member));

    private static IEnumerable<IPropertySymbol> GetOptionalWithDefaultMembers(INamedTypeSymbol structSymbol) =>
        structSymbol.GetMembers().OfType<IPropertySymbol>()
            .Where(member =>
                member.GetAttributes()
                    .Any(attribute => attribute.AttributeClass?.Name == "OptionalWithDefaultAttribute"));

    private static IEnumerable<IOptionalProperty> GetOptionalWithDefaultProperties(INamedTypeSymbol structSymbol) =>
        from member in GetOptionalWithDefaultMembers(structSymbol)
        let attribute = member.GetAttributes()
            .FirstOrDefault(a => a.AttributeClass?.Name == "OptionalWithDefaultAttribute")
        select new OptionalWithDefaultProperty(
            member,
            attribute.ConstructorArguments[0].Value?.ToString(),
            attribute.ConstructorArguments[1].Value?.ToString());

    private static INamedTypeSymbol GetStructSymbol(GeneratorSyntaxContext context)
    {
        var structDeclaration = (StructDeclarationSyntax) context.Node;
        return context.SemanticModel.GetDeclaredSymbol(structDeclaration) as INamedTypeSymbol;
    }

    private static bool HasBuilderAttribute(SyntaxNode node) =>
        node is StructDeclarationSyntax declaration
        && declaration.AttributeLists
            .SelectMany(syntax => syntax.Attributes)
            .Any(syntax => syntax.Name.ToString() == BuilderAttribute);

    private static IncrementalValueProvider<ImmutableArray<INamedTypeSymbol>> IdentifyBuilders(
        IncrementalGeneratorInitializationContext context) =>
        context.SyntaxProvider
            .CreateSyntaxProvider(
                static (node, _) => HasBuilderAttribute(node),
                static (context, _) => GetStructSymbol(context)
            )
            .Where(symbol => symbol is not null)
            .Select((symbol, _) => symbol)
            .Collect();
}

internal record MandatoryProperty(IPropertySymbol Property, int Order, params ValidationRule[] ValidationRules)
    : IProperty
{
    public string FieldDeclaration => $"private {this.Property.Type.ToDisplayString()} {this.Property.Name.ToLower()};";
    public string DefaultValueAssignment => $"this.{this.Property.Name.ToLower()} = default;";
}

internal record OptionalProperty(IPropertySymbol Property, params ValidationRule[] ValidationRules) : IOptionalProperty
{
    private string InnerType =>
        this.Property.Type is INamedTypeSymbol namedType &&
        namedType.OriginalDefinition.ToDisplayString().Contains("Maybe<")
            ? namedType.TypeArguments[0].ToDisplayString()
            : this.Property.Type.ToString();

    public string Declaration => $"IBuilderForOptional With{this.Property.Name}({this.InnerType} value);";

    public string FieldDeclaration => $"private {this.Property.Type.ToDisplayString()} {this.Property.Name.ToLower()};";
    public string DefaultValueAssignment => $"this.{this.Property.Name.ToLower()} = Maybe<{this.InnerType}>.None;";

    public string Implementation =>
        $"public IBuilderForOptional With{this.Property.Name}({this.InnerType} value) => this with {{ {this.Property.Name.ToLower()} = value }};";
}

internal record OptionalBooleanProperty(IPropertySymbol Property, bool DefaultValue, string MethodName)
    : IOptionalProperty
{
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
}

internal record OptionalWithDefaultProperty(
    IPropertySymbol Property,
    string type,
    string defaultValue,
    params ValidationRule[] ValidationRules) : IOptionalProperty
{
    public string Declaration => $"IBuilderForOptional With{this.Property.Name}({this.Property.Type} value);";
    public string FieldDeclaration => $"private {this.Property.Type.ToDisplayString()} {this.Property.Name.ToLower()};";
    public string DefaultValueAssignment => $"this.{this.Property.Name.ToLower()} = {this.ParseDefaultValue()};";

    public string Implementation =>
        $"public IBuilderForOptional With{this.Property.Name}({this.Property.Type} value) => this with {{ {this.Property.Name.ToLower()} = value }};";

    private string ParseDefaultValue()
    {
        return this.type switch
        {
            "int" => this.defaultValue,
            "string" => $"\"{this.defaultValue}\"",
            _ => "null",
        };
    }
}

internal record ValidationRule(string MethodName);

internal interface IOptionalProperty : IProperty
{
    string Declaration { get; }
    string Implementation { get; }
}

internal interface IProperty
{
    string FieldDeclaration { get; }
    IPropertySymbol Property { get; }
    string DefaultValueAssignment { get; }
}