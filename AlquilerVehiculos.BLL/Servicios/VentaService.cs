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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace AlquilerVehiculos.BLL.Servicios
{
    public class VentaService: IVentaService
    {
        private readonly IVentaRepository _ventaRepositorio;
        private readonly IGenericRepository<DetalleVenta> _detalleVentaRepositorio;
        private readonly IMapper _mapper;

        public VentaService(IVentaRepository ventaRepositorio, IGenericRepository<DetalleVenta> detalleVentaRepositorio, IMapper mapper)
        {
            _ventaRepositorio = ventaRepositorio;
            _detalleVentaRepositorio = detalleVentaRepositorio;
            _mapper = mapper;
        }

        public async Task<VentaDTO> Registrar(VentaDTO modelo)
        {
            try
            {
                var ventaGenerada = await _ventaRepositorio.Registrar(_mapper.Map<Venta>(modelo));

                if (ventaGenerada.IdVenta == 0)
                    throw new Exception("No se pudo registrar la venta");

                return _mapper.Map<VentaDTO>(ventaGenerada);

            }
            catch
            {
                throw;
            }
        }

        public async Task<List<VentaDTO>> Historial(string buscarPor, string numeroVenta, string fechaInicio, string fechaFin)
        {
            IQueryable<Venta> ventas = await _ventaRepositorio.Consultar();
            var ListaResultado = new List<VentaDTO>();

            try
            {
                if (buscarPor == "fecha")
                {
                    DateTime fech_Inicio = DateTime.Parse(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-DR"));
                    DateTime fech_Fin = DateTime.Parse(fechaFin, "dd/MM/yyyy", new CultureInfo("es-DR"));

                    ListaResultado = await query.Where(v =>
                    v.FechaRegistro.Value.Date >= fech_Inicio.Date &&
                    v.FechaRegistro.Value.Date <= fech_Fin.Date
                    ).Include(dv => dv.DetalleVenta)
                    .ThenInclude(p => p.IdVehiculoNavigation)
                    .ToListAsync();
                }
                else
                {
                    ListaResultado = await query.Where(v =>
                    v.NumeroDocumento == numeroVenta
                    ).Include(dv => dv.DetalleVenta)
                    .ThenInclude(p => p.IdVehiculoNavigation)
                    .ToListAsync();
                }
            }
            catch
            {
                throw;
            }

            return _mapper.Map<List<VentaDTO>>(ListaResultado);
        }

        public Task<List<ReporteDTO>> Reporte(string fehcaInicio, string FechaFin)
        {
            throw new NotImplementedException();
        }
    }

}
