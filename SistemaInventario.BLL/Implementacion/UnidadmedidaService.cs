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
    public class UnidadmedidaService : IUnidadmedidaService
    {
        private readonly IGenericReposity<UnidadMedida> _reposity;
        public UnidadmedidaService(IGenericReposity<UnidadMedida> reposity)
        {
            _reposity = reposity;
        }
        public async Task<UnidadMedida> crear(UnidadMedida entidad)
        {
            try
            {
               UnidadMedida Unidadmedida_creada = await _reposity.Crear(entidad);
                if (Unidadmedida_creada.IdUnidadMedida == 0)

                    throw new Exception("No se pudo crear la Unidad");
                return Unidadmedida_creada;

            }
            catch
            {
                throw;
            }
        }
        public async Task<UnidadMedida> editar(UnidadMedida entidad)
        {
            try
            {
                UnidadMedida  Unidadmedida_encontrada = await _reposity.Obtener(c => c.IdUnidadMedida == entidad.IdUnidadMedida);
                Unidadmedida_encontrada.Descripcion = entidad.Descripcion;
                Unidadmedida_encontrada.EsActivo = entidad.EsActivo;
                bool respuesta = await _reposity.Editar(Unidadmedida_encontrada);
                if (!respuesta) throw new TaskCanceledException("No fue encontrada la unidad medida");
                return Unidadmedida_encontrada;

            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> eliminar(int idunidadmedida)
        {
            try
            {
                UnidadMedida unidadmedida_encontrada = await _reposity.Obtener(c => c.IdUnidadMedida == idunidadmedida);
                if (unidadmedida_encontrada == null)

                    throw new TaskCanceledException("la unidad medida no existe");
                bool respuesta = await _reposity.Eliminar(unidadmedida_encontrada);
                return respuesta;
            }
            catch
            {
                throw;
            }
        }
        public async Task<List<UnidadMedida>> lista()
        {
            IQueryable<UnidadMedida> query = await _reposity.Consultar();
            return query.ToList();
        }
    }
}
