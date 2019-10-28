using Nexmo.Api.Request;
using Nexmo.Api.Conversations;
using Nexmo.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Nexmo.Api.ClientMethods
{
    public class Conversation
    {
        public Credentials Credentials { get; set; }

        public Conversation(Credentials creds)
        {
            Credentials = creds;
        }
        public Conversations.Conversation CreateConversation(CreateConversationRequest request, Credentials creds = null)
        {
            return Api.Conversation.CreateConversation(request, creds ?? Credentials);
        }

        public CursorBasedListResponse<ConversationList> ListConversations(CursorListParams parameters, Credentials creds = null)
        {
            return Api.Conversation.ListConversations(parameters, creds ?? Credentials);
        }

        public Conversations.Conversation GetConversation(string id, Credentials creds = null)
        {
            return Api.Conversation.GetConversation(id, creds ?? Credentials);
        }
        public Conversations.Conversation UpdateConversation(UpdateConversationRequest request, string id, Credentials creds = null)
        {
            return Api.Conversation.UpdateConversation(request, id, creds ?? Credentials);
        }
        public HttpStatusCode DeleteConversation(string id, Credentials creds = null)
        {
            return Api.Conversation.DeleteConversation(id, creds ?? Credentials);
        }

        public User CreateUser(CreateUserRequest request, Credentials creds = null)
        {
            return Api.Conversation.CreateUser(request, creds ?? Credentials);
        }

        public CursorBasedListResponse<UserList> ListUsers(CursorListParams parameters, Credentials creds = null)
        {
            return Api.Conversation.ListUsers(parameters, creds ?? Credentials);
        }

        public User GetUser(string id, Credentials creds = null)
        {
            return Api.Conversation.GetUser(id, creds ?? Credentials);
        }

        public User UpdateUser(UpdateUserRequest request, string id, Credentials creds = null)
        {
            return Api.Conversation.UpdateUser(request, id, creds ?? Credentials);
        }

        public HttpStatusCode DeleteUser(string id, Credentials creds = null)
        {
            return Api.Conversation.DeleteUser(id, creds ?? Credentials);
        }

        public Member CreateMember(CreateMemberRequestBase request, string conversation_id, Credentials creds = null)
        {
            return Api.Conversation.CreateMember(request, conversation_id, creds ?? Credentials);
        }

        public CursorBasedListResponse<MemberList> ListMembers(CursorListParams parameters, string conversation_id, Credentials creds = null)
        {
            return Api.Conversation.ListMembers(parameters, conversation_id, creds ?? Credentials);
        }

        public Member GetMember(string memberId, string conversation_id, Credentials creds = null)
        {
            return Api.Conversation.GetMember(memberId, conversation_id, creds ?? Credentials);
        }

        public Member UpdateMember(UpdateMemberRequest request, string memberId, string conversation_id, Credentials creds = null)
        {
            return Api.Conversation.UpdateMember(request, memberId, conversation_id, creds ?? Credentials);
        }

        public HttpStatusCode DeleteMember(string memberId, string conversation_id, Credentials creds = null)
        {
            return Api.Conversation.DeleteMember(memberId, conversation_id, creds ?? Credentials);
        }

        public Event CreateEvent(CreateEventRequestBase request, string conversation_id, Credentials creds = null)
        {
            return Api.Conversation.CreateEvent(request, conversation_id, creds ?? Credentials);
        }

        public CursorBasedListResponse<EventList> ListEvents(EventListParams request, string conversation_id, Credentials creds = null)
        {
            return Api.Conversation.ListEvents(request, conversation_id, creds ?? Credentials);
        }

        public Event GetEvent(string eventId, string conversation_id, Credentials creds = null)
        {
            return Api.Conversation.GetEvent(eventId, conversation_id, creds ?? Credentials);
        }

        public HttpStatusCode DeleteEvent(string eventId, string conversation_id, Credentials creds = null)
        {
            return Api.Conversation.DeleteEvent(eventId, conversation_id, creds ?? Credentials);
        }
    }
}
