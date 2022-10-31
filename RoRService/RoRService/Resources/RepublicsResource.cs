using Newtonsoft.Json;
using RoRService.Helpers;
using RoRService.Models.DataModels;
using RoRService.Resources.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace RoRService.Resources
{
    public class RepublicsResource : IRepublicsResource
    {
        public async Task<Republic> GetRepublic(long gameId)
        {
            var republic = new Republic();

            using (var client = HttpClientHelper.GetHttpClient())
            {
                HttpResponseMessage result = await client.GetAsync("api/Games/" + gameId + "/Republics");

                if (result.IsSuccessStatusCode)
                {
                    var response = result.Content.ReadAsStringAsync().Result;
                    republic = JsonConvert.DeserializeObject<Republic>(response);
                }

                return republic;
            }
        }

        public async Task CreateRepublic(long gameId, Republic republic)
        {
            using (var client = HttpClientHelper.GetHttpClient())
            {
                string postURL = "api/Games/" + gameId + "/Republics";
                HttpResponseMessage resultGame = await client.PostAsJsonAsync(postURL, republic);
            }
        }

        public async Task UpdateRepublic(long gameId, Republic republic)
        {
            using (var client = HttpClientHelper.GetHttpClient())
            {
                string postURL = "api/Games/" + gameId + "Republics";
                HttpResponseMessage resultGame = await client.PutAsJsonAsync(postURL, republic);
            }
        }
    }
}