using SistemaInventario.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.BLL.Interfaces
{
    public  interface IAreaService
    {
        Task<List<Area>> lista();
        Task<Area> crear(Area entidad);
        Task<Area> editar(Area entidad);
        Task<bool> eliminar(int idarea);
    }
}
