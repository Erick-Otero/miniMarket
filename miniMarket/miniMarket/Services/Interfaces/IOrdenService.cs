using miniMarket.Dto;

namespace miniMarket.Services.Interfaces
{
    public interface IOrdenService
    {
        Task<List<OrdenLecturaDto>> GetAllAsync();

        Task<OrdenLecturaDto?> GetByIdAsync(int id);

        Task<OrdenLecturaDto> AddAsync(OrdenEscrituraDto dto);

        Task<bool> UpdateAsync(int id, OrdenEscrituraDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
