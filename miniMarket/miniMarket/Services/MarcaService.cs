using miniMarket.Dto;
using miniMarket.Models;
using miniMarket.Repositories.Interfaces;
using miniMarket.Services.Interfaces;

namespace miniMarket.Services
{
    public class MarcaService : IMarcaService
    {
        private readonly IMarcaRepository _repository;

        public MarcaService(IMarcaRepository repository)
        {
            _repository = repository;
        }

        public async Task<MarcaLecturaDto> AddAsync(MarcaEscrituraDto dto)
        {
            var nuevo = new Marca
            {
                NombreMarca = dto.NombreMarca
            };

            await _repository.AddAsync(nuevo);

            return new MarcaLecturaDto
            {
                IdMarca = nuevo.IdMarca,
                NombreMarca = nuevo.NombreMarca
            };
            
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var marca = await _repository.GetByIdAsync(id);
            if(marca == null)
            {
                return false;
            }

            await _repository.DeleteAsync(marca);
            return true;
        }

        public async Task<List<MarcaLecturaDto>> GetAllAsync()
        {
            var marcas = await _repository.GetAllAsync();
            var listaMarcas = marcas.Select(c => new MarcaLecturaDto
            {
                IdMarca = c.IdMarca,
                NombreMarca = c.NombreMarca
            }).ToList();

            return listaMarcas;
        }

        public async Task<MarcaLecturaDto?> GetByIdAsync(int id)
        {
            var marca = await _repository.GetByIdAsync(id);
            if(marca == null)
            {
                return null;
            }

            return new MarcaLecturaDto
            {
                IdMarca = marca.IdMarca,
                NombreMarca = marca.NombreMarca
            };
        }

        public async Task<bool> UpdateAsync(int id, MarcaEscrituraDto dto)
        {
            var marca = await _repository.GetByIdAsync(id);
            if(marca == null)
            {
                return false;
            }

            marca.NombreMarca = dto.NombreMarca;
            await _repository.UpdateAsync(marca);

            return true;

        }
    }
}
