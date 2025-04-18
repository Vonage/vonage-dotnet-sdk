﻿#region
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
#endregion

namespace Vonage.SourceGenerator;

[Generator]
public class AttributesGenerator : IIncrementalGenerator
{
    private const string MandatoryAttributeSource = @"
[AttributeUsage(AttributeTargets.Property)]
public sealed class MandatoryAttribute : Attribute
{
    public string ValidationMethodName { get; }
    public int Order { get; }
    public MandatoryAttribute(int order) => this.Order = order;
    public MandatoryAttribute(int order, string validationMethodName)
        : this(order) => this.ValidationMethodName = validationMethodName;
}";

    private const string OptionalAttributeSource =
        @"
[AttributeUsage(AttributeTargets.Property)] public sealed class OptionalAttribute : Attribute { }

[AttributeUsage(AttributeTargets.Property)] public sealed class OptionalBooleanAttribute(string TrueMethodName, string FalseMethodName) : Attribute { }
";

    private const string BuilderAttributeSource =
        "[AttributeUsage(AttributeTargets.Struct)] public sealed class BuilderAttribute : Attribute { }";

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(static ctx =>
        {
            var builder = new StringBuilder();
            builder.AppendLine("using System;");
            builder.AppendLine(BuilderAttributeSource);
            builder.AppendLine(OptionalAttributeSource);
            builder.AppendLine(MandatoryAttributeSource);
            ctx.AddSource("Attributes.g.cs", SourceText.From(builder.ToString(), Encoding.UTF8));
        });
    }
}