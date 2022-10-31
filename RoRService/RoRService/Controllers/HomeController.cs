using System.Web.Mvc;

namespace RoRService.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Index", "GameLobby");
        }
    }
}
