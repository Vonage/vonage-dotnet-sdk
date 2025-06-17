#region
using Microsoft.CodeAnalysis;
#endregion

namespace Vonage.SourceGenerator.Properties;

internal record ValidationRuleMethod(string Name)
{
    public const string AttributeName = "ValidationRuleAttribute";

    public static ValidationRuleMethod FromMember(IMethodSymbol member) => new ValidationRuleMethod(member.Name);
}