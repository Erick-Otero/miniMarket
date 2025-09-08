using miniMarket.Models;

namespace miniMarket.Repositories.Interfaces
{
    public interface IClienteRepository
    {

        Task<List<Cliente>> GetAllAsync();

        Task<Cliente?> GetByIdAsync(int id);

        Task AddASync(Cliente cliente);

        Task UpdateAsync(Cliente cliente);

        Task DeleteAsync(Cliente cliente);
    }
}
