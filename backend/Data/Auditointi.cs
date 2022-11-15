using System;
using System.Collections.Generic;

namespace backend.Data;

public partial class Auditointi
{
    public int Idauditointi { get; set; }

    public DateTime Luotu { get; set; }

    public string? Selite { get; set; }

    public int? Lopputulos { get; set; }

    public int KohdeId { get; set; }

    public int AuditointipohjaId { get; set; }

    public int KayttajaId { get; set; }

    public virtual Auditointipohja Auditointipohja { get; set; } = null!;

    public virtual Kohde Kohde { get; set; } = null!;
}
