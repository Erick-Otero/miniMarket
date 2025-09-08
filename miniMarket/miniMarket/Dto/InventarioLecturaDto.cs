namespace miniMarket.Dto
{
    public class InventarioLecturaDto
    {
        public int IdTienda { get; set; }

        public string? NombreTienda { get; set; }

        public int IdProducto { get; set; }

        public string? NombreProducto { get; set; }

        public int Cantidad { get; set; }

        public decimal? Precio { get; set; }
    }
}
