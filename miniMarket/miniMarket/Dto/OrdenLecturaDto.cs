namespace miniMarket.Dto
{
    /// <summary>
    /// (DTO - LECTURA DE DATOS) Representa el Data Transfer Object (DTO) de una orden, 
    /// el cual permite realizar consultas en la entidad Ordenes.
    /// </summary>
    public class OrdenLecturaDto
    {
        public int IdOrden { get; set; }

        public int IdCliente { get; set; }

        public string? NombreCliente { get; set; }

        public string? EstadoOrden { get; set; }

        public DateOnly FechaOrden { get; set; }

        public DateOnly? FechaRequerida { get; set; }

        public DateOnly? FechaEnvio { get; set; }

        public int IdTienda { get; set; }

        public int IdEmpleado { get; set; }

        public string? NombreEmpleado { get; set; }
    }
}
