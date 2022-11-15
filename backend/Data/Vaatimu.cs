using System;
using System.Collections.Generic;

namespace backend.Data;

public partial class Vaatimu
{
    public int Idvaatimus { get; set; }

    public string? Kuvaus { get; set; }

    public string? Pakollisuus { get; set; }

    public int? Taytetty { get; set; }

    public int AuditointipohjaId { get; set; }
}
