using Nexmo.Api.Request;
namespace Nexmo.Api
{
    [System.Obsolete("This item is rendered obsolete by version 5 - please use the new Interfaces provided by the Vonage.VonageClient class")]
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
            ApplicationV2 = new ClientMethods.ApplicationV2(Credentials);
            Call = new ClientMethods.Call(Credentials);
            Conversion = new ClientMethods.Conversion(Credentials);
            Number = new ClientMethods.Number(Credentials);
            NumberInsight = new ClientMethods.NumberInsight(Credentials);
            NumberVerify = new ClientMethods.NumberVerify(Credentials);            
            ShortCode = new ClientMethods.ShortCode(Credentials);
            SMS = new ClientMethods.SMS(Credentials);
            Redact = new ClientMethods.Redact(Credentials);
        }
        
        public ClientMethods.Account Account { get; private set; }
        public ClientMethods.ApiSecret ApiSecret { get; private set; }
        public ClientMethods.ApplicationV2 ApplicationV2 { get; private set; }
        public ClientMethods.Call Call { get; private set; }
        public ClientMethods.Conversion Conversion { get; private set; }
        public ClientMethods.Number Number { get; private set; }
        public ClientMethods.NumberInsight NumberInsight { get; private set; }
        public ClientMethods.NumberVerify NumberVerify { get; private set; }        
        public ClientMethods.ShortCode ShortCode { get; private set; }
        public ClientMethods.SMS SMS { get; private set; }
        public ClientMethods.Redact Redact { get; private set; }
    }
}