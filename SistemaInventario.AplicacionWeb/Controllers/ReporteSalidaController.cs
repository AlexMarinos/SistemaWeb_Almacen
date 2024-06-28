using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using SistemaInventario.AplicacionWeb.Models.ViewModels;
using SistemaInventario.BLL.Interfaces;

namespace SistemaInventario.AplicacionWeb.Controllers
{
    public class ReporteSalidaController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IsalidaService _salidaServicio;

        public ReporteSalidaController(IMapper mapper, IsalidaService salidaServicio)
        {
            _mapper = mapper;
            _salidaServicio = salidaServicio;
        }

        public IActionResult ReporteSalidas()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> ReporteSalida(string fechainicio, string fechafin)
        {
            List<VMReporteSalida> vmLista = _mapper.Map<List<VMReporteSalida>>(await _salidaServicio.Reporte(fechainicio, fechafin));
            return StatusCode(StatusCodes.Status200OK, new { data = vmLista });
        }
    }
}
