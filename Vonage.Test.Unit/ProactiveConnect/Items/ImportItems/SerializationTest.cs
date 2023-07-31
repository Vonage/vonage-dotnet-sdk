using Vonage.Common;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.ProactiveConnect.Items.ImportItems;
using Xunit;

namespace Vonage.Test.Unit.ProactiveConnect.Items.ImportItems
{
    public class SerializationTest
    {
        private readonly SerializationTestHelper helper;

        public SerializationTest() =>
            this.helper = new SerializationTestHelper(typeof(SerializationTest).Namespace,
                JsonSerializer.BuildWithSnakeCase());

        [Fact]
        public void ShouldDeserialize200() =>
            this.helper.Serializer
                .DeserializeObject<ImportItemsResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(new ImportItemsResponse(50, 100, 200));
    }
}