using RoRService.Resources.Contracts;
using System.Collections.Generic;
using RoRService.Models.DataModels;
using System.Threading.Tasks;
using System.Net.Http;
using RoRService.Helpers;
using Newtonsoft.Json;
using System.Linq;

namespace RoRService.Resources
{
    public class EventsResource : IEventsResource
    {
        public async Task<Event> GetEvent(int eventId, int eventLevel)
        {
            var newEvent = new Event();

            using (var client = HttpClientHelper.GetHttpClient())
            {
                string url = "api/Events/" + eventId + "/Level/" + eventLevel;

                HttpResponseMessage result = await client.GetAsync(url);

                if (result.IsSuccessStatusCode)
                {
                    var response = result.Content.ReadAsStringAsync().Result;
                    newEvent = JsonConvert.DeserializeObject<Event>(response);
                }

                return newEvent;
            }
        }

        public async Task<List<Event>> GetEventsForGame(long gameId)
        {
            var eventList = new List<Event>();

            using (var client = HttpClientHelper.GetHttpClient())
            {
                string url = "api/Games/" + gameId.ToString() + "/Events";

                HttpResponseMessage result = await client.GetAsync(url);

                if (result.IsSuccessStatusCode)
                {
                    var response = result.Content.ReadAsStringAsync().Result;
                    eventList = JsonConvert.DeserializeObject<List<Event>>(response);
                }

                return eventList;
            }
        }

        public async Task<HttpResponseMessage> CreateEventForGame(long gameId, Event newEvent)
        {
            var eventList = await GetEventsForGame(gameId);

            // Event does not exist so Create (Post) at Level 1.
            if (eventList == null || eventList.Count == 0 || !eventList.Exists(e => e.No == newEvent.No))
            {
                using (var client = HttpClientHelper.GetHttpClient())
                {
                    string url = "api/Games/" + gameId + "/Events";

                    HttpResponseMessage result = await client.PostAsJsonAsync(url, newEvent);

                    return result;
                }
            }
            // Event exists at level 1, Update (Put) to Level 2.
            else if (eventList.Exists(e => e.No == newEvent.No && e.Level == 1))
            {
                newEvent.Level = 2;

                using (var client = HttpClientHelper.GetHttpClient())
                {
                    string url = "api/Games/" + gameId + "/Events/" + newEvent.No;

                    HttpResponseMessage result = await client.PutAsJsonAsync(url, newEvent);

                    return result;
                }
            }
            // Event already at Level 2. Do nothing.
            else
            {
                return null;
            }
        }
    }
}