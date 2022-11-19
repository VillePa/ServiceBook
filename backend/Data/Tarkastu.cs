using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Data;

public partial class Tarkastu
{
    public int Idtarkastus { get; set; }

    public DateTime Aikaleima { get; set; }

    public string Syy { get; set; } = null!;

    public string Havainnot { get; set; } = null!;

    public int TilanMuutos { get; set; }

    public int Idkayttaja { get; set; }

    [ForeignKey("IdKayttaja")]
    public Kayttaja Kayttaja { get; set; }

    public int Idkohde { get; set; }

    /*
    [ForeignKey("Idkohde")]
    public Kohde Kohde { get; set; }
    */
    public virtual Kohde IdkohdeNavigation { get; set; } = null!;
}
