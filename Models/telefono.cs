using System;
using System.Collections.Generic;

namespace api_db.Models;

public partial class telefono
{
    public string numero_telefono { get; set; } = null!;

    public uint id_pasajero { get; set; }

    public virtual pasajero id_pasajeroNavigation { get; set; } = null!;
}
