using miniMarket.Dto;
using miniMarket.Models;
using miniMarket.Repositories.Interfaces;
using miniMarket.Services.Interfaces;

namespace miniMarket.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repository;

        public ClienteService(IClienteRepository repository)
        {
            _repository = repository;
        }

        public async Task<ClienteLecturaDto> AddAsync(ClienteEscrituraDto dto)
        {
            var nuevo = new Cliente
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Telefono = dto.Telefono,
                Correo = dto.Correo,
                Calle = dto.Calle,
                Ciudad = dto.Ciudad,
                Estado = dto.Estado,
                CodigoPostal = dto.CodigoPostal
            };
            await _repository.AddASync(nuevo);

            return new ClienteLecturaDto
            {
                IdCliente = nuevo.IdCliente,
                Nombre = nuevo.Nombre,
                Apellido = nuevo.Apellido,
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
            var cliente = await _repository.GetByIdAsync(id);
            if(cliente == null)
            {
                return false;
            }

            await _repository.DeleteAsync(cliente);
            return true;
        }

        public async Task<List<ClienteLecturaDto>> GetAllAsync()
        {
            var clientes = await _repository.GetAllAsync();
            var listaClientes = clientes.Select(c => new ClienteLecturaDto
            {
                IdCliente = c.IdCliente,
                Nombre = c.Nombre,
                Apellido = c.Apellido,
                Telefono = c.Telefono,
                Correo = c.Correo,
                Calle = c.Calle,
                Ciudad = c.Ciudad,
                Estado = c.Estado,
                CodigoPostal = c.CodigoPostal
            }).ToList();

            return listaClientes;
        }

        public async Task<ClienteLecturaDto?> GetByIdAsync(int id)
        {
            var cliente = await _repository.GetByIdAsync(id);
            if (cliente == null)
            {
                return null;
            }
            return new ClienteLecturaDto
            {
                IdCliente = cliente.IdCliente,
                Nombre = cliente.Nombre,
                Apellido = cliente.Apellido,
                Telefono = cliente.Telefono,
                Correo = cliente.Correo,
                Calle = cliente.Calle,
                Ciudad = cliente.Ciudad,
                Estado = cliente.Estado,
                CodigoPostal = cliente.CodigoPostal
            };
        }

        public async Task<bool> UpdateAsync(int id, ClienteEscrituraDto dto)
        {
            var cliente = await _repository.GetByIdAsync(id);
            if(cliente == null)
            {
                return false;
            }

            cliente.Nombre = dto.Nombre;
            cliente.Apellido = dto.Apellido;
            cliente.Telefono = dto.Telefono;
            cliente.Correo = dto.Correo;
            cliente.Calle = dto.Calle;
            cliente.Ciudad = dto.Ciudad;
            cliente.Estado = dto.Estado;
            cliente.CodigoPostal = dto.CodigoPostal;

            await _repository.UpdateAsync(cliente);
            return true;
        }
    }
}
