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

        /// <response code="200">La operación fue exitosa y se devuelve la lista completa de inventario disponible.</response>
        [HttpGet]
        public async Task<ActionResult<List<InventarioLecturaDto>>> Get()
        {
            return Ok(await _service.GetAllAsync());
        }
    }
}
