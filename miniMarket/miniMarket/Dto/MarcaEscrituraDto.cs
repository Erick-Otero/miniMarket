using System.ComponentModel.DataAnnotations;

namespace miniMarket.Dto
{
    /// <summary>
    /// (DTO - ESCRITURA DE DATOS) Representa el Data Transfer Object (DTO) de la marca de productos, 
    /// el cual permite realizar registros y actualizaciones en la entidad Marcas.
    /// </summary>
    public class MarcaEscrituraDto
    {
        [Required(ErrorMessage = "El 'nombre de la marca' es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El 'nombre de la marca' no puede superar los 100 caracteres.")]
        public string NombreMarca { get; set; } = null!;
    }
}
