using System;
using FluentAssertions;
using Vonage.Common;
using Vonage.Conversations;
using Vonage.Conversations.GetMembers;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.Conversations.GetMembers;

[Trait("Category", "Serialization")]
public class SerializationTest
{
    private readonly SerializationTestHelper helper = new SerializationTestHelper(
        typeof(SerializationTest).Namespace,
        JsonSerializerBuilder.BuildWithSnakeCase());
    
    [Fact]
    public void ShouldDeserialize200() => this.helper.Serializer
        .DeserializeObject<GetMembersResponse>(this.helper.GetResponseJson())
        .Should()
        .BeSuccess(VerifyExpectedResponse);
    
    internal static void VerifyExpectedResponse(GetMembersResponse response)
    {
        response.PageSize.Should().Be(10);
        response.Embedded.Members.Should().BeEquivalentTo(new[]
        {
            new Member(
                "MEM-63f61863-4a51-4f6b-86e1-46edebio0391",
                null,
                "JOINED",
                null,
                null,
                null,
                new MemberEmbedded(new MemberEmbeddedUser("USR-82e028d9-5201-4f1e-8188-604b2d3471ec", "my_user_name",
                    "My User Name", new HalLinks<HalLink>
                    {
                        Self = new HalLink(
                            new Uri("https://api.nexmo.com/v1/users/USR-82e028d9-5201-4f1e-8188-604b2d3471ec")),
                    })),
                null,
                null,
                null,
                new HalLink(new Uri(
                    "https://api.nexmo.com/v1/conversations/CON-63f61863-4a51-4f6b-86e1-46edebio0391/members/MEM-63f61863-4a51-4f6b-86e1-46edebio0391"))),
        });
        response.Links.First.Href.Should()
            .Be(new Uri(
                "https://api.nexmo.com/v1/conversations/CON-d66d47de-5bcb-4300-94f0-0c9d4b948e9a/members?order=desc&page_size=10"));
        response.Links.Self.Href.Should()
            .Be(new Uri(
                "https://api.nexmo.com/v1/conversations/CON-d66d47de-5bcb-4300-94f0-0c9d4b948e9a/members?order=desc&page_size=10&cursor=88b395c167da4d94e929705cbd63b82973771e7d390d274a58e301386d5762600a3ffd799bfb3fc5190c5a0d124cdd0fc72fe6e450506b18e4e2edf9fe84c7a0"));
        response.Links.Next.Href.Should()
            .Be(new Uri(
                "https://api.nexmo.com/v1/conversations/CON-d66d47de-5bcb-4300-94f0-0c9d4b948e9a/members?order=desc&page_size=10&cursor=88b395c167da4d94e929705cbd63b829a650e69a39197bfd4c949f4243f60dc4babb696afa404d2f44e7775e32b967f2a1a0bb8fb259c0999ba5a4e501eaab55"));
        response.Links.Previous.Href.Should()
            .Be(new Uri(
                "https://api.nexmo.com/v1/conversations/CON-d66d47de-5bcb-4300-94f0-0c9d4b948e9a/members?order=desc&page_size=10&cursor=069626a3de11d2ec900dff5042197bd75f1ce41dafc3f2b2481eb9151086e59aae9dba3e3a8858dc355232d499c310fbfbec43923ff657c0de8d49ffed9f7edb"));
    }
}