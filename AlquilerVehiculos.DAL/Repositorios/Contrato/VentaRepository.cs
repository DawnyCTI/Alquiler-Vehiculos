using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AlquilerVehiculos.DAL.DBContext;
using AlquilerVehiculos.DAL.Repositorios.Contrato;
using AlquilerVehiculos.Model;

namespace AlquilerVehiculos.DAL.Repositorios
{
    public class VentaRepository : GenericRepository<Venta> , IVentaRepository
    {
        private readonly DbalquilerVehiculosContext _dbcontext;

        public VentaRepository(DbalquilerVehiculosContext dbcontext) : base(dbcontext) 
        {
           _dbcontext = dbcontext;
        }

        public async Task<Venta> Registrar(Venta modelo)
        {
            Venta VentaGenerada = new Venta();

            using (var trasaction = _dbcontext.Database.BeginTransaction())
            {
                try
                {
                    foreach (DetalleVenta dv in modelo.DetalleVenta)
                    {
                        Vehiculo vehiculo_encontrado = _dbcontext.Vehiculos.Where(p => p.IdVehiculo == dv.IdVehiculo).First();

                        vehiculo_encontrado.Stock = vehiculo_encontrado.Stock - dv.Cantidad;
                        _dbcontext.Vehiculos.Update(vehiculo_encontrado);
                    }
                    await _dbcontext.SaveChangesAsync();

                    NumeroDocumento correlativo = _dbcontext.NumeroDocumentos.First();

                    correlativo.UltimoNumero = correlativo.UltimoNumero + 1;
                    correlativo.FechaRegistro = DateTime.Now;

                    _dbcontext.NumeroDocumentos.Update(correlativo);
                    await _dbcontext.SaveChangesAsync();

                    int CantidadDigitos = 4;
                    string ceros = string.Concat(Enumerable.Repeat("0", CantidadDigitos));
                    string numeroVenta = ceros + correlativo.UltimoNumero.ToString();

                    numeroVenta = numeroVenta.Substring(numeroVenta.Length - CantidadDigitos, CantidadDigitos);

                    modelo.NumeroDocumento = numeroVenta;

                    await _dbcontext.AddAsync(modelo);
                    await _dbcontext.SaveChangesAsync();

                    VentaGenerada = modelo;

                    trasaction.Commit();

                } catch {
                    trasaction.Rollback();
                    throw;
                }
                return VentaGenerada;
            }
        }
    }
}
