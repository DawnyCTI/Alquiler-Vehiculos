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
using System.Globalization;

namespace AlquilerVehiculos.BLL.Servicios;

public class DashBoardService : IDashBoardService
{
    private readonly IVentaRepository _ventaRepositorio;
    private readonly IGenericRepository<Vehiculo> _vehiculoRepositorio;
    private readonly IMapper _mapper;

    public DashBoardService(IVentaRepository ventaRepositorio, IGenericRepository<Vehiculo> vehiculoRepositorio, IMapper mapper)
    {
        _ventaRepositorio = ventaRepositorio ?? throw new ArgumentNullException(nameof(ventaRepositorio));
        _vehiculoRepositorio = vehiculoRepositorio ?? throw new ArgumentNullException(nameof(vehiculoRepositorio));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    private IQueryable<Venta> retornarVentas(IQueryable<Venta> tablaVenta, int restarCantidadDias)
    {
        DateTime? ultimaFecha = tablaVenta.OrderByDescending(v => v.FechaRegistro).Select(v => v.FechaRegistro).FirstOrDefault();

        if (ultimaFecha.HasValue)
        {
            ultimaFecha = ultimaFecha.Value.AddDays(restarCantidadDias);
            return tablaVenta.Where(v => v.FechaRegistro.HasValue && v.FechaRegistro.Value.Date >= ultimaFecha.Value.Date);
        }

        return Enumerable.Empty<Venta>().AsQueryable();
    }

    private async Task<int> TotalVentaUltimaSemana()
    {
        int total = 0;
        IQueryable<Venta> _ventaQuery = await _ventaRepositorio.Consultar();

        if (_ventaQuery.Any())
        {
            var tablaVenta = retornarVentas(_ventaQuery, -7);
            total = tablaVenta.Count();
        }

        return total;
    }

    private async Task<string> TotalIngresosUltimaSemana()
    {
        decimal resultado = 0;
        IQueryable<Venta> _ventaQuery = await _ventaRepositorio.Consultar();

        if (_ventaQuery.Any())
        {
            var tablaVenta = retornarVentas(_ventaQuery, -7);
            resultado = tablaVenta.Select(v => v.Total).Sum(v => v ?? 0);
        }

        return Convert.ToString(resultado, new CultureInfo("es-DR"));
    }

    private async Task<int> TotalVehiculos()
    {
        IQueryable<Vehiculo> _vehiculoQuery = await _vehiculoRepositorio.Consultar();
        
        int total = _vehiculoQuery.Count();
        return total;
    }

    private async Task<Dictionary<string, int>> VentaUltimaSemana()
    {
        Dictionary<string, int> resultado = new Dictionary<string, int>();

        IQueryable<Venta> _ventaQuery = await _ventaRepositorio.Consultar();

        if (_ventaQuery.Any())
        {
            var tablaVenta = retornarVentas(_ventaQuery, -7);
            resultado = tablaVenta.GroupBy(v => v.FechaRegistro.Value.Date).OrderBy(g => g.Key)
                .Select(dv => new { Fecha = dv.Key.ToString("dd/MM/yyyy"), total = dv.Count() }).ToDictionary
                (keySelector: r => r.Fecha, elementSelector: r => r.total);
        }

        return resultado;
    }

    public async Task<List<DashBoardDTO>> Resumen()
    {
        DashBoardDTO vwDashBoard = new DashBoardDTO();

        try
        {
            vwDashBoard.TotalVentas = await TotalVentaUltimaSemana();
            vwDashBoard.TotalIngresos = await TotalIngresosUltimaSemana();
            vwDashBoard.TotalVehiculos = await TotalVehiculos();

            List<VentaSemanaDTO> listaVentaSemana = new List<VentaSemanaDTO>();

            foreach (KeyValuePair<string, int> item in await VentaUltimaSemana())
            {
                listaVentaSemana.Add(new VentaSemanaDTO
                {
                    Fecha = item.Key,
                    Total = item.Value
                });
            }

            vwDashBoard.VentasUltimaSemana = listaVentaSemana;
        }
        catch (Exception)
        {
            throw;
        }

        return new List<DashBoardDTO> { vwDashBoard };
    }
}
