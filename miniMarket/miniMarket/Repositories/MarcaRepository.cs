using Microsoft.EntityFrameworkCore;
using miniMarket.Data;
using miniMarket.Models;
using miniMarket.Repositories.Interfaces;

namespace miniMarket.Repositories
{
    public class MarcaRepository : IMarcaRepository
    {
        private readonly MarketContext _dbContext;

        public MarcaRepository(MarketContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(Marca marca)
        {
            await _dbContext.Marcas.AddAsync(marca);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Marca marca)
        {
            _dbContext.Marcas.Remove(marca);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Marca>> GetAllAsync()
        {
            return await _dbContext.Marcas.ToListAsync();
        }

        public async Task<Marca?> GetByIdAsync(int id)
        {
            return await _dbContext.Marcas.FindAsync(id);
        }

        public async Task UpdateAsync(Marca marca)
        {
            _dbContext.Marcas.Update(marca);
            await _dbContext.SaveChangesAsync();
        }
    }
}
