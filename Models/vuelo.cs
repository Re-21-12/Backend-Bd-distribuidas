using System;
using System.Collections.Generic;

namespace api_db.Models;

public partial class vuelo
{
    public string numero_vuelo { get; set; } = null!;

    public DateTime hora_salida { get; set; }

    public DateTime hora_llegada { get; set; }

    public int aeropuerto_origen { get; set; }

    public int aeropuerto_destino { get; set; }

    public int id_avion { get; set; }


}
