using miniMarket.Dto;

namespace miniMarket.Services.Interfaces
{
    public interface IEmpleadoService
    {
        Task<List<EmpleadoLecturaDto>> GetAllAsync();

        Task<EmpleadoLecturaDto?> GetByIdAsync(int id);

        Task<EmpleadoLecturaDto> AddAsync(EmpleadoEscrituraDto dto);

        Task<bool> UpdateAsync(int id, EmpleadoEscrituraDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
