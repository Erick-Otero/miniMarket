using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using miniMarket.Services.Interfaces;
using miniMarket.Dto;

namespace miniMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaService _service;

        public CategoriasController(ICategoriaService service)
        {
            _service = service;
        }

        /// <response code="200">La operación fue exitosa y se devuelve la lista completa de categorías disponibles.</response>
        [HttpGet]
        public async Task<ActionResult<List<CategoriaLecturaDto>>> Get()
        {
            return Ok(await _service.GetAllAsync());
        }

        /// <param name="id">ID de la categoría que se desea consultar.</param>
        /// <response code="200">Devuelve una categoría según el parámetro 'ID'.</response>
        /// <response code="404">Si no existe una categoría.</response>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<string>> GetById(int id)
        {
            var categoria = await _service.GetByIdAsync(id);
            if(categoria == null)
            {
                return NotFound(new {message = $"La categoría con ID {id} no existe."});
            }

            return Ok(categoria);
        }

        /// <param name="dto">Objeto con los datos de la categoría a crear.</param>
        /// <response code="201">La categoría se creó correctamente y se devuelve el objeto creado.</response>
        /// <response code="400">Los datos enviados no cumplen con las validaciones requeridas.</response>
        /// <response code="500">Ocurrió un error interno al intentar crear la categoría.</response>
        [HttpPost]
        public async Task<ActionResult<string>> Post([FromBody] CategoriaEscrituraDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var nuevaCategoria = await _service.AddAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = nuevaCategoria.IdCategoria}, nuevaCategoria);
            } catch (Exception)
            {
                return StatusCode(500, new { message = "Ocurrió un error al crear la categoría."});
            }
        }

        /// <param name="id">ID de la categoría que se desea actualizar.</param>
        /// <param name="dto">Objeto con los datos actualizados de la categoría.</param>
        /// <response code="204">La ategoría se actualizó correctamente.</response>
        /// <response code="400">Los datos enviados no cumplen con las validaciones requeridas.</response>
        /// <response code="404">No se encontró ninguna categoría con el ID especificado.</response>
        /// <response code="500">Ocurrió un error interno al intentar actualizar la categoría.</response>
        [HttpPut("{id:int}")]
        public async Task<ActionResult<string>> Put(int id, [FromBody] CategoriaEscrituraDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var categoria = await _service.UpdateAsync(id, dto);
                if (!categoria)
                {
                    return NotFound(new { message = $"La categoría con ID {id} no existe."});
                }
                return NoContent();
            } catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error al actuaizar la categoría.", error = ex.Message});
            }
        }

        /// <param name="id">ID de la categoría que se desea eliminar.</param>
        /// <response code="204">La categoría se eliminó correctamente.</response>
        /// <response code="404">No se encontró ninguna categoría con el ID especificado.</response>
        /// <response code="500">Ocurrió un error interno al intentar eliminar la categoría.</response>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<string>> Delete(int id)
        {
            try
            {
                var categoria = await _service.DeleteAsync(id);
                if (!categoria)
                {
                    return NotFound(new { message =$"La categoría con ID {id} no existe."});
                }
                return NoContent();
            } catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrio un error al eliminar la categoría.", error = ex.Message });
            }
        }
    }
}
