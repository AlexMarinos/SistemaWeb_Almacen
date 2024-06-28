using SistemaInventario.BLL.Interfaces;
using SistemaInventario.DAL.Interfaces;
using SistemaInventario.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.BLL.Implementacion
{
    public class OrigenService : IOrigenService
    {

        private readonly IGenericReposity<Origen> _reposity;
        public OrigenService(IGenericReposity<Origen> reposity)
        {
            _reposity = reposity;
        }
        public async Task<Origen> crear(Origen entidad)
        {
            try
            {
                Origen origen_creada = await _reposity.Crear(entidad);
                if (origen_creada.IdOrigen == 0)

                    throw new Exception("No se pudo crear el origen");
                return origen_creada;

            }
            catch
            {
                throw;
            }
        }


        public async Task<Origen> editar(Origen entidad)
        {
            try
            {
                Origen Origen_encontrada = await _reposity.Obtener(c => c.IdOrigen == entidad.IdOrigen);
               Origen_encontrada.Descripcion = entidad.Descripcion;
                Origen_encontrada.EsActivo = entidad.EsActivo;
                bool respuesta = await _reposity.Editar(Origen_encontrada);
                if (!respuesta) throw new TaskCanceledException("No fue encontrada el origen");
                return Origen_encontrada;

            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> eliminar(int idorigen)
        {
            try
            {
              Origen origen_encontrada = await _reposity.Obtener(c => c.IdOrigen == idorigen);
                if (origen_encontrada == null)

                    throw new TaskCanceledException("el origen no existe");
                bool respuesta = await _reposity.Eliminar(origen_encontrada);
                return respuesta;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Origen>> lista()
        {
            IQueryable<Origen> query = await _reposity.Consultar();
            return query.ToList();
        }
    }
}
