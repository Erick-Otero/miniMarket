namespace miniMarket.Dto
{
    /// <summary>
    /// (DTO - LECTURA DE DATOS) Representa el Data Transfer Object (DTO) de un empleado, 
    /// el cual permite realizar consultas en la entidad Empleados.
    /// </summary>
    public class EmpleadoLecturaDto
    {
        public int IdEmpleado { get; set; }

        public string Nombre { get; set; } = null!;

        public string Apellido { get; set; } = null!;

        public string? Correo { get; set; }

        public string? Telefono { get; set; }

        public bool Activo { get; set; }

        public int IdTienda { get; set; }

        public string? NombreTienda { get; set; }

        public int? IdJefe { get; set; }

        public string? NombreSuperior { get; set; }

        //public string? Contrasena { get; set; } = null!;
    }
}
