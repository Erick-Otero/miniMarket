using miniMarket.Dto;
using miniMarket.Models;

namespace miniMarket.Repositories.Interfaces
{
    public interface IOrdenRepository
    {
        Task<List<Orden>> GetAllAsync();

        Task<Orden?> GetByIdAsync(int id);

        Task AddAsync(Orden orden);

        Task UpdateAsync(Orden orden);

        Task DeleteAsync(Orden orden);
    }
}
