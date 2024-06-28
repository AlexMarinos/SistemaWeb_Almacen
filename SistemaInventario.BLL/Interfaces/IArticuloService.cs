using SistemaInventario.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.BLL.Interfaces
{
     public  interface IArticuloService
    {
        Task<List<Articulo>> Lista();
        Task<Articulo>Crear(Articulo articulo);
        Task<Articulo> Editar(Articulo articulo);
        Task<bool> Eliminar(int idarticulo);

      
    }
}
