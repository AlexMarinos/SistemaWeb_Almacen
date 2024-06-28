using AutoMapper;
using SistemaInventario.AplicacionWeb.Controllers;
using SistemaInventario.AplicacionWeb.Models.ViewModels;
using SistemaInventario.Entity.Models;
using System.Globalization;
using SistemaInventario.Entity;
namespace SistemaInventario.AplicacionWeb.Utilidades.Automapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {


            #region categoria
            CreateMap<Categoria, VMcategoria>()
                .ForMember(destino => destino.EsActivo,
                           opt => opt.MapFrom(origen => origen.EsActivo.HasValue && origen.EsActivo.Value ? 1 : 0));

            CreateMap<VMcategoria, Categoria>()
                .ForMember(destino => destino.EsActivo,
                           opt => opt.MapFrom(origen => origen.EsActivo == 1 ? (bool?)true : (bool?)false));
            #endregion categoria

            #region subcategoria
            CreateMap<Subcategoria, VMSubcategoria>()
            .ForMember(dest => dest.esActivo, opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0)
            )
            .ForMember(dest => dest.categoriaDescripcion, opt => opt.MapFrom(origen => origen.IdCategoriaNavigation.Descripcion)

            );

            CreateMap<VMSubcategoria, Subcategoria>()
                .ForMember(destino => destino.EsActivo,
                           opt => opt.MapFrom(origen => origen.esActivo == 1 ? true : false)
            )
                .ForMember(destino => destino.IdCategoriaNavigation,
                           opt => opt.Ignore()
           );

            #endregion subcategoria

            #region servicio
            CreateMap<Servicio, VMservicio>()
                .ForMember(dest => dest.EsActivo, opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0)
                )
                .ForMember(dest => dest.Descripcionarea, opt => opt.MapFrom(origen => origen.IdAreaNavigation.Descripcion)   
                );

            CreateMap<VMservicio, Servicio>()
                .ForMember(destino => destino.EsActivo,
                           opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false)
            )
                .ForMember(destino => destino.IdAreaNavigation,
                           opt => opt.Ignore()

           );

            #endregion servicio

            #region area
            CreateMap<Area, VMarea>()
                .ForMember(destino => destino.EsActivo,
                           opt => opt.MapFrom(origen => origen.EsActivo.HasValue && origen.EsActivo.Value ? 1 : 0));

            CreateMap<VMarea, Area>()
                .ForMember(destino => destino.EsActivo,
                           opt => opt.MapFrom(origen => origen.EsActivo == 1 ? (bool?)true : (bool?)false));
            #endregion area

            #region origen
            CreateMap<Origen, VMorigen>()
                .ForMember(destino => destino.EsActivo,
                           opt => opt.MapFrom(origen => origen.EsActivo.HasValue && origen.EsActivo.Value ? 1 : 0));

            CreateMap<VMorigen, Origen>()
                .ForMember(destino => destino.EsActivo,
                           opt => opt.MapFrom(origen => origen.EsActivo == 1 ? (bool?)true : (bool?)false));
            #endregion origen
            #region marca
            CreateMap<Marca, VMmarca>()
                .ForMember(destino => destino.EsActivo,
                           opt => opt.MapFrom(origen => origen.EsActivo.HasValue && origen.EsActivo.Value ? 1 : 0));

            CreateMap<VMmarca, Marca>()
                .ForMember(destino => destino.EsActivo,
                           opt => opt.MapFrom(origen => origen.EsActivo == 1 ? (bool?)true : (bool?)false));
            #endregion marca

            #region unidadmedida
            CreateMap<UnidadMedida, VMunidadmedida>()
                .ForMember(destino => destino.EsActivo,
                           opt => opt.MapFrom(origen => origen.EsActivo.HasValue && origen.EsActivo.Value ? 1 : 0));

            CreateMap<VMunidadmedida, UnidadMedida>()
                .ForMember(destino => destino.EsActivo,
                           opt => opt.MapFrom(origen => origen.EsActivo == 1 ? (bool?)true : (bool?)false));
            #endregion unidadmedida

           
           #region articulo
        CreateMap<Articulo, VMarticulo>()
            .ForMember(dest => dest.EsActivo, opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0)
            )
            .ForMember(dest => dest.SubcategoriaDescripcion, opt => opt.MapFrom(origen => origen.IdSubcategoriaNavigation.Descripcion)
            )
            .ForMember(dest => dest.MarcaDescripcion, opt => opt.MapFrom(origen => origen.IdMarcaNavigation.Descripcion)
             )
            .ForMember(dest => dest.UnidadMedidaDescripcion, opt => opt.MapFrom(origen => origen.IdUnidadMedidaNavigation.Descripcion)
            );

            CreateMap<VMarticulo, Articulo>()
                .ForMember(destino => destino.EsActivo,
                           opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false)
            )
                .ForMember(destino => destino.IdSubcategoriaNavigation,
                           opt => opt.Ignore()
            )
               .ForMember(destino => destino.IdMarcaNavigation,
                           opt => opt.Ignore()
            )
                .ForMember(destino => destino.IdUnidadMedidaNavigation,
                           opt => opt.Ignore()
           );

            #endregion articulo

            #region personal
            CreateMap<Personal, VMpersonal>()
                .ForMember(dest => dest.EsActivo, opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0)
                )
                .ForMember(dest => dest.ServicioDescripcion, opt => opt.MapFrom(origen => origen.IdServicioNavigation.Descripcion)
                
   
                );

            CreateMap<VMpersonal, Personal>()
                .ForMember(destino => destino.EsActivo,
                           opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false)
            )
                
               .ForMember(destino => destino.IdServicioNavigation,
                           opt => opt.Ignore()              
           );

            #endregion personal

            #region salida

            CreateMap<Salida , VMsalida>()
                  .ForMember(dest => dest.personalDescripcion, opt => opt.MapFrom(origen =>origen.IdPersonalNavigation.Nombres)
                )
                .ForMember(dest => dest.FechaSalida, opt => opt.MapFrom(origen => origen.FechaSalida.ToString("dd/MM/yyyy"))
                );

            CreateMap<VMsalida, Salida>()
                  .ForMember(destino => destino.IdPersonalNavigation,
                           opt => opt.Ignore()
             );
            #endregion salida
        }
    }
}
