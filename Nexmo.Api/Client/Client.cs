using Nexmo.Api.Request;

namespace Nexmo.Api
{
    /// <summary>
    /// Note that Client and Credentials are _not_ thread-safe!
    /// If you need to change credentials in your app frequently, create multiple instances of
    /// Client and ensure each have their own copy of Credentials.
    /// </summary>
    public class Client
    {
        private Credentials _credentials;
        public Credentials Credentials
        {
            get => _credentials;
            set
            {
                _credentials = value;
                PropagateCredentials();
            }
        }

        public Client(Credentials creds)
        {
            Credentials = creds;
        }

        private void PropagateCredentials()
        {
            Account = new ClientMethods.Account(Credentials);
            ApiSecret = new ClientMethods.ApiSecret(Credentials);
            Application = new ClientMethods.Application(Credentials);
            Call = new ClientMethods.Call(Credentials);
            Number = new ClientMethods.Number(Credentials);
            NumberInsight = new ClientMethods.NumberInsight(Credentials);
            NumberVerify = new ClientMethods.NumberVerify(Credentials);
            Search = new ClientMethods.Search(Credentials);
            ShortCode = new ClientMethods.ShortCode(Credentials);
            SMS = new ClientMethods.SMS(Credentials);
            Redact = new ClientMethods.Redact(Credentials);
        }

        public ClientMethods.Account Account { get; private set; }
        public ClientMethods.ApiSecret ApiSecret { get; private set; }
        public ClientMethods.Application Application { get; private set; }
        public ClientMethods.Call Call { get; private set; }
        public ClientMethods.Number Number { get; private set; }
        public ClientMethods.NumberInsight NumberInsight { get; private set; }
        public ClientMethods.NumberVerify NumberVerify { get; private set; }
        public ClientMethods.Search Search { get; private set; }
        public ClientMethods.ShortCode ShortCode { get; private set; }
        public ClientMethods.SMS SMS { get; private set; }
        public ClientMethods.Redact Redact { get; private set; }
    }
}
