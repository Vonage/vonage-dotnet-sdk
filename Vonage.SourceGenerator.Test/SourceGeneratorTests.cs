﻿#region
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
#endregion

namespace Vonage.SourceGenerator.Test;

[Trait("Category", "SourceGeneration")]
public class BuilderGeneratorTests
{
    private const string TestNamespace = "TestNamespace";

    [Theory]
    [InlineData("EmptyRequest")]
    [InlineData("MandatoryOnly")]
    [InlineData("MandatorySingleValidationRule")]
    [InlineData("MandatoryMultipleValidationRule")]
    [InlineData("OptionalOnly")]
    [InlineData("OptionalBoolean")]
    [InlineData("OptionalWithDefault")]
    [InlineData("CustomUsing")]
    public void VerifyCodeGeneration(string sample)
    {
        var inputCode = File.ReadAllText($"Files/{sample}_Input.txt").Trim();
        var expectedGeneratedCode = File.ReadAllText($"Files/{sample}_Expected.txt").Trim();
        Assert.Equal(NormalizeLineBreaks(expectedGeneratedCode), NormalizeLineBreaks(GenerateCode(inputCode)));
    }

    private static string GenerateCode(string inputCode)
    {
        var compilation = CSharpCompilation.Create(TestNamespace, [CSharpSyntaxTree.ParseText(inputCode)],
            [MetadataReference.CreateFromFile(typeof(object).Assembly.Location)],
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
        );
        var driver = CSharpGeneratorDriver.Create(new AttributesGenerator(), new BuilderGenerator());
        driver.RunGeneratorsAndUpdateCompilation(compilation, out var outputCompilation, out _);
        return outputCompilation.SyntaxTrees.Last().ToString().Trim();
    }

    private static string NormalizeLineBreaks(string input) => input.Replace("\r\n", "\n").Replace("\r", "\n");
}