using miniMarket.Dto;

namespace miniMarket.Services.Interfaces
{
    public interface IProductoService
    {
        Task<List<ProductoLecturaDto>> GetAllAsync();

        Task<ProductoLecturaDto?> GetByIdAsync(int id);

        Task<ProductoLecturaDto> AddAsync(ProductoEscrituraDto dto);

        Task<bool> UpdateAsync(int id, ProductoEscrituraDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
