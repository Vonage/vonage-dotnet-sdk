using System;
using Vonage.Common;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Video.Sip.PlayToneIntoConnection;
using Xunit;

namespace Vonage.Server.Test.Video.Sip.PlayToneIntoConnection
{
    public class PlayToneIntoConnectionSerializationTest
    {
        private readonly SerializationTestHelper helper;

        public PlayToneIntoConnectionSerializationTest() =>
            this.helper = new SerializationTestHelper(typeof(PlayToneIntoConnectionSerializationTest).Namespace,
                JsonSerializer.BuildWithCamelCase());

        [Fact]
        public void ShouldSerialize() =>
            PlayToneIntoConnectionRequest.Parse(Guid.NewGuid(), "414ac9c2-9a6f-4f4b-aad4-202dbe7b1d8d",
                    "414ac9c2-9a6f-4f4b-aad4-202dbe7b1d8d", "1713")
                .GetStringContent()
                .Should()
                .BeSuccess(this.helper.GetRequestJson());
    }
}