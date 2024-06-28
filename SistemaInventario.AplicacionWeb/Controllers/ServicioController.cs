using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SistemaInventario.AplicacionWeb.Models.ViewModels;
using SistemaInventario.AplicacionWeb.Utilidades.Response;
using SistemaInventario.BLL.Interfaces;
using SistemaInventario.Entity.Models;

namespace SistemaInventario.AplicacionWeb.Controllers
{
    public class ServicioController : Controller
    {


        private readonly IMapper _mapper;
        private readonly IServicioService _servicioServicio;
        public ServicioController(IMapper mapper, IServicioService servicioServicio)
        {
            _servicioServicio = servicioServicio;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> lista()
        {

            List<VMservicio> vmserviciolista = _mapper.Map<List<VMservicio>>(await _servicioServicio.lista());
            return StatusCode(StatusCodes.Status200OK, new { data = vmserviciolista });
        }
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] VMservicio modelo)
        {
            GenericResponse<VMservicio> gResponse = new GenericResponse<VMservicio>();
            try
            {
                Servicio Servicio_creada = await _servicioServicio.crear(_mapper.Map<Servicio>(modelo));
                modelo = _mapper.Map<VMservicio>(Servicio_creada);
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
        public async Task<IActionResult> Editar([FromBody] VMservicio modelo)
        {
            GenericResponse<VMservicio> gResponse = new GenericResponse<VMservicio>();
            try
            {
                Servicio servicio_editada = await _servicioServicio.editar(_mapper.Map<Servicio>(modelo));
                modelo = _mapper.Map<VMservicio>(servicio_editada);
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
        public async Task<IActionResult> Eliminar(int idservicio)
        {
            GenericResponse<string> gResponse = new GenericResponse<string>();
            try
            {
                gResponse.Estado = await _servicioServicio.eliminar(idservicio);

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
