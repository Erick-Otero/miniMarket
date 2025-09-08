using System;
using System.Collections.Generic;

namespace miniMarket.Models;

public partial class Inventario
{
    public int IdTienda { get; set; }

    public int IdProducto { get; set; }

    public int Cantidad { get; set; }

    public virtual Producto IdProductoNavigation { get; set; } = null!;

    public virtual Tienda IdTiendaNavigation { get; set; } = null!;
}
