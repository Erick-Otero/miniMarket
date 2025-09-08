using miniMarket.Dto;
using miniMarket.Models;
using miniMarket.Repositories.Interfaces;
using miniMarket.Services.Interfaces;

namespace miniMarket.Services
{
    public class InventarioService : IInventarioService
    {
        private readonly IInventarioRepository _repository;

        public InventarioService(IInventarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<InventarioLecturaDto> AddAsync(InventarioEscrituraDto dto)
        {
            var nuevoInventario = new Inventario
            {
                IdProducto = dto.IdProducto,
                IdTienda = dto.IdTienda,
                Cantidad = dto.Cantidad,
            };
            
            await _repository.AddAsync(nuevoInventario);

            return new InventarioLecturaDto
            {
                IdProducto = nuevoInventario.IdProducto,
                NombreProducto = nuevoInventario.IdProductoNavigation.NombreProducto,
                IdTienda = nuevoInventario.IdTienda,
                NombreTienda = nuevoInventario.IdTiendaNavigation.NombreTienda,
                Cantidad = nuevoInventario.Cantidad,
                Precio = nuevoInventario.IdProductoNavigation.PrecioLista
            };
        }

        public async Task<bool> DeleteAsync(int idProducto, int idTienda)
        {
            var inventario = await _repository.GetByIdAsync(idProducto, idTienda);
            if(inventario  == null)
            {
                return false;
            }

            await _repository.DeleteAsync(inventario);
            return true;
        }

        public async Task<List<InventarioLecturaDto>> GetAllAsync()
        {
            var inventario = await _repository.GetAllAsync();

            var listaInventario = inventario.Select( c => new InventarioLecturaDto
            {
                IdProducto = c.IdProducto,
                NombreProducto = c.IdProductoNavigation.NombreProducto,
                IdTienda = c.IdTienda,
                NombreTienda = c.IdTiendaNavigation.NombreTienda,
                Cantidad = c.Cantidad,
                Precio = c.IdProductoNavigation.PrecioLista
            }).ToList();

            return listaInventario;

        }

        public async Task<InventarioLecturaDto?> GetByIdAsync(int idProducto, int idTienda)
        {
            var inventario = await _repository.GetByIdAsync(idProducto, idTienda);
            if(inventario == null)
            {
                return null;
            }

            return new InventarioLecturaDto
            {
                IdProducto = inventario.IdProducto,
                NombreProducto = inventario.IdProductoNavigation.NombreProducto,
                IdTienda = inventario.IdTienda,
                NombreTienda = inventario.IdTiendaNavigation.NombreTienda,
                Cantidad = inventario.Cantidad,
                Precio = inventario.IdProductoNavigation.PrecioLista
            };
        }

        public async Task<bool> UpdateAsync(int idProducto, int idTienda, InventarioEscrituraDto dto)
        {
            var inventario = await _repository.GetByIdAsync(idProducto, idTienda);
            if (inventario == null)
            {
                return false;
            }

            inventario.Cantidad = dto.Cantidad;

            await _repository.UpdateAsync(inventario);
            return true;
        }
    }
}
