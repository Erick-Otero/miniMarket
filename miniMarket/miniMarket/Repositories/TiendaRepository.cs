using Microsoft.EntityFrameworkCore;
using miniMarket.Data;
using miniMarket.Models;
using miniMarket.Repositories.Interfaces;

namespace miniMarket.Repositories
{
    public class TiendaRepository : ITiendaRepository
    {
        private readonly MarketContext _dbContext;

        public TiendaRepository(MarketContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Tienda tienda)
        {
            await _dbContext.Tiendas.AddAsync(tienda);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Tienda tienda)
        {
            _dbContext.Tiendas.Remove(tienda);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Tienda>> GetAllAsync()
        {
            return await _dbContext.Tiendas.ToListAsync();
        }

        public async Task<Tienda?> GetByIdAsync(int id)
        {
            return await _dbContext.Tiendas.FindAsync(id);

        }

        public async Task UpdateAsync(Tienda tienda)
        {
            _dbContext.Tiendas.Update(tienda);
            await _dbContext.SaveChangesAsync();
        }
    }
}
