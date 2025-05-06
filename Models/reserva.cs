using System;
using System.Collections.Generic;

namespace api_db.Models;

public partial class reserva
{
    public uint id_reserva { get; set; }

    public string letra_fila { get; set; } = null!;

    public int numero_plaza { get; set; }

    public DateOnly fecha_reserva { get; set; }

    public string estado { get; set; } = null!;

    public string numero_vuelo { get; set; } = null!;

    public virtual vuelo numero_vueloNavigation { get; set; } = null!;

    public virtual plaza plaza { get; set; } = null!;

    public virtual ICollection<pasajero> id_pasajeros { get; set; } = new List<pasajero>();
}
