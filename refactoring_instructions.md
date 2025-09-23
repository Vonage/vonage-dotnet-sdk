# Test Refactoring Instructions: Object Mother + Custom Assertions Pattern

## Overview

Apply the Object Mother and Custom Assertions patterns to simplify cluttered test classes with large data models, reducing code duplication and improving maintainability.

## Task

Apply Object Mother and Custom Assertions patterns to refactor tests in [TEST_DIRECTORY_PATH].

## Context

- **Problem**: Tests have verbose setup code, complex object creation, and repetitive assertion logic making them hard to read and maintain
- **Solution**: Use Object Mother pattern for test data creation and Custom Assertions pattern for validation logic
- **Goal**: Achieve 47-75% code reduction while maintaining all test functionality
- **Track Record**: Successfully applied to ApplicationTests (75% reduction), MessagingTests (46% reduction), NumberInsights (70% reduction), AccountTest (54% reduction), and ConversionTest (47% reduction)

## Expected Deliverables

### 1. Create Object Mother Class ([TestClass]TestData.cs)

- **Purpose**: Centralize test data creation with factory methods
- **Naming**: `Create[Scenario]Request()` methods for different test scenarios
- **Location**: Same directory as test class
- **Use Expression-Bodied Members**: For single-line factory methods
- **Pattern**:
```csharp
internal static class [TestClass]TestData
{
    internal static [RequestType] CreateBasicRequest() =>
        new [RequestType] { /* minimal setup */ };

    internal static [RequestType] CreateRequestWithAllProperties() =>
        new [RequestType] { /* full setup */ };

    // Additional variations as needed
}
```

### 2. Create Custom Assertions Class ([TestClass]Assertions.cs)

- **Purpose**: Centralize assertion logic with intention-revealing methods
- **Naming**: `Should[Verb][Scenario]()` methods for different validation scenarios
- **Location**: Same directory as test class
- **Use FluentAssertions**: ALWAYS use `actual.Property.Should().Be(expected)` instead of `Assert.Equal()`
- **Use Expression-Bodied Members**: For single-line assertion methods
- **Pattern**:
```csharp
#region
using FluentAssertions;
#endregion

namespace Vonage.Test.[TestArea];

internal static class [TestClass]Assertions
{
    internal static void ShouldBeSuccessfulResponse(this [ResponseType] actual) => 
        actual.Should().BeTrue();

    internal static void ShouldMatchExpectedResponse(this [ResponseType] actual)
    {
        actual.Property.Should().Be(expectedValue);
        actual.AnotherProperty.Should().Be(expectedValue);
    }

    // Private helper methods for granular assertions if needed
    private static void AssertCommonProperties([ResponseType] actual) { }
}
```

### 3. Extract JSON Response Files

- **Purpose**: Separate test data from code into external files
- **Location**: Create `Data/` directory in same location as test class
- **Naming**: `[MethodName]-response.json` (matching test method names without "Async" suffix)
- **Configuration**: MUST add to `.csproj` with `<CopyToOutputDirectory>Always</CopyToOutputDirectory>`
- **Pattern**:
```xml
<None Update="[TestArea]\Data\[MethodName]-response.json">
    <CopyToOutputDirectory>Always</CopyToOutputDirectory>
</None>
```

### 4. Refactor Test Methods

- **Target Pattern**: EXACTLY 3 lines per test following AAA (Arrange-Act-Assert)
- **NO Variables**: Inline all values directly in method calls
- **NO Branching**: Remove `if` statements, `passCreds` parameters, etc.
- **Remove "Async" Suffix**: From test method names for cleaner naming
- **Structure**:
```csharp
[Fact]
public async Task MethodName()
{
    this.Setup($"{this.ApiUrl}/endpoint", this.helper.GetResponseJson(), "expected-request-data");
    var response = await this.BuildClient().MethodAsync([TestClass]TestData.CreateBasicRequest());
    response.ShouldBeExpectedResult();
}

private I[Area]Client BuildClient() =>
    this.BuildVonageClient(Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret)).[Area]Client;
```

### 5. Remove Legacy Code

- Delete old private assertion helper methods from the test class
- Remove verbose object creation code from individual tests
- Remove unnecessary variables like `expectedUri`, `creds`, etc.
- Keep only the essential test infrastructure methods
- Convert `[Theory]` with `[InlineData]` to multiple `[Fact]` methods if they had branching logic

## Success Criteria

1. ✅ All existing tests continue to pass
2. ✅ 47-75% code reduction in test methods
3. ✅ Object Mother pattern implemented with factory methods for different scenarios
4. ✅ Custom Assertions pattern implemented with FluentAssertions and intention-revealing method names
5. ✅ Tests follow clean EXACTLY 3-line AAA pattern
6. ✅ No duplicate test data creation or assertion logic
7. ✅ JSON files properly configured in .csproj with CopyToOutputDirectory
8. ✅ Expression-bodied members used for single-line methods

