using System.ComponentModel.DataAnnotations;

namespace miniMarket.Dto
{
    /// <summary>
    /// (DTO - ESCRITURA DE DATOS) Representa el Data Transfer Object (DTO) de una tienda, 
    /// el cual permite realizar registros y actualizaciones en la entidad Tiendas.
    /// </summary>
    public class TiendaEscrituraDto
    {
        [Required(ErrorMessage = "El 'nombre de la tienda' es obligatorio.")]
        [MaxLength(150, ErrorMessage = "El 'nombre de la tienda' no puede superar los 150 caracteres.")]
        public string NombreTienda { get; set; } = null!;

        [Required(ErrorMessage = "El 'número de teléfono' es obligatorio.")]
        [Phone(ErrorMessage = "El 'número de teléfono' no tiene un formato válido.")]
        public string? Telefono { get; set; }

        /// <summary>
        /// La expresión regular permite validar si el correo electrónico tiene una estructura válida. 
        /// </summary>
        [Required(ErrorMessage = "El 'correo electrónico' es obligatorio.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$",
        ErrorMessage = "El 'correo electrónico' no tiene un formato válido.")]
        [MaxLength(100, ErrorMessage = "El 'correo electrónico' no puede superar los 100 caracteres.")]
        public string? Correo { get; set; }

        [MaxLength(200, ErrorMessage = "La 'calle' no puede superar los 200 caracteres.")]
        public string? Calle { get; set; }

        [MaxLength(100, ErrorMessage = "La 'ciudad' no puede superar los 100 caracteres.")]
        public string? Ciudad { get; set; }

        [MaxLength(100, ErrorMessage = "El 'estado' no puede superar los 100 caracteres.")]
        public string? Estado { get; set; }

        /// <summary>
        /// La expresión regular permite validar que el código postal contenga 4 dígitos. 
        /// </summary>
        [RegularExpression(@"(^\d{4})", ErrorMessage = "El 'código postal' no tiene un formato válido.")]
        public string? CodigoPostal { get; set; }
    }
}
