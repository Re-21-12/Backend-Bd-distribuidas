using System;
using System.Collections.Generic;

namespace api_db.Models;

public partial class pai
{
    public string codigo_pais { get; set; } = null!;

    public string nombre_pais { get; set; } = null!;

    public virtual ICollection<aeropuerto> aeropuertos { get; set; } = new List<aeropuerto>();

    public virtual ICollection<ciudad> ciudads { get; set; } = new List<ciudad>();

    public virtual ICollection<pasajero> pasajeros { get; set; } = new List<pasajero>();
}