## Process Steps

1. **Examine** the test class structure and identify patterns
2. **Create** Object Mother class with factory methods for test data (use expression-bodied members)
3. **Create** Custom Assertions class with validation methods using FluentAssertions
4. **Extract** JSON response files to Data/ directory
5. **Configure** JSON files in .csproj with CopyToOutputDirectory=Always
6. **Refactor** each test method to exactly 3-line AAA pattern
7. **Remove** old helper methods, variables, and verbose setup code
8. **Verify** all tests pass after refactoring

## Critical Rules (MUST FOLLOW)

### Test Structure Rules
- **EXACTLY 3 lines per test**: Setup → Act → Assert
- **NO variables**: Inline all values directly
- **NO branching**: Remove if/else, passCreds parameters
- **Remove "Async"**: From test method names
- **Use FluentAssertions**: Never use Assert.Equal(), always use `.Should().Be()`
- **Expression-bodied members**: For single-line methods in TestData and Assertions classes

### JSON File Rules  
- **File naming**: `[MethodName]-response.json` (no "Async" suffix)
- **Location**: `Data/` directory next to test class
- **Configuration**: MUST add to .csproj with `<CopyToOutputDirectory>Always</CopyToOutputDirectory>`
- **Content**: Use exact response content from original tests (may be empty string)

## Common Pitfalls to Avoid

### ❌ Forgot JSON Files Configuration
**Problem**: Tests fail with NullReferenceException because JSON files aren't copied to output directory.
**Solution**: Always add JSON files to .csproj with `<CopyToOutputDirectory>Always</CopyToOutputDirectory>`.

### ❌ Using Variables Instead of Inlining
**Problem**: 
```csharp
var expectedUri = $"{this.ApiUrl}/endpoint";
var creds = Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);
this.Setup(expectedUri, ...);
```
**Solution**: Inline directly:
```csharp
this.Setup($"{this.ApiUrl}/endpoint", this.helper.GetResponseJson(), "request-data");
```

### ❌ Using Assert.Equal Instead of FluentAssertions
**Problem**: `Assert.Equal(expected, actual.Property);`
**Solution**: `actual.Property.Should().Be(expected);`

### ❌ Not Using Expression-Bodied Members
**Problem**: 
```csharp
internal static ConversionRequest CreateBasicRequest()
{
    return new ConversionRequest { /* ... */ };
}
```
**Solution**: 
```csharp
internal static ConversionRequest CreateBasicRequest() =>
    new ConversionRequest { /* ... */ };
```

### ❌ Keeping Branching Logic
**Problem**: Tests with `if (passCreds)` conditionals or `[Theory]` with `[InlineData]` that create branching.
**Solution**: Remove parameters entirely and create separate `[Fact]` methods if needed.

## Example Before/After

### Before (verbose - 66 lines):
```csharp
[Theory]
[InlineData(false)]
[InlineData(true)]
public async Task SmsConversionAsync(bool passCreds)
{
    var expectedUri = $"{this.ApiUrl}/conversions/sms";
    var expectedContent = "message-id=00A0B0C0&delivered=true&timestamp=2020-01-01+12%3A00%3A00&api_key=testkey&api_secret=testSecret&";
    var expectedResponse = "";
    this.Setup(expectedUri, expectedResponse, expectedContent);
    var request = new ConversionRequest
        {Delivered = true, MessageId = "00A0B0C0", TimeStamp = "2020-01-01 12:00:00"};
    var creds = Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);
    var client = this.BuildVonageClient(creds);
    bool response;
    if (passCreds)
    {
        response = await client.ConversionClient.SmsConversionAsync(request, creds);
    }
    else
    {
        response = await client.ConversionClient.SmsConversionAsync(request);
    }
    Assert.True(response);
}
```

### After (clean - 35 lines, 47% reduction):
```csharp
[Fact]
public async Task SmsConversion()
{
    this.Setup($"{this.ApiUrl}/conversions/sms", this.helper.GetResponseJson(), "message-id=00A0B0C0&delivered=true&timestamp=2020-01-01+12%3A00%3A00&api_key=testkey&api_secret=testSecret&");
    var response = await this.BuildConversionClient().SmsConversionAsync(ConversionTestData.CreateBasicRequest());
    response.ShouldBeSuccessfulConversion();
}

private IConversionClient BuildConversionClient() =>
    this.BuildVonageClient(Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret)).ConversionClient;
```

## Track Record

Successfully applied with all tests passing:
- **ApplicationTests**: 75% code reduction  
- **MessagingTests**: 46% code reduction
- **NumberInsights**: 70% code reduction
- **AccountTest**: 54% code reduction (266 → 121 lines)
- **ConversionTest**: 47% code reduction (66 → 35 lines)