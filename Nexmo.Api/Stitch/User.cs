using Newtonsoft.Json;
using Nexmo.Api.Request;
using System;
using System.Collections.Generic;
using static Nexmo.Api.Stitch.Conversation;

namespace Nexmo.Api.Stitch
{
    public class User
    {
        public class UserRequest
        {
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("display-name")]
            public string DisplayName { get; set; }

            [JsonProperty("image-url")]
            public string ImageUrl { get; set; }

            [JsonProperty("channels")]
            public string Channels { get; set; }
        }

        public class UserResponse
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("href")]
            public string Href { get; set; }
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

        public class UsersList
        {
            public List<UserResponse> Users { get; set; }
        }

        public static UserResponse CreateUser(UserRequest request, Credentials creds = null)
        {
            var response = VersionedApiRequest.DoRequest("POST", ApiRequest.GetBaseUriFor(typeof(User), "/beta/users"), request, creds);
            return JsonConvert.DeserializeObject<UserResponse>(response.JsonResponse);
        }

        public static UserResponse UpdateUser(string userId, UserRequest request, Credentials creds = null)
        {
            var response = VersionedApiRequest.DoRequest("PUT", ApiRequest.GetBaseUriFor(typeof(User), $"/beta/users/{userId}"), request, creds);
            return JsonConvert.DeserializeObject<UserResponse>(response.JsonResponse);
        }

        public static UserResponse DeleteUser(string userId, Credentials creds = null)
        {
            var response = VersionedApiRequest.DoRequest("DELETE", ApiRequest.GetBaseUriFor(typeof(User), $"/beta/users/{userId}"), new { }, creds);
            return JsonConvert.DeserializeObject<UserResponse>(response.JsonResponse);
        }

        public static UserResponse GetUser(string userId, Credentials creds = null)
        {
            var response = VersionedApiRequest.DoRequest(ApiRequest.GetBaseUriFor(typeof(User), $"/beta/users/{userId}"), new { }, creds);
            return JsonConvert.DeserializeObject<UserResponse>(response);
        }

        public static PaginatedResponse<UsersList> GetUsersList(SearchFilter searchFilter, Credentials creds = null)
        {
            var response = VersionedApiRequest.DoRequest(ApiRequest.GetBaseUriFor(typeof(User), "/beta/users"), searchFilter, creds);

            return JsonConvert.DeserializeObject<PaginatedResponse<UsersList>>(response);
        }

        public static PaginatedResponse<UsersList> List(Credentials creds = null)
        {
            return GetUsersList(new SearchFilter
            {
                page_size = 10
            }, creds);
        }

        public static PaginatedResponse<ConversationList> GetUserConversationsList(string userId, Credentials creds = null)
        {
            var response = VersionedApiRequest.DoRequest(ApiRequest.GetBaseUriFor(typeof(User), $"/beta/users/{userId}/conversations"), new { }, creds);

            return JsonConvert.DeserializeObject<PaginatedResponse<ConversationList>>(response);
        }
    }
}