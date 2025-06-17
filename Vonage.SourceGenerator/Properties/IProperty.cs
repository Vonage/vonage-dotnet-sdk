#region
using Microsoft.CodeAnalysis;
#endregion

namespace Vonage.SourceGenerator.Properties;

internal interface IProperty
{
    string FieldDeclaration { get; }
    IPropertySymbol Property { get; }
    string DefaultValueAssignment { get; }
}

internal interface IOptionalProperty : IProperty
{
    string Declaration { get; }
    string Implementation { get; }
}