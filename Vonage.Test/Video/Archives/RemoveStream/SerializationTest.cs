using System;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Vonage.Video.Archives.RemoveStream;
using Xunit;

namespace Vonage.Test.Video.Archives.RemoveStream
{
    public class SerializationTest
    {
        private readonly SerializationTestHelper helper = new SerializationTestHelper(
            typeof(SerializationTest).Namespace,
            JsonSerializerBuilder.BuildWithCamelCase());

        [Fact]
        public void ShouldSerialize() =>
            RemoveStreamRequest
                .Build()
                .WithApplicationId(Guid.NewGuid())
                .WithArchiveId(Guid.NewGuid())
                .WithStreamId(Guid.Parse("12312312-3811-4726-b508-e41a0f96c68f"))
                .Create()
                .GetStringContent()
                .Should()
                .BeSuccess(this.helper.GetRequestJson());
    }
}