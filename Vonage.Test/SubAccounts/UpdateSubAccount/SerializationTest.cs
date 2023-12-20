using System;
using Vonage.Serialization;
using Vonage.SubAccounts;
using Vonage.SubAccounts.UpdateSubAccount;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.SubAccounts.UpdateSubAccount
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
            UpdateSubAccountRequest.Build()
                .WithSubAccountKey("RandomKey")
                .WithName("Subaccount department B")
                .SuspendAccount()
                .DisableSharedAccountBalance()
                .Create()
                .GetStringContent()
                .Should()
                .BeSuccess(this.helper.GetRequestJson());

        [Fact]
        public void ShouldSerializeWithOnlyEnabledAccount() =>
            UpdateSubAccountRequest.Build()
                .WithSubAccountKey("RandomKey")
                .EnableAccount()
                .Create()
                .GetStringContent()
                .Should()
                .BeSuccess(this.helper.GetRequestJson());

        [Fact]
        public void ShouldSerializeWithOnlyEnabledSharedBalance() =>
            UpdateSubAccountRequest.Build()
                .WithSubAccountKey("RandomKey")
                .EnableSharedAccountBalance()
                .Create()
                .GetStringContent()
                .Should()
                .BeSuccess(this.helper.GetRequestJson());

        [Fact]
        public void ShouldSerializeWithOnlyName() =>
            UpdateSubAccountRequest.Build()
                .WithSubAccountKey("RandomKey")
                .WithName("Subaccount department B")
                .Create()
                .GetStringContent()
                .Should()
                .BeSuccess(this.helper.GetRequestJson());
    }
}