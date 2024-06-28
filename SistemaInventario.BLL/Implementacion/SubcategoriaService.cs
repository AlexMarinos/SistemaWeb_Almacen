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
    public class SubcategoriaService : ISubcategoriaService
    {
        private readonly IGenericReposity<Subcategoria> _reposity;
        public SubcategoriaService(IGenericReposity<Subcategoria> reposity)
        {
            _reposity = reposity;
        }



        public async Task<List<Subcategoria>> lista()
        {
            IQueryable<Subcategoria> query = await _reposity.Consultar();
            return query.Include(c => c.IdCategoriaNavigation).ToList();
        }
        public async Task<Subcategoria> Crear(Subcategoria entidad)
        {
            try
            {
                Subcategoria sub_creado = await _reposity.Crear(entidad);
                if (sub_creado.IdSubcategoria == 0)

                    throw new TaskCanceledException("No se pudo crear subcategoria");
                IQueryable<Subcategoria> query = await _reposity.Consultar(s => s.IdSubcategoria == sub_creado.IdSubcategoria);
                sub_creado = query.Include(c => c.IdCategoriaNavigation).First();
                return sub_creado;

            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el subcategoria", ex);

            }

        }
        public async Task<Subcategoria> editar(Subcategoria entidad)
        {
            try
            {
                IQueryable<Subcategoria> queryarticulo = await _reposity.Consultar(p => p.IdSubcategoria == entidad.IdSubcategoria);
                Subcategoria SubCategoria_editar = queryarticulo.First();
                SubCategoria_editar.Descripcion = entidad.Descripcion;
                SubCategoria_editar.IdCategoria = entidad.IdCategoria;
                SubCategoria_editar.EsActivo = entidad.EsActivo;

                bool respuesta = await _reposity.Editar(SubCategoria_editar);
                if (!respuesta)
                    throw new TaskCanceledException("No se pudo editar el Subcategoria.");

                Subcategoria Subcategoria_editado = queryarticulo
                                 .Include(c => c.IdCategoriaNavigation)
                                 .First();

                return Subcategoria_editado;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el subcategoria", ex);

            }
        }

        public async Task<bool> eliminar(int idsubcategoria)
        {

            try
            {
                Subcategoria Subcategoria_encontrado = await _reposity.Obtener(p => p.IdSubcategoria == idsubcategoria);
                if (Subcategoria_encontrado == null)
                {
                    throw new TaskCanceledException("El Subcategoria no existe");
                }

                bool respuesta = await _reposity.Eliminar(Subcategoria_encontrado);
                return true; // Devuelve directamente la respuesta de la operación de eliminar.
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el Subcategoria", ex);

            }
        }

       
    }
}
