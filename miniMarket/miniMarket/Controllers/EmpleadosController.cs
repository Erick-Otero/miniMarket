using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using miniMarket.Dto;
using miniMarket.Services.Interfaces;

namespace miniMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        private readonly IEmpleadoService _service;

        public EmpleadosController(IEmpleadoService service)
        {
            _service = service;
        }

        /// <summary>Obtiene todos los empleados disponibles en la entidad.</summary>
        /// <remarks>Devuelve la lista completa de empleados registrados en el sistema.</remarks>
        /// <response code="200">La operación fue exitosa y se devuelve la lista de empleados.</response>
        [HttpGet]
        public async Task<ActionResult<List<EmpleadoLecturaDto>>> Get()
        {
            return Ok(await _service.GetAllAsync());
        }

        /// <summary>Obtiene un empleado específico por su identificador único.</summary>
        /// <param name="id">ID único del empleado que se desea consultar.</param>
        /// <response code="200">Devuelve el empleado correspondiente al ID proporcionado.</response>
        /// <response code="404">No se encontró ningún empleado con el ID especificado.</response>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<EmpleadoLecturaDto>> GetById(int id)
        {
            var empleado = await _service.GetByIdAsync(id);
            if (empleado == null)
            {
                return NotFound(new { message = $"El empleado con ID {id} no existe." });
            }
            return Ok(empleado);
        }

        /// <summary>Crea un nuevo empleado en la entidad.</summary>
        /// <param name="dto">Objeto con los datos requeridos para crear el empleado.</param>
        /// <response code="201">El empleado se creó correctamente y se devuelve el objeto creado.</response>
        /// <response code="400">Los datos enviados no cumplen con las validaciones requeridas.</response>
        /// <response code="500">Ocurrió un error interno al intentar crear el empleado.</response>
        [HttpPost]
        public async Task<ActionResult<EmpleadoLecturaDto>> Post([FromBody] EmpleadoEscrituraDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var nuevoEmpleado = await _service.AddAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = nuevoEmpleado.IdEmpleado }, nuevoEmpleado);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Ocurrió un error al crear el empleado." });
            }
        }

        /// <summary>Actualiza un empleado existente por medio de su ID.</summary>
        /// <param name="id">ID del empleado que se desea actualizar.</param>
        /// <param name="dto">Objeto con los datos actualizados del empleado.</param>
        /// <response code="204">El empleado se actualizó correctamente.</response>
        /// <response code="400">Los datos enviados no cumplen con las validaciones requeridas.</response>
        /// <response code="404">No se encontró ningún empleado con el ID especificado.</response>
        /// <response code="500">Ocurrió un error interno al intentar actualizar el empleado.</response>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] EmpleadoEscrituraDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var empleado = await _service.UpdateAsync(id, dto);
                if (!empleado)
                {
                    return NotFound(new { message = $"El empleado con ID {id} no existe." });
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error al actualizar el empleado.", error = ex.Message });
            }
        }

        /// <summary>Elimina un empleado existente por medio de su ID.</summary>        
        /// <param name="id">ID del empleado que se desea eliminar.</param>
        /// <response code="204">El empleado se eliminó correctamente.</response>
        /// <response code="404">No se encontró ningún empleado con el ID especificado.</response>
        /// <response code="500">Ocurrió un error interno al intentar eliminar el empleado.</response>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var empleado = await _service.DeleteAsync(id);
                if (!empleado)
                {
                    return NotFound(new { message = $"El empleado con ID {id} no existe." });
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error al eliminar el empleado.", error = ex.Message });
            }
        }
    }
}
