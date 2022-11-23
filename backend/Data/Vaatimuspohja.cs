using System;
using System.Collections.Generic;

namespace backend.Data;

public partial class Vaatimuspohja
{
	public int Idvaatimuspohja { get; set; }

	public string? Kuvaus { get; set; }

	public string? Pakollisuus { get; set; }

	public int Idauditointipohja { get; set; }

	public virtual Auditointipohja IdauditointipohjaNavigation { get; set; } = null!;
}
