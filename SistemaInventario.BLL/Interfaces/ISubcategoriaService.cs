using SistemaInventario.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.BLL.Interfaces
{
    public  interface ISubcategoriaService
    {
        Task<List<Subcategoria>> lista();
     
        Task<Subcategoria> Crear(Subcategoria entidad);
        Task<Subcategoria> editar(Subcategoria entidad);
        Task<bool> eliminar(int idsubcategoria);

       
    }
}
