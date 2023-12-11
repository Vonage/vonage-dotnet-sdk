using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Serialization;
using Vonage.Users;
using Xunit;

namespace Vonage.Test.Unit.Users.GetUser
{
    public class SerializationTest
    {
        private readonly SerializationTestHelper helper = new SerializationTestHelper(
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