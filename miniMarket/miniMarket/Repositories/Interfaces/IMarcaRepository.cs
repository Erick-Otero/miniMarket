using miniMarket.Models;

namespace miniMarket.Repositories.Interfaces
{
    public interface IMarcaRepository
    {
        Task<List<Marca>> GetAllAsync();

        Task<Marca?> GetByIdAsync(int id);

        Task AddAsync(Marca marca);

        Task UpdateAsync(Marca marca);

        Task DeleteAsync(Marca marca);
    }
}
