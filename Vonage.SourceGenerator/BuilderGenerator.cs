#region
using System.Collections.Immutable;
using System.Diagnostics;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
#endregion

namespace Vonage.SourceGenerator;

[Generator]
public class BuilderGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        if (!Debugger.IsAttached)
        {
            Debugger.Launch();
        }

        var structDeclarations = IdentifyBuilders(context);
        GenerateCode(context, structDeclarations);
    }

    private static void GenerateCode(IncrementalGeneratorInitializationContext context,
        IncrementalValueProvider<ImmutableArray<INamedTypeSymbol>> structDeclarations)
    {
        context.RegisterSourceOutput(structDeclarations, (productionContext, structSymbols) =>
        {
            foreach (var structSymbol in structSymbols)
            {
                var generatedCode = GenerateBuilder(structSymbol);
                var fileName = $"{structSymbol.Name}Builder.g.cs";
                productionContext.AddSource(fileName, SourceText.From(generatedCode, Encoding.UTF8));
            }
        });
    }

    private static IncrementalValueProvider<ImmutableArray<INamedTypeSymbol>> IdentifyBuilders(
        IncrementalGeneratorInitializationContext context)
    {
        return context.SyntaxProvider
            .CreateSyntaxProvider(
                static (node, _) => IsStructWithBuilderAttribute(node),
                static (context, _) => GetStructSymbol(context)
            )
            .Where(symbol => symbol is not null)
            .Select((symbol, _) => symbol!)
            .Collect();
    }

    private static bool IsStructWithBuilderAttribute(SyntaxNode node)
    {
        return node is StructDeclarationSyntax structDecl &&
               structDecl.AttributeLists
                   .SelectMany(al => al.Attributes)
                   .Any(attr => attr.Name.ToString() == "Builder");
    }

    private static INamedTypeSymbol? GetStructSymbol(GeneratorSyntaxContext context)
    {
        var structDeclaration = (StructDeclarationSyntax) context.Node;
        var model = context.SemanticModel;
        return model.GetDeclaredSymbol(structDeclaration) as INamedTypeSymbol;
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
                validationRules.Add(new ValidationRule(member.Name,
                    mandatoryAttr.ConstructorArguments[1].Value?.ToString() ?? string.Empty));
            }
        }

        foreach (var member in GetOptionalMembers(structSymbol))
        {
            optionalProps.Add(member);
        }

        return new CodeGenerator(structSymbol, mandatoryProps.ToArray(), optionalProps.ToArray(), validationRules)
            .GenerateCode();
    }

    private static IEnumerable<IPropertySymbol> GetOptionalMembers(INamedTypeSymbol structSymbol) =>
        structSymbol.GetMembers().OfType<IPropertySymbol>()
            .Where(member =>
                member.GetAttributes().Any(attribute => attribute.AttributeClass?.Name == "OptionalAttribute"));

    private static IEnumerable<IPropertySymbol> GetMandatoryMembers(INamedTypeSymbol structSymbol) =>
        structSymbol.GetMembers().OfType<IPropertySymbol>()
            .Where(member =>
                member.GetAttributes().Any(attribute => attribute.AttributeClass?.Name == "MandatoryAttribute"));
}

internal record ValidationRule(string Property, string MethodName);