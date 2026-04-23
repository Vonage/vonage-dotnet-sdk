#region
using System;
using FluentAssertions;
using Vonage.Reports;
using Vonage.Reports.LoadRecords;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Xunit;
#endregion

namespace Vonage.Test.Reports.LoadRecords;

[Trait("Category", "Serialization")]
[Trait("Product", "Reports")]
public class SerializationTest
{
    private readonly SerializationTestHelper helper = new SerializationTestHelper(
        typeof(SerializationTest).Namespace,
        JsonSerializerBuilder.BuildWithSnakeCase());

    [Fact]
    public void ShouldDeserialize200() =>
        this.helper.Serializer
            .DeserializeObject<LoadRecordsResponse>(this.helper.GetResponseJson())
            .Should()
            .BeSuccess(VerifyExpectedResponse);

    internal static void VerifyExpectedResponse(LoadRecordsResponse response)
    {
        response.RequestId.Should().Be(Guid.Parse("a91b34c2-5d98-4c0e-8f23-a6b1c7d4e9f0"));
        response.RequestStatus.Should().Be(SyncRequestStatus.Success);
        response.ReceivedAt.Should().Be(DateTimeOffset.Parse("2024-02-07T14:22:08+00:00"));
        response.ItemsCount.Should().Be(50);
        response.Product.Should().Be(ReportProduct.Sms);
        response.Cursor.Should().BeSome("MTY0OTQ3ODAwMDAwMA");
        response.Iv.Should().BeSome("8a2c4e6f-12d3-45b6-78c9-0a1b2c3d4e5f");
        response.IdsNotFound.Should().BeNone();
        response.Links.Self.Href.Should().NotBeNull();
        response.Links.Next.Href.Should().NotBeNull();
        response.Records.Length.Should().Be(1);
    }
}
