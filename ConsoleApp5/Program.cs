// See https://aka.ms/new-console-template for more information

using Vonage;
using Vonage.Request;

Console.WriteLine("Hello, World!");
try
{
    var credentials = Credentials.FromAppIdAndPrivateKeyPath("9e4a2469-bd42-4192-bdc8-0567e13dce33", "Key.txt");
    var client = new VonageClient(credentials);
    var token = new Jwt().GenerateToken(credentials);

    // var smsRequest = 
    //     StartVerificationRequestBuilder.ForSms()
    //     .WithBrand("Yolo Test")
    //     .WithWorkflow(SmsWorkflow.Parse("+33607118924"))
    //     .Create();
    // var whatsAppRequest = StartVerificationRequestBuilder.ForWhatsApp()
    //     .WithBrand("WhatsApp Test")
    //     .WithWorkflow(WhatsAppWorkflow.Parse("+33607118924"))
    //     .Create();
    // var whatsAppInteractiveRequest = StartVerificationRequestBuilder.ForWhatsAppInteractive()
    //     .WithBrand("Interactive Test")
    //     .WithWorkflow(WhatsAppInteractiveWorkflow.Parse("+33607118924"))
    //     .Create();
    // var emailRequest = StartVerificationRequestBuilder.ForEmail()
    //     .WithBrand("Email Test")
    //     .WithWorkflow(EmailWorkflow.Parse("guillaume.faas@vonage.com"))
    //     .Create();
    // var voiceRequest = StartVerificationRequestBuilder.ForVoice()
    //     .WithBrand("Voice Test")
    //     .WithWorkflow(VoiceWorkflow.Parse("+33607118924"))
    //     .Create();
    // var silentAuthRequest = StartVerificationRequestBuilder.ForSilentAuth()
    //     .WithBrand("SilentAuth Test")
    //     .WithWorkflow(SilentAuthWorkflow.Parse("+33607118924"))
    //     .Create();
    // var response = client.VerifyV2Client.StartVerificationAsync(silentAuthRequest).Result;
    // Console.WriteLine(response.Map(value => value.RequestId.ToString()).IfFailure(f => f.GetFailureMessage()));
    // Console.WriteLine("Enter code:");
    // var verifyResponse = VerifyCodeRequestBuilder.Build()
    //     .WithRequestId(response.Map(value => value.RequestId).IfFailure(Guid.Empty).ToString())
    //     .WithCode(Console.ReadLine())
    //     .Create()
    //     .Map(verifyRequest => client.VerifyV2Client.VerifyCodeAsync(verifyRequest).Result);
    // Console.WriteLine(verifyResponse.Match(_ => "Success", _ => "Failure"));
}
catch (Exception e)
{
}