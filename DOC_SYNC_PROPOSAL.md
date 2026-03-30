# Documentation Sync Proposal

This document outlines a proposed approach for keeping XML documentation examples in sync with the snippets repository.

## Problem

XML documentation examples can become stale after API changes. There's no compile-time check that catches invalid examples, and maintaining examples in both the SDK and snippets repository leads to duplication and drift.

## Proposed Solution

Sync XML doc examples from the snippets repository, making snippets the single source of truth.

```
vonage-dotnet-code-snippets (source of truth)
            │
            ▼
       [Sync Tool]
            │
            ▼
    Vonage SDK XML docs
```

---

## 1. Convention in Snippets Repo

Add markers to identify extractable regions and their targets:

```csharp
// In snippets repo: IdentityInsights/GetInsights.cs

public async Task GetInsightsExample()
{
    // [DocExample: Vonage.IdentityInsights.IIdentityInsightsClient.GetInsightsAsync]
    var request = GetInsightsRequest.Build()
        .WithPhoneNumber("+14155552671")
        .WithFormat()
        .WithSimSwap(new SimSwapRequest(24))
        .Create();
    var response = await client.GetInsightsAsync(request);
    // [/DocExample]
}
```

### Marker Format

```
// [DocExample: {Namespace}.{Type}.{Member}]
...code...
// [/DocExample]
```

- `Namespace.Type.Member` identifies the target XML doc location
- Code between markers is extracted verbatim (minus leading indentation)
- Multiple markers can exist in one file
- Same target can have multiple examples (tool would combine or pick first)

---

## 2. Sync Tool

A .NET console application that:

1. Scans snippets repository for `[DocExample]` markers
2. Extracts code between markers
3. Locates corresponding XML doc in SDK source files
4. Replaces `<example><code><![CDATA[...]]></code></example>` blocks
5. Writes updated files

### File Structure

```
/tools
  /Vonage.DocSync
    Vonage.DocSync.csproj
    Program.cs
    DocExampleExtractor.cs
    XmlDocUpdater.cs
```

### Project File

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
    <PackAsTool>true</PackAsTool>
    <ToolCommandName>doc-sync</ToolCommandName>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.CodeAnalysis.CSharp" Version="4.8.0" />
  </ItemGroup>
</Project>
```

### Core Logic (Pseudocode)

```csharp
// Program.cs
var snippetsPath = args[0];  // Path to snippets repo
var sdkPath = args[1];       // Path to SDK Vonage/ folder
var verifyOnly = args.Contains("--verify");

// 1. Extract all doc examples from snippets
var examples = DocExampleExtractor.Extract(snippetsPath);
// Returns: IEnumerable<(string Target, string Code)>

// 2. For each example, update corresponding XML doc
foreach (var (target, code) in examples)
{
    // target = "Vonage.IdentityInsights.IIdentityInsightsClient.GetInsightsAsync"
    var result = XmlDocUpdater.Update(sdkPath, target, code, verifyOnly);

    if (result.Changed && verifyOnly)
    {
        Console.WriteLine($"OUT OF SYNC: {target}");
        exitCode = 1;
    }
}

return exitCode;
```

### DocExampleExtractor.cs

```csharp
public static class DocExampleExtractor
{
    private static readonly Regex StartMarker = new(@"//\s*\[DocExample:\s*(.+?)\]");
    private static readonly Regex EndMarker = new(@"//\s*\[/DocExample\]");

    public static IEnumerable<(string Target, string Code)> Extract(string snippetsPath)
    {
        foreach (var file in Directory.GetFiles(snippetsPath, "*.cs", SearchOption.AllDirectories))
        {
            var lines = File.ReadAllLines(file);
            string? currentTarget = null;
            var codeLines = new List<string>();
            int? baseIndent = null;

            foreach (var line in lines)
            {
                if (currentTarget == null)
                {
                    var startMatch = StartMarker.Match(line);
                    if (startMatch.Success)
                    {
                        currentTarget = startMatch.Groups[1].Value.Trim();
                        codeLines.Clear();
                        baseIndent = null;
                    }
                }
                else if (EndMarker.IsMatch(line))
                {
                    yield return (currentTarget, string.Join("\n", codeLines));
                    currentTarget = null;
                }
                else
                {
                    // Normalize indentation
                    baseIndent ??= line.TakeWhile(char.IsWhiteSpace).Count();
                    var normalizedLine = line.Length > baseIndent.Value
                        ? line[baseIndent.Value..]
                        : line.TrimStart();
                    codeLines.Add(normalizedLine);
                }
            }
        }
    }
}
```

### XmlDocUpdater.cs

```csharp
public static class XmlDocUpdater
{
    public static (bool Changed, string? Error) Update(
        string sdkPath,
        string target,
        string code,
        bool verifyOnly)
    {
        // Parse target: "Vonage.IdentityInsights.IIdentityInsightsClient.GetInsightsAsync"
        var parts = target.Split('.');
        var memberName = parts[^1];
        var typeName = parts[^2];

        // Find file containing the type (simple approach: grep for "interface {typeName}" or "class {typeName}")
        var file = FindFileForType(sdkPath, typeName);
        if (file == null)
            return (false, $"Could not find file for type {typeName}");

        var content = File.ReadAllText(file);

        // Find the XML doc example block for this member
        // This regex finds: /// <example>...<![CDATA[...]]>...</example> before the member
        var pattern = $@"(/// <example>\s*/// <code><!\[CDATA\[)[\s\S]*?(\]\]></code>\s*/// </example>\s*.*?{memberName})";

        var newExample = $"$1\n{IndentCode(code, "/// ")}\n/// $2";

        var newContent = Regex.Replace(content, pattern, newExample);

        if (content == newContent)
            return (false, null);  // No change needed

        if (!verifyOnly)
            File.WriteAllText(file, newContent);

        return (true, null);
    }

    private static string IndentCode(string code, string prefix)
    {
        return string.Join("\n", code.Split('\n').Select(line => prefix + line));
    }
}
```

---

## 3. Integration Options

### Option A: Manual Execution

Run before releases:

```bash
dotnet run --project tools/Vonage.DocSync -- \
    ../vonage-dotnet-code-snippets \
    ./Vonage
```

### Option B: Pre-commit Hook

```bash
#!/bin/sh
# .husky/pre-commit or .git/hooks/pre-commit

dotnet run --project tools/Vonage.DocSync -- \
    ../vonage-dotnet-code-snippets \
    ./Vonage \
    --verify

if [ $? -ne 0 ]; then
    echo "Doc examples are out of sync. Run 'dotnet doc-sync' to update."
    exit 1
fi
```

### Option C: GitHub Actions CI

```yaml
name: Verify Doc Examples

on: [push, pull_request]

jobs:
  verify-docs:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v4

      - name: Checkout snippets repo
        uses: actions/checkout@v4
        with:
          repository: Vonage/vonage-dotnet-code-snippets
          path: snippets

      - name: Setup .NET
        uses: actions/setup-dotnet@v4
        with:
          dotnet-version: '8.0.x'

      - name: Verify doc examples are in sync
        run: |
          dotnet run --project tools/Vonage.DocSync -- \
              ./snippets \
              ./Vonage \
              --verify
```

---

## 4. Challenges and Mitigations

| Challenge | Mitigation |
|-----------|------------|
| Snippets repo is separate Git repo | CI clones both repos; local dev uses relative paths |
| Indentation differences | Tool normalizes indentation when extracting |
| Not all XML docs need snippets | Only processes where markers exist; others unchanged |
| Complex examples with setup code | Markers define exact boundaries to extract |
| Member overloads | Use full signature in marker if needed |
| Finding correct file for type | Use Roslyn for accurate lookup, or simple grep |

---

## 5. Migration Path

### Phase 1: Proof of Concept
1. Create basic sync tool
2. Add markers to 5-10 snippets (IdentityInsights, Messages basics)
3. Verify it works locally

### Phase 2: Expand Coverage
1. Add markers to remaining snippets
2. Add CI verification step (non-blocking initially)
3. Document the marker convention in snippets repo

### Phase 3: Full Integration
1. Make CI check blocking
2. Add pre-commit hook (optional)
3. Update contribution guidelines

---

## 6. Alternative: Compile-Only Verification

If full sync is too complex initially, a simpler alternative is to just verify that existing XML doc examples compile:

```csharp
// Tool that:
// 1. Extracts all <![CDATA[...]]> blocks from XML docs
// 2. Wraps them in a compilable class
// 3. Attempts to compile using Roslyn
// 4. Reports any compilation errors

// This catches stale examples without requiring sync infrastructure
```

This could be a stepping stone before implementing full sync.

---

## 7. Decision Points

Before implementation, decide:

1. **Marker format**: Is `[DocExample: Full.Type.Name]` clear enough?
2. **Snippets repo structure**: Any restructuring needed to support this?
3. **CI vs local**: Where should this primarily run?
4. **Blocking vs warning**: Should out-of-sync examples block builds/PRs?
5. **Scope**: All examples, or just key entry points (client methods)?
