using System;
using System.Collections.Generic;

namespace backend.Data;

public partial class Kohderyhma
{
    public int Idkohderyhma { get; set; }

    public string Nimi { get; set; } = null!;

    public int KohdeId { get; set; }

    public virtual Kohde Kohde { get; set; } = null!;
}
