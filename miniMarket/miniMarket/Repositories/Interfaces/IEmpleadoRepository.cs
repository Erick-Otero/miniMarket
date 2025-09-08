using miniMarket.Data;
using miniMarket.Models;

namespace miniMarket.Repositories.Interfaces
{
    public interface IEmpleadoRepository
    {
        Task<List<Empleado>> GetAllAsync();

        Task<Empleado?> GetByIdAsync(int id);

        Task AddAsync(Empleado empleado);

        Task UpdateAsync(Empleado empleado);

        Task DeleteAsync(Empleado empleado);
    }
}
