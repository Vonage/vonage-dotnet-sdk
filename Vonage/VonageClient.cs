using Vonage.Accounts;
using Vonage.Voice;
using Vonage.Applications;
using Vonage.Conversions;
using Vonage.Messages;
using Vonage.Numbers;
using Vonage.NumberInsights;
using Vonage.Verify;
using Vonage.ShortCodes;
using Vonage.Redaction;
using Vonage.Messaging;
using Vonage.Request;
using Vonage.Pricing;
using System.Collections.Generic;

namespace Vonage
{
    public class VonageClient
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

        public IDictionary<string, int?> TimeoutCollection { get; private set; }

        public IAccountClient AccountClient { get; private set; }

        public IApplicationClient ApplicationClient { get; private set; }

        public IVoiceClient VoiceClient { get; private set; }

        public IConversionClient ConversionClient { get; private set; }

        public INumbersClient NumbersClient { get; private set; }

        public INumberInsightClient NumberInsightClient { get; private set; }

        public IVerifyClient VerifyClient { get; private set; }

        public IShortCodesClient ShortCodesClient { get; private set; }

        public IRedactClient RedactClient { get; private set; }

        public ISmsClient SmsClient { get; private set; }

        public IPricingClient PricingClient { get; private set; }

        public IMessagesClient MessagesClient { get; private set; }

        public VonageClient(Credentials credentials, IDictionary<string, int?> timeoutCollection = null)
        {
            Credentials = credentials;
            TimeoutCollection = timeoutCollection ?? new Dictionary<string, int?>();
        }

        private void PropagateCredentials()
        {
            AccountClient = new AccountClient(Credentials, GetTimeout(nameof(AccountClient)));
            ApplicationClient = new ApplicationClient(Credentials, GetTimeout(nameof(ApplicationClient)));
            VoiceClient = new VoiceClient(Credentials, GetTimeout(nameof(VoiceClient)));
            ConversionClient = new ConversionClient(Credentials, GetTimeout(nameof(ConversionClient)));
            NumbersClient = new NumbersClient(Credentials, GetTimeout(nameof(NumbersClient)));
            NumberInsightClient = new NumberInsightClient(Credentials, GetTimeout(nameof(NumberInsightClient)));
            VerifyClient = new VerifyClient(Credentials, GetTimeout(nameof(VerifyClient)));
            ShortCodesClient = new ShortCodesClient(Credentials, GetTimeout(nameof(ShortCodesClient)));
            RedactClient = new RedactClient(Credentials, GetTimeout(nameof(RedactClient)));
            SmsClient = new SmsClient(Credentials, GetTimeout(nameof(SmsClient)));
            PricingClient = new PricingClient(Credentials, GetTimeout(nameof(PricingClient)));
            MessagesClient = new MessagesClient(Credentials, GetTimeout(nameof(MessagesClient)));
        }

        private int? GetTimeout(string k)
        {
            TimeoutCollection.TryGetValue(k, out int? timeout);
            return timeout;
        }
    }
}