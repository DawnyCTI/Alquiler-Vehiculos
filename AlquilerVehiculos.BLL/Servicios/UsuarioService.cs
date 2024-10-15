using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using AlquilerVehiculos.BLL.Servicios.Contrato;
using AlquilerVehiculos.DAL.Repositorios.Contrato;
using AlquilerVehiculos.DTO;
using AlquilerVehiculos.Model;

namespace AlquilerVehiculos.BLL.Servicios
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IGenericRepository<Usuario> _usuarioRepositorio;
        private readonly IMapper _mapper;

        public UsuarioService(IGenericRepository<Usuario> usuarioRepositorio, IMapper mapper)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _mapper = mapper;
        }

        public Task<SesionDTO> Crear(UsuarioDTO modelo)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Editar(UsuarioDTO modelo)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<UsuarioDTO>> Lista()
        {
            throw new NotImplementedException();
        }

        public Task<SesionDTO> ValidarCredenciales(string correo, string clave)
        {
            throw new NotImplementedException();
        }
    }
}
