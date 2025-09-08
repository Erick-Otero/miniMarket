using System.ComponentModel.DataAnnotations;

namespace miniMarket.Dto
{
    public class InventarioEscrituraDto
    {
        [Required(ErrorMessage = "La selección de una 'tienda' es obligatoria.")]
        public int IdTienda { get; set; }

        [Required(ErrorMessage = "La selección de un 'producto' es obligatoria.")]
        public int IdProducto { get; set; }

        [Required(ErrorMessage = "La 'cantidad del producto' es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "La 'cantidad del producto' debe ser mayor a 1.")]
        public int Cantidad { get; set; }
    }
}
