using System;
using System.Collections.Generic;

namespace miniMarket.Models;

public partial class DetalleOrden
{
    public int IdOrden { get; set; }

    public int IdItem { get; set; }

    public int IdProducto { get; set; }

    public int Cantidad { get; set; }

    public decimal PrecioLista { get; set; }

    public decimal Descuento { get; set; }

    public virtual Orden IdOrdenNavigation { get; set; } = null!;

    public virtual Producto IdProductoNavigation { get; set; } = null!;
}
