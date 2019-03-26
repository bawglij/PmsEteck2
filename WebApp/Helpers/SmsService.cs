using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PmsEteck.Helpers
{
    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            /*
             var soapSms = new PmsEteck.ASPSMSX2.ASPSMSX2SoapClient("ASPSMSX2Soap");
             soapSms.SendSimpleTextSMS(
               Keys.SMSAccountIdentification,
               Keys.SMSAccountPassword,
               message.Destination,
               Keys.SMSAccountFrom,
               message.Body);
             soapSms.Close();
             */
             return Task.FromResult(0);
        }
    }
}
