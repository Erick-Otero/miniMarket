using miniMarket.Dto;

namespace miniMarket.Services.Interfaces
{
    public interface IInventarioService
    {
        Task<List<InventarioLecturaDto>> GetAllAsync();

        Task<InventarioLecturaDto?> GetByIdAsync(int idProducto, int idTienda);

        Task<InventarioLecturaDto> AddAsync(InventarioEscrituraDto dto);

        Task<bool> UpdateAsync(int idProducto, int idTienda, InventarioEscrituraDto dto);

        Task<bool> DeleteAsync(int idProducto, int idTienda);
    }
}
