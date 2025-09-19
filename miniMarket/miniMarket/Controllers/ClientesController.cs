using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using miniMarket.Dto;
using miniMarket.Services.Interfaces;

namespace miniMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _service;

        public ClientesController(IClienteService service)
        {
            _service = service;
        }

        /// <summary>Obtiene todos los clientes disponibles en la entidad.</summary>
        /// <response code="200">La operación fue exitosa y se devuelve la lista completa de clientes.</response>
        [HttpGet]
        public async Task<ActionResult<List<ClienteLecturaDto>>> Get()
        {
            return Ok(await _service.GetAllAsync());
        }

        /// <summary>Obtiene un cliente específico por su identificador único.</summary>
        /// <param name="id">ID del cliente que se desea consultar.</param>
        /// <response code="200">Devuelve el cliente correspondiente al ID proporcionado.</response>
        /// <response code="404">No se encontró ningún cliente con el ID especificado.</response>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ClienteLecturaDto?>> GetById(int id)
        {
            var cliente = await _service.GetByIdAsync(id);
            if (cliente == null)
            {
                return NotFound(new { message = $"El cliente con ID {id} no existe." });
            }
            return Ok(cliente);
        }

        /// <summary>Crea un nuevo cliente en la entidad.</summary>
        /// <param name="dto">Objeto con los datos del cliente a crear.</param>
        /// <response code="201">El cliente se creó correctamente y se devuelve el objeto creado.</response>
        /// <response code="400">Los datos enviados no cumplen con las validaciones requeridas.</response>
        /// <response code="500">Ocurrió un error interno al intentar crear el cliente.</response>
        [HttpPost]
        public async Task<ActionResult<ClienteLecturaDto>> Post([FromBody] ClienteEscrituraDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var nuevoCliente = await _service.AddAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = nuevoCliente.IdCliente }, nuevoCliente);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Ocurrió un error al crear el cliente." });
            }
        }

        /// <summary>Actualiza un cliente existente por medio de su ID.</summary>
        /// <param name="id">ID del cliente que se desea actualizar.</param>
        /// <param name="dto">Objeto con los datos actualizados del cliente.</param>
        /// <response code="204">El cliente se actualizó correctamente.</response>
        /// <response code="400">Los datos enviados no cumplen con las validaciones requeridas.</response>
        /// <response code="404">No se encontró ningún cliente con el ID especificado.</response>
        /// <response code="500">Ocurrió un error interno al intentar actualizar el cliente.</response>
        [HttpPut("{id:int}")]
        public async Task<ActionResult<string>> Put(int id, [FromBody] ClienteEscrituraDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var cliente = await _service.GetByIdAsync(id);
                if (cliente == null)
                {
                    return NotFound(new { message = $"El cliente con ID {id} no existe." });
                }

                await _service.UpdateAsync(id, dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error al actualizar el cliente.", error = ex.Message });
            }
        }

        /// <summary>Elimina un cliente existente por medio de su ID.</summary>
        /// <param name="id">ID del cliente que se desea eliminar.</param>
        /// <response code="204">El cliente se eliminó correctamente.</response>
        /// <response code="404">No se encontró ningún cliente con el ID especificado.</response>
        /// <response code="500">Ocurrió un error interno al intentar eliminar el cliente.</response>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<string>> Delete(int id)
        {
            try
            {
                var cliente = await _service.GetByIdAsync(id);
                if (cliente == null)
                {
                    return NotFound(new { message = $"El cliente con ID {id} no existe." });
                }

                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error al eliminar el cliente.", error = ex.Message });
            }
        }
    }
}
