namespace miniMarket.Dto
{
    /// <summary>
    /// (DTO - LECTURA DE DATOS) Representa el Data Transfer Object (DTO) de la marca de productos, 
    /// el cual permite realizar consultas en la entidad Marcas.
    /// </summary>
    public class MarcaLecturaDto
    {
        public int IdMarca { get; set; }

        public string NombreMarca { get; set; } = null!;
    }
}
