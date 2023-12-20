using System;
using Vonage.Serialization;
using Vonage.SubAccounts;
using Vonage.Test.Unit.Common;
using Vonage.Test.Unit.Common.Extensions;
using Xunit;

namespace Vonage.Test.Unit.SubAccounts.GetSubAccount
{
    public class SerializationTest
    {
        private readonly SerializationTestHelper helper;

        public SerializationTest() =>
            this.helper = new SerializationTestHelper(
                typeof(SerializationTest).Namespace,
                JsonSerializerBuilder.BuildWithSnakeCase());

        [Fact]
        public void ShouldDeserialize200() =>
            this.helper.Serializer
                .DeserializeObject<Account>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(new Account(
                    "aze1243v",
                    "SubAccount department A",
                    "bbe6222f",
                    false,
                    DateTimeOffset.Parse("2018-03-02T17:34:49Z"),
                    true,
                    (decimal) 1.25,
                    15
                ));
    }
}