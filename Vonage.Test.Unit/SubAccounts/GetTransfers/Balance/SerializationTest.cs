using System;
using FluentAssertions;
using Vonage.Common;
using Vonage.Serialization;
using Vonage.SubAccounts;
using Vonage.SubAccounts.GetTransfers;
using Vonage.Test.Unit.Common;
using Vonage.Test.Unit.Common.Extensions;
using Xunit;

namespace Vonage.Test.Unit.SubAccounts.GetTransfers.Balance
{
    public class SerializationTest
    {
        private readonly SerializationTestHelper helper;

        public SerializationTest() =>
            this.helper = new SerializationTestHelper(
                typeof(SerializationTest).Namespace,
                JsonSerializerBuilder.BuildWithSnakeCase());

        public static Transfer[] GetExpectedTransfers() => new[]
        {
            new Transfer(
                new Guid("c268d4a0-bff5-4865-a0c2-a8dbab781c3a"),
                (decimal) 1.0,
                "7c9738e6",
                "ad6dc56f",
                "Test from SDK",
                DateTimeOffset.Parse("2023-06-16T13:28:34.000Z")),
        };

        [Fact]
        public void ShouldDeserialize200() =>
            this.helper.Serializer
                .DeserializeObject<EmbeddedResponse<GetTransfersResponse>>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(success =>
                    success.Content.BalanceTransfers.Should().BeEquivalentTo(GetExpectedTransfers()));
    }
}