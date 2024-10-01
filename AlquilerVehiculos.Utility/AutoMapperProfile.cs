using AlquilerVehiculos.DTO;
using AlquilerVehiculos.Model;
using AutoMapper;
using System.Globalization;

namespace AlquilerVehiculos.Utility
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Rol
            CreateMap<Rol, RolDTO>().ReverseMap();
            #endregion Rol

            #region Menu
            CreateMap<Menu, MenuDTO>().ReverseMap();
            #endregion Menu

            #region Usuario
            CreateMap<Usuario, UsuarioDTO>()
              .ForMember(destino =>
                destino.RolDescripcion,
                opt => opt.MapFrom(origen => origen.IdRolNavigation.Nombre)
                )
              .ForMember(destino =>
                 destino.EsActivo,
                 opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0)
                 );

            CreateMap<Usuario, UsuarioDTO>()
                .ForMember(destino =>
                destino.RolDescripcion,
                opt => opt.MapFrom(origen => origen.IdRolNavigation.Nombre)
                );

            CreateMap<UsuarioDTO, Usuario>()
                .ForMember(destino =>
                   destino.IdRolNavigation,
                   opt => opt.Ignore()
                )
                .ForMember(destino =>
                destino.EsActivo,
                 opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0)
                 );
            #endregion Usuario

            #region Categoria
            CreateMap<Categoria, CategoriaDTO>().ReverseMap();
            #endregion Categoria

            #region Vehiculo
            CreateMap<Vehiculo, VehiculoDTO>()
                .ForMember(destino =>
                    destino.DescripcionCategoria,
                    opt => opt.MapFrom(origen => origen.IdCategoriaNavigation.Nombre)
                    )
                .ForMember(destino =>
                   destino.PrecioAlquiler,
                   opt => opt.MapFrom(origen => Convert.ToString(origen.PrecioAlquiler.Value, new CultureInfo("es-DR")))
                    );
            #endregion Vehiculo
        }
    }
}