using Nexmo.Api.Request;
using Nexmo.Api.Conversations;
using Newtonsoft.Json;
using System.Net;

namespace Nexmo.Api
{
    public class Conversation
    {
        const string POST = "POST";
        const string PUT = "PUT";
        const string DELETE = "DELETE";
        const string GET = "GET";
        const string VERSION = "/v0.1";
        const string CONVERSATIONS = "conversations";
        const string USERS = "users";
        const string MEMBERS = "members";
        const string EVENTS = "events";
        static readonly string CONVERSATIONS_URI = $"{VERSION}/{CONVERSATIONS}";
        static readonly string USERS_URI = $"{VERSION}/{USERS}";
        static readonly string MEMBERS_URI_FORMAT = CONVERSATIONS_URI +  "/{0}/" + MEMBERS;
        static readonly string EVENTS_URI_FORMAT = CONVERSATIONS_URI + "/{0}/" + EVENTS;
        static readonly string EVENT_SPECIFIC_URI = EVENTS_URI_FORMAT + "/{1}";
        static readonly string MEMBER_SPECIFIC_URI = MEMBERS_URI_FORMAT + "/{1}";
        static readonly JsonSerializerSettings settings = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore };

        public Conversation()
        {         
        }

        public static Conversations.Conversation CreateConversation(CreateConversationRequest request, Credentials creds = null)
        {
            var response = VersionedApiRequest.DoRequest(POST, ApiRequest.GetBaseUriFor(typeof(Conversation), CONVERSATIONS_URI),request,creds);
            return JsonConvert.DeserializeObject<Conversations.Conversation>(response.JsonResponse, settings);
        }        

        public static CursorBasedListResponse<ConversationList> ListConversations(CursorListParams parameters, Credentials creds = null)
        {
            var response = VersionedApiRequest.DoRequest(ApiRequest.GetBaseUriFor(typeof(Conversation), CONVERSATIONS_URI), parameters, creds);
            return JsonConvert.DeserializeObject<CursorBasedListResponse<ConversationList>>(response);
        }
        
        public static Conversations.Conversation GetConversation(string id, Credentials creds = null)
        {
            var response = VersionedApiRequest.DoRequest(ApiRequest.GetBaseUriFor(typeof(Conversation), $"{CONVERSATIONS_URI}/{id}"), new { }, creds);
            return JsonConvert.DeserializeObject<Conversations.Conversation>(response);
        }
        public static Conversations.Conversation UpdateConversation(UpdateConversationRequest request, string id, Credentials creds = null)
        {
            var response = VersionedApiRequest.DoRequest(PUT,ApiRequest.GetBaseUriFor(typeof(Conversation), $"{CONVERSATIONS_URI}/{id}"), request, creds);
            return JsonConvert.DeserializeObject<Conversations.Conversation>(response.JsonResponse);
        }
        public static HttpStatusCode DeleteConversation(string id, Credentials creds = null)
        {
            var response = VersionedApiRequest.DoRequest(DELETE, ApiRequest.GetBaseUriFor(typeof(Conversation), $"{CONVERSATIONS_URI}/{id}"), null, creds);
            return response.Status;
        }

        public static User CreateUser(CreateUserRequest request, Credentials creds = null)
        {
            var response = VersionedApiRequest.DoRequest(POST, ApiRequest.GetBaseUriFor(typeof(Conversation), USERS_URI), request, creds);
            return JsonConvert.DeserializeObject<User>(response.JsonResponse);
        }

        public static CursorBasedListResponse<UserList> ListUsers(CursorListParams parameters, Credentials creds = null)
        {
            var response = VersionedApiRequest.DoRequest(ApiRequest.GetBaseUriFor(typeof(Conversation), USERS_URI), parameters, creds);
            return JsonConvert.DeserializeObject<CursorBasedListResponse<UserList>>(response);
        }

        public static User GetUser (string id, Credentials creds = null)
        {
            var response = VersionedApiRequest.DoRequest(ApiRequest.GetBaseUriFor(typeof(Conversation), $"{USERS_URI}/{id}"), new { }, creds);
            return JsonConvert.DeserializeObject<User>(response);
        }

        public static User UpdateUser(UpdateUserRequest request, string id, Credentials creds = null)
        {
            var response = VersionedApiRequest.DoRequest(PUT, ApiRequest.GetBaseUriFor(typeof(Conversation), $"{USERS_URI}/{id}"), request, creds);
            return JsonConvert.DeserializeObject<User>(response.JsonResponse);
        }

