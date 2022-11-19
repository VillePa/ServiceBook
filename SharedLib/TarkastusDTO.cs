using System;
using System.ComponentModel.DataAnnotations;

namespace SharedLib
{
	public class TarkastusDTO
	{
        public int Idtarkastus { get; set; }

        public DateTime Aikaleima { get; set; }

        [Required]
        public string Syy { get; set; } = null!;

        [Required]
        public string Havainnot { get; set; } = null!;

        [Required]
        public int TilanMuutos { get; set; }

        public int Idkayttaja { get; set; }
        public string? KayttajanNimi { get; set; }

        [Required]
        public int Idkohde { get; set; }
        public string? KohteenNimi { get; set; }

    }
}

