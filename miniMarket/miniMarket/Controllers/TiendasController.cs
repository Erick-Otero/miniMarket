using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using miniMarket.Dto;
using miniMarket.Services.Interfaces;

namespace miniMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiendasController : ControllerBase
    {
        private readonly ITiendaService _service;

        public TiendasController(ITiendaService service)
        {
            _service = service;
        }

        /// <response code="200">La operación fue exitosa y se devuelve la lista completa de tiendas disponibles.</response>
        [HttpGet]
        public async Task<ActionResult<List<TiendaLecturaDto>>> Get()
        {
            return Ok(await _service.GetAllAsync());
        }

        /// <param name="id">ID de la tienda que se desea consultar.</param>
        /// <response code="200">Devuelve una tienda según el parámetro 'ID'.</response>
        /// <response code="404">Si no existe una tienda.</response>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TiendaLecturaDto>> GetById(int id)
        {
            var tienda = await _service.GetByIdAsync(id);
            if(tienda == null)
            {
                return NotFound(new {message = $"La tienda con ID {id} no existe." });
            }

            return Ok(tienda);
        }

        /// <param name="dto">Objeto con los datos de la tienda a crear.</param>
        /// <response code="201">La tienda se creó correctamente y se devuelve el objeto creado.</response>
        /// <response code="400">Los datos enviados no cumplen con las validaciones requeridas.</response>
        /// <response code="500">Ocurrió un error interno al intentar crear la tienda.</response>
        [HttpPost]
        public async Task<ActionResult<TiendaLecturaDto>> Post([FromBody] TiendaEscrituraDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var nuevaTienda = await _service.AddAsync(dto);
                return CreatedAtAction(nameof(GetById), new {id = nuevaTienda.IdTienda}, nuevaTienda);
            }catch (Exception)
            {
                return StatusCode(500, new { message = "Ocurrió un error al crear la tienda." });
            }

        }

        /// <param name="id">ID de la tienda que se desea actualizar.</param>
        /// <param name="dto">Objeto con los datos actualizados de la tienda.</param>
        /// <response code="204">La tienda se actualizó correctamente.</response>
        /// <response code="400">Los datos enviados no cumplen con las validaciones requeridas.</response>
        /// <response code="404">No se encontró ninguna tienda con el ID especificado.</response>
        /// <response code="500">Ocurrió un error interno al intentar actualizar la tienda.</response>
        [HttpPut("{id:int}")]
        public async Task<ActionResult<string>> Put(int id, [FromBody] TiendaEscrituraDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var tienda = await _service.GetByIdAsync(id);
                if(tienda == null)
                {
                    return NotFound(new { message = $"La tienda con ID {id} no existe." });
                }

                await _service.UpdateAsync(id, dto);
                return NoContent();
            } catch(Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error al actualizar la marca.", error = ex.Message });
            }
        }

        /// <param name="id">ID de la tienda que se desea eliminar.</param>
        /// <response code="204">La tienda se eliminó correctamente.</response>
        /// <response code="404">No se encontró ninguna tienda con el ID especificado.</response>
        /// <response code="500">Ocurrió un error interno al intentar eliminar la tienda.</response>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<string>> Delete(int id)
        {
            var tienda = await _service.GetByIdAsync(id);
            if(tienda == null)
            {
                return NotFound(new { message = $"La tienda con ID {id} no existe." });
            }

            try
            {
                await _service.DeleteAsync(id);
                return NoContent();
            }catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrio un error al eliminar la tienda.", error = ex.Message});
            }
        }
    }
}
