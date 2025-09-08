using Microsoft.EntityFrameworkCore;
using miniMarket.Data;
using miniMarket.Models;
using miniMarket.Repositories.Interfaces;

namespace miniMarket.Repositories
{
    public class InventarioRepository : IInventarioRepository
    {
        private readonly MarketContext _dbContext;

        public InventarioRepository(MarketContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Inventario inventario)
        {
            await _dbContext.Inventarios.AddAsync(inventario);
            await _dbContext.SaveChangesAsync();

            await _dbContext.Entry(inventario).Reference(c => c.IdProductoNavigation).LoadAsync();
            await _dbContext.Entry(inventario).Reference(c => c.IdTiendaNavigation).LoadAsync();
        }

        public async Task DeleteAsync(Inventario inventario)
        {
            _dbContext.Inventarios.Remove(inventario);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Inventario>> GetAllAsync()
        {
            return await _dbContext.Inventarios
                .Include(c => c.IdProductoNavigation)
                .Include(c => c.IdTiendaNavigation)
                .ToListAsync();
        }

        public async Task<Inventario?> GetByIdAsync(int idProducto, int idTienda)
        {
            return await _dbContext.Inventarios
                .Include(c => c.IdProductoNavigation)
                .Include(c => c.IdTiendaNavigation)
                .FirstOrDefaultAsync(c => c.IdProducto == idProducto && c.IdTienda == idTienda);
        }

        public async Task UpdateAsync(Inventario inventario)
        {
            _dbContext.Inventarios.Update(inventario);
            await _dbContext.SaveChangesAsync();
        }
    }
}
