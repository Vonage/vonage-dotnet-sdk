﻿using System;
using Vonage.Common;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.SubAccounts;
using Xunit;

namespace Vonage.Test.Unit.SubAccounts.Transfer.Credit
{
    public class SerializationTest
    {
        private readonly SerializationTestHelper helper;

        public SerializationTest() =>
            this.helper = new SerializationTestHelper(
                typeof(SerializationTest).Namespace,
                JsonSerializer.BuildWithSnakeCase());

        [Fact]
        public void ShouldDeserialize200() =>
            this.helper.Serializer
                .DeserializeObject<CreditTransfer>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(new CreditTransfer(
                    new Guid("297016aa-4061-430d-b805-a4d00522bb00"),
                    (decimal) 123.45,
                    "7c9738e6",
                    "ad6dc56f",
                    "This gets added to the audit log",
                    DateTimeOffset.Parse("2019-03-02T16:34:49Z")));
    }
}