using Newtonsoft.Json;
using RoRService.Helpers;
using RoRService.Models.DataModels.Enums;
using RoRService.Models.ViewModels.GamesViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RoRService.Controllers
{
    public class FactionController : Controller
    {
        public async Task<ActionResult> GetFactionList(string checkedOptions)
        {
            var checkedOptionList = HttpUtility.ParseQueryString(checkedOptions);
            var gameList = new List<GameListVM>();

            using (var client = HttpClientHelper.GetHttpClient())
            {
                HttpResponseMessage result = await client.GetAsync("api/Games");

                if (result.IsSuccessStatusCode)
                {
                    var GameResponse = result.Content.ReadAsStringAsync().Result;
                    gameList = JsonConvert.DeserializeObject<List<GameListVM>>(GameResponse);
                }

                return PartialView("_gameList",
                                   gameList.Where(g => (g.Status != GameStatus.Open || checkedOptionList["openGames"] != null) &&
                                                       (g.Status != GameStatus.Playing || checkedOptionList["playingGames"] != null) &&
                                                       (g.Status != GameStatus.Completed || checkedOptionList["completedGames"] != null)
                                  ));
            }
        }
    }
}