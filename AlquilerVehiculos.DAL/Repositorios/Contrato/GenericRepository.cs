using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AlquilerVehiculos.DAL.Repositorios.Contrato;
using AlquilerVehiculos.DAL.DBContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace AlquilerVehiculos.DAL.Repositorios.Contrato
{
    public class GenericRepository<TModelo> : IGenericRepository<TModelo> where TModelo : class
    {
        private readonly DbalquilerVehiculosContext dbcontext;

        public GenericRepository(DbalquilerVehiculosContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public async Task<TModelo> Obtener(Expression<Func<TModelo, bool>> filtro)
        {
            try
            {
                TModelo modelo = await _dbcontext.Set<TModelo>().FirstOrDefaultAsync(filtro);
                return modelo;
            }
            catch
            {
                throw;
            }
        }

        public Task<TModelo> Crear(TModelo modelo)
        {
            try
            {

            }
            catch
            {
                throw;
            }
        }

        public Task<bool> Editar(TModelo modelo)
        {
            try
            {

            }
            catch
            {
                throw;
            }
        }

        public Task<bool> Eliminar(TModelo modelo)
        {
            try
            {

            }
            catch
            {
                throw;
            }
        }
        public Task<IQueryable<TModelo>> Consultar(Expression<Func<TModelo, bool>> filtro = null)
        {
            try
            {

            }
            catch
            {
                throw;
            }
        }


    }
}
