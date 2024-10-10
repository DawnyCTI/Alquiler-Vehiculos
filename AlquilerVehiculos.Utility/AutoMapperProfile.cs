﻿using AlquilerVehiculos.DTO;
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
                    )
                 .ForMember(destino =>
                                        destino.EsActivo,
                                                           opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0)
                                                                              );

            CreateMap<VehiculoDTO, Vehiculo>()
                .ForMember(destino =>
                    destino.IdCategoriaNavigation,
                    opt => opt.Ignore()
                    )
                .ForMember(destino =>
                   destino.PrecioAlquiler,
                   opt => opt.MapFrom(origen => Convert.ToDecimal(origen.PrecioAlquiler, new CultureInfo("es-DR")))
                    )
                 .ForMember(destino =>
                    destino.EsActivo,
                    opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true :false)
                      );
            #endregion Vehiculo

            #region Venta
            CreateMap<Venta, VentaDTO>()
                .ForMember(destino =>
                   destino.Total,
                   opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-DR")))
                )
                .ForMember(destino =>
                   destino.FechaRegistro,
                   opt => opt.MapFrom(origen => origen.FechaRegistro.Value.ToString("dd/MM/yyyy"))
                );
            CreateMap<VentaDTO, Venta>()
                .ForMember(destino =>
                    destino.Total,
                    opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Total, new CultureInfo("es-DR")))
                 );
            #endregion Venta

            #region DetalleVenta
            CreateMap<DetalleVenta, DetalleVentaDTO>()
                .ForMember(destino =>
                    destino.DescripcionProducto,
                    opt => opt.MapFrom(origen => origen.IdVehiculoNavigation.Nombre)
                )
                .ForMember(destino =>
                    destino.PrecioTexto,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-DR")))
                )
                .ForMember(destino =>
                    destino.TotalTexto,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-DR")))
                );
            CreateMap<DetalleVentaDTO, DetalleVenta>()
                .ForMember(destino =>
                    destino.Precio,
                    opt => opt.MapFrom(origen => Convert.ToDecimal(origen.PrecioTexto, new CultureInfo("es-DR")))
                )
                .ForMember(destino =>
                    destino.Total,
                    opt => opt.MapFrom(origen => Convert.ToDecimal(origen.TotalTexto, new CultureInfo("es-DR")))
                );
            #endregion DetalleVenta

            #region Reporte
            #endregion Reporte

        }
    }
}
