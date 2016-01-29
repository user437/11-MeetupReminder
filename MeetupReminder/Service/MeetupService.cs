using CSharp.Meetup.Connect;
using Spring.Social.OAuth1;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using MeetupReminder.Domain;
using Newtonsoft.Json;
using MeetupReminder;
namespace MeetupReminder.service
{
    public class MeetupService
    {
        private const string MeetupApiKey = "c0klmsomllhdh30alqra82ihnn";
        private const string MeetupSecretKey = "uv6ofas6c1g3kckl3sqq60javr";
        private async static Task<OAuthToken>  authenticate()
        {
            var meetupServiceProvider = new MeetupServiceProvider(MeetupApiKey, MeetupSecretKey);
            var oauthToken = await meetupServiceProvider.OAuthOperations.FetchRequestTokenAsync("oob", null);
            var authenticateUrl = meetupServiceProvider.OAuthOperations.BuildAuthorizeUrl(oauthToken.Value, null);
            Process.Start(authenticateUrl);
            Console.WriteLine("Enter the pin");
            var pinCode = Console.ReadLine();
            var requestToken = new AuthorizedRequestToken(oauthToken, pinCode);
            var oauthAccessToken = await meetupServiceProvider.OAuthOperations.ExchangeForAccessTokenAsync(requestToken, null);
            Console.WriteLine("Done");
            return oauthAccessToken;
        }
        public static  async Task<List<MeetupInfo>> GetMeetupsFor(string groupName)
        {
            var token =  await authenticate();
            var meetupServiceProvider = new MeetupServiceProvider(MeetupApiKey, MeetupSecretKey);     
            var meetup = meetupServiceProvider.GetApi(token.Value, token.Secret);
            string json = await meetup.RestOperations.GetForObjectAsync<string>($"https://api.meetup.com/2/events?group_urlname={groupName}");
            var oEvents = JObject.Parse(json);
            List<MeetupInfo> meetupEvents = JsonConvert.DeserializeObject<List<MeetupInfo>>(oEvents["results"].ToString());
            return meetupEvents;
        }
    }
}
