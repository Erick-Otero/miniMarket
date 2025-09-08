using miniMarket.Models;

namespace miniMarket.Repositories.Interfaces
{
    public interface ICaregoriaRepository
    {
        Task <List<Categoria>> GetAllAsync();

        Task<Categoria?> GetByIdAsync(int id);

        Task AddAsync(Categoria categoria);

        Task UpdateAsync(Categoria categoria);

        Task DeleteAsync(Categoria categoria);


    }
}
