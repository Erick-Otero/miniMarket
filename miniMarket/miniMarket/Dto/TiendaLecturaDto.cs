namespace miniMarket.Dto
{
    /// <summary>
    /// (DTO - LECTURA DE DATOS) Representa el Data Transfer Object (DTO) de una tienda, 
    /// el cual permite realizar consultas en la entidad Tiendas.
    /// </summary>
    public class TiendaLecturaDto
    {
        public int IdTienda { get; set; }

        public string NombreTienda { get; set; } = null!;

        public string? Telefono { get; set; }

        public string? Correo { get; set; }

        public string? Calle { get; set; }

        public string? Ciudad { get; set; }

        public string? Estado { get; set; }

        public string? CodigoPostal { get; set; }
    }
}
