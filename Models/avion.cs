using System;
using System.Collections.Generic;

namespace api_db.Models;

public partial class avion
{
    public int id_avion { get; set; }

    public string matricula { get; set; } = null!;

    public string modelo { get; set; } = null!;

    public int capacidad_total { get; set; }

    public string id_aerolinea { get; set; } = null!;


}
