using SistemaInventario.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.BLL.Interfaces
{
    public  interface IOrigenService
    {
        Task<List<Origen>> lista();
        Task<Origen> crear(Origen entidad);
        Task<Origen> editar(Origen entidad);

        Task<bool> eliminar(int idorigen);
    }
}
