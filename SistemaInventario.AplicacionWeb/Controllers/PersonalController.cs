using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SistemaInventario.AplicacionWeb.Models.ViewModels;
using SistemaInventario.AplicacionWeb.Utilidades.Response;
using SistemaInventario.BLL.Implementacion;
using SistemaInventario.BLL.Interfaces;
using SistemaInventario.Entity.Models;

namespace SistemaInventario.AplicacionWeb.Controllers
{
    public class PersonalController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IpersonalService _personalService;


        public PersonalController(IMapper mapper, IpersonalService personalService)
        {
            _mapper = mapper;
            _personalService = personalService;

        }
        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<VMpersonal> vmpersonal = _mapper.Map<List<VMpersonal>>(await _personalService.Lista());
            return StatusCode(StatusCodes.Status200OK, new { data = vmpersonal });
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] VMpersonal vmpersonal)
        {
            GenericResponse<VMpersonal> gResponse = new GenericResponse<VMpersonal>();
            try
            {
                if (vmpersonal == null)
                {
                    return BadRequest("El modelo no puede ser nulo");
                }

                Personal personal_creada = await _personalService.Crear(_mapper.Map<Personal>(vmpersonal));
                vmpersonal = _mapper.Map<VMpersonal>(personal_creada);
                gResponse.Estado = true;
                gResponse.Objeto = vmpersonal;
            }
            catch (Exception ex)
            {
                gResponse.Estado = false;
                gResponse.mensaje = ex.Message;
            }
            return StatusCode(StatusCodes.Status200OK, gResponse);
        }

        [HttpPut]
        public async Task<IActionResult> Editar([FromBody] VMpersonal vmpersonal)
        {
            GenericResponse<VMpersonal> gResponse = new GenericResponse<VMpersonal>();
            try
            {
                if (vmpersonal == null)
                {
                    return BadRequest("El modelo no puede ser nulo");
                }

                Personal personal_editar = await _personalService.Editar(_mapper.Map<Personal>(vmpersonal));
                vmpersonal = _mapper.Map<VMpersonal>(personal_editar);
                gResponse.Estado = true;
                gResponse.Objeto = vmpersonal;
            }
            catch (Exception ex)
            {
                gResponse.Estado = false;
                gResponse.mensaje = ex.Message;
            }
            return StatusCode(StatusCodes.Status200OK, gResponse);
        }


        [HttpDelete]
        public async Task<IActionResult> Eliminar(int idpersonal)
        {
            GenericResponse<string> gResponse = new GenericResponse<string>();
            try
            {

                gResponse.Estado = await _personalService.Eliminar(idpersonal);
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
