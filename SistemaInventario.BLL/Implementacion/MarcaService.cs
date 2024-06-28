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
    public class MarcaService : IMarcaService
    {

        private readonly IGenericReposity<Marca> _reposity;
        public MarcaService(IGenericReposity<Marca> reposity)
        {
            _reposity = reposity;
        }
        public async Task<Marca> crear(Marca entidad)
        {
            try
            {
                Marca Marca_creada = await _reposity.Crear(entidad);
                if (Marca_creada.IdMarca == 0)

                    throw new Exception("No se pudo crear la marca");
                return Marca_creada;

            }
            catch
            {
                throw;
            }
        }


        public async Task<Marca> editar(Marca entidad)
        {
            try
            {
                Marca Marca_encontrada = await _reposity.Obtener(c => c.IdMarca== entidad.IdMarca);
                Marca_encontrada.Descripcion = entidad.Descripcion;
                Marca_encontrada.EsActivo = entidad.EsActivo;
                bool respuesta = await _reposity.Editar(Marca_encontrada);
                if (!respuesta) throw new TaskCanceledException("No fue encontrada la marca");
                return Marca_encontrada;

            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> eliminar(int idmarca)
        {
            try
            {
                Marca marca_encontrada = await _reposity.Obtener(c => c.IdMarca == idmarca);
                if (marca_encontrada == null)

                    throw new TaskCanceledException("la marca no existe");
                bool respuesta = await _reposity.Eliminar(marca_encontrada);
                return respuesta;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Marca>> lista()
        {
            IQueryable<Marca> query = await _reposity.Consultar();
            return query.ToList();
        }
    }
}
