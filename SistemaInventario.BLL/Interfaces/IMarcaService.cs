using SistemaInventario.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.BLL.Interfaces
{
  public interface IMarcaService
    {
        Task<List<Marca>> lista();
        Task<Marca> crear(Marca entidad);
        Task<Marca> editar(Marca entidad);
        Task<bool> eliminar(int idmarca);
    }
}
