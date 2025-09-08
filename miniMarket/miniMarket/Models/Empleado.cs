using System;
using System.Collections.Generic;

namespace miniMarket.Models;

public partial class Empleado
{
    public int IdEmpleado { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string? Correo { get; set; }

    public string? Telefono { get; set; }

    public bool Activo { get; set; }

    public int IdTienda { get; set; }

    public int? IdJefe { get; set; }

    public string Contrasena { get; set; } = null!;

    public virtual Empleado? IdJefeNavigation { get; set; }

    public virtual Tienda IdTiendaNavigation { get; set; } = null!;

    public virtual ICollection<Empleado> InverseIdJefeNavigation { get; set; } = new List<Empleado>();

    public virtual ICollection<Orden> Ordenes { get; set; } = new List<Orden>();
}
