using SistemaInventario.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.BLL.Interfaces
{
    public interface IServicioService
    {
        Task<List<Servicio>> lista();
        Task<Servicio> crear(Servicio entidad);
        Task<Servicio> editar(Servicio entidad);

        Task<bool> eliminar(int idservicio);
    }
}
