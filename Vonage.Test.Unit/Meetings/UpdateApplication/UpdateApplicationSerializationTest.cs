using Vonage.Common;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Meetings.UpdateApplication;
using Xunit;

namespace Vonage.Test.Unit.Meetings.UpdateApplication
{
    public class UpdateApplicationSerializationTest
    {
        private readonly SerializationTestHelper helper;

        public UpdateApplicationSerializationTest() =>
            this.helper = new SerializationTestHelper(typeof(UpdateApplicationSerializationTest).Namespace,
                JsonSerializer.BuildWithSnakeCase());

        [Fact]
        public void ShouldSerialize() =>
            UpdateApplicationRequest
                .Parse("e86a7335-35fe-45e1-b961-5777d4748022")
                .Map(value => value.BuildRequestMessage())
                .Map(value => value.Content.ReadAsStringAsync().Result)
                .Should()
                .BeSuccess(this.helper.GetRequestJson());
    }
}