using miniMarket.Dto;

namespace miniMarket.Services.Interfaces
{
    public interface IClienteService
    {
        Task<List<ClienteLecturaDto>> GetAllAsync();

        Task<ClienteLecturaDto?> GetByIdAsync(int id);

        Task<ClienteLecturaDto> AddAsync(ClienteEscrituraDto dto);

        Task<bool> UpdateAsync(int id, ClienteEscrituraDto dto);

        Task<bool> DeleteAsync(int id);

    }
}
