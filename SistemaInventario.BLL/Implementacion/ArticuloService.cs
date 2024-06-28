using Microsoft.EntityFrameworkCore;
using SistemaInventario.BLL.Interfaces;
using SistemaInventario.DAL.Implementacion;
using SistemaInventario.DAL.Interfaces;
using SistemaInventario.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.BLL.Implementacion
{
    public class ArticuloService : IArticuloService
    {

        private readonly IGenericReposity<Articulo> _reposity;
        public ArticuloService(IGenericReposity<Articulo> reposity)
        {
            _reposity = reposity;
        }
       

        public async Task<List<Articulo>> Lista()
        {
            IQueryable<Articulo> query = await _reposity.Consultar();
            return query.Include(c => c.IdSubcategoriaNavigation)
                                     .Include(c=>c.IdMarcaNavigation)
                                     .Include(c=>c.IdUnidadMedidaNavigation)
                                    .ToList() ;  
        }
        ///listar con los demas tablas /
        public async Task<Articulo> Crear(Articulo entidad)
        {
            Articulo producto_existe = await _reposity.Obtener(p => p.CodigoArticulo == entidad.CodigoArticulo
                                                      && p.Serie == entidad.Serie);

            if (producto_existe != null)  // Cambiado a '!= null' para que tenga sentido con el mensaje de error
            {
                throw new TaskCanceledException("El código de producto y serie ya existe.");
            }

            try
            {
                Articulo articulo_Creado = await _reposity.Crear(entidad);
                if (articulo_Creado.IdArticulo == 0)

                    throw new TaskCanceledException("No se pudo crear  el articulo");
                IQueryable<Articulo> query = await _reposity.Consultar(s => s.IdArticulo == articulo_Creado.IdArticulo);
                articulo_Creado = query.Include(c => c.IdSubcategoriaNavigation).First();
                articulo_Creado = query.Include(c => c.IdMarcaNavigation).First();
                articulo_Creado = query.Include(c => c.IdUnidadMedidaNavigation).First();
                return articulo_Creado;

            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el articulo", ex);

            }

        }

       
        public async Task<Articulo> Editar(Articulo entidad)
        {
            Articulo producto_existe = await _reposity.Obtener(p => p.CodigoArticulo == entidad.CodigoArticulo
                                                         && p.Serie == entidad.Serie
                                                         && p.IdArticulo != entidad.IdArticulo);

            if (producto_existe != null)
            {
                throw new TaskCanceledException("El código de producto y serie ya existe.");
            }

            try
            {
                IQueryable<Articulo> queryarticulo = await _reposity.Consultar(p => p.IdArticulo == entidad.IdArticulo);
                Articulo producto_editar = queryarticulo.First();

                producto_editar.CodigoArticulo = entidad.CodigoArticulo;
                producto_editar.Serie = entidad.Serie;
                producto_editar.Descripcion = entidad.Descripcion;
                producto_editar.IdSubcategoria = entidad.IdSubcategoria;
                producto_editar.IdMarca = entidad.IdMarca;
                producto_editar.IdUnidadMedida = entidad.IdUnidadMedida;
                producto_editar.Stock = entidad.Stock;
                producto_editar.EsActivo = entidad.EsActivo;

                bool respuesta = await _reposity.Editar(producto_editar);
                if (!respuesta)
                {
                    throw new TaskCanceledException("No se pudo editar el artículo.");
                }

                Articulo producto_editado = queryarticulo
                                    .Include(c => c.IdSubcategoriaNavigation)
                                    .Include(c => c.IdMarcaNavigation)
                                    .Include(c => c.IdUnidadMedidaNavigation)
                                    .First();

                return producto_editado;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al editar el artículo", ex);
            }
        }

        public async Task<bool> Eliminar(int idarticulo)
        {
            try
            {
                Articulo producto_encontrado = await _reposity.Obtener(p => p.IdArticulo == idarticulo);
                if (producto_encontrado == null)
                {
                    throw new TaskCanceledException("El articulo no existe");
                }

                bool respuesta = await _reposity.Eliminar(producto_encontrado);
                return true; // Devuelve directamente la respuesta de la operación de eliminar.
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el artículo", ex);

            }
        }

        
    }
}
