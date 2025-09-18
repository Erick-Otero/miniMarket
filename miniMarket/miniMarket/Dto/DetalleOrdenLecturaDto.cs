namespace miniMarket.Dto
{
    public class DetalleOrdenLecturaDto
    {
        public int IdOrden { get; set; }

        public int IdItem { get; set; }

        public int IdProducto { get; set; }

        public int Cantidad { get; set; }

        public decimal PrecioLista { get; set; }

        public decimal Descuento { get; set; }
    }
}
