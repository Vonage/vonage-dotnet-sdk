#region
using System.Collections.Generic;
using FluentAssertions;
using Vonage.Messaging;
using Xunit;
#endregion

namespace Vonage.Test.Messaging;

public class SignatureValidationTest
{
    [Fact]
    public void BuildQueryString_ShouldExcludeSigKey() =>
        SignatureValidation.BuildQueryString(new Dictionary<string, string>
        {
            {"sig", "signature_value"},
            {"param1", "value1"},
            {"param2", "value2"},
        }).Should().Be("&param1=value1&param2=value2");

    [Fact]
    public void BuildQueryString_ShouldOrderParametersAlphabetically() =>
        SignatureValidation.BuildQueryString(new Dictionary<string, string>
        {
            {"order", "value3"},
            {"messageId", "value1"},
            {"type", "value4"},
            {"message-timestamp", "value2"},
        }).Should().Be("&message-timestamp=value2&messageId=value1&order=value3&type=value4");

    [Fact]
    public void BuildQueryString_ShouldReturnEmpty_GivenDictionaryIsEmpty() =>
        SignatureValidation.BuildQueryString(new Dictionary<string, string>())
            .Should().BeEmpty();

    [Fact]
    public void BuildQueryString_ShouldReturnEmpty_WhenNullDictionaryProvided() =>
        SignatureValidation.BuildQueryString(null).Should().BeEmpty();

    [Fact]
    public void BuildQueryString_ShouldSkipNullValues() =>
        SignatureValidation.BuildQueryString(new Dictionary<string, string>
        {
            {"key1", null},
            {"key2", "value2"},
        }).Should().Be("&key2=value2");
}