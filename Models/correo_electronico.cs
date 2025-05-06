using System;
using System.Collections.Generic;

namespace api_db.Models;

public partial class correo_electronico
{
    public string correo { get; set; } = null!;

    public uint id_pasajero { get; set; }

    public virtual pasajero id_pasajeroNavigation { get; set; } = null!;
}
