using Newtonsoft.Json;
using RoRService.Helpers;
using RoRService.Models.DataModels;
using RoRService.Resources.Contracts;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace RoRService.Resources
{
    public class PlayersResource : IPlayersResource
    {
        public async Task<HttpResponseMessage> AddPlayerToGame(long gameId, Player newPlayer)
        {
            using (var client = HttpClientHelper.GetHttpClient())
            {
                string postURL = "api/Games/" + gameId.ToString() + "/Players";
                var result = await client.PostAsJsonAsync(postURL, newPlayer);
                return result;
            }
        }

        public async Task<List<Player>> GetPlayers(long gameId)
        {
            using (var client = HttpClientHelper.GetHttpClient())
            {
                string getURL = "api/Games/" + gameId.ToString() + "/Players";
                HttpResponseMessage result = await client.GetAsync(getURL);

                if (result.IsSuccessStatusCode)
                {
                    var response = result.Content.ReadAsStringAsync().Result;
                    var playerList = JsonConvert.DeserializeObject<List<Player>>(response);

                    return playerList;
                }
                else
                {
                    throw new System.Exception("Unable to retrieve Player list for Game " + gameId.ToString());
                }
            }
        }
    }
}