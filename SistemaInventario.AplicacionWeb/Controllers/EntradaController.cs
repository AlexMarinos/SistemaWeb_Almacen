using Microsoft.AspNetCore.Mvc;

namespace SistemaInventario.AplicacionWeb.Controllers
{
    public class EntradaController : Controller
    {
        public IActionResult NuevaEntrada()
        {
            return View();
        }

        public IActionResult HistorialEntrada()
        {
            return View();
        }
    }
}
