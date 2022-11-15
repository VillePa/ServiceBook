using System;
using System.Collections.Generic;

namespace backend.Data;

public partial class Kohde
{
    public int Idkohde { get; set; }

    public string Nimi { get; set; } = null!;

    public string Kuvaus { get; set; } = null!;

    public string Kohderyhma { get; set; } = null!;

    public string Sijainti { get; set; } = null!;

    public string Tyyppi { get; set; } = null!;

    public string Malli { get; set; } = null!;

    public string Tunnus { get; set; } = null!;

    public string Tila { get; set; } = null!;

    public DateTime Luotu { get; set; }

    public int LuojaIdkayttaja { get; set; }

    public virtual ICollection<Auditointi> Auditointis { get; } = new List<Auditointi>();

    public virtual ICollection<Kohderyhma> Kohderyhmas { get; } = new List<Kohderyhma>();

    public virtual Kayttaja LuojaIdkayttajaNavigation { get; set; } = null!;

    public virtual ICollection<Tarkastu> Tarkastus { get; } = new List<Tarkastu>();
}
