using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using SistemaInventario.DAL.Interfaces;
using SistemaInventario.Entity;
using SistemaInventario.Entity.Models;

namespace SistemaInventario.DAL.Implementacion
{
    public class SalidaRepository : GenericRepository<Salida>, IsalidaRepository
    {
        private readonly dbhospitalcontext dbcontext;   

        public SalidaRepository(dbhospitalcontext _dbcontext):base(_dbcontext)
        {
            dbcontext = _dbcontext;
        }
        public async Task<Salida> Registrar(Salida entidad)
        {
            Salida salidaGenerada = new Salida();

            using (var transaction = dbcontext.Database.BeginTransaction())
            {
                try
                {
                    foreach (DetalleSalida dv in entidad.DetalleSalida)
                    {
                        Articulo producto_econtrando = dbcontext.Articulos.Where(p => p.IdArticulo == dv.IdArticulo).First();
                        producto_econtrando.Stock = producto_econtrando.Stock - dv.Cantidad;
                        dbcontext.Articulos.Update(producto_econtrando);
                    }
                    await dbcontext.SaveChangesAsync();
                    salidaGenerada = entidad;
                    transaction.Commit();   
                }
                //NumeroCorrelativo correlativo = dbcontext.numerosCorrelativos.Where

                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
            return salidaGenerada;
        }

        public async Task<List<DetalleSalida>> Reporte(DateTime FechaInicio, DateTime FechaFin)
        {
            List<DetalleSalida> listaresumen = await dbcontext.DetalleSalida
                .Include(v => v.IdSalidaNavigation)
                .ThenInclude(u => u.IdPersonalNavigation)
                .Include(v => v.IdSalidaNavigation)
                .Where(dv => dv.IdSalidaNavigation.FechaSalida.Date >= FechaInicio.Date &&
                dv.IdSalidaNavigation.FechaSalida.Date <= FechaFin.Date)
                .ToListAsync();

            return listaresumen;
                
        }
    }
}
