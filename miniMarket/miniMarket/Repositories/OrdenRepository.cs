using Microsoft.EntityFrameworkCore;
using miniMarket.Data;
using miniMarket.Dto;
using miniMarket.Models;
using miniMarket.Repositories.Interfaces;

namespace miniMarket.Repositories
{
    public class OrdenRepository : IOrdenRepository
    {
       private readonly MarketContext _dbContext;
        public OrdenRepository(MarketContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Orden orden)
        {
            await _dbContext.Ordenes.AddAsync(orden);
            await _dbContext.SaveChangesAsync();

            await _dbContext.Entry(orden).Reference(o => o.IdClienteNavigation).LoadAsync();
            await _dbContext.Entry(orden).Reference(o => o.IdEmpleadoNavigation).LoadAsync();
            await _dbContext.Entry(orden).Reference(o => o.IdTiendaNavigation).LoadAsync();
        }

        public async Task DeleteAsync(Orden orden)
        {
            _dbContext.Ordenes.Remove(orden);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Orden>> GetAllAsync()
        {
            return await _dbContext.Ordenes
                .Include(o => o.IdClienteNavigation)
                .Include(O => O.IdEmpleadoNavigation)
                .ToListAsync();
        }

        public async Task<Orden?> GetByIdAsync(int id)
        {
            return await _dbContext.Ordenes
                .Include(o => o.IdClienteNavigation)
                .Include(o => o.IdEmpleadoNavigation)
                .FirstOrDefaultAsync(o => o.IdOrden == id);
        }

        public async Task UpdateAsync(Orden orden)
        {
            _dbContext.Ordenes.Update(orden);
            await _dbContext.SaveChangesAsync();
        }
    }
}
