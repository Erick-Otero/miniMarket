using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using miniMarket.Dto;
using miniMarket.Services.Interfaces;

namespace miniMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenesController : ControllerBase
    {
        private readonly IOrdenService _service;
        public OrdenesController(IOrdenService service)
        {
            _service = service;
        }

        /// <summary>Obtiene todas las ordenes disponibles en la entidad.</summary>
        /// <response code="200">La operación fue exitosa y se devuelve la lista completa de ordenes.</response>
        [HttpGet]
        public async Task<ActionResult<List<OrdenLecturaDto>>> Get()
        {
            return await _service.GetAllAsync();
        }

        /// <summary>Obtiene una orden específica por su identificador único.</summary>
        /// <param name="id">ID de la orden que se desea consultar.</param>
        /// <response code="200">Devuelve la orden correspondiente al ID proporcionado.</response>
        /// <response code="404">No se encontró ninguna orden con el ID especificado.</response>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<OrdenLecturaDto?>> GetById(int id)
        {
            var orden = await _service.GetByIdAsync(id);
            if(orden == null)
            {
                return NotFound(new { message = $"La Orden con ID {id} no existe." });
            }
            return Ok(orden);
        }

        /// <summary>Crea una nueva orden en la entidad.</summary>
        /// <param name="dto">Objeto con los datos de la orden a crear.</param>
        /// <response code="201">La orden se creó correctamente y se devuelve el objeto creado.</response>
        /// <response code="400">Los datos enviados no cumplen con las validaciones requeridas.</response>
        /// <response code="500">Ocurrió un error interno al intentar crear la orden.</response>
        [HttpPost]
        public async Task<ActionResult<OrdenLecturaDto?>> Post([FromBody] OrdenEscrituraDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var nuevaOrden = await _service.AddAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = nuevaOrden.IdOrden }, nuevaOrden);
            }catch(Exception)
            {
                return StatusCode(500, new { message = "Ocurrió un error al crear la orden." });
            }
        }

        /// <summary>Actualiza una orden existente por medio de su ID.</summary>
        /// <param name="id">ID de la orden que se desea actualizar.</param>
        /// <param name="dto">Objeto con los datos actualizados de la orden.</param>
        /// <response code="204">La orden se actualizó correctamente.</response>
        /// <response code="400">Los datos enviados no cumplen con las validaciones requeridas.</response>
        /// <response code="404">No se encontró ninguna orden con el ID especificado.</response>
        /// <response code="500">Ocurrió un error interno al intentar actualizar la orden.</response>
        [HttpPut("{id:int}")]
        public async Task<ActionResult<string>> Put(int id, [FromBody] OrdenEscrituraDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var orden = await _service.UpdateAsync(id, dto);
                if(!orden)
                {
                    return NotFound(new { message = $"La Orden con ID {id} no existe." });
                }
                return NoContent();
            } catch(Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error al actualizar la orden", error = ex.Message});
            }
        }

        /// <summary>Elimina una orden existente por medio de su ID.</summary>
        /// <param name="id">ID de la orden que se desea eliminar.</param>
        /// <response code="204">La orden se eliminó correctamente.</response>
        /// <response code="404">No se encontró ninguna orden con el ID especificado.</response>
        /// <response code="500">Ocurrió un error interno al intentar eliminar la orden.</response>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<string>> Delete(int id)
        {
            try
            {
                var orden = await _service.DeleteAsync(id);
                if (!orden)
                {
                    return NotFound(new { message = $"La orden con ID {id} no existe." });
                }
                return NoContent();
            } catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error al eliminar la orden.", error = ex.Message });
            }
        }
    }
}
