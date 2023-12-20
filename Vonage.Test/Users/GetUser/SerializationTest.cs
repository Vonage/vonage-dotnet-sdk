using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Vonage.Users;
using Xunit;

namespace Vonage.Test.Users.GetUser
{
    public class SerializationTest
    {
        private readonly SerializationTestHelper helper = new(
            typeof(SerializationTest).Namespace,
            JsonSerializerBuilder.BuildWithSnakeCase());

        [Fact]
        public void ShouldDeserialize200() =>
            this.helper.Serializer
                .DeserializeObject<User>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(E2EBase.VerifyUser);
    }
}