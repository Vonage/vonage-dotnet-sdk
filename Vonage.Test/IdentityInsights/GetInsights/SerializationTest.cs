#region
using System;
using Vonage.IdentityInsights.GetInsights;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Xunit;
#endregion

namespace Vonage.Test.IdentityInsights.GetInsights;

[Trait("Category", "Serialization")]
[Trait("Product", "IdentityInsights")]
public class SerializationTest
{
    private readonly SerializationTestHelper helper = new SerializationTestHelper(
        typeof(SerializationTest).Namespace,
        JsonSerializerBuilder.BuildWithSnakeCase());

    [Fact]
    public void ShouldDeserialize200() =>
        this.helper.Serializer
            .DeserializeObject<GetInsightsResponse>(this.helper.GetResponseJson())
            .Should()
            .BeSuccess(GetExpectedInsights());

    internal static GetInsightsResponse GetExpectedInsights() => new GetInsightsResponse(
        new Guid("7e190c2f-22aa-4732-b44e-3794de3fa035"),
        new Insights(
            new FormatInsights("US", "United States", "1", "Georgia", ["America/New_York"], "+14040000000",
                "(404) 000-0000", true, new InsightStatus("OK", "Success")),
            new SimSwapInsights(DateTimeOffset.Parse("2024-07-08T09:30:27.504Z"), true,
                new InsightStatus("OK", "Success")),
            new CarrierInsights("Orange Espana, S.A. Unipersonal", "MOBILE", "ES", "21403",
                new InsightStatus("OK", "Success")),
            new CarrierInsights("Orange Espana, S.A. Unipersonal", "MOBILE", "ES", "21403",
                new InsightStatus("OK", "Success"))));

    [Fact]
    public void ShouldSerializeWithDefaultValues() =>
        RequestBuilderTest.BuildRequestWithDefaultValues()
            .GetStringContent()
            .Should()
            .BeSuccess(this.helper.GetRequestJson());

    [Fact]
    public void ShouldSerialize() =>
        RequestBuilderTest.BuildRequest()
            .GetStringContent()
            .Should()
            .BeSuccess(this.helper.GetRequestJson());
}