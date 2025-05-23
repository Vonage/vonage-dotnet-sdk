namespace Vonage.SourceGenerator.Builders;

internal interface IBuilderInterface
{
    string Name { get; }
    string BuildDeclaration();
    string BuildImplementation();
}