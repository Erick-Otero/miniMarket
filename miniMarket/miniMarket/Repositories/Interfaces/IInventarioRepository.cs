using miniMarket.Data;
using miniMarket.Models;

namespace miniMarket.Repositories.Interfaces
{
    public interface IInventarioRepository
    {
        Task<List<Inventario>> GetAllAsync();
        
        Task<Inventario?> GetByIdAsync(int idProducto, int idTienda);

        Task AddAsync(Inventario inventario);

        Task UpdateAsync(Inventario inventario);

        Task DeleteAsync(Inventario inventario);
    }
}
