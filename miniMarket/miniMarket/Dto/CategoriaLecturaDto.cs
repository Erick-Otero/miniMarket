using System.ComponentModel.DataAnnotations;

namespace miniMarket.Dto
{
    /// <summary>
    /// (DTO - LECTURA DE DATOS) Representa el Data Transfer Object (DTO) de la categoría de productos, 
    /// el cual permite realizar consultas en la entidad Categorías.
    /// </summary>
    public class CategoriaLecturaDto
    {
        public int IdCategoria { get; set; }

        public string NombreCategoria { get; set; } = null!;
        
    }
}
