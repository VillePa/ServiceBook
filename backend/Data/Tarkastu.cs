using System;
using System.Collections.Generic;

namespace backend.Data;

public partial class Tarkastu
{
    public int Idtarkastus { get; set; }

    public DateTime Aikaleima { get; set; }

    public string Syy { get; set; } = null!;

    public string Havainnot { get; set; } = null!;

    public int TilanMuutos { get; set; }

    public int TekijaKayttajaid { get; set; }

    public int KohdeId { get; set; }

    public byte[]? Liite { get; set; }

    public virtual Kohde Kohde { get; set; } = null!;

    public virtual Kayttaja TekijaKayttaja { get; set; } = null!;
}
