using Microsoft.AspNetCore.Mvc;

namespace Business.Areas.EAdmin.Controllers
{
    [Area("EAdmin")]
    public class DashBoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
