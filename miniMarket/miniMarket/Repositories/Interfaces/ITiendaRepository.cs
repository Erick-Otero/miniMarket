using miniMarket.Models;

namespace miniMarket.Repositories.Interfaces
{
    public interface ITiendaRepository
    {
        Task<List<Tienda>> GetAllAsync();

        Task<Tienda?> GetByIdAsync(int id);

        Task AddAsync(Tienda tienda);

        Task UpdateAsync(Tienda tienda);

        Task DeleteAsync(Tienda tienda);
    }
}
