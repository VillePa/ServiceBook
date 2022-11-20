using System;
using System.Collections.Generic;

namespace backend.Data;

public partial class Kayttaja
{
    public int Idkayttaja { get; set; }

    public string Nimi { get; set; } = null!;

    public string Kayttajatunnus { get; set; } = null!;

    public string Salasana { get; set; } = null!;

    public DateTime Luotu { get; set; }

    public DateTime? ViimeisinKirjautuminen { get; set; }

    public string Rooli { get; set; } = null!;

    public string? SalasanaSalt { get; set; }

    public int Poistettu { get; set; }

    public  List<Tarkastu>? Tarkastus { get; }
}
