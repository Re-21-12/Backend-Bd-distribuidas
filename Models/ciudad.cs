using System;
using System.Collections.Generic;

namespace api_db.Models;

public partial class ciudad
{
    public string codigo_ciudad { get; set; } = null!;

    public string nombre_ciudad { get; set; } = null!;

    public string codigo_pais { get; set; } = null!;

    public virtual ICollection<aeropuerto> aeropuertos { get; set; } = new List<aeropuerto>();

    public virtual pai codigo_paisNavigation { get; set; } = null!;

    public virtual ICollection<pasajero> pasajeros { get; set; } = new List<pasajero>();
}
