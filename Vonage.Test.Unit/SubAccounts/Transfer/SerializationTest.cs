﻿using Vonage.Common;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.SubAccounts.Transfer;
using Xunit;

namespace Vonage.Test.Unit.SubAccounts.Transfer
{
    public class SerializationTest
    {
        private readonly SerializationTestHelper helper;

        public SerializationTest() =>
            this.helper = new SerializationTestHelper(
                typeof(SerializationTest).Namespace,
                JsonSerializer.BuildWithSnakeCase());

        [Fact]
        public void ShouldSerialize() =>
            TransferRequest.Build()
                .WithFrom("7c9738e6")
                .WithTo("ad6dc56f")
                .WithAmount((decimal) 123.45)
                .WithReference("This gets added to the audit log")
                .Create()
                .GetStringContent()
                .Should()
                .BeSuccess(this.helper.GetRequestJson());
    }
}