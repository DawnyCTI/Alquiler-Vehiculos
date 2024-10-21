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
using Microsoft.EntityFrameworkCore;

namespace AlquilerVehiculos.BLL.Servicios
{
    public class VehiculoService : IVehiculoService
    {
        private readonly IGenericRepository<Vehiculo> _vehiculoRepositorio;
        private readonly IMapper _mapper;

        public VehiculoService(IGenericRepository<Vehiculo> vehiculoRepositorio, IMapper mapper)
        {
            _vehiculoRepositorio = vehiculoRepositorio;
            _mapper = mapper;
        }

        public async Task<List<VehiculoDTO>> Lista()
        {
            try
            {
                var queryVehiculo = await _vehiculoRepositorio.Consultar();

                var listaVehiculos = queryVehiculo.Include(cat => cat.IdCategoriaNavigation).ToList();

                return _mapper.Map<List<VehiculoDTO>>(listaVehiculos);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<VehiculoDTO> Crear(VehiculoDTO modelo)
        {
            try
            {
                var vehiculoCreado = await _vehiculoRepositorio.Crear(_mapper.Map<Vehiculo>(modelo));

                if (vehiculoCreado.IdVehiculo == 0)
                    throw new TaskCanceledException("No se pudo crear el vehiculo");

                return _mapper.Map<VehiculoDTO>(vehiculoCreado);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<VehiculoDTO> Editar(VehiculoDTO modelo)
        {
            try
            {
                var vehiculoModelo = _mapper.Map<Vehiculo>(modelo);

                var vehiculoEncontrado = await _vehiculoRepositorio.Obtener(v =>
                   v.IdVehiculo == vehiculoModelo.IdVehiculo
                );

                if (vehiculoEncontrado == null)
                    throw new TaskCanceledException("El Vehiculo no existe");

                vehiculoEncontrado.Nombre = vehiculoModelo.Nombre;
                vehiculoEncontrado.IdCategoria = vehiculoModelo.IdCategoria;
                vehiculoEncontrado.Stock = vehiculoModelo.Stock;
                vehiculoEncontrado.PrecioAlquiler = vehiculoModelo.PrecioAlquiler;
                vehiculoEncontrado.EsActivo = vehiculoModelo.EsActivo;

                bool respuesta = await _vehiculoRepositorio.Editar(vehiculoEncontrado);

                if (!respuesta)
                    throw new TaskCanceledException("No se pudo editar el vehiculo");

                return _mapper.Map<VehiculoDTO>(vehiculoEncontrado);

            }

            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<VehiculoDTO> Eliminar(int id)
        {
            try
            {
                var vehiculoEncontrado = await _vehiculoRepositorio.Obtener(v => v.IdVehiculo == id);

                if (vehiculoEncontrado == null)
                    throw new TaskCanceledException("El Vehiculo no existe");

                bool respuesta = await _vehiculoRepositorio.Eliminar(vehiculoEncontrado);

                if (!respuesta)
                    throw new TaskCanceledException("No se pudo eliminar el vehiculo");

                return _mapper.Map<VehiculoDTO>(vehiculoEncontrado);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        
    }
}
