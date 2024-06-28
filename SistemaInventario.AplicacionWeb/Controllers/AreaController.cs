using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SistemaInventario.AplicacionWeb.Models.ViewModels;
using SistemaInventario.AplicacionWeb.Utilidades.Response;
using SistemaInventario.BLL.Interfaces;
using SistemaInventario.Entity.Models;

namespace SistemaInventario.AplicacionWeb.Controllers
{
    public class AreaController : Controller
    {

        private readonly IMapper _mapper;
        private readonly IAreaService _AreaServicio;
        public AreaController(IMapper mapper, IAreaService areaServicio)
        {
            _AreaServicio = areaServicio;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> lista()
        {

            List<VMarea> vmserviciolista = _mapper.Map<List<VMarea>>(await _AreaServicio.lista());
            return StatusCode(StatusCodes.Status200OK, new { data = vmserviciolista });
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] VMarea modelo)
        {
            GenericResponse<VMarea> gResponse = new GenericResponse<VMarea>();
            try
            {
                Area area_creada = await _AreaServicio.crear(_mapper.Map<Area>(modelo));
                modelo = _mapper.Map<VMarea>(area_creada);
                gResponse.Estado = true;
                gResponse.Objeto = modelo;
            }
            catch (Exception ex)
            {
                gResponse.Estado = true;
                gResponse.mensaje = ex.Message;
            }
            return StatusCode(StatusCodes.Status200OK, gResponse);
        }

        [HttpPut]
        public async Task<IActionResult> Editar([FromBody] VMarea modelo)
        {
            GenericResponse<VMarea> gResponse = new GenericResponse<VMarea>();
            try
            {
               Area area_editada = await _AreaServicio.editar(_mapper.Map<Area>(modelo));
                modelo = _mapper.Map<VMarea>(area_editada);
                gResponse.Estado = true;
                gResponse.Objeto = modelo;
            }
            catch (Exception ex)
            {
                gResponse.Estado = true;
                gResponse.mensaje = ex.Message;
            }
            return StatusCode(StatusCodes.Status200OK, gResponse);
        }
        [HttpDelete]
        public async Task<IActionResult> Eliminar(int idarea)
        {
            GenericResponse<string> gResponse = new GenericResponse<string>();
            try
            {
                gResponse.Estado = await _AreaServicio.eliminar(idarea);

            }
            catch (Exception ex)
            {
                gResponse.Estado = false;
                gResponse.mensaje = ex.Message;
            }
            return StatusCode(StatusCodes.Status200OK, gResponse);
        }
    }
}
