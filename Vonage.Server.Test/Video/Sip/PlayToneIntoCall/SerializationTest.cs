﻿using System;
using Vonage.Common;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Video.Sip.PlayToneIntoCall;
using Xunit;

namespace Vonage.Server.Test.Video.Sip.PlayToneIntoCall
{
    public class SerializationTest
    {
        private readonly SerializationTestHelper helper;

        public SerializationTest() =>
            this.helper = new SerializationTestHelper(typeof(SerializationTest).Namespace,
                JsonSerializer.BuildWithCamelCase());

        [Fact]
        public void ShouldSerialize() =>
            PlayToneIntoCallRequest.Parse(Guid.NewGuid(), "414ac9c2-9a6f-4f4b-aad4-202dbe7b1d8d", "1713")
                .GetStringContent()
                .Should()
                .BeSuccess(this.helper.GetRequestJson());
    }
}