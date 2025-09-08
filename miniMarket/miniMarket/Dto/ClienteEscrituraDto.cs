using System.ComponentModel.DataAnnotations;

namespace miniMarket.Dto
{
    /// <summary>
    /// (DTO - ESCRITURA DE DATOS) Representa el Data Transfer Object (DTO) de un cliente, 
    /// el cual permite realizar registros y actualizaciones en la entidad Clientes.
    /// </summary>
    public class ClienteEscrituraDto
    {
        [Required(ErrorMessage = "El 'nombre del cliente' es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El 'nombre del cliente' no puede superar los 100 caracteres.")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "El 'apellido del empleado' es obligatorio.")]
        [MaxLength(150, ErrorMessage = "El 'apellido del empleado' no puede superar los 100 caracteres.")]
        public string Apellido { get; set; } = null!;

        [Required(ErrorMessage = "El 'número de teléfono' es obligatorio.")]
        [Phone(ErrorMessage = "El 'número de teléfono' no tiene un formato válido.")]
        public string? Telefono { get; set; }

        /// <summary>
        /// La expresión regular permite validar si el correo electrónico tiene una estructura válida. 
        /// </summary>
        [Required(ErrorMessage = "El 'correo electrónico' es obligatorio.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$",
        ErrorMessage = "El 'correo electrónico' no tiene un formato válido.")]
        public string? Correo { get; set; }

        public string? Calle { get; set; }

        public string? Ciudad { get; set; }

        public string? Estado { get; set; }

        /// <summary>
        /// La expresión regular permite validar que el código postal contenga 4 dígitos. 
        /// </summary>
        [RegularExpression(@"(^\d{4})", ErrorMessage = "El 'código postal' no tiene un formato válido.")]
        public string? CodigoPostal { get; set; }
    }
}
