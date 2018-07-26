using Newtonsoft.Json;
using Nexmo.Api.Request;
using System;
using System.Collections.Generic;

namespace Nexmo.Api.Stitch
{
    public static class Conversation
    {
        public class ConversationRequest
        {
            /// <summary>
            /// A string for the conversation Unique Name.
            /// </summary>
            [JsonProperty("name")]
            public string Name { get; set; }
            /// <summary>
            /// A string for the conversation Name.
            /// </summary>
            [JsonProperty("display-name")]
            public string DisplayName { get; set; }
        }

        public class ConversationResponse
        {
            /// <summary>
            /// A string returning the conversation ID.
            /// </summary>
            [JsonProperty("id")]
            public string Id { get; set; }
            /// <summary>
            /// The url to the conversation.
            /// </summary>
            [JsonProperty("href")]
            public string Href { get; set; }
            /// <summary>
            /// A string returning the conversation UUID.
            /// </summary>
            [JsonProperty("uuid")]
            public string Uuid { get; set; }
            /// <summary>
            /// A string for the conversation Unique Name.
            /// </summary>
            [JsonProperty("name")]
            public string Name { get; set; }
            /// <summary>
            /// A string for the conversation Name.
            /// </summary>
            [JsonProperty("display-name")]
            public string DisplayName { get; set; }

            [JsonProperty("timestamp")]
            public DateTime? Timestamp { get; set; }
        }

        public class SearchFilter
        {
            public string name { get; set; }
            
            public DateTime? date_start { get; set; }
            
            public DateTime? date_end { get; set; }
            
            public int page_size { get; set; }
            
            public int record_index { get; set; }
            
            public string order { get; set; }
        }

        public class ConversationList
        {
            public List<ConversationResponse> Conversations { get; set; }
        }

        public static ConversationResponse CreateConversation(ConversationRequest request, Credentials creds= null)
        {
            var response = VersionedApiRequest.DoRequest("POST", ApiRequest.GetBaseUriFor(typeof(Conversation), "/beta/conversations"), request, creds);
            return JsonConvert.DeserializeObject<ConversationResponse>(response.JsonResponse);
        }

        public static ConversationResponse UpdateConversation(string conversationId,ConversationRequest request, Credentials creds = null)
        {
            var response = VersionedApiRequest.DoRequest("PUT", ApiRequest.GetBaseUriFor(typeof(Conversation), $"/beta/conversations/{conversationId}"), request, creds);
            return JsonConvert.DeserializeObject<ConversationResponse>(response.JsonResponse);
        }

        public static ConversationResponse DeleteConversation(string conversationId, Credentials creds = null)
        {
            var response = VersionedApiRequest.DoRequest("DELETE", ApiRequest.GetBaseUriFor(typeof(Conversation), $"/beta/conversations/{conversationId}"), creds);
            return JsonConvert.DeserializeObject<ConversationResponse>(response.JsonResponse);
        }

        public static ConversationResponse GetConversation(string conversationId, Credentials creds = null)
        {
            var response = VersionedApiRequest.DoRequest(ApiRequest.GetBaseUriFor(typeof(Conversation), $"/beta/conversations/{conversationId}"), new { }, creds);
            return JsonConvert.DeserializeObject<ConversationResponse>(response);
        }

        public static PaginatedResponse<ConversationList> GetConversationList (SearchFilter searchFilter, Credentials creds = null)
        {
            var response = VersionedApiRequest.DoRequest(ApiRequest.GetBaseUriFor(typeof(Conversation), "/beta/conversations"), searchFilter, creds);

            return JsonConvert.DeserializeObject<PaginatedResponse<ConversationList>>(response);
        }

        public static PaginatedResponse<ConversationList> List(Credentials creds = null)
        {
            return GetConversationList(new SearchFilter
            {
                page_size = 10
            }, creds);
        }
    }
}
