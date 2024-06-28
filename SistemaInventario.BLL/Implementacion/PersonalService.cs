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
    public class PersonalService : IpersonalService
    {
        private readonly IGenericReposity<Personal> _reposity;
        public PersonalService(IGenericReposity<Personal> reposity)
        {
            _reposity = reposity;
        }

        public  async Task<Personal> Crear(Personal entidad)
        {
  
            try
            {
                Personal personal_creado = await _reposity.Crear(entidad);
                if (personal_creado.IdPersonal == 0)

                    throw new TaskCanceledException("No se pudo crear personal");
                IQueryable<Personal> query = await _reposity.Consultar(p => p.IdPersonal== personal_creado.IdPersonal);
                personal_creado = query.Include(c => c.IdServicioNavigation).First();
                return personal_creado;

            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear personal", ex);

            }
        }

        public async Task<Personal> Editar(Personal entidad)
        {

            try
            {
                IQueryable<Personal> querypersonal = await _reposity.Consultar(p => p.IdPersonal == entidad.IdPersonal);
                Personal personal_editar = querypersonal.First();

                personal_editar.Nombres = entidad.Nombres;
                personal_editar.Apellidos = entidad.Apellidos;
                personal_editar.Dni = entidad.Dni;
                personal_editar.Condicion = entidad.Condicion;
                personal_editar.Celular = entidad.Celular;
                personal_editar.IdServicio = entidad.IdServicio;
                personal_editar.EsActivo = entidad.EsActivo;

                bool respuesta = await _reposity.Editar(personal_editar);
                if (!respuesta)
                {
                    throw new TaskCanceledException("No se pudo editar el personal");
                }

                Personal personal_editado = querypersonal

                                    .Include(c => c.IdServicioNavigation)
                                    .First();

                return personal_editado;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al editar el personal", ex);
            }
        }


        public async Task<bool> Eliminar(int idpersonal)
        {
            try
            {
                Personal personal_encontrado = await _reposity.Obtener(p => p.IdPersonal== idpersonal);
                if (personal_encontrado == null)
                {
                    throw new TaskCanceledException("El personal no existe");
                }

                bool respuesta = await _reposity.Eliminar(personal_encontrado);
                return true; // Devuelve directamente la respuesta de la operación de eliminar.
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar personal", ex);

            }
        }

        public  async Task<List<Personal>> Lista()
        {
            IQueryable<Personal> query = await _reposity.Consultar();
            return query.Include(c => c.IdServicioNavigation)
                                    .ToList();
        }
    }
}
