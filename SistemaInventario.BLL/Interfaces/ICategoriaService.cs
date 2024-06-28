using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaInventario.Entity;
using SistemaInventario.Entity.Models;

namespace SistemaInventario.BLL.Interfaces
{
   public interface ICategoriaService
    {
        Task<List<Categoria>> lista();
        Task<Categoria> crear(Categoria entidad);
        Task<Categoria> editar( Categoria entidad);

        Task<bool> eliminar(int idcategoria);  
    }
}
