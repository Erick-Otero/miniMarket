using Microsoft.EntityFrameworkCore;
using miniMarket.Data;
using miniMarket.Models;
using miniMarket.Repositories.Interfaces;

namespace miniMarket.Repositories
{
    public class EmpleadoRepository : IEmpleadoRepository
    {
        private readonly MarketContext _dbContext;

        public EmpleadoRepository(MarketContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Empleado empleado)
        {
            await _dbContext.Empleados.AddAsync(empleado);
            await _dbContext.SaveChangesAsync();

            await _dbContext.Entry(empleado).Reference(e => e.IdTiendaNavigation).LoadAsync();
            await _dbContext.Entry(empleado).Reference(e => e.IdJefeNavigation).LoadAsync();

        }

        public async Task DeleteAsync(Empleado empleado)
        {
            _dbContext.Empleados.Remove(empleado);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Empleado>> GetAllAsync()
        {
            return await _dbContext.Empleados
                .Include(c => c.IdTiendaNavigation)
                .Include(c => c.IdJefeNavigation)
                .ToListAsync();
        }

        public async Task<Empleado?> GetByIdAsync(int id)
        {
            return await _dbContext.Empleados
                .Include(c => c.IdTiendaNavigation)
                .Include(c => c.IdJefeNavigation)
                .FirstOrDefaultAsync(c => c.IdEmpleado == id);
        }

        public async Task UpdateAsync(Empleado empleado)
        {
            _dbContext.Empleados.Update(empleado);
            await _dbContext.SaveChangesAsync();
        }
    }
}
