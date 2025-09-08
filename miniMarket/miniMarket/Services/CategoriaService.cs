using miniMarket.Dto;
using miniMarket.Services.Interfaces;
using miniMarket.Repositories.Interfaces;
using miniMarket.Models;

namespace miniMarket.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICaregoriaRepository _repository;

        public CategoriaService(ICaregoriaRepository repository)
        {
            _repository = repository;
        }

        public async Task<CategoriaLecturaDto> AddAsync(CategoriaEscrituraDto dto)
        {
            var nuevo = new Categoria
            {
                NombreCategoria = dto.NombreCategoria
            };

            await _repository.AddAsync(nuevo);

            return new CategoriaLecturaDto
            {
                IdCategoria = nuevo.IdCategoria,
                NombreCategoria = nuevo.NombreCategoria                
            };

        }

        public async Task<bool> DeleteAsync(int id)
        {
            var categoria = await _repository.GetByIdAsync(id);
            if(categoria == null)
            {
                return false;
            }

            await _repository.DeleteAsync(categoria);
            return true;
        }

        public async Task<List<CategoriaLecturaDto>> GetAllAsync()
        {
            var categorias = await _repository.GetAllAsync();
            var listaCategorias = categorias.Select(c => new CategoriaLecturaDto
            {
                IdCategoria = c.IdCategoria,
                NombreCategoria = c.NombreCategoria
            }).ToList();
            return listaCategorias;
        }

        public async Task<CategoriaLecturaDto?> GetByIdAsync(int id)
        {
            var categoria = await _repository.GetByIdAsync(id);
            if(categoria == null)
            {
                return null;
            }

            return new CategoriaLecturaDto
            {
                IdCategoria = categoria.IdCategoria,
                NombreCategoria = categoria.NombreCategoria
            };

        }

        public async Task<bool> UpdateAsync(int id, CategoriaEscrituraDto dto)
        {
            var categoria = await _repository.GetByIdAsync(id);
            if (categoria == null)
            {
                return false;
            }

            categoria.NombreCategoria = dto.NombreCategoria;
            await _repository.UpdateAsync(categoria);

            return true;
        }
    }
}
