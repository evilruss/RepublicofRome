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
    public class LegionsResource : ILegionsResource
    {
        public async Task<List<Legion>> GetLegions(long gameId)
        {
            var legionList = new List<Legion>();

            using (var client = HttpClientHelper.GetHttpClient())
            {
                string url = "api/Games/" + gameId + "/Legions";

                HttpResponseMessage result = await client.GetAsync(url);

                if (result.IsSuccessStatusCode)
                {
                    var GameResponse = result.Content.ReadAsStringAsync().Result;
                    legionList = JsonConvert.DeserializeObject<List<Legion>>(GameResponse);
                }

                return legionList;
            }
        }

        public async Task<HttpResponseMessage> CreateLegions(long gameId, List<Legion> legionList)
        {
            using (var client = HttpClientHelper.GetHttpClient())
            {
                string url = "api/Games/" + gameId + "/Legions";

                HttpResponseMessage result = await client.PostAsJsonAsync(url, legionList);

                return result;
            }
        }
    }
}