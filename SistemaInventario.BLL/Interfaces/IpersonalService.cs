using SistemaInventario.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.BLL.Interfaces
{
    public  interface IpersonalService
    {
        Task<List<Personal>> Lista();
        Task<Personal> Crear(Personal entidad);
        Task<Personal> Editar(Personal entidad);
        Task<bool> Eliminar(int idpersonal);
    }
}
