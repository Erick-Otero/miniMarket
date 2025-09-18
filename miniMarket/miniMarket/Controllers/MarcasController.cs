using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using miniMarket.Dto;
using miniMarket.Services.Interfaces;

namespace miniMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcasController : ControllerBase
    {
        private readonly IMarcaService _service;

        public MarcasController(IMarcaService service)
        {
            _service = service;
        }

        /// <summary>Obtiene todas las marcas disponibles en la entidad.</summary>
        /// <response code="200">La operación fue exitosa y se devuelve la lista completa de marcas.</response>
        [HttpGet]
        public async Task<ActionResult<List<MarcaLecturaDto>>> Get()
        {
            return Ok(await _service.GetAllAsync());
        }

        /// <summary>Obtiene una marca específica por su identificador único.</summary>
        /// <param name="id">ID de la marca que se desea consultar.</param>
        /// <response code="200">Devuelve la marca correspondiente al ID proporcionado.</response>
        /// <response code="404">No se encontró ninguna marca con el ID especificado.</response>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<MarcaLecturaDto>> GetById(int id)
        {
            var marca = await _service.GetByIdAsync(id);
            if (marca == null)
            {
                return NotFound(new { message = $"La marca con ID {id} no existe." });
            }

            return Ok(marca);
        }

        /// <summary>Crea una nueva marca en la entidad.</summary>
        /// <param name="dto">Objeto con los datos de la marca a crear.</param>
        /// <response code="201">La marca se creó correctamente y se devuelve el objeto creado.</response>
        /// <response code="400">Los datos enviados no cumplen con las validaciones requeridas.</response>
        /// <response code="500">Ocurrió un error interno al intentar crear la marca.</response>
        [HttpPost]
        public async Task<ActionResult<MarcaLecturaDto>> Post([FromBody] MarcaEscrituraDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var nuevaMarca = await _service.AddAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = nuevaMarca.IdMarca }, nuevaMarca);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Ocurrió un error al crear la marca." });
            }
        }

        /// <summary>Actualiza una marca existente por medio de su ID.</summary>
        /// <param name="id">ID de la marca que se desea actualizar.</param>
        /// <param name="dto">Objeto con los datos actualizados de la marca.</param>
        /// <response code="204">La marca se actualizó correctamente.</response>
        /// <response code="400">Los datos enviados no cumplen con las validaciones requeridas.</response>
        /// <response code="404">No se encontró ninguna marca con el ID especificado.</response>
        /// <response code="500">Ocurrió un error interno al intentar actualizar la marca.</response>
        [HttpPut("{id:int}")]
        public async Task<ActionResult<string>> Put(int id, [FromBody] MarcaEscrituraDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var marca = await _service.UpdateAsync(id, dto);
                if (!marca)
                {
                    return NotFound(new { message = $"La marca con ID {id} no existe." });
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error al actualizar la marca.", error = ex.Message });
            }
        }

        /// <summary>Elimina una marca existente por medio de su ID.</summary>
        /// <param name="id">ID de la marca que se desea eliminar.</param>
        /// <response code="204">La marca se eliminó correctamente.</response>
        /// <response code="404">No se encontró ninguna marca con el ID especificado.</response>
        /// <response code="500">Ocurrió un error interno al intentar eliminar la marca.</response>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<string>> Delete(int id)
        {
            try
            {
                var marca = await _service.DeleteAsync(id);
                if (!marca)
                {
                    return NotFound(new { message = $"La marca con ID {id} no existe." });
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error al eliminar la marca.", error = ex.Message });
            }
        }
    }
}
