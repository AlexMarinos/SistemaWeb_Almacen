using Microsoft.EntityFrameworkCore;
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
    public class ServicioService : IServicioService
    {

        private readonly IGenericReposity<Servicio> _reposity;
        public ServicioService(IGenericReposity<Servicio> reposity)
        {
            _reposity = reposity;
        }

        public async  Task<Servicio> crear(Servicio entidad)
        {

            try
            {
                Servicio servicio_creado = await _reposity.Crear(entidad);
                if (servicio_creado.IdServicio == 0)

                    throw new TaskCanceledException("No se pudo crear Servicio");
                IQueryable<Servicio> query = await _reposity.Consultar(p => p.IdServicio == servicio_creado.IdServicio);
                servicio_creado = query.Include(c => c.IdAreaNavigation).First();
                return servicio_creado;

            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear servicio", ex);

            }
        }

        public  async Task<Servicio> editar(Servicio entidad)
        {
            try
            {
                IQueryable<Servicio> queryservicio = await _reposity.Consultar(p => p.IdServicio == entidad.IdServicio);
                Servicio servicio_editar = queryservicio.First();

                servicio_editar.Descripcion = entidad.Descripcion;
                servicio_editar.EsActivo = entidad.EsActivo;
                servicio_editar.IdArea = entidad.IdArea;

                bool respuesta = await _reposity.Editar(servicio_editar);
                if (!respuesta)
                {
                    throw new TaskCanceledException("No se pudo editar el servicio");
                }

                Servicio Servicio_editado = queryservicio
                                    .Include(c => c.IdAreaNavigation)
                                    .First();

                return Servicio_editado;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al editar el Servicio", ex);
            }
        }

        public  async Task<bool> eliminar(int idservicio)
        {
            try
            {
                Servicio servicio_encontrado = await _reposity.Obtener(p => p.IdServicio == idservicio);
                if (servicio_encontrado == null)
                {
                    throw new TaskCanceledException("El servicio no existe");
                }

                bool respuesta = await _reposity.Eliminar(servicio_encontrado);
                return true; // Devuelve directamente la respuesta de la operación de eliminar.
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar servicio", ex);

            }
        }

        public  async Task<List<Servicio>> lista()
        {
            IQueryable<Servicio> query = await _reposity.Consultar();
            return query.Include(c => c.IdAreaNavigation)
                                    .ToList();
        }
    }
}
