using System;
using System.Collections.Generic;

namespace api_db.Models;

public partial class aeropuerto
{
    public int id_aeropuerto { get; set; }

    public string codigo_pais { get; set; } = null!;

    public string codigo_ciudad { get; set; } = null!;

    public string nombre { get; set; } = null!;


}
