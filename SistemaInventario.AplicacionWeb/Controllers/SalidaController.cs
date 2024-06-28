using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SistemaInventario.AplicacionWeb.Models.ViewModels;
using SistemaInventario.AplicacionWeb.Utilidades.Response;
using SistemaInventario.BLL.Interfaces;
using SistemaInventario.DAL.Interfaces;
using SistemaInventario.Entity.Models;

namespace SistemaInventario.AplicacionWeb.Controllers
{
    public class SalidaController : Controller
    {
        private readonly IsalidaService _salidaService;
        private readonly IMapper _mapper;

        public SalidaController(IsalidaService salidaS, IMapper mapper)
        {
            _salidaService = salidaS;
            _mapper = mapper;
        }



        public IActionResult NuevaSalida()
        {
            return View();
        }
        public IActionResult HistorialSalida()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Obtenerarticulos(string busqueda)
        {
            List<VMarticulo> vmlista = _mapper.Map<List<VMarticulo>>(await _salidaService.Obtenerproducto(busqueda));
            return StatusCode(StatusCodes.Status200OK, vmlista);    
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarSalida([FromBody ]VMsalida modelo)
        {
            GenericResponse<VMsalida> gResponse = new GenericResponse<VMsalida>();
            try
            {
                //modelo.IdPersonal = 1;
                Salida salida_creada=await _salidaService.Registrar(_mapper.Map<Salida>(modelo));
                modelo = _mapper.Map<VMsalida>(salida_creada);
                gResponse.Estado = true;
                gResponse.Objeto = modelo;

            }
            catch (Exception ex) {
                gResponse.Estado =false;
                gResponse.mensaje = ex.Message;
            }
           
            return StatusCode(StatusCodes.Status200OK, gResponse);
        }

        [HttpGet]
        public async Task<IActionResult> Historial(string numerosalida , string fechainicio , string fechafin )
        {
            List<VMsalida> vmhistorialsalida = _mapper.Map<List<VMsalida>>(await _salidaService.Historial(numerosalida ,fechainicio,fechafin));
            return StatusCode(StatusCodes.Status200OK, vmhistorialsalida);
        }




    }
}
