using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SistemaInventario.AplicacionWeb.Models.ViewModels;
using SistemaInventario.AplicacionWeb.Utilidades.Response;
using SistemaInventario.BLL.Interfaces;
using SistemaInventario.Entity.Models;

namespace SistemaInventario.AplicacionWeb.Controllers
{
    public class UnidadMedidaController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnidadmedidaService _unidadmedidaServicio;
        public UnidadMedidaController(IMapper mapper, IUnidadmedidaService unidadmedidaServicio)
        {
            _unidadmedidaServicio = unidadmedidaServicio;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> lista()
        {

            List<VMunidadmedida> vmunidadmedidalista = _mapper.Map<List<VMunidadmedida>>(await _unidadmedidaServicio.lista());
            return StatusCode(StatusCodes.Status200OK, new { data = vmunidadmedidalista });
        }
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] VMunidadmedida modelo)
        {
            GenericResponse<VMunidadmedida> gResponse = new GenericResponse<VMunidadmedida>();
            try
            {
                UnidadMedida Unidadmedida_creada = await _unidadmedidaServicio.crear(_mapper.Map<UnidadMedida>(modelo));
                modelo = _mapper.Map<VMunidadmedida>(Unidadmedida_creada);
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
        public async Task<IActionResult> Editar([FromBody] VMunidadmedida modelo)
        {
            GenericResponse<VMunidadmedida> gResponse = new GenericResponse<VMunidadmedida>();
            try
            {
                UnidadMedida unidadmedida_editada = await _unidadmedidaServicio.editar(_mapper.Map<UnidadMedida>(modelo));
                modelo = _mapper.Map<VMunidadmedida>(unidadmedida_editada);
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
        public async Task<IActionResult> Eliminar(int idunidadmedida)
        {
            GenericResponse<string> gResponse = new GenericResponse<string>();
            try
            {
                gResponse.Estado = await _unidadmedidaServicio.eliminar(idunidadmedida);

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
