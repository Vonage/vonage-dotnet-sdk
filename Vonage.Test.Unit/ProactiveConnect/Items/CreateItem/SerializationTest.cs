using System;
using System.Collections.Generic;
using Vonage.Common;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.ProactiveConnect.Items.CreateItem;
using Xunit;

namespace Vonage.Test.Unit.ProactiveConnect.Items.CreateItem
{
    public class SerializationTest
    {
        private readonly SerializationTestHelper helper;

        public SerializationTest() =>
            this.helper = new SerializationTestHelper(typeof(SerializationTest).Namespace,
                JsonSerializer.BuildWithSnakeCase());

        [Fact]
        public void ShouldSerialize() =>
            CreateItemRequest
                .Build()
                .WithListId(Guid.NewGuid())
                .WithCustomData(new KeyValuePair<string, object>("value1", "value"))
                .WithCustomData(new KeyValuePair<string, object>("value2", 0))
                .WithCustomData(new KeyValuePair<string, object>("value3", true))
                .Create()
                .GetStringContent()
                .Should()
                .BeSuccess(this.helper.GetRequestJson());
    }
}