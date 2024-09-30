using AlquilerVehiculos.DTO;
using AlquilerVehiculos.Model;
using AutoMapper;

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
        }
    }
}