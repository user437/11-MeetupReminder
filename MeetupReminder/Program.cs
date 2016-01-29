using MeetupReminder.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeetupReminder.Domain;
using MeetupReminder.Service;

namespace MeetupReminder
{
    class Program
    {
        static void Main(string[] arg)
        {
            Console.WriteLine("Enter the name of the group");
            string groupname = Console.ReadLine();


            var meetups = MeetupService.GetMeetupsFor(groupname).Result;

            for (int i = 0; i < meetups.Count; i++)
            {
                var currentTime = UnixToDateTime(meetups[i].time);
                var Time = currentTime.ToString();
                //Your personal number▼
                SmsService.SendSms("+1 ", "Meetup Name:- " + meetups[i].name + ", Location:- " + meetups[i].Venue.city + "," + meetups[i].Venue.address_1 + Time);
            }
        }

        //DateTime vice versa
                public static DateTime UnixToDateTime(double i)
        {
            DateTime dt;
            dt = new DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
            dt = dt.AddMilliseconds(i).ToLocalTime();
            return dt;
        }
   }
}
