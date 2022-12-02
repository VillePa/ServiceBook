using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib
{
	public class AuditointiDTO
	{
		public int Idauditointi { get; set; }

		public DateTime Luotu { get; set; }

		public string? Selite { get; set; }

		public int? Lopputulos { get; set; }

		public int Idkohde { get; set; }

		public int Idkayttaja { get; set; }

		public List<VaatimusDTO>? Vaatimukset { get; set; }
	}
}
