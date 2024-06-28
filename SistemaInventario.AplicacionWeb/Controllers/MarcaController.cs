using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SistemaInventario.AplicacionWeb.Models.ViewModels;
using SistemaInventario.AplicacionWeb.Utilidades.Response;
using SistemaInventario.BLL.Interfaces;
using SistemaInventario.Entity.Models;

namespace SistemaInventario.AplicacionWeb.Controllers
{
    public class MarcaController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMarcaService _MarcaServicio;
        public MarcaController(IMapper mapper, IMarcaService marcaServicio)
        {
            _MarcaServicio = marcaServicio;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> lista()
        {

            List<VMmarca> vmmarcalista = _mapper.Map<List<VMmarca>>(await _MarcaServicio.lista());
            return StatusCode(StatusCodes.Status200OK, new { data = vmmarcalista });
        }
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] VMmarca modelo)
        {
            GenericResponse<VMmarca> gResponse = new GenericResponse<VMmarca>();
            try
            {
                Marca marca_creada = await _MarcaServicio.crear(_mapper.Map<Marca>(modelo));
                modelo = _mapper.Map<VMmarca>(marca_creada);
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
        public async Task<IActionResult> Editar([FromBody] VMmarca modelo)
        {
            GenericResponse<VMmarca> gResponse = new GenericResponse<VMmarca>();
            try
            {
                Marca marca_editada = await _MarcaServicio.editar(_mapper.Map<Marca>(modelo));
                modelo = _mapper.Map<VMmarca>(marca_editada);
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
        public async Task<IActionResult> Eliminar(int idmarca)
        {
            GenericResponse<string> gResponse = new GenericResponse<string>();
            try
            {
                gResponse.Estado = await _MarcaServicio.eliminar(idmarca);

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
