using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib
{
    public class AuditointipohjaDTO
    {
        public int Idauditointipohja { get; set; }

        public string? Selite { get; set; }

        public DateTime? Luontiaika { get; set; }

        public int Idkayttaja { get; set; }

        public string KayttajaNimi { get; set; }

        public int Idkohderyhma { get; set; }

        public string KohderyhmaNimi { get; set; }

		//public ICollection<VaatimuspohjaDTO> Vaatimuspohjas { get; } = new List<VaatimuspohjaDTO>();
	}
}
