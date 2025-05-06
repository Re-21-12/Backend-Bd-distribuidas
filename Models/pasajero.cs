using System;
using System.Collections.Generic;

namespace api_db.Models;

public partial class pasajero
{
    public uint id_pasajero { get; set; }

    public string primer_nombre { get; set; } = null!;

    public string? segundo_nombre { get; set; }

    public string? tercer_nombre { get; set; }

    public string primer_apellido { get; set; } = null!;

    public string segundo_apellido { get; set; } = null!;

    public string pasaporte { get; set; } = null!;

    public string codigo_pais { get; set; } = null!;

    public string codigo_ciudad { get; set; } = null!;

    public virtual ciudad codigo_ciudadNavigation { get; set; } = null!;

    public virtual pai codigo_paisNavigation { get; set; } = null!;

    public virtual ICollection<correo_electronico> correo_electronicos { get; set; } = new List<correo_electronico>();

    public virtual ICollection<telefono> telefonos { get; set; } = new List<telefono>();

    public virtual ICollection<reserva> id_reservas { get; set; } = new List<reserva>();
}
