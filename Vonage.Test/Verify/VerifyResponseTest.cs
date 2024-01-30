using FluentAssertions;
using Newtonsoft.Json;
using Vonage.Verify;
using Xunit;

namespace Vonage.Test.Verify;

[Trait("Category", "Serialization")]
public class VerifyResponseTest
{
    [Theory]
    [InlineData(
        @"{ ""request_id"": ""f8aaf4d4-d740-4984-8c6f-5c1146c5af94"", ""status"": ""0"", ""error_text"": ""Some error"", ""network"": ""Some network issue"" }",
        "f8aaf4d4-d740-4984-8c6f-5c1146c5af94", "0", "Some error", "Some network issue")]
    [InlineData(@"{ ""request_id"": ""f8aaf4d4-d740-4984-8c6f-5c1146c5af94"", ""status"": ""0"" }",
        "f8aaf4d4-d740-4984-8c6f-5c1146c5af94", "0", null, null)]
    [InlineData("{}", null, null, null, null)]
    public void Deserialize_ShouldReturnValidInstance(string json,
        string expectedRequestId, string expectedStatus, string expectedErrorText, string expectedNetwork)
    {
        var response = JsonConvert.DeserializeObject<VerifyResponse>(json);
        response.RequestId.Should().Be(expectedRequestId);
        response.Status.Should().Be(expectedStatus);
        response.ErrorText.Should().Be(expectedErrorText);
        response.Network.Should().Be(expectedNetwork);
    }
}