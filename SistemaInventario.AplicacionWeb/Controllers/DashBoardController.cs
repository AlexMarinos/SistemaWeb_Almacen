using Microsoft.AspNetCore.Mvc;

namespace SistemaInventario.AplicacionWeb.Controllers
{
    public class DashBoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
