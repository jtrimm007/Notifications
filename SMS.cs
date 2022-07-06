using Microsoft.Extensions.Configuration;
using Notifications.ViewModels;
using System;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Notifications
{
    public class SMS : Notifications
    {
        private readonly IConfiguration _config;
        private readonly string _accountSid;
        private readonly string _authToken;
        private readonly PhoneNumber _fromPhoneNumber;
        private readonly PhoneNumber _toPhoneNumber;

        public SMS(IConfiguration config, TextMessageVM textMessageVM) : base(textMessageVM)
        {
            _config = config;
            Message = textMessageVM.Message;
            _accountSid = _config.GetSection("Twilio").GetSection("accountSid").Value;
            _authToken = _config.GetSection("Twilio").GetSection("authToken").Value;
            _fromPhoneNumber = new PhoneNumber(_config.GetSection("Twilio").GetSection("TwilioPhone").Value);
            _toPhoneNumber = new PhoneNumber(textMessageVM.SendToAddress);
        }

        public override void ConstructMessageToSend()
        {
            throw new NotImplementedException();
        }

        public async override Task SendNotification()
        {
            TwilioClient.Init(_accountSid, _authToken);
            MessageResource.Create(
                        to: _toPhoneNumber,
                        from: _fromPhoneNumber,
                        body: $"{Message}\n");
        }
    }
}
