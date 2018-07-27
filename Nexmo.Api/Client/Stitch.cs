using Nexmo.Api.Request;
using Nexmo.Api.Stitch;

namespace Nexmo.Api.ClientMethods
{
	public class Stitch
	{
		public Credentials Credentials { get; set; }
		public Stitch(Credentials creds)
		{
			Credentials = creds;
		}

		public Conversation.ConversationResponse CreateConversation(Conversation.ConversationRequest request, Credentials creds = null)
		{
			return Api.Stitch.Conversation.CreateConversation(request, creds ?? Credentials);
		}

		public Conversation.ConversationResponse UpdateConversation(string conversationId, Conversation.ConversationRequest request, Credentials creds = null)
		{
			return Api.Stitch.Conversation.UpdateConversation(conversationId, request, creds ?? Credentials);
		}

		public Conversation.ConversationResponse DeleteConversation(string conversationId, Credentials creds = null)
		{
			return Api.Stitch.Conversation.DeleteConversation(conversationId, creds ?? Credentials);
		}

		public Conversation.ConversationResponse GetConversation(string conversationId, Credentials creds = null)
		{
			return Api.Stitch.Conversation.GetConversation(conversationId, creds ?? Credentials);
		}

		public PaginatedResponse<Conversation.ConversationList> GetConversationList(Conversation.SearchFilter searchFilter, Credentials creds = null)
		{
			return Api.Stitch.Conversation.GetConversationList(searchFilter, creds ?? Credentials);
		}

		public PaginatedResponse<Conversation.ConversationList> ListConversations(Credentials creds = null)
		{
			return Api.Stitch.Conversation.GetConversationList(new Conversation.SearchFilter
			{
				page_size = 10
			}, creds ?? Credentials);
		}



		public User.UserResponse CreateUser(User.UserRequest request, Credentials creds = null)
		{
			return Api.Stitch.User.CreateUser(request, creds ?? Credentials);
		}

		public User.UserResponse UpdateUser(string userId, User.UserRequest request, Credentials creds = null)
		{
			return Api.Stitch.User.UpdateUser(userId, request, creds ?? Credentials);
		}

		public User.UserResponse DeleteUser(string userId, Credentials creds = null)
		{
			return Api.Stitch.User.DeleteUser(userId, creds ?? Credentials);
		}

		public User.UserResponse GetUser(string userId, Credentials creds = null)
		{
			return Api.Stitch.User.GetUser(userId, creds ?? Credentials);
		}

		public PaginatedResponse<User.UsersList> GetUsersList(User.SearchFilter searchFilter, Credentials creds = null)
		{
			return Api.Stitch.User.GetUsersList(searchFilter, creds ?? Credentials);
		}

		public PaginatedResponse<User.UsersList> List(Credentials creds = null)
		{
			return Api.Stitch.User.GetUsersList(new User.SearchFilter
			{
				page_size = 10
			}, creds ?? Credentials);
		}

		public PaginatedResponse<Conversation.ConversationList> GetUserConversationsList(string userId, Credentials creds = null)
		{
			return Api.Stitch.User.GetUserConversationsList(userId, creds ?? Credentials);
		}



		public Member.MemberResponse CreateMember(string conversationId, Member.MemberRequest request, Credentials creds = null)
		{
			return Api.Stitch.Member.CreateMember(conversationId, request, creds ?? Credentials);
		}

		public Member.MemberResponse UpdateMember(string conversationId, string memberId, Member.MemberRequest request, Credentials creds = null)
		{
			return Api.Stitch.Member.UpdateMember(conversationId, memberId, request, creds ?? Credentials);
		}

		public Member.MemberResponse DeleteMember(string conversationId, string memberId, Credentials creds = null)
		{
			return Api.Stitch.Member.DeleteMember(conversationId, memberId, creds ?? Credentials);
		}

		public Member.MemberResponse GetMember(string conversationId, string memberId, Credentials creds = null)
		{
			return Api.Stitch.Member.GetMember(conversationId, memberId, creds ?? Credentials);
		}

		public PaginatedResponse<Member.MembersList> GetMembersList(string conversationId, Credentials creds = null)
		{
			return Api.Stitch.Member.GetMembersList(conversationId, creds ?? Credentials);
		}
	}
}