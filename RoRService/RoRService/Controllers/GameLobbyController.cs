using AutoMapper;
using RoRService.Helpers;
using RoRService.Models.DataModels;
using RoRService.Models.ViewModels.PlayersViewModels;
using RoRService.Models.ViewModels.GamesViewModels;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using RoRService.Resources.Contracts;
using System.Collections.Generic;
using RoRService.Models.DataModels.Enums;

namespace RoRService.Controllers
{
    [Authorize]
    public class GameLobbyController : Controller
    {
        IGamesResource gamesResource;
        IPlayersResource playersResource;

        public GameLobbyController(IGamesResource gamesResource, IPlayersResource playersResource)
        {
            this.gamesResource = gamesResource;
            this.playersResource = playersResource;
        }

        public ActionResult Index()
        {
            ViewBag.Title = "The Republic of Rome";
            return View();
        }

        public async Task<ActionResult> GetGameList(string checkedOptions)
        {
            var checkedOptionList = HttpUtility.ParseQueryString(checkedOptions);
            var gameList = await gamesResource.GetGameList();

            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Game, GameListVM>(); });
            var iMapper = config.CreateMapper();
            var gameListVM = iMapper.Map<List<Game>, List<GameListVM>>(gameList);

            return PartialView("_gameList",
                               gameListVM.Where(g => (g.Status != GameStatus.Open || checkedOptionList["openGames"] != null) &&
                                                     (g.Status != GameStatus.Playing || checkedOptionList["playingGames"] != null) &&
                                                     (g.Status != GameStatus.Completed || checkedOptionList["completedGames"] != null))
                                         .OrderByDescending(g => g.DateOfLastAction)
                                           
                              );
        }

        public async Task<ActionResult> GetMyGameList(string playerName, string checkedOptions)
        {
            var checkedOptionList = HttpUtility.ParseQueryString(checkedOptions);
            var gameList = await gamesResource.GetGameList();

            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Game, GameListVM>(); });
            var iMapper = config.CreateMapper();
            var gameListVM = iMapper.Map<List<Game>, List<GameListVM>>(gameList);

            return PartialView("_gameList",
                                   gameListVM.Where(g => (g.Owner == playerName) &&
                                                         (g.Status != GameStatus.Open || checkedOptionList["openGames"] != null) &&
                                                         (g.Status != GameStatus.Playing || checkedOptionList["playingGames"] != null) &&
                                                         (g.Status != GameStatus.Completed || checkedOptionList["completedGames"] != null))
                                             .OrderByDescending(g => g.DateOfLastAction)
                                  );
        }

        public async Task<ActionResult> GetGameDetails(long id)
        {
            var game = await gamesResource.GetGameDetails(id);

            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Game, GameDetailsVM>(); });
            var iMapper = config.CreateMapper();
            var gameDetailVM = iMapper.Map<Game, GameDetailsVM>(game);

            return PartialView("_gameDetails", gameDetailVM);
        }

        public async Task<ActionResult> AddPlayerToGame(GameDetailsVM gameDetailsVM)
        {
            if (gameDetailsVM.PlayerList.Count > 5)
            {
                ModelState.AddModelError("CustomError", "Cannot have more than 6 Players in a game");
            }

            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<NewPlayerVM, Player>(); });
                var iMapper = config.CreateMapper();
                var newPlayer = iMapper.Map<NewPlayerVM, Player>(gameDetailsVM.NewPlayer);

                var response = await playersResource.AddPlayerToGame(gameDetailsVM.Id, newPlayer);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetGameDetails", new { id = gameDetailsVM.Id });
                }
                else
                {
                    ModelState.AddModelError("CustomError", response.ToString());
                    return PartialView("_gameDetails", gameDetailsVM);
                }
            }
            else
            {
                return PartialView("_gameDetails", gameDetailsVM);
            }
        }

        [HttpGet]
        public ActionResult CreateNewGame()
        {
            var newGameCreateVM = new GameCreateVM();
            return PartialView("_gameCreate", newGameCreateVM);
        }

        [HttpPost]
        public async Task<ActionResult> CreateNewGame(GameCreateVM newGameCreateVM)
        {
            newGameCreateVM.NewPlayer.Name = newGameCreateVM.Owner;

            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<GameCreateVM, Game>(); });
                IMapper iMapper = config.CreateMapper();
                var newGame = iMapper.Map<GameCreateVM, Game>(newGameCreateVM);

                config = new MapperConfiguration(cfg => { cfg.CreateMap<NewPlayerVM, Player>(); });
                iMapper = config.CreateMapper();
                var newPlayer = iMapper.Map<NewPlayerVM, Player>(newGameCreateVM.NewPlayer);

                newGame.Status = GameStatus.Open;
                newGame.PlayerList.Add(newPlayer);

                var result = await gamesResource.CreateNewGame(newGame);

                if (result.IsSuccessStatusCode)
                {
                    return PartialView("_gameCreateSuccessful");
                }
                else
                {
                    ModelState.AddModelError("CustomError", result.ToString());
                    return PartialView("_gameCreate", newGameCreateVM);
                }
            }
            else
            {
                return PartialView("_gameCreate", newGameCreateVM);
            }
        }

        public async Task<ActionResult> DeleteGame(long id)
        {
            using (var client = HttpClientHelper.GetHttpClient())
            {
                HttpResponseMessage result = await client.DeleteAsync("api/Games/" + id.ToString());

                if (result.IsSuccessStatusCode)
                {
                }

                return RedirectToAction("Index", "Home");
            }
        }
    }
}