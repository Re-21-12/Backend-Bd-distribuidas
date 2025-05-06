using System;
using System.Collections.Generic;

namespace api_db.Models;

public partial class aeropuerto
{
    public int id_aeropuerto { get; set; }

    public string codigo_pais { get; set; } = null!;

    public string codigo_ciudad { get; set; } = null!;

    public string nombre { get; set; } = null!;

    public virtual ciudad codigo_ciudadNavigation { get; set; } = null!;

    public virtual pai codigo_paisNavigation { get; set; } = null!;

    public virtual ICollection<vuelo> vueloaeropuerto_destinoNavigations { get; set; } = new List<vuelo>();

    public virtual ICollection<vuelo> vueloaeropuerto_origenNavigations { get; set; } = new List<vuelo>();
}
