using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SistemaInventario.AplicacionWeb.Models.ViewModels;
using SistemaInventario.AplicacionWeb.Utilidades.Response;
using SistemaInventario.BLL.Interfaces;
using SistemaInventario.Entity.Models;

namespace SistemaInventario.AplicacionWeb.Controllers
{
    public class ArticuloController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IArticuloService _articuloService;
       

        public ArticuloController(IMapper mapper, IArticuloService articuloService)
        {
            _mapper = mapper;
            _articuloService = articuloService;
            
        }
    
        public IActionResult Index()
        {
            return View();
        }

       

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<VMarticulo> vmarticulos=_mapper.Map<List<VMarticulo>>(await _articuloService.Lista());
            return StatusCode(StatusCodes.Status200OK, new {data=vmarticulos});
        }

       
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] VMarticulo vmarticulo)
        {
            GenericResponse<VMarticulo> gResponse = new GenericResponse<VMarticulo>();
            try
            {
                if (vmarticulo == null)
                {
                    return BadRequest("El modelo no puede ser nulo");
                }

                Articulo articulo_creada = await _articuloService.Crear(_mapper.Map<Articulo>(vmarticulo));
                vmarticulo = _mapper.Map<VMarticulo>(articulo_creada);
                gResponse.Estado = true;
                gResponse.Objeto = vmarticulo;
            }
            catch (Exception ex)
            {
                gResponse.Estado = false;
                gResponse.mensaje = ex.Message;
            }
            return StatusCode(StatusCodes.Status200OK, gResponse);
        }

        [HttpPut]
        public async Task<IActionResult> Editar([FromBody] VMarticulo vmarticulo)
        {
            GenericResponse<VMarticulo> gResponse = new GenericResponse<VMarticulo>();
            try
            {
                if (vmarticulo == null)
                {
                    return BadRequest("El modelo no puede ser nulo");
                }

                Articulo articulo_editar = await _articuloService.Editar(_mapper.Map<Articulo>(vmarticulo));
                vmarticulo = _mapper.Map<VMarticulo>(articulo_editar);
                gResponse.Estado = true;
                gResponse.Objeto = vmarticulo;
            }
            catch (Exception ex)
            {
                gResponse.Estado = false;
                gResponse.mensaje = ex.Message;
            }
            return StatusCode(StatusCodes.Status200OK, gResponse);
        }

        

        [HttpDelete]
        public async Task<IActionResult> Eliminar(int idarticulo)
        {
            GenericResponse<string> gResponse = new GenericResponse<string>();
            try
            {

                gResponse.Estado = await _articuloService.Eliminar(idarticulo);
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
