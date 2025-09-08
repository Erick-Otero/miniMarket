using System;
using System.Collections.Generic;

namespace miniMarket.Models;

public partial class Tienda
{
    public int IdTienda { get; set; }

    public string NombreTienda { get; set; } = null!;

    public string? Telefono { get; set; }

    public string? Correo { get; set; }

    public string? Calle { get; set; }

    public string? Ciudad { get; set; }

    public string? Estado { get; set; }

    public string? CodigoPostal { get; set; }

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();

    public virtual ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();

    public virtual ICollection<Orden> Ordenes { get; set; } = new List<Orden>();
}
