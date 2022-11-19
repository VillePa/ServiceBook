using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib
{
    public class HuoltokohdeDTO
    {
        public int Idkohde { get; set; }

        public string Nimi { get; set; } = null!;

        public string Kuvaus { get; set; } = null!;

        public string Sijainti { get; set; } = null!;

        public string Tyyppi { get; set; } = null!;

        public string Malli { get; set; } = null!;

        public string Tunnus { get; set; } = null!;

        public string Tila { get; set; } = null!;

        public DateTime Luotu { get; set; }

        public int Idkayttaja { get; set; }

        public int Idkohderyhma { get; set; }

    }
}
