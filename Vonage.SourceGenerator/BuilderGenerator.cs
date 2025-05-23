#region
using System.Collections.Immutable;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using Vonage.SourceGenerator.Properties;
#endregion

namespace Vonage.SourceGenerator;

[Generator]
public class BuilderGenerator : IIncrementalGenerator
{
    private const string BuilderAttribute = "Builder";
    private const string OptionalAttributeName = "OptionalAttribute";

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

    private static IEnumerable<MandatoryProperty> GetMandatoryProperties(INamedTypeSymbol structSymbol) =>
        GetMembersForAttribute(structSymbol, MandatoryProperty.AttributeName).Select(MandatoryProperty.FromMember);

    private static IEnumerable<IPropertySymbol> GetMembersForAttribute(INamedTypeSymbol structSymbol,
        string attributeName) =>
        structSymbol.GetMembers().OfType<IPropertySymbol>()
            .Where(member =>
                member.GetAttributes().Any(attribute => attribute.AttributeClass?.Name == attributeName));

    private static IEnumerable<IOptionalProperty> GetOptionalBooleanProperties(INamedTypeSymbol structSymbol) =>
        GetMembersForAttribute(structSymbol, OptionalBooleanProperty.AttributeName)
            .Select(OptionalBooleanProperty.FromMember);

    private static IEnumerable<IPropertySymbol> GetOptionalMembers(INamedTypeSymbol structSymbol) =>
        GetMembersForAttribute(structSymbol, OptionalAttributeName);

    private static IEnumerable<IOptionalProperty> GetOptionalProperties(INamedTypeSymbol structSymbol) =>
        GetOptionalMembers(structSymbol).Select(member => new OptionalProperty(member));

    private static IEnumerable<IOptionalProperty> GetOptionalWithDefaultProperties(INamedTypeSymbol structSymbol) =>
        GetMembersForAttribute(structSymbol, OptionalWithDefaultProperty.AttributeName)
            .Select(OptionalWithDefaultProperty.FromMember);

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