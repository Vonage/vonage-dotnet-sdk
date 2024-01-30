using System;
using FluentAssertions;
using Vonage.Common;
using Vonage.Serialization;
using Vonage.SubAccounts;
using Vonage.SubAccounts.GetTransfers;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.SubAccounts.GetTransfers.Credit;

[Trait("Category", "Serialization")]
public class SerializationTest
{
    private readonly SerializationTestHelper helper;

    public SerializationTest() =>
        this.helper = new SerializationTestHelper(
            typeof(SerializationTest).Namespace,
            JsonSerializerBuilder.BuildWithSnakeCase());

    public static Transfer[] GetExpectedTransfers() =>
        new[]
        {
            new Transfer(
                new Guid("297016aa-4061-430d-b805-a4d00522bb00"),
                (decimal) 123.45,
                "7c9738e6",
                "ad6dc56f",
                "This gets added to the audit log",
                DateTimeOffset.Parse("2019-03-02T16:34:49Z")),
        };

    [Fact]
    public void ShouldDeserialize200() =>
        this.helper.Serializer
            .DeserializeObject<EmbeddedResponse<GetTransfersResponse>>(this.helper.GetResponseJson())
            .Should()
            .BeSuccess(success =>
                success.Content.CreditTransfers.Should().BeEquivalentTo(GetExpectedTransfers()));
}