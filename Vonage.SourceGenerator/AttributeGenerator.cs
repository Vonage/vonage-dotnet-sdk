#region
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
#endregion

namespace Vonage.SourceGenerator;

[Generator]
public class AttributesGenerator : IIncrementalGenerator
{
    private const string BuilderAttributeSource =
        @"
[AttributeUsage(AttributeTargets.Struct)] public sealed class BuilderAttribute : Attribute 
{
    public string[] Usings { get; }
    public BuilderAttribute(params string[] usings) => this.Usings = usings;
}
";

    private const string ValidationAttributeSource =
        "[AttributeUsage(AttributeTargets.Method)] public sealed class ValidationRuleAttribute : Attribute { }";

    private const string MandatoryAttributeSource = @"
[AttributeUsage(AttributeTargets.Property)]
public sealed class MandatoryAttribute : Attribute
{
    public int Order { get; }
    public MandatoryAttribute(int order) => this.Order = order;
}

[AttributeUsage(AttributeTargets.Property)]
public sealed class MandatoryWithParsingAttribute : Attribute
{
    public int Order { get; }
    public string ParserMethodName { get; }
    public MandatoryWithParsingAttribute(int order, string parserMethodName)
    {
        this.Order = order;
        this.ParserMethodName = parserMethodName;
    }
}";

    private const string OptionalAttributeSource =
        @"
[AttributeUsage(AttributeTargets.Property)] public sealed class OptionalAttribute : Attribute { }

[AttributeUsage(AttributeTargets.Property)] public sealed class OptionalBooleanAttribute(bool DefaultValue, string MethodName) : Attribute { }

[AttributeUsage(AttributeTargets.Property)]
public sealed class OptionalWithDefaultAttribute : Attribute
{
    public OptionalWithDefaultAttribute(string type, string defaultValue)
    {
        Type = type;
        DefaultValue = defaultValue;
    }
    public string Type { get; }
    public string DefaultValue { get; }
}

[AttributeUsage(AttributeTargets.Property)]
public sealed class OptionalWithParsingAttribute : Attribute
{
    public string ParserMethodName { get; }
    public OptionalWithParsingAttribute(string parserMethodName) => this.ParserMethodName = parserMethodName;
}
";

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(static ctx =>
        {
            var builder = new StringBuilder();
            builder.AppendLine("using System;");
            builder.AppendLine(BuilderAttributeSource);
            builder.AppendLine(OptionalAttributeSource);
            builder.AppendLine(MandatoryAttributeSource);
            builder.AppendLine(ValidationAttributeSource);
            ctx.AddSource("Attributes.g.cs", SourceText.From(builder.ToString(), Encoding.UTF8));
        });
    }
}