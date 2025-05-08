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


}
