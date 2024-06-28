using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SistemaInventario.AplicacionWeb.Models.ViewModels;
using SistemaInventario.AplicacionWeb.Utilidades.Response;
using SistemaInventario.BLL.Interfaces;
using SistemaInventario.Entity.Models;

namespace SistemaInventario.AplicacionWeb.Controllers
{
    public class OrigenController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IOrigenService _origenServicio;
        public OrigenController(IMapper mapper, IOrigenService OrigenServicio)
        {
            _origenServicio = OrigenServicio;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> lista()
        {

            List<VMorigen> vmorigenlista = _mapper.Map<List<VMorigen>>(await _origenServicio.lista());
            return StatusCode(StatusCodes.Status200OK, new { data = vmorigenlista });
        }
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] VMorigen modelo)
        {
            GenericResponse<VMorigen> gResponse = new GenericResponse<VMorigen>();
            try
            {
                Origen Origen_creada = await _origenServicio.crear(_mapper.Map<Origen>(modelo));
                modelo = _mapper.Map<VMorigen>(Origen_creada);
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
        public async Task<IActionResult> Editar([FromBody] VMorigen modelo)
        {
            GenericResponse<VMorigen> gResponse = new GenericResponse<VMorigen>();
            try
            {
                Origen origen_editada = await _origenServicio.editar(_mapper.Map<Origen>(modelo));
                modelo = _mapper.Map<VMorigen>(origen_editada);
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
        public async Task<IActionResult> Eliminar(int idorigen)
        {
            GenericResponse<string> gResponse = new GenericResponse<string>();
            try
            {
                gResponse.Estado = await _origenServicio.eliminar(idorigen);

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
