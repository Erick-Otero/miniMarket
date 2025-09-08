using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace miniMarket.Dto
{
    /// <summary>
    /// (DTO - ESCRITURA DE DATOS) Representa el Data Transfer Object (DTO) de un producto, 
    /// el cual permite realizar registros y actualizaciones en la entidad Productos.
    /// </summary>
    public class ProductoEscrituraDto
    {
        [Required(ErrorMessage = "El 'nombre del producto' es obligatorio.")]
        [MaxLength(150, ErrorMessage = "El 'nombre del producto' no puede superar los 150 caracteres.")]
        public string NombreProducto { get; set; } = null!;

        [Required(ErrorMessage = "La 'marca' es obligatorio.")]
        public int IdMarca { get; set; }

        [Required(ErrorMessage = "La 'categoría' es obligatorio.")]
        public int IdCategoria { get; set; }

        [Required(ErrorMessage = "El 'año del modelo' es obligatorio.")]
        public int AnioModelo { get; set; }

        [Required(ErrorMessage = "El 'precio del producto' es obligatorio.")]
        [Range(0.1, double.MaxValue, ErrorMessage = "El 'precio del producto' debe ser mayor a $0.00")]
        public decimal PrecioLista { get; set; }
    }
}
