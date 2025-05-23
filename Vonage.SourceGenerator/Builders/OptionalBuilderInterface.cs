#region
using Vonage.SourceGenerator.Properties;
#endregion

namespace Vonage.SourceGenerator.Builders;

internal record OptionalBuilderInterface(IOptionalProperty[] Properties, string GenericType) : IBuilderInterface
{
    public string Name => "IBuilderForOptional";

    public string BuildDeclaration() => $$"""
                                          public interface IBuilderForOptional : IVonageRequestBuilder<{{this.GenericType}}>
                                          {
                                                {{string.Join("\n", this.Properties.Select(property => property.Declaration))}}
                                          }
                                          """;

    public string BuildImplementation() =>
        string.Join("\n", this.Properties.Select(property => property.Implementation));
}