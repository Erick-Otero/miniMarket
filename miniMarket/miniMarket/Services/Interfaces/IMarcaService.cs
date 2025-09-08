using miniMarket.Dto;

namespace miniMarket.Services.Interfaces
{
    public interface IMarcaService
    {
        Task<List<MarcaLecturaDto>> GetAllAsync();

        Task<MarcaLecturaDto?> GetByIdAsync(int id);

        Task<MarcaLecturaDto> AddAsync(MarcaEscrituraDto dto);

        Task<bool> UpdateAsync(int id, MarcaEscrituraDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
