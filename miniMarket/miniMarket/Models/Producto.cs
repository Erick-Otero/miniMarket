using System;
using System.Collections.Generic;

namespace miniMarket.Models;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string NombreProducto { get; set; } = null!;

    public int IdMarca { get; set; }

    public int IdCategoria { get; set; }

    public int AnioModelo { get; set; }

    public decimal PrecioLista { get; set; }

    public virtual ICollection<DetalleOrden> DetalleOrdenes { get; set; } = new List<DetalleOrden>();

    public virtual Categoria IdCategoriaNavigation { get; set; } = null!;

    public virtual Marca IdMarcaNavigation { get; set; } = null!;

    public virtual ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();
}