        public static HttpStatusCode DeleteUser(string id, Credentials creds = null)
        {
            var response = VersionedApiRequest.DoRequest(DELETE, ApiRequest.GetBaseUriFor(typeof(Conversation), $"{USERS_URI}/{id}"), null, creds);
            return response.Status;
        }

        public static Member CreateMember(CreateMemberRequestBase request, string conversation_id, Credentials creds = null)
        {
            var response = VersionedApiRequest.DoRequest(POST, ApiRequest.GetBaseUriFor(typeof(Conversation), string.Format(MEMBERS_URI_FORMAT, conversation_id)), request, creds);
            return JsonConvert.DeserializeObject<Member>(response.JsonResponse);
        }

        public static CursorBasedListResponse<MemberList> ListMembers(CursorListParams parameters, string conversation_id, Credentials creds = null)
        {
            var response = VersionedApiRequest.DoRequest(ApiRequest.GetBaseUriFor(typeof(Conversation), string.Format(MEMBERS_URI_FORMAT, conversation_id)), parameters, creds);
            return JsonConvert.DeserializeObject<CursorBasedListResponse<MemberList>>(response);
        }

        public static Member GetMember(string memberId, string conversation_id, Credentials creds = null)
        {
            var end_of_uri = string.Format(MEMBER_SPECIFIC_URI, conversation_id, memberId);
            var response = VersionedApiRequest.DoRequest(ApiRequest.GetBaseUriFor(typeof(Conversation), end_of_uri), new { }, creds);
            return JsonConvert.DeserializeObject<Member>(response);
        }

        public static Member UpdateMember(UpdateMemberRequest request, string memberId, string conversation_id, Credentials creds = null)
        {
            var end_of_uri = string.Format(MEMBER_SPECIFIC_URI, conversation_id, memberId);
            var response = VersionedApiRequest.DoRequest(ApiRequest.GetBaseUriFor(typeof(Conversation), end_of_uri), request, creds);
            return JsonConvert.DeserializeObject<Member>(response);
        }

        public static HttpStatusCode DeleteMember(string memberId, string conversation_id, Credentials creds = null)
        {
            var end_of_uri = string.Format(MEMBER_SPECIFIC_URI, conversation_id, memberId);
            var response = VersionedApiRequest.DoRequest(DELETE,ApiRequest.GetBaseUriFor(typeof(Conversation), end_of_uri), null, creds);
            return response.Status;
        }

        public static Event CreateEvent(CreateEventRequestBase request, string conversation_id, Credentials creds = null)
        {
            var endOfUrl = string.Format(EVENTS_URI_FORMAT, conversation_id);
            var response = VersionedApiRequest.DoRequest(POST, ApiRequest.GetBaseUriFor(typeof(Conversation), endOfUrl), request, creds);
            return JsonConvert.DeserializeObject<Event>(response.JsonResponse);
        }

        public static CursorBasedListResponse<EventList> ListEvents(EventListParams request, string conversation_id, Credentials creds = null)
        {
            var endOfUrl = string.Format(EVENTS_URI_FORMAT, conversation_id);
            var response = VersionedApiRequest.DoRequest(ApiRequest.GetBaseUriFor(typeof(Conversation), endOfUrl), request, creds);
            return JsonConvert.DeserializeObject<CursorBasedListResponse<EventList>>(response);
        }
        
        public static Event GetEvent(string eventId, string conversation_id, Credentials creds = null)
        {
            var endOfUrl = string.Format(EVENT_SPECIFIC_URI, conversation_id, eventId);
            var response = VersionedApiRequest.DoRequest(ApiRequest.GetBaseUriFor(typeof(Conversation), endOfUrl), new { }, creds);
            return JsonConvert.DeserializeObject<Event>(response);
        }

        public static HttpStatusCode DeleteEvent(string eventId, string conversation_id, Credentials creds = null)
        {
            var endOfUrl = string.Format(EVENT_SPECIFIC_URI, conversation_id, eventId);
            var response = VersionedApiRequest.DoRequest(DELETE, ApiRequest.GetBaseUriFor(typeof(Conversation), endOfUrl), null, creds);
            return response.Status;
        }
    }
}
