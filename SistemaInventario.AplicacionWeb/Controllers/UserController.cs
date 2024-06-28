using Microsoft.AspNetCore.Mvc;

namespace SistemaInventario.AplicacionWeb.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
