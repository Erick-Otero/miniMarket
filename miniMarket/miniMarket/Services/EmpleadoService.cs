using miniMarket.Dto;
using miniMarket.Models;
using miniMarket.Repositories.Interfaces;
using miniMarket.Services.Interfaces;

namespace miniMarket.Services
{
    public class EmpleadoService : IEmpleadoService
    {
        private readonly IEmpleadoRepository _repository;

        public EmpleadoService(IEmpleadoRepository repository)
        {
            _repository = repository;
        }

        public async Task<EmpleadoLecturaDto> AddAsync(EmpleadoEscrituraDto dto)
        {
            var nuevo = new Empleado
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Correo = dto.Correo,
                Telefono = dto.Telefono,
                Activo = dto.Activo,
                IdTienda = dto.IdTienda,
                IdJefe = dto.IdJefe,
                Contrasena = dto.Contrasena
            };

            await _repository.AddAsync(nuevo);

            return new EmpleadoLecturaDto
            {
                IdEmpleado = nuevo.IdEmpleado,
                Nombre = nuevo.Nombre,
                Apellido = nuevo.Apellido,
                Correo = nuevo.Correo,
                Telefono = nuevo.Telefono,
                Activo = nuevo.Activo,
                IdTienda = nuevo.IdTienda,
                NombreTienda = nuevo.IdTiendaNavigation.NombreTienda,
                IdJefe = nuevo.IdJefeNavigation?.IdEmpleado,
                NombreSuperior = nuevo.IdJefeNavigation != null ? $"{nuevo.IdJefeNavigation.Nombre} {nuevo.IdJefeNavigation.Apellido}" : "Sin superior."
            };   
            
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var empleado = await _repository.GetByIdAsync(id);
            if(empleado == null)
            {
                return false;
            }

            await _repository.DeleteAsync(empleado);
            return true;
        }

        public async Task<List<EmpleadoLecturaDto>> GetAllAsync()
        {
            var empleados = await _repository.GetAllAsync();
            var listaEmpleados = empleados.Select(c => new EmpleadoLecturaDto
            {
                IdEmpleado = c.IdEmpleado,
                Nombre = c.Nombre,
                Apellido = c.Apellido,
                Correo = c.Correo,
                Telefono = c.Telefono,
                Activo = c.Activo,
                IdTienda = c.IdTienda,
                NombreTienda = c.IdTiendaNavigation.NombreTienda,
                IdJefe = c.IdJefeNavigation?.IdEmpleado,
                NombreSuperior = c.IdJefeNavigation != null ? $"{c.IdJefeNavigation.Nombre} {c.IdJefeNavigation.Apellido}" : "Sin superior."
            }).ToList();

            return listaEmpleados;
        }

        public async Task<EmpleadoLecturaDto?> GetByIdAsync(int id)
        {
            var empleado = await _repository.GetByIdAsync(id);
            if( empleado == null)
            {
                return null;
            }
            return new EmpleadoLecturaDto
            {
                IdEmpleado = empleado.IdEmpleado,
                Nombre = empleado.Nombre,
                Apellido = empleado.Apellido,
                Correo = empleado.Correo,
                Telefono = empleado.Telefono,
                Activo = empleado.Activo,
                IdTienda = empleado.IdTienda,
                NombreTienda = empleado.IdTiendaNavigation.NombreTienda,
                IdJefe = empleado.IdJefeNavigation?.IdEmpleado,
                NombreSuperior = empleado.IdJefeNavigation != null ? $"{empleado.IdJefeNavigation.Nombre} {empleado.IdJefeNavigation.Apellido}" : "Sin superior."
            };            
        }

        public async Task<bool> UpdateAsync(int id, EmpleadoEscrituraDto dto)
        {
            var empleado = await _repository.GetByIdAsync(id);
            if(empleado == null)
            {
                return false;
            }

            empleado.Nombre = dto.Nombre;
            empleado.Apellido = dto.Apellido;
            empleado.Correo = dto.Correo;
            empleado.Telefono = dto.Telefono;
            empleado.Activo = dto.Activo;
            empleado.IdTienda = dto.IdTienda;
            empleado.IdJefe = dto.IdJefe;

            await _repository.UpdateAsync(empleado);
            return true;
        }
    }
}
