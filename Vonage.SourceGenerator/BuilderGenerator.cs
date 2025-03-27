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

    private static bool HasBuilderAttribute(SyntaxNode node) =>
        node is StructDeclarationSyntax declaration
        && declaration.AttributeLists
            .SelectMany(syntax => syntax.Attributes)
            .Any(syntax => syntax.Name.ToString() == BuilderAttribute);

    private static INamedTypeSymbol GetStructSymbol(GeneratorSyntaxContext context)
    {
        var structDeclaration = (StructDeclarationSyntax) context.Node;
        return context.SemanticModel.GetDeclaredSymbol(structDeclaration) as INamedTypeSymbol;
    }

    private static string GenerateBuilder(INamedTypeSymbol structSymbol)
    {
        var mandatoryProps = new List<IPropertySymbol>();
        var optionalProps = new List<IPropertySymbol>();
        var validationRules = new List<ValidationRule>();
        foreach (var member in GetMandatoryMembers(structSymbol)
                     .OrderBy(member =>
                         int.Parse(member.GetAttributes()
                             .FirstOrDefault(a => a.AttributeClass?.Name == "MandatoryAttribute")
                             .ConstructorArguments[0].Value?.ToString() ?? "")))
        {
            var mandatoryAttr = member.GetAttributes()
                .FirstOrDefault(a => a.AttributeClass?.Name == "MandatoryAttribute");
            mandatoryProps.Add(member);
            if (mandatoryAttr.ConstructorArguments.Length > 1)
            {
                validationRules.Add(new ValidationRule(mandatoryAttr.ConstructorArguments[1].Value?.ToString() ??
                                                       string.Empty));
            }
        }

        foreach (var member in GetOptionalMembers(structSymbol))
        {
            optionalProps.Add(member);
        }

        return new CodeGenerator(structSymbol,
            GetMandatoryProperties(structSymbol).ToArray(),
            GetOptionalProperties(structSymbol).ToArray(),
            validationRules).GenerateCode();
    }

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
                property = property with
                {
                    ValidationRules =
                    [new ValidationRule(mandatoryAttr.ConstructorArguments[1].Value?.ToString() ?? string.Empty)],
                };
            }

            yield return property;
        }
    }

    private static IEnumerable<OptionalProperty> GetOptionalProperties(INamedTypeSymbol structSymbol) =>
        GetOptionalMembers(structSymbol).Select(member => new OptionalProperty(member));

    private static IEnumerable<IPropertySymbol> GetOptionalMembers(INamedTypeSymbol structSymbol) =>
        structSymbol.GetMembers().OfType<IPropertySymbol>()
            .Where(member =>
                member.GetAttributes().Any(attribute => attribute.AttributeClass?.Name == "OptionalAttribute"));

    private static IEnumerable<IPropertySymbol> GetMandatoryMembers(INamedTypeSymbol structSymbol) =>
        structSymbol.GetMembers().OfType<IPropertySymbol>()
            .Where(member =>
                member.GetAttributes().Any(attribute => attribute.AttributeClass?.Name == "MandatoryAttribute"));
}

internal record MandatoryProperty(IPropertySymbol Property, int Order, params ValidationRule[] ValidationRules);
internal record OptionalProperty(IPropertySymbol Property, params ValidationRule[] ValidationRules);
internal record ValidationRule(string MethodName);