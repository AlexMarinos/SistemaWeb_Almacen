using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SistemaInventario.AplicacionWeb.Models.ViewModels;
using SistemaInventario.AplicacionWeb.Utilidades.Response;
using SistemaInventario.BLL.Implementacion;
using SistemaInventario.BLL.Interfaces;
using SistemaInventario.Entity.Models;

namespace SistemaInventario.AplicacionWeb.Controllers
{
    public class SubcategoriaController : Controller
    {

        private readonly IMapper _mapper;
        private readonly ISubcategoriaService _subcategoriaService;


        public SubcategoriaController(IMapper mapper, ISubcategoriaService subcategoriaService)
        {
            _mapper = mapper;
            _subcategoriaService = subcategoriaService;

        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<VMSubcategoria> vmsubcategoria = _mapper.Map<List<VMSubcategoria>>(await _subcategoriaService.lista());
            return StatusCode(StatusCodes.Status200OK, new { data = vmsubcategoria });
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] VMSubcategoria vmsub)
        {
            GenericResponse<VMSubcategoria> gResponse = new GenericResponse<VMSubcategoria>();
            try
            {
                if (vmsub == null)
                {
                    return BadRequest("El modelo no puede ser nulo");
                }

                Subcategoria sub_creada = await _subcategoriaService.Crear(_mapper.Map<Subcategoria>(vmsub));
                vmsub = _mapper.Map<VMSubcategoria>(sub_creada);
                gResponse.Estado = true;
                gResponse.Objeto = vmsub;
            }
            catch (Exception ex)
            {
                gResponse.Estado = false;
                gResponse.mensaje = ex.Message;
            }
            return StatusCode(StatusCodes.Status200OK, gResponse);
        }

        [HttpPut]
        public async Task<IActionResult> Editar([FromBody] VMSubcategoria vmsubcategoria)
        {
            GenericResponse<VMSubcategoria> gResponse = new GenericResponse<VMSubcategoria>();
            try
            {
                if (vmsubcategoria == null)
                {
                    return BadRequest("El modelo no puede ser nulo");
                }

                Subcategoria Subcategoria_editar = await _subcategoriaService.editar(_mapper.Map<Subcategoria>(vmsubcategoria));
                vmsubcategoria = _mapper.Map<VMSubcategoria>(Subcategoria_editar);
                gResponse.Estado = true;
                gResponse.Objeto = vmsubcategoria;
            }
            catch (Exception ex)
            {
                gResponse.Estado = false;
                gResponse.mensaje = ex.Message;
            }
            return StatusCode(StatusCodes.Status200OK, gResponse);
        }



        [HttpDelete]
        public async Task<IActionResult> Eliminar(int idsubcategoria)
        {
            GenericResponse<string> gResponse = new GenericResponse<string>();
            try
            {

                gResponse.Estado = await _subcategoriaService.eliminar(idsubcategoria);
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
