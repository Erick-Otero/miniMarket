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

        /// <response code="200">La operación fue exitosa y se devuelve la lista completa de empleados disponibles.</response>
        [HttpGet]
        public async Task<ActionResult<List<EmpleadoLecturaDto>>> Get()
        {
            return Ok(await _service.GetAllAsync());
        }

        /// <param name="id">ID del empleado que se desea consultar.</param>
        /// <response code="200">Devuelve un empleado según el parámetro 'ID'.</response>
        /// <response code="404">Si no existe un empleado.</response>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<EmpleadoLecturaDto>> GetById(int id)
        {
            var empleado = await _service.GetByIdAsync(id);
            if(empleado == null)
            {
                return NotFound(new { message = $"El empleado con ID {id} no existe." });
            }
            return Ok(empleado);
        }

        /// <param name="dto">Objeto con los datos del empleado a crear.</param>
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

        /// <param name="id">ID del empleado que se desea actualizar.</param>
        /// <param name="dto">Objeto con los datos actualizados del empleado.</param>
        /// <response code="204">El empleado se actualizó correctamente.</response>
        /// <response code="400">Los datos enviados no cumplen con las validaciones requeridas.</response>
        /// <response code="404">No se encontró ningún empleado con el ID especificado.</response>
        /// <response code="500">Ocurrió un error interno al intentar actualizar el empleado.</response>
        [HttpPut("{id:int}")]
        public async Task<ActionResult<string>> Put(int id, [FromBody] EmpleadoEscrituraDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var empleado = await _service.UpdateAsync(id, dto);
                if(!empleado)
                {
                    return NotFound(new { message = $"El empleado con ID {id} no existe." });
                }
                return NoContent();
            } catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error al actualizar el empleado.", error = ex.Message });
            }
        }

        /// <param name="id">ID del empleado que se desea eliminar.</param>
        /// <response code="204">El empleado se eliminó correctamente.</response>
        /// <response code="404">No se encontró ningún empleado con el ID especificado.</response>
        /// <response code="500">Ocurrió un error interno al intentar eliminar el empleado.</response>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<string>> Delete(int id)
        {
            try
            {
                var empleado = await _service.DeleteAsync(id);
                if (!empleado)
                {
                    return NotFound(new { message = $"El empleado con ID {id} no existe." });
                }
                return NoContent();
            } catch(Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrio un error al eliminar el empleado.", error = ex.Message });
            }
        }

    }
}
