using miniMarket.Dto;
using miniMarket.Models;
using miniMarket.Repositories.Interfaces;
using miniMarket.Services.Interfaces;

namespace miniMarket.Services
{
    public class OrdenService : IOrdenService
    {
        private readonly IOrdenRepository _repository;

        public OrdenService(IOrdenRepository repository)
        {
            _repository = repository;
        }

        public async Task<OrdenLecturaDto> AddAsync(OrdenEscrituraDto dto)
        {
            var orden = new Orden
            {
                IdCliente = dto.IdCliente,
                EstadoOrden = dto.EstadoOrden,
                FechaOrden = DateOnly.FromDateTime(DateTime.UtcNow),
                FechaRequerida = dto.FechaRequerida,
                FechaEnvio = dto.FechaEnvio,
                IdTienda = dto.IdTienda,
                IdEmpleado = dto.IdEmpleado
            };

            await _repository.AddAsync(orden);

            return new OrdenLecturaDto
            {
                IdOrden = orden.IdOrden,
                IdCliente = orden.IdCliente,
                IdTienda = orden.IdTienda,
                NombreCliente = orden.IdClienteNavigation.Nombre,
                EstadoOrden = orden.EstadoOrden,
                FechaOrden = orden.FechaOrden,
                FechaRequerida = orden.FechaRequerida,
                FechaEnvio = orden.FechaEnvio,
                IdEmpleado = orden.IdEmpleado,
                NombreEmpleado = orden.IdEmpleadoNavigation.Nombre
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var orden = await _repository.GetByIdAsync(id);
            if(orden == null)
            {
                return false;
            }
            await _repository.DeleteAsync(orden);
            return true;
        }

        public async Task<List<OrdenLecturaDto>> GetAllAsync()
        {
           var ordenes = await _repository.GetAllAsync();
           return ordenes.Select(o => new OrdenLecturaDto
           {
               IdOrden = o.IdOrden,
               IdCliente = o.IdCliente,
               IdTienda = o.IdTienda,
               NombreCliente = o.IdClienteNavigation.Nombre,
               EstadoOrden = o.EstadoOrden,
               FechaOrden = o.FechaOrden,
               FechaRequerida = o.FechaRequerida,
               FechaEnvio = o.FechaEnvio,
               IdEmpleado = o.IdEmpleado,
               NombreEmpleado = o.IdEmpleadoNavigation.Nombre
           }).ToList();
        }

        public async Task<OrdenLecturaDto?> GetByIdAsync(int id)
        {
            var orden  = await _repository.GetByIdAsync(id);
            if(orden == null)
            {
                return null;
            }
            return new OrdenLecturaDto
            {
                IdOrden = orden.IdOrden,
                IdCliente = orden.IdCliente,
                IdTienda = orden.IdTienda,
                NombreCliente = orden.IdClienteNavigation.Nombre,
                EstadoOrden = orden.EstadoOrden,
                FechaOrden = orden.FechaOrden,
                FechaRequerida = orden.FechaRequerida,
                FechaEnvio = orden.FechaEnvio,
                IdEmpleado = orden.IdEmpleado,
                NombreEmpleado = orden.IdEmpleadoNavigation.Nombre
            };
        }

        public async Task<bool> UpdateAsync(int id, OrdenEscrituraDto dto)
        {
            var orden = await _repository.GetByIdAsync(id);
            if(orden == null)
            {
                return false;
            }
            orden.EstadoOrden = dto.EstadoOrden;
            orden.FechaEnvio = dto.FechaEnvio;
            await _repository.UpdateAsync(orden);
            return true;
        }
    }
}
