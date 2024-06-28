using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

using AutoMapper;

using SistemaInventario.AplicacionWeb.Models.ViewModels;
using SistemaInventario.AplicacionWeb.Utilidades.Response;
using SistemaInventario.BLL.Interfaces;
using SistemaInventario.Entity;
using SistemaInventario.DAL.Implementacion;
using System.Globalization;
using SistemaInventario.Entity.Models;



namespace SistemaInventario.AplicacionWeb.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICategoriaService _categoriaServicio;
        public CategoriaController( IMapper mapper, ICategoriaService categoriaServicio)
        {
            _categoriaServicio = categoriaServicio;
            _mapper = mapper;
        }

            public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> lista()
        {

            List<VMcategoria> vmcategorialista = _mapper.Map<List<VMcategoria>>(await _categoriaServicio.lista());
            return StatusCode(StatusCodes.Status200OK,new {data=vmcategorialista});
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody]VMcategoria modelo)
        {
            GenericResponse<VMcategoria> gResponse = new GenericResponse<VMcategoria>();
            try
            {
                Categoria categoria_creada = await _categoriaServicio.crear(_mapper.Map<Categoria>(modelo));
                modelo=_mapper.Map<VMcategoria>(categoria_creada);
                gResponse.Estado = true;
                gResponse.Objeto = modelo;
            }
            catch (Exception ex)
            {
                gResponse.Estado = true;
                gResponse.mensaje = ex.Message;
            }
            return StatusCode(StatusCodes.Status200OK,gResponse);
        }

        [HttpPut]
        public async Task<IActionResult> Editar([FromBody] VMcategoria modelo)
        {
            GenericResponse<VMcategoria> gResponse = new GenericResponse<VMcategoria>();
            try
            {
                Categoria categoria_editada = await _categoriaServicio.editar(_mapper.Map<Categoria>(modelo));
                modelo = _mapper.Map<VMcategoria>(categoria_editada);
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
        public async Task<IActionResult> Eliminar(int idcategoria)
        {
            GenericResponse<string> gResponse = new GenericResponse<string>();
            try
            {
                gResponse.Estado = await _categoriaServicio.eliminar(idcategoria);

            }
            catch(Exception ex) {
                gResponse.Estado = false;
                gResponse.mensaje=ex.Message;   
            }
            return StatusCode(StatusCodes.Status200OK, gResponse);
        }
    }
}
