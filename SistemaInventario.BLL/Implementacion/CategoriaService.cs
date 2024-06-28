using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using SistemaInventario.BLL.Interfaces;
using SistemaInventario.DAL.Interfaces;
using SistemaInventario.Entity;
using SistemaInventario.Entity.Models;

namespace SistemaInventario.BLL.Implementacion
{
    public class CategoriaService : ICategoriaService
    {
        private readonly IGenericReposity<Categoria> _reposity;
        public CategoriaService(IGenericReposity<Categoria> reposity)
        {
            _reposity = reposity;
        }

        public async Task<List<Categoria>> lista()
        {
            IQueryable<Categoria> query = await _reposity.Consultar();
            return query.ToList();
        }
        public async Task<Categoria> crear(Categoria entidad)
        {
            try
            {
                Categoria categoria_creada = await _reposity.Crear(entidad);
                if (categoria_creada.IdCategoria == 0)

                    throw new Exception("No se pudo crear la categoria");
                return categoria_creada;

            }
            catch
            {
                throw;
            }
        }

        public async Task<Categoria> editar(Categoria entidad)
        {
            try
            {
                Categoria categoria_encontrada = await _reposity.Obtener(c => c.IdCategoria == entidad.IdCategoria);
                categoria_encontrada.Descripcion = entidad.Descripcion;
                categoria_encontrada.EsActivo = entidad.EsActivo;
                bool respuesta = await _reposity.Editar(categoria_encontrada);
                if (!respuesta) throw new TaskCanceledException("No fue encontrada la categoria");
                return categoria_encontrada;

            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> eliminar(int idcategoria)
        {
            try
            {
                Categoria categoria_encontrada = await _reposity.Obtener(c => c.IdCategoria == idcategoria);
                if (categoria_encontrada == null)

                    throw new TaskCanceledException("la categoria no existe");
                bool respuesta = await _reposity.Eliminar(categoria_encontrada);
                return respuesta;
            }
            catch
            {
                throw;
            }
        }


    }
}
