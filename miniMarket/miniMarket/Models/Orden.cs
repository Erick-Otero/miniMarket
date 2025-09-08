using System;
using System.Collections.Generic;

namespace miniMarket.Models;

public partial class Orden
{
    public int IdOrden { get; set; }

    public int IdCliente { get; set; }

    public string? EstadoOrden { get; set; }

    public DateOnly FechaOrden { get; set; }

    public DateOnly? FechaRequerida { get; set; }

    public DateOnly? FechaEnvio { get; set; }

    public int IdTienda { get; set; }

    public int IdEmpleado { get; set; }

    public virtual ICollection<DetalleOrden> DetalleOrdenes { get; set; } = new List<DetalleOrden>();

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual Empleado IdEmpleadoNavigation { get; set; } = null!;

    public virtual Tienda IdTiendaNavigation { get; set; } = null!;
}
