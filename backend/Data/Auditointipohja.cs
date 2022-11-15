using System;
using System.Collections.Generic;

namespace backend.Data;

public partial class Auditointipohja
{
    public int Idauditointipohja { get; set; }

    public string? Selite { get; set; }

    public DateTime? Luontiaika { get; set; }

    public int KayttajaId { get; set; }

    public virtual ICollection<Auditointi> Auditointis { get; } = new List<Auditointi>();

    public virtual Kayttaja Kayttaja { get; set; } = null!;
}
