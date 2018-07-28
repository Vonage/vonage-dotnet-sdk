using Newtonsoft.Json;
using Nexmo.Api.Request;
using System.Collections.Generic;

namespace Nexmo.Api.Stitch
{
    public class Member
    {
	    public class MemberChannel
	    {
		    [JsonProperty("type")]
		    public string Type { get; set; }

		    [JsonProperty("leg_ids")]
		    public string[] LegIds { get; set; }
	    }

	    public class MemberTimestamp
	    {
		    [JsonProperty("joined")]
		    public string Joined { get; set; }
	    }

	    public class MemberRequest
	    {
		    [JsonProperty("user_id")]
		    public string UserId { get; set; }
		    
		    [JsonProperty("user_name")]
		    public string UserName { get; set; }
		    
		    [JsonProperty("member_id")]
		    public string MemberId { get; set; }

		    [JsonProperty("channel")]
		    public MemberChannel Channel { get; set; }

		    [JsonProperty("media")]
		    public string Media { get; set; }

		    [JsonProperty("action")]
		    public string Action { get; set; }
	    }

	    public class MemberResponse
	    {
		    [JsonProperty("id")]
		    public string Id { get; set; }

		    [JsonProperty("user_id")]
		    public string UserId { get; set; }

		    [JsonProperty("state")]
		    public string State { get; set; }

		    [JsonProperty("timestamp")]
		    public MemberTimestamp Timestamp { get; set; }

		    [JsonProperty("channel")]
		    public MemberChannel Channel { get; set; }

		    [JsonProperty("href")]
		    public string Href { get; set; }
	    }

	    public class MembersList
	    {
		    public List<MemberResponse> Members { get; set; }
	    }

        public static MemberResponse CreateMember(string conversationId, MemberRequest request, Credentials creds = null)
        {
            var response = VersionedApiRequest.DoRequest("POST", ApiRequest.GetBaseUriFor(typeof(Member), $"/beta/conversations/{conversationId}/members"), request, creds);
            return JsonConvert.DeserializeObject<MemberResponse>(response.JsonResponse);
        }

        public static MemberResponse UpdateMember(string conversationId, string memberId, MemberRequest request, Credentials creds = null)
        {
            var response = VersionedApiRequest.DoRequest("PUT", ApiRequest.GetBaseUriFor(typeof(Member), $"/beta/conversations/{conversationId}/members/{memberId}"), request, creds);
            return JsonConvert.DeserializeObject<MemberResponse>(response.JsonResponse);
        }

        public static MemberResponse DeleteMember(string conversationId, string memberId, Credentials creds = null)
        {
            var response = VersionedApiRequest.DoRequest("DELETE", ApiRequest.GetBaseUriFor(typeof(Member), $"/beta/conversations/{conversationId}/members/{memberId}"), new { }, creds);
            return JsonConvert.DeserializeObject<MemberResponse>(response.JsonResponse);
        }

        public static MemberResponse GetMember(string conversationId, string memberId, Credentials creds = null)
        {
            var response = VersionedApiRequest.DoRequest(ApiRequest.GetBaseUriFor(typeof(Member), $"/beta/conversations/{conversationId}/members/{memberId}"), new { }, creds);
            return JsonConvert.DeserializeObject<MemberResponse>(response);
        }

        public static PaginatedResponse<MembersList> GetMembersList(string conversationId, Credentials creds = null)
        {
            var response = VersionedApiRequest.DoRequest(ApiRequest.GetBaseUriFor(typeof(Member), "/beta/conversations/{conversationId}/members"), new { }, creds);

            return JsonConvert.DeserializeObject<PaginatedResponse<MembersList>>(response);
        }
    }
}