using System.ComponentModel.DataAnnotations;

namespace miniMarket.Dto
{
    /// <summary>
    /// (DTO - ESCRITURA DE DATOS) Representa el Data Transfer Object (DTO) de la categoría de productos, 
    /// el cual permite realizar registros y actualizaciones en la entidad Categorías.
    /// </summary>
    public class CategoriaEscrituraDto
    {
        [Required(ErrorMessage = "El 'nombre de la categoría' es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El 'nombre de la categoría' no puede superar los 100 caracteres.")]
        public string NombreCategoria { get; set; } = null!;
    }
}
