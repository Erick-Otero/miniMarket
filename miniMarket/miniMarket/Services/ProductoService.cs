using miniMarket.Dto;
using miniMarket.Models;
using miniMarket.Repositories.Interfaces;
using miniMarket.Services.Interfaces;

namespace miniMarket.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _repository;
        public ProductoService(IProductoRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProductoLecturaDto> AddAsync(ProductoEscrituraDto dto)
        {
            var nuevo = new Producto
            {
                NombreProducto = dto.NombreProducto,
                IdMarca = dto.IdMarca,
                IdCategoria = dto.IdCategoria,
                AnioModelo = dto.AnioModelo,
                PrecioLista = dto.PrecioLista
            };

            await _repository.AddAsync(nuevo);

            return new ProductoLecturaDto
            {
                IdProducto = nuevo.IdProducto,
                NombreProducto = nuevo.NombreProducto,
                IdMarca = nuevo.IdMarca,
                NombreMarca = nuevo.IdMarcaNavigation.NombreMarca,
                IdCategoria = nuevo.IdCategoria,
                NombreCategoria = nuevo.IdCategoriaNavigation.NombreCategoria,
                AnioModelo = nuevo.AnioModelo,
                PrecioLista = nuevo.PrecioLista
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var producto = await _repository.GetByIdAsync(id);
            if(producto == null)
            {
                return false;
            }

            await _repository.DeleteAsync(producto);
            return true;

        }

        public async Task<List<ProductoLecturaDto>> GetAllAsync()
        {
            var productos = await _repository.GetAllAsync();
            var listaProductos = productos.Select(c => new ProductoLecturaDto
            {
                IdProducto = c.IdProducto,
                NombreProducto = c.NombreProducto,
                IdMarca = c.IdMarca,
                NombreMarca = c.IdMarcaNavigation.NombreMarca,
                IdCategoria = c.IdCategoria,
                NombreCategoria = c.IdCategoriaNavigation.NombreCategoria,
                AnioModelo = c.AnioModelo,
                PrecioLista = c.PrecioLista

            }).ToList();

            return listaProductos;

        }

        public async Task<ProductoLecturaDto?> GetByIdAsync(int id)
        {
            var producto = await _repository.GetByIdAsync(id);
            if(producto == null)
            {
                return null;
            }
            return new ProductoLecturaDto
            {
                IdProducto = producto.IdProducto,
                NombreProducto = producto.NombreProducto,
                IdMarca = producto.IdMarca,
                NombreMarca = producto.IdMarcaNavigation.NombreMarca,
                IdCategoria = producto.IdCategoria,
                NombreCategoria = producto.IdCategoriaNavigation.NombreCategoria,
                AnioModelo = producto.AnioModelo,
                PrecioLista = producto.PrecioLista
            };
        }

        public async Task<bool> UpdateAsync(int id, ProductoEscrituraDto dto)
        {
            var producto = await _repository.GetByIdAsync(id);
            if (producto == null)
            {
                return false;
            }

            producto.NombreProducto = dto.NombreProducto;
            producto.IdMarca = dto.IdMarca;
            producto.IdCategoria = dto.IdCategoria;
            producto.AnioModelo = dto.AnioModelo;
            producto.PrecioLista = dto.PrecioLista;
            await _repository.UpdateAsync(producto);

            return true;
        }
    }
}
