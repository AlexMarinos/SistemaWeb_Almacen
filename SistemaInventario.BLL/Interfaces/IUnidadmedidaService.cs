using SistemaInventario.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.BLL.Interfaces
{
    public  interface IUnidadmedidaService
    {
        Task<List<UnidadMedida>> lista();
        Task<UnidadMedida> crear(UnidadMedida entidad);
        Task<UnidadMedida> editar(UnidadMedida entidad);

        Task<bool> eliminar(int idunidadmedida);
    }
}
