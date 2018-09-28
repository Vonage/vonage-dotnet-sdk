using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using Nexmo.Api.Request;

namespace Nexmo.Api
{
    public static class ApiSecret
    {
        public class SecretList
        {
            [JsonProperty("secrets")]
            public List<Secret> Secrets { get; set; }
        }

        public class Secret
        {
            public HALLinks _links { get; set; }
            [JsonProperty("id")]
            public string Id { get; set; }
            [JsonProperty("created_at")]
            public DateTime? CreatedAt { get; set;  }
        }

        public class SecretRequest
        {
            [JsonProperty("secret")]
            public string Secret { get; set; }
        }

        /// <summary>
        /// List the secrets associated with the provided api key.
        /// </summary>
        /// <param name="apiKey">The API key to manage secrets for</param>
        /// <returns>List of secrets</returns>
        public static List<Secret> ListSecrets(string apiKey, Credentials creds = null)
        {
            var json = ApiRequest.DoRequest(ApiRequest.GetBaseUriFor(typeof(ApiSecret),
                    $"/accounts/{apiKey}/secrets"),
                // TODO: using this method sig allows us to have the api auth injected at the expense of opaque code here
                new Dictionary<string, string>(),
                creds);

            var response = JsonConvert.DeserializeObject<Response<SecretList>>(json);
            return response._embedded.Secrets;
        }

        /// <summary>
        /// Obtain the details of the secret identified by the provided secret ID.
        /// </summary>
        /// <param name="apiKey">The API key to manage secrets for</param>
        /// <param name="secretId">ID of the API Secret</param>
        /// <returns>The secret</returns>
        public static Secret GetSecret(string apiKey, string secretId, Credentials creds = null)
        {
            var json = ApiRequest.DoRequest(ApiRequest.GetBaseUriFor(typeof(ApiSecret),
                    $"/accounts/{apiKey}/secrets/{secretId}"),
                // TODO: using this method sig allows us to have the api auth injected at the expense of opaque code here
                new Dictionary<string, string>(),
                creds);

            return JsonConvert.DeserializeObject<Secret>(json);
        }

        /// <summary>
        /// Create a secret with the details provided in new secret.
        /// </summary>
        /// <param name="apiKey">The API key to manage secrets for</param>
        /// <param name="newSecret">The new secret must follow these rules:
        ///   minimum 8 characters
        ///   maximum 25 characters
        ///   minimum 1 lower case character
        ///   minimum 1 upper case character
        ///   minimum 1 digit
        /// </param>
        /// <returns>The created secret</returns>
        public static Secret CreateSecret(string apiKey, string newSecret, Credentials creds = null)
        {
            var response = VersionedApiRequest.DoRequest("POST", ApiRequest.GetBaseUriFor(typeof(ApiSecret), $"/accounts/{apiKey}/secrets"), new SecretRequest { Secret = newSecret }, creds);

            return JsonConvert.DeserializeObject<Secret>(response.JsonResponse);
        }

        /// <summary>
        /// Delete the secret associated with the provided secret ID.
        /// </summary>
        /// <param name="apiKey">The API key to manage secrets for</param>
        /// <param name="secretId">ID of the API Secret</param>
        public static bool DeleteSecret(string apiKey, string secretId, Credentials creds = null)
        {
            var response = VersionedApiRequest.DoRequest("DELETE", ApiRequest.GetBaseUriFor(typeof(ApiSecret),
                $"/accounts/{apiKey}/secrets/{secretId}"), null, creds);

            return response.Status == HttpStatusCode.NoContent;
        }
    }
}
