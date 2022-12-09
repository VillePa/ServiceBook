using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib
{
    public class HuoltokohdeDTO
    {
        public int Idkohde { get; set; }

        [Required]
        public string Nimi { get; set; } = null!;

        [Required]
        public string Kuvaus { get; set; } = null!;

        [Required]
        public string Sijainti { get; set; } = null!;

        [Required]
        public string Tyyppi { get; set; } = null!;

        [Required]
        public string Malli { get; set; } = null!;

        [Required]
        public string Tunnus { get; set; } = null!;

        [Required]
        public int IdkohteenTila { get; set; }

        public DateTime Luotu { get; set; }

        public int Idkayttaja { get; set; }

        [Required]
        public int Idkohderyhma { get; set; }

    }
}
