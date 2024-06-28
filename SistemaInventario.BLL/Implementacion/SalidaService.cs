using Microsoft.EntityFrameworkCore;
using SistemaInventario.BLL.Interfaces;
using SistemaInventario.DAL.Interfaces;
using SistemaInventario.Entity.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.BLL.Implementacion
{
    public class SalidaService : IsalidaService
    {
        private readonly IGenericReposity<Articulo> _repositorioarticulo;
        private readonly IsalidaRepository _repositoriosalida;
        public SalidaService(IGenericReposity<Articulo>repositorioarticulo, IsalidaRepository repositoriosalida)
        {
            _repositorioarticulo = repositorioarticulo;
            _repositoriosalida = repositoriosalida;
        }

        public async Task<List<Articulo>> Obtenerproducto(string busqueda)
        {
            IQueryable<Articulo> query = await _repositorioarticulo.Consultar(
                p => p.EsActivo == true &&
                p.Stock > 0 &&
                string.Concat(p.Serie, p.CodigoArticulo, p.Descripcion).Contains(busqueda));
                return query.Include(c => c.IdSubcategoriaNavigation)
                .Include(c => c.IdMarcaNavigation)
                .Include(c => c.IdUnidadMedidaNavigation)
                .ToList();
        }

        public async Task<Salida> Registrar(Salida entidad)
        {
            try
            {
                return await _repositoriosalida.Registrar(entidad); 
            }
            catch (Exception ex) {
                throw;
            }
        }

        public async Task<List<Salida>> Historial(string numeropedido, string fechainicio, string fechafin)
        {
            IQueryable<Salida> query = await _repositoriosalida.Consultar();
            fechainicio=fechainicio is null?"":fechainicio;
            fechafin = fechafin is null ? "" : fechafin;

            if(fechainicio!="" && fechafin !="")
            {
                DateTime fecha_inicio = DateTime.ParseExact(fechainicio, "dd/MM/yyyy", new CultureInfo("es-PE"));
                DateTime fecha_fin = DateTime.ParseExact(fechafin, "dd/MM/yyyy", new CultureInfo("es-PE"));
               return  query.Where(v =>
                v.FechaSalida.Date >= fecha_inicio &&
                v.FechaSalida.Date <= fecha_fin)
                .Include(u => u.IdPersonalNavigation)
                .Include(dv => dv.DetalleSalida).ToList() ;
            }
            else
            {
             return    query.Where(v =>v.NumeroPedido==numeropedido)
                .Include(u => u.IdPersonalNavigation)
                .Include(dv => dv.DetalleSalida).ToList();
            }
        }


        public async Task<Salida> Detalle(string numerosalida)
        {
            IQueryable<Salida> query = await _repositoriosalida.Consultar(v =>v.NumeroPedido == numerosalida);

           return  query
            .Include(u => u.IdPersonalNavigation)
            .Include(dv => dv.DetalleSalida).First();
        }
     
        public async Task<List<DetalleSalida>> Reporte(string fechainicio, string fechafin)
        {
            DateTime fecha_inicio = DateTime.ParseExact(fechainicio, "dd/MM/yyyy", new CultureInfo("es-PE"));
            DateTime fecha_fin = DateTime.ParseExact(fechafin, "dd/MM/yyyy", new CultureInfo("es-PE"));
            List<DetalleSalida> lista = await _repositoriosalida.Reporte(fecha_inicio, fecha_fin);
            return lista;   
        }
    }
}
