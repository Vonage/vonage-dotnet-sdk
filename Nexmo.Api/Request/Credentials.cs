namespace Nexmo.Api.Request
{
    public class Credentials
    {
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
        public string SecuritySecret { get; set; }
        public string ApplicationId { get; set; }
        public string ApplicationKey { get; set; }

        // Optional app useragent value to pass with every request
        public string AppUserAgent { get; set; }
    }
}
