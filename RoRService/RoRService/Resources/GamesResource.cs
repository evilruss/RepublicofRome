using Newtonsoft.Json;
using RoRService.Helpers;
using RoRService.Models.DataModels;
using RoRService.Resources.Contracts;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace RoRService.Resources
{
    public class GamesResource : IGamesResource
    {
        public async Task<List<Game>> GetGameList()
        {
            var gameList = new List<Game>();

            using (var client = HttpClientHelper.GetHttpClient())
            {
                string url = "api/Games";

                HttpResponseMessage result = await client.GetAsync(url);

                if (result.IsSuccessStatusCode)
                {
                    var GameResponse = result.Content.ReadAsStringAsync().Result;
                    gameList = JsonConvert.DeserializeObject<List<Game>>(GameResponse);
                }

                return gameList;
            }
        }

        public async Task<Game> GetGameDetails(long id)
        {
            var game = new Game();

            using (var client = HttpClientHelper.GetHttpClient())
            {
                string url = "api/Games/" + id.ToString();

                HttpResponseMessage result = await client.GetAsync(url);

                if (result.IsSuccessStatusCode)
                {
                    var gameResponse = result.Content.ReadAsStringAsync().Result;
                    game = JsonConvert.DeserializeObject<Game>(gameResponse);
                }

                return game;
            }
        }

        public async Task<HttpResponseMessage> CreateNewGame(Game newGame)
        {
            using (var client = HttpClientHelper.GetHttpClient())
            {
                string url = "api/Games";

                HttpResponseMessage result = await client.PostAsJsonAsync(url, newGame);

                return result;
            }
        }

        public async Task<HttpResponseMessage> UpdateGame(Game game)
        {
            using (var client = HttpClientHelper.GetHttpClient())
            {
                string url = "api/Games/" + game.Id;

                HttpResponseMessage result = await client.PutAsJsonAsync(url, game);

                return result;
            }
        }

    }
}