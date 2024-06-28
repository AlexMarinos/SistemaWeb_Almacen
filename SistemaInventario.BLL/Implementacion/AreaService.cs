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
    public class AreaService : IAreaService
    {
        private readonly IGenericReposity<Area> _reposity;
        public AreaService(IGenericReposity<Area> reposity)
        {
            _reposity = reposity;
        }

        public async Task<Area> crear(Area entidad)
        {
            try
            {
                Area Area_creada = await _reposity.Crear(entidad);
                if (Area_creada.IdArea == 0)

                    throw new Exception("No se pudo crear el area");
                return Area_creada;

            }
            catch
            {
                throw;
            }
        }


        public async Task<Area> editar(Area entidad)
        {
            try
            {
                Area Area_encontrada = await _reposity.Obtener(c => c.IdArea == entidad.IdArea);
                Area_encontrada.Descripcion = entidad.Descripcion;
                Area_encontrada.EsActivo = entidad.EsActivo;
                bool respuesta = await _reposity.Editar(Area_encontrada);
                if (!respuesta) throw new TaskCanceledException("No fue encontrada el área");
                return Area_encontrada;

            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> eliminar(int idarea)
        {
            try
            {
                Area Area_encontrada = await _reposity.Obtener(c => c.IdArea == idarea);
                if (Area_encontrada == null)

                    throw new TaskCanceledException("el área no existe");
                bool respuesta = await _reposity.Eliminar(Area_encontrada);
                return respuesta;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Area>> lista()
        {
            IQueryable<Area> query = await _reposity.Consultar();
            return query.ToList();
        }
    }
}
