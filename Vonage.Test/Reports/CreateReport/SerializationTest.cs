#region
using Vonage.Reports.CreateReport;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Xunit;
#endregion

namespace Vonage.Test.Reports.CreateReport;

[Trait("Category", "Serialization")]
[Trait("Product", "Reports")]
public class SerializationTest
{
    private readonly SerializationTestHelper helper = new SerializationTestHelper(
        typeof(SerializationTest).Namespace,
        JsonSerializerBuilder.BuildWithSnakeCase());

    [Fact]
    public void ShouldSerialize() =>
        RequestBuilderTest.BuildRequest()
            .GetStringContent()
            .Should()
            .BeSuccess(this.helper.GetRequestJson());

    [Fact]
    public void ShouldSerializeWithRequiredFieldsOnly() =>
        RequestBuilderTest.BuildRequestWithRequiredFieldsOnly()
            .GetStringContent()
            .Should()
            .BeSuccess(this.helper.GetRequestJson());
}
