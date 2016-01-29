using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;

namespace MeetupReminder.Service
{
    public class SmsService
    {
        private const string FromNumber = "+17603137573";
        private const string TwilioAccountSid = "AC77747708f81ab44dd2969f5918952d40";
        private const string TwilioAuthToken = "2352d3d423e06932047db934e83a6218";

        public static void SendSms(string to, string message)
        {


            var twilio = new TwilioRestClient(TwilioAccountSid, TwilioAuthToken);
            twilio.SendMessage(FromNumber, to, message);





        }
    }
}
