using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using miniMarket.Dto;
using miniMarket.Services.Interfaces;

namespace miniMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventariosController : ControllerBase
    {
        private readonly IInventarioService _service;

        public InventariosController(IInventarioService service)
        {
            _service = service;
        }

        /// <summary>Obtiene todos los registros de inventarios disponibles en la entidad.</summary>
        /// <remarks>Devuelve una lista completa de inventarios registrados en la base de datos. </remarks>
        /// <response code="200">La operación fue exitosa y se devuelve la lista de inventarios.</response>
        [HttpGet]
        public async Task<ActionResult<List<InventarioLecturaDto>>> Get()
        {
            return Ok(await _service.GetAllAsync());
        }

        /// <summary> Obtiene un inventario específico a partir de los dos identificadores proporcionados.</summary>
        /// <remarks> Este método requiere dos parámetros, <c>idProducto</c> e <c>idTienda</c>, los cuales conforman una clave primaria compuesta en la entidad Inventarios. </remarks>
        /// <param name="idProducto">ID del producto asociado al inventario.</param>
        /// <param name="idTienda">ID de la tienda donde se encuentra el inventario.</param>
        /// <response code="200">Devuelve el inventario correspondiente a los IDs proporcionados.</response>
        /// <response code="404">No existe un inventario con los IDs especificados.</response>
        [HttpGet("{idTienda:int}/{idProducto:int}")]
        public async Task<ActionResult<InventarioLecturaDto>> GetById(int idTienda, int idProducto)
        {
            var inventario = await _service.GetByIdAsync(idProducto, idTienda);
            if (inventario == null)
            {
                return NotFound(new { message = "El inventario no existe." });
            }
            return Ok(inventario);
        }

        /// <summary>Crea un nuevo inventario en la entidad.</summary>
        /// <param name="dto">Objeto con los datos requeridos para crear el inventario.</param>
        /// <response code="201">El inventario se creó correctamente.</response>
        /// <response code="400">Los datos enviados no cumplen con las validaciones requeridas.</response>
        /// <response code="500">Ocurrió un error interno al intentar crear el inventario.</response>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] InventarioEscrituraDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var nuevoInventario = await _service.AddAsync(dto);
                return CreatedAtAction(nameof(GetById), 
                    new { idProducto = nuevoInventario.IdProducto, idTienda = nuevoInventario.IdTienda }, 
                    nuevoInventario);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Ocurrió un error al crear el inventario." });
            }
        }

        /// <summary> Actualiza un inventario existente a partir de su clave compuesta. </summary>
        /// <param name="idProducto">ID del producto que identifica el inventario.</param>
        /// <param name="idTienda">ID de la tienda que identifica el inventario.</param>
        /// <param name="dto">Objeto con los datos actualizados del inventario.</param>
        /// <response code="204">El inventario se actualizó correctamente.</response>
        /// <response code="400">Los datos enviados no cumplen con las validaciones requeridas.</response>
        /// <response code="404">No se encontró ningún inventario con los IDs especificados.</response>
        /// <response code="500">Ocurrió un error interno al intentar actualizar el inventario.</response>
        [HttpPut("{idTienda:int}/{idProducto:int}")]
        public async Task<IActionResult> Put(int idTienda, int idProducto, [FromBody] InventarioEscrituraDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var inventario = await _service.UpdateAsync(idProducto, idTienda, dto);
                if (!inventario)
                {
                    return NotFound(new { message = "El inventario no existe." });
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error al actualizar el inventario.", error = ex.Message });
            }
        }

        /// <summary>Elimina un inventario existente a partir de su clave compuesta.</summary>
        /// <param name="idProducto">ID del producto que identifica el inventario.</param>
        /// <param name="idTienda">ID de la tienda que identifica el inventario.</param>
        /// <response code="204">El inventario se eliminó correctamente.</response>
        /// <response code="404">No se encontró ningún inventario con los IDs especificados.</response>
        /// <response code="500">Ocurrió un error interno al intentar eliminar el inventario.</response>
        [HttpDelete("{idTienda:int}/{idProducto:int}")]
        public async Task<IActionResult> Delete(int idTienda, int idProducto)
        {
            try
            {
                var inventario = await _service.DeleteAsync(idProducto, idTienda);
                if (!inventario)
                {
                    return NotFound(new { message = "El inventario no existe." });
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error al eliminar el inventario.", error = ex.Message });
            }
        }
    }
}
