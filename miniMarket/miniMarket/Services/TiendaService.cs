using Microsoft.EntityFrameworkCore.Metadata.Internal;
using miniMarket.Dto;
using miniMarket.Models;
using miniMarket.Repositories.Interfaces;
using miniMarket.Services.Interfaces;

namespace miniMarket.Services
{
    public class TiendaService : ITiendaService
    {
        private readonly ITiendaRepository _repository;

        public TiendaService(ITiendaRepository repository)
        {
            _repository = repository;
        }

        public async Task<TiendaLecturaDto> AddAsync(TiendaEscrituraDto dto)
        {
            var nuevo = new Tienda
            {
                NombreTienda = dto.NombreTienda,
                Telefono = dto.Telefono,
                Correo = dto.Correo,
                Calle = dto.Calle,
                Ciudad = dto.Ciudad,
                Estado = dto.Estado,
                CodigoPostal = dto.CodigoPostal
            };

            await _repository.AddAsync(nuevo);

            return new TiendaLecturaDto
            {
                IdTienda = nuevo.IdTienda,
                Telefono = nuevo.Telefono,
                Correo = nuevo.Correo,
                Calle = nuevo.Calle,
                Ciudad = nuevo.Ciudad,
                Estado = nuevo.Estado,
                CodigoPostal = nuevo.CodigoPostal
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var tienda = await _repository.GetByIdAsync(id);
            if(tienda == null)
            {
                return false;
            }

            await _repository.DeleteAsync(tienda);
            return true;
        }

        public async Task<List<TiendaLecturaDto>> GetAllAsync()
        {

            var tiendas = await _repository.GetAllAsync();

            var listaTiendas = tiendas.Select(c => new TiendaLecturaDto
            {
                IdTienda = c.IdTienda,
                NombreTienda = c.NombreTienda,
                Telefono = c.Telefono,
                Correo = c.Correo,
                Calle = c.Calle,
                Ciudad = c.Ciudad,
                Estado = c.Estado,
                CodigoPostal = c.CodigoPostal
            }).ToList();

            return listaTiendas;
        }

        public async Task<TiendaLecturaDto?> GetByIdAsync(int id)
        {
            var tienda = await _repository.GetByIdAsync(id);
            if(tienda == null)
            {
                return null;
            }

            return new TiendaLecturaDto
            {
                IdTienda = tienda.IdTienda,
                NombreTienda = tienda.NombreTienda,
                Telefono = tienda.Telefono,
                Correo = tienda.Correo,
                Calle = tienda.Calle,
                Ciudad = tienda.Ciudad,
                Estado = tienda.Estado,
                CodigoPostal = tienda.CodigoPostal
            };
        }

        public async Task<bool> UpdateAsync(int id, TiendaEscrituraDto dto)
        {
            var tienda = await _repository.GetByIdAsync(id);
            if(tienda == null)
            {
                return false;
            }

            tienda.NombreTienda = dto.NombreTienda;
            tienda.Telefono = dto.Telefono;
            tienda.Correo = dto.Correo;
            tienda.Calle = dto.Calle;
            tienda.Ciudad = dto.Ciudad;
            tienda.Estado = dto.Estado;
            tienda.CodigoPostal = dto.CodigoPostal;
            await _repository.UpdateAsync(tienda);

            return true;

        }
    }
}
