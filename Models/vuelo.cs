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

    public virtual aeropuerto aeropuerto_destinoNavigation { get; set; } = null!;

    public virtual aeropuerto aeropuerto_origenNavigation { get; set; } = null!;

    public virtual avion id_avionNavigation { get; set; } = null!;

    public virtual ICollection<reserva> reservas { get; set; } = new List<reserva>();
}
