using Newtonsoft.Json;
using RoRService.Helpers;
using RoRService.Models.DataModels;
using RoRService.Models.DataModels.Enums;
using RoRService.Resources.Contracts;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System;

namespace RoRService.Resources
{
    public class OfficesResource : IOfficesResource
    {
        public async Task<List<Office>> GetOffices(long gameId)
        {
            var office = new Office();

            using (var client = HttpClientHelper.GetHttpClient())
            {
                HttpResponseMessage result = await client.GetAsync("api/Offices/" + (int)officeTitle);

                if (result.IsSuccessStatusCode)
                {
                    var gameResponse = result.Content.ReadAsStringAsync().Result;
                    office = JsonConvert.DeserializeObject<Office>(gameResponse);
                }

                return office;
            }
        }

        public async Task<Office> GetOffice(long gameId, OfficeTitle officeTitle)
        {
            var office = new Office();

            using (var client = HttpClientHelper.GetHttpClient())
            {
                HttpResponseMessage result = await client.GetAsync("api/Offices/" + (int)officeTitle);

                if (result.IsSuccessStatusCode)
                {
                    var gameResponse = result.Content.ReadAsStringAsync().Result;
                    office = JsonConvert.DeserializeObject<Office>(gameResponse);
                }

                return office;
            }
        }

        public async Task CreateOffices(long gameId)
        {
            var postURL = "api/Games/" + gameId + "/Offices";

            using (var client = HttpClientHelper.GetHttpClient())
            {
                for (int i = 1; i < 7; i++)
                {
                    var office = new Office()
                    {
                        GameId = gameId,
                        OfficeId = i,
                        CardNo = 0
                    };

                    HttpResponseMessage result = await client.PostAsJsonAsync(postURL, office);
                }
            }
        }

        public async Task UpdateOffice(long gameId, Office office)
        {
            var postURL = "api/Games/" + gameId + "/Offices/" + office.OfficeId;

            using (var client = HttpClientHelper.GetHttpClient())
            {
                HttpResponseMessage result = await client.PutAsJsonAsync(postURL, office);
            }
        }
    }
}