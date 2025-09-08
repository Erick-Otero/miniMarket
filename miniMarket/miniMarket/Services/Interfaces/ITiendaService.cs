using miniMarket.Dto;

namespace miniMarket.Services.Interfaces
{
    public interface ITiendaService
    {
        Task<List<TiendaLecturaDto>> GetAllAsync();

        Task<TiendaLecturaDto?> GetByIdAsync(int id);

        Task<TiendaLecturaDto> AddAsync(TiendaEscrituraDto dto);

        Task<bool> UpdateAsync(int id, TiendaEscrituraDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
