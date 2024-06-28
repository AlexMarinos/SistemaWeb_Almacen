using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaInventario.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using SistemaInventario.Entity.Models;

namespace SistemaInventario.DAL.Implementacion
{
    public class GenericRepository<TEntity> : IGenericReposity<TEntity> where TEntity : class
    {
        private readonly dbhospitalcontext dbcontext;
        public GenericRepository(dbhospitalcontext db)
        {
            dbcontext = db;
        }

        public async Task<TEntity> Obtener(Expression<Func<TEntity, bool>> filtro)
        {
            try
            {
                TEntity entidad = await dbcontext.Set<TEntity>().FirstOrDefaultAsync(filtro);
                return entidad;
            }
            catch 
            {

                throw;
            }
        }
        public async Task<TEntity> Crear(TEntity entidad)
        {
            try
            {
                dbcontext.Add(entidad);
                await dbcontext.SaveChangesAsync();
                return entidad;
            }
            catch 
            {

                throw;
            }
        }

        public async Task<bool> Editar(TEntity entidad)
        {
            try
            {
                dbcontext.Update(entidad);
                await dbcontext.SaveChangesAsync();
                return true;
            }
            catch 
            {

                throw;
            }
        }

        public async Task<bool> Eliminar(TEntity entidad)
        {
            try
            {
                dbcontext.Remove(entidad);
                await dbcontext.SaveChangesAsync();
                return true;
            }
            catch 
            {

                throw;
            }
        }
        public async Task<IQueryable<TEntity>> Consultar(Expression<Func<TEntity, bool>> filtro = null)
        {
            IQueryable<TEntity> queryEntidad = filtro == null ? dbcontext.Set<TEntity>() : dbcontext.Set<TEntity>().Where(filtro);
            return queryEntidad;
        }

        

      

        
    }
      
}
