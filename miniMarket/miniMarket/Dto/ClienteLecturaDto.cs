namespace miniMarket.Dto
{
    /// <summary>
    /// (DTO - LECTURA DE DATOS) Representa el Data Transfer Object (DTO) de un cliente, 
    /// el cual permite realizar consultas en la entidad Clientes.
    /// </summary>
    public class ClienteLecturaDto
    {
        public int IdCliente { get; set; }

        public string Nombre { get; set; } = null!;

        public string Apellido { get; set; } = null!;

        public string? Telefono { get; set; }

        public string? Correo { get; set; }

        public string? Calle { get; set; }

        public string? Ciudad { get; set; }

        public string? Estado { get; set; }

        public string? CodigoPostal { get; set; }

    }
}
