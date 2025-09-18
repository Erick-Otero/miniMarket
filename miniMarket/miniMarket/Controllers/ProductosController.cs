using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using miniMarket.Dto;
using miniMarket.Services.Interfaces;

namespace miniMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoService _service;

        public ProductosController(IProductoService service)
        {
            _service = service;
        }

        /// <summary>
        /// Obtiene la lista completa de productos disponibles.
        /// </summary>
        /// <response code="200">La operación fue exitosa y devuelve todos los productos.</response>
        [HttpGet]
        public async Task<ActionResult<List<ProductoLecturaDto>>> Get()
        {
            return Ok(await _service.GetAllAsync());
        }

        /// <summary>
        /// Obtiene un producto por su identificador único.
        /// </summary>
        /// <param name="id">ID del producto que se desea consultar.</param>
        /// <response code="200">Devuelve el producto correspondiente al ID especificado.</response>
        /// <response code="404">No existe ningún producto con el ID especificado.</response>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<string>> GetById(int id)
        {
            var producto = await _service.GetByIdAsync(id);
            if (producto == null)
            {
                return NotFound(new { message = $"El producto con ID {id} no existe." });
            }
            return Ok(producto);
        }

        /// <summary>
        /// Crea un nuevo producto en el sistema.
        /// </summary>
        /// <param name="dto">Objeto con los datos del producto a crear.</param>
        /// <response code="201">El producto se creó correctamente y se devuelve el objeto creado.</response>
        /// <response code="400">Los datos enviados no cumplen con las validaciones requeridas.</response>
        /// <response code="500">Ocurrió un error interno al intentar crear el producto.</response>
        [HttpPost]
        public async Task<ActionResult<string>> Post([FromBody] ProductoEscrituraDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var nuevoProducto = await _service.AddAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = nuevoProducto.IdProducto }, nuevoProducto);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Ocurrió un error al crear el producto." });
            }
        }

        /// <summary>
        /// Actualiza los datos de un producto existente.
        /// </summary>
        /// <param name="id">ID del producto que se desea actualizar.</param>
        /// <param name="dto">Objeto con los datos actualizados del producto.</param>
        /// <response code="204">El producto se actualizó correctamente.</response>
        /// <response code="400">Los datos enviados no cumplen con las validaciones requeridas.</response>
        /// <response code="404">No se encontró ningún producto con el ID especificado.</response>
        /// <response code="500">Ocurrió un error interno al intentar actualizar el producto.</response>
        [HttpPut("{id:int}")]
        public async Task<ActionResult<string>> Put(int id, [FromBody] ProductoEscrituraDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var producto = await _service.UpdateAsync(id, dto);
                if (!producto)
                {
                    return NotFound(new { message = $"El producto con ID {id} no existe." });
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error al actualizar el producto.", error = ex.Message });
            }
        }

        /// <summary>
        /// Elimina un producto existente de la base de datos.
        /// </summary>
        /// <param name="id">ID del producto que se desea eliminar.</param>
        /// <response code="204">El producto se eliminó correctamente.</response>
        /// <response code="404">No se encontró ningún producto con el ID especificado.</response>
        /// <response code="500">Ocurrió un error interno al intentar eliminar el producto.</response>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<string>> Delete(int id)
        {
            try
            {
                var producto = await _service.GetByIdAsync(id);
                if (producto == null)
                {
                    return NotFound(new { message = $"El producto con ID {id} no existe." });
                }

                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error al eliminar el producto.", error = ex.Message });
            }
        }
    }
}
