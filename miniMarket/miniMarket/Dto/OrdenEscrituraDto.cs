using System.ComponentModel.DataAnnotations;

namespace miniMarket.Dto
{
    /// <summary>
    /// (DTO - ESCRITURA DE DATOS) Representa el Data Transfer Object (DTO) de una orden, 
    /// el cual permite realizar registros y actualizaciones en la entidad Ordenes.
    /// </summary>
    public class OrdenEscrituraDto
    {
        [Required(ErrorMessage = "Debe vincular la orden a un 'cliente'.")]
        public int IdCliente { get; set; }

        public string? EstadoOrden { get; set; }

        [Required(ErrorMessage = "La 'fecha de entrega' es obligatoria.")]
        public DateOnly? FechaRequerida { get; set; }

        [Required(ErrorMessage = "La 'fecha de envío' es obligatoria.")]
        public DateOnly? FechaEnvio { get; set; }

        [Required(ErrorMessage = "Debe vincular la orden a una 'tienda'.")]
        public int IdTienda { get; set; }

        [Required(ErrorMessage = "Debe vincular la orden a un 'empleado'.")]
        public int IdEmpleado { get; set; }
    }
}
