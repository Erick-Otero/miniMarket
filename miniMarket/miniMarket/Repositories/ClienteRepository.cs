using Microsoft.EntityFrameworkCore;
using miniMarket.Data;
using miniMarket.Models;
using miniMarket.Repositories.Interfaces;

namespace miniMarket.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly MarketContext _dbContext;

        public ClienteRepository(MarketContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddASync(Cliente cliente)
        {
            await _dbContext.Clientes.AddAsync(cliente);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Cliente cliente)
        {
            _dbContext.Clientes.Remove(cliente);
            await _dbContext.SaveChangesAsync();
        }

        public Task<List<Cliente>> GetAllAsync()
        {
            return _dbContext.Clientes.ToListAsync();
        }

        public async Task<Cliente?> GetByIdAsync(int id)
        {
            return await _dbContext.Clientes.FindAsync(id);
        }

        public async Task UpdateAsync(Cliente cliente)
        {
            _dbContext.Clientes.Update(cliente);
            await _dbContext.SaveChangesAsync();
        }
    }
}
