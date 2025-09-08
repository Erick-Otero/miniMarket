using Microsoft.EntityFrameworkCore;
using miniMarket.Data;
using miniMarket.Models;
using miniMarket.Repositories.Interfaces;

namespace miniMarket.Repositories
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly MarketContext _dbContext;

        public ProductoRepository(MarketContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(Producto producto)
        {
            await _dbContext.Productos.AddAsync(producto);
            await _dbContext.SaveChangesAsync();

            await _dbContext.Entry(producto).Reference(p => p.IdMarcaNavigation).LoadAsync();
            await _dbContext.Entry(producto).Reference(p => p.IdCategoriaNavigation).LoadAsync();
        }

        public async Task DeleteAsync(Producto producto)
        {
            _dbContext.Productos.Remove(producto);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Producto>> GetAllAsync()
        {
            return await _dbContext.Productos
                .Include(c => c.IdMarcaNavigation)
                .Include(c => c.IdCategoriaNavigation)
                .ToListAsync();
        }

        public async Task<Producto?> GetByIdAsync(int id)
        {
            return await _dbContext.Productos
                .Include(c => c.IdMarcaNavigation)
                .Include(c => c.IdCategoriaNavigation)
                .FirstOrDefaultAsync(c => c.IdProducto == id);
        }

        public async Task UpdateAsync(Producto producto)
        {
            _dbContext.Productos.Update(producto);
            await _dbContext.SaveChangesAsync();
        }
    }
}
