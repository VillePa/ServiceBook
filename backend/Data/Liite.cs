using System;
using System.Collections.Generic;

namespace backend.Data;

public partial class Liite
{
	public int Idliite { get; set; }

	public string? Sijainti { get; set; }

	public int Idtarkastus { get; set; }

	public virtual Tarkastu IdtarkastusNavigation { get; set; } = null!;
}
