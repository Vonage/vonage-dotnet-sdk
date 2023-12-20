using System;
using Vonage.Serialization;
using Vonage.SubAccounts;
using Vonage.SubAccounts.CreateSubAccount;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.SubAccounts.CreateSubAccount
{
    public class SerializationTest
    {
        private readonly SerializationTestHelper helper;

        public SerializationTest() =>
            this.helper = new SerializationTestHelper(
                typeof(SerializationTest).Namespace,
                JsonSerializerBuilder.BuildWithSnakeCase());

        public static Account GetExpectedAccount() =>
            new Account(
                "aze1243v",
                "SubAccount department A",
                "bbe6222f",
                false,
                DateTimeOffset.Parse("2018-03-02T17:34:49Z"),
                true,
                (decimal) 1.25,
                15
            );

        [Fact]
        public void ShouldDeserialize200() =>
            this.helper.Serializer
                .DeserializeObject<Account>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(GetExpectedAccount());

        [Fact]
        public void ShouldSerialize() =>
            CreateSubAccountRequest.Build()
                .WithName("My SubAccount")
                .WithSecret("123456789AbcDef")
                .DisableSharedAccountBalance()
                .Create()
                .GetStringContent()
                .Should()
                .BeSuccess(this.helper.GetRequestJson());

        [Fact]
        public void ShouldSerializeWithDefaultValues() =>
            CreateSubAccountRequest.Build()
                .WithName("My SubAccount")
                .Create()
                .GetStringContent()
                .Should()
                .BeSuccess(this.helper.GetRequestJson());
    }
}