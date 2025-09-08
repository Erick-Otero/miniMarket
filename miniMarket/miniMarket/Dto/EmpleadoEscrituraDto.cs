using System.ComponentModel.DataAnnotations;

namespace miniMarket.Dto
{
    /// <summary>
    /// (DTO - ESCRITURA DE DATOS) Representa el Data Transfer Object (DTO) de un empleado, 
    /// el cual permite realizar registros y actualizaciones en la entidad Empleados.
    /// </summary>
    public class EmpleadoEscrituraDto
    {
        [Required(ErrorMessage = "El 'nombre del empleado' es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El 'nombre del empleado' no puede superar los 100 caracteres.")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "El 'apellido del empleado' es obligatorio.")]
        [MaxLength(150, ErrorMessage = "El 'apellido del empleado' no puede superar los 100 caracteres.")]
        public string Apellido { get; set; } = null!;

        /// <summary>
        /// La expresión regular permite validar si el correo electrónico tiene una estructura válida. 
        /// </summary>
        [Required(ErrorMessage = "El 'correo electrónico' es obligatorio.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$",
        ErrorMessage = "El 'correo electrónico' no tiene un formato válido.")]
        public string? Correo { get; set; }

        [Required(ErrorMessage = "El 'número de teléfono' es obligatorio.")]
        [Phone(ErrorMessage = "El 'número de teléfono' no tiene un formato válido.")]
        public string? Telefono { get; set; }

        [Required(ErrorMessage = "El 'estado del empleado' es obigatorio.")]
        public bool Activo { get; set; }

        [Required(ErrorMessage = "Debe vincular al empleado a una 'tienda'.")]
        public int IdTienda { get; set; }

        [Required(ErrorMessage = "Debe vincular al empleado a un 'superior'.")]
        public int? IdJefe { get; set; }

        [Required(ErrorMessage = "La 'contraseña' es obligatoria.")]
        [MinLength(8, ErrorMessage = "La 'contraseña' debe tener almenos 8 caracteres.")]
        [MaxLength(16, ErrorMessage = "La 'contraseña' no puede superar los 16 caracteres.")]
        public string Contrasena { get; set; } = null!;
    }
}
