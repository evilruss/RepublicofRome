using RoRService.Engines.Contracts;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RoRService.Controllers
{
    public class GameBoardController : Controller
    {
        readonly string _gameCookieName = "GAME_COOKIE";

        IGameEngine gameEngine;

        public GameBoardController(IGameEngine gameEngine)
        {
            this.gameEngine = gameEngine;
        }

        public ActionResult Index()
        {
            var gameCookie = Request.Cookies.Get(_gameCookieName);

            if (gameCookie != null)
            {
                return RedirectToAction("ResumeGame", new { gameId = gameCookie.Value});
            }
            else
            {
                return View();
            }
        }

        public async Task<ActionResult> ResumeGame(long gameId)
        {
            var gameBoardVM = await gameEngine.ResumeGame(gameId);

            return View(gameBoardVM);
        }

        public async Task<ActionResult> StartGame(long gameId)
        {
            await gameEngine.StartGame(gameId);

            return RedirectToAction("ResumeGame", new { gameId = gameId});
        }
    }
}