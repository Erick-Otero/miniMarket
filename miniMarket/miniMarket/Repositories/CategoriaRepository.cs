using Microsoft.EntityFrameworkCore;
using miniMarket.Data;
using miniMarket.Models;
using miniMarket.Repositories.Interfaces;

namespace miniMarket.Repositories
{
    public class CategoriaRepository : ICaregoriaRepository
    {
        private readonly MarketContext _dbContext;

        public CategoriaRepository(MarketContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Categoria categoria)
        {
            await _dbContext.Categorias.AddAsync(categoria);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Categoria categoria)
        {
            _dbContext.Categorias.Remove(categoria);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Categoria>> GetAllAsync()
        {
            return await _dbContext.Categorias.ToListAsync();
        }

        public async Task<Categoria?> GetByIdAsync(int id)
        {
            return await _dbContext.Categorias.FindAsync(id);
        }

        public async Task UpdateAsync(Categoria categoria)
        {
            _dbContext.Categorias.Update(categoria);
            await _dbContext.SaveChangesAsync();
        }
    }
}
