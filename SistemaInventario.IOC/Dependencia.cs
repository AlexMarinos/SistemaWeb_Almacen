using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

using SistemaInventario.DAL.Implementacion;
using SistemaInventario.DAL.Interfaces;
using SistemaInventario.BLL.Implementacion;
using SistemaInventario.BLL.Interfaces;
using Microsoft.Extensions.Options;
using System.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaInventario.Entity.Models;



namespace SistemaInventario.IOC
{
    public static class Dependencia
    {


        public static void InyectarDependencia(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<dbhospitalcontext>(options =>
            {
                options.UseMySql(configuration.GetConnectionString("CadenaMysql"), Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.22-mysql"));
            });

            services.AddTransient(typeof(IGenericReposity<>),typeof(GenericRepository<>));
            //services.AddTransient(typeof(IGenericReposity<>), typeof(SalidaRepository<>));
            services.AddScoped<IsalidaRepository, SalidaRepository>();


            services.AddScoped<ICategoriaService, CategoriaService>();
            services.AddScoped<IServicioService, ServicioService>();
            services.AddScoped<IAreaService, AreaService>();
            services.AddScoped<IOrigenService, OrigenService>();
            services.AddScoped<IMarcaService, MarcaService>();
            services.AddScoped<IUnidadmedidaService, UnidadmedidaService>();
            services.AddScoped<IArticuloService, ArticuloService>();
            services.AddScoped<ISubcategoriaService, SubcategoriaService>();
            services.AddScoped<IpersonalService, PersonalService>();
            services.AddScoped<IsalidaService, SalidaService>();



        }


    }

}
