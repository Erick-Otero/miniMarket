using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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

        /// <response code="200">La operación fue exitosa y se devuelve la lista completa de productos disponibles.</response>
        [HttpGet]
        public async Task<ActionResult<List<ProductoLecturaDto>>> Get()
        {
            return Ok(await _service.GetAllAsync());
        }

        /// <param name="id">ID del producto que se desea consultar.</param>
        /// <response code="200">Devuelve un producto según el parámetro 'ID'.</response>
        /// <response code="404">Si no existe una producto.</response>
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
                return CreatedAtAction(nameof(GetById), new { id = nuevoProducto.IdProducto }, nuevoProducto );
            } catch (Exception)
            {
                return StatusCode(500, new { message = "Ocurrió un error al crear el producto." });
            }
        }

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
            } catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error al actualizar la marca.", error = ex.Message });
            }
        }

        /// <param name="id">ID del podructo que se desea eliminar.</param>
        /// <response code="204">El producto se eliminó correctamente.</response>
        /// <response code="404">No se encontró ningún producto con el ID especificado.</response>
        /// <response code="500">Ocurrió un error interno al intentar eliminar el producto.</response>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<string>> Delete(int id)
        {
            try
            {
                var producto = await _service.GetByIdAsync(id);
                if(producto == null)
                {
                    return NotFound(new { message = $"El producto con ID {id} no existe." });
                }

                await _service.DeleteAsync(id);
                return NoContent();
            } catch(Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrio un error al eliminar la marca.", error = ex.Message});
            }
        }
    }
}
