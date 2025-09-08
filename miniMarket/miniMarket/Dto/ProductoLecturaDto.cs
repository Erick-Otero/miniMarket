using System.Globalization;

namespace miniMarket.Dto
{
    /// <summary>
    /// (DTO - LECTURA DE DATOS) Representa el Data Transfer Object (DTO) de un producto, 
    /// el cual permite realizar consultas en la entidad Productos.
    /// </summary>
    
    public class ProductoLecturaDto
    {
        public int IdProducto { get; set; }

        public string NombreProducto { get; set; } = null!;

        public int IdMarca { get; set; }

        public String? NombreMarca { get; set; }

        public int IdCategoria { get; set; }

        public string? NombreCategoria { get; set; }

        public int AnioModelo { get; set; }

        public decimal PrecioLista { get; set; }

    }
}
