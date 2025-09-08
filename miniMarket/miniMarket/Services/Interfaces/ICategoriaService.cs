using miniMarket.Dto;

namespace miniMarket.Services.Interfaces
{
    public interface ICategoriaService
    {
        Task<List<CategoriaLecturaDto>> GetAllAsync();

        Task<CategoriaLecturaDto?> GetByIdAsync(int id);

        Task<CategoriaLecturaDto> AddAsync(CategoriaEscrituraDto dto);

        Task<bool> UpdateAsync(int id, CategoriaEscrituraDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
