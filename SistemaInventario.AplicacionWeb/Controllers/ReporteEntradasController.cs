using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using SistemaInventario.AplicacionWeb.Models.ViewModels;
using SistemaInventario.BLL.Interfaces;
namespace SistemaInventario.AplicacionWeb.Controllers
{
    public class ReporteEntradasController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IIngresoService_ingresoServicio;
        
        public ReporteEntradasController(IMapper mapper, IIngresoService ingresoServicio )
        {
            _mapper = mapper;
            _ingresoServicio = ingresoServicio;        }
        public IActionResult ReporteEntradas()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> ReporteEntradas(string fechainicio, string fechafin)
        {
            List<VMReporteIngreso>

            return View();
        }
    }
}
