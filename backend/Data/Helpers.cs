﻿using SharedLib;
namespace backend.Data

{
	public static class Helpers
	{
		public static KayttajaDTO KayttajaToDTO(this Kayttaja item)
		{
			return new KayttajaDTO
			{
				Idkayttaja = item.Idkayttaja,
				Nimi = item.Nimi,
				Kayttajatunnus = item.Kayttajatunnus,
				Luotu = item.Luotu,
				Rooli = item.Rooli,
				Poistettu = item.Poistettu,
				//ViimeisinKirjautuminen = item.ViimeisinKirjautuminen
			};
		}


		// to TarkastusDTO class

		public static TarkastusDTO TarkastusToDTO(this Tarkastu t)
		{

			return new TarkastusDTO
			{
				Idtarkastus = t.Idtarkastus,
				Aikaleima = t.Aikaleima,
				Syy = t.Syy,
				Havainnot = t.Havainnot,
				TilanMuutos = t.TilanMuutos,
				Idkohde = t.Idkohde,
				Idkayttaja = t.Idkayttaja,
				KayttajanNimi = t.IdkayttajaNavigation.Nimi,
				KohteenNimi = $"{t.IdkohdeNavigation.Nimi}, {t.IdkohdeNavigation.Sijainti}",
				Liitteet = t.Liites.Select(x => new UploadResult
				{
					Location = x.Sijainti,
					FileName = x.Sijainti != null && x.Sijainti.Length > 68 ? x.Sijainti.Substring(68) : ""
				}).ToList()
			};
		}

		// tarkastusDTO to Tarkastus class
		public static Tarkastu DTOtoTarkastu(TarkastusDTO t)
		{
			return new Tarkastu
			{
				Aikaleima = t.Aikaleima,
				Syy = t.Syy,
				Havainnot = t.Havainnot,
				TilanMuutos = t.TilanMuutos,
				Idkohde = t.Idkohde,
				Idkayttaja = t.Idkayttaja
			};
		}

		// Huoltokohde helpperi

		public static HuoltokohdeDTO KohdeToDTO(this Kohde a)
		{
			return new HuoltokohdeDTO
			{
				Idkohde = a.Idkohde,
				Nimi = a.Nimi,
				Kuvaus = a.Kuvaus,
				Sijainti = a.Sijainti,
				Tyyppi = a.Tyyppi,
				Malli = a.Malli,
				Tunnus = a.Tunnus,
				IdkohteenTila = a.IdkohteenTila,
				Luotu = a.Luotu,
				Idkayttaja = a.Idkayttaja,
				Idkohderyhma = a.Idkohderyhma

			};
		}

		// huoltokohdeDTO to huoltokohde
		public static Kohde DTOtoKohde(this HuoltokohdeDTO a)
		{
			return new Kohde
			{
				Idkohde = a.Idkohde,
				Nimi = a.Nimi,
				Kuvaus = a.Kuvaus,
				Sijainti = a.Sijainti,
				Tyyppi = a.Tyyppi,
				Malli = a.Malli,
				Tunnus = a.Tunnus,
				IdkohteenTila = a.IdkohteenTila,
				Luotu = a.Luotu,
				Idkayttaja = a.Idkayttaja,
				Idkohderyhma = a.Idkohderyhma
			};
		}

		// Kohderyhmä helpperi

		public static KohderyhmaDTO KohderyhmaToDTO(this Kohderyhma a)
		{
			return new KohderyhmaDTO
			{
				Idkohderyhma = a.Idkohderyhma,
				Nimi = a.Nimi,


			};
		}

		// Tila helpperi

		public static TilaDTO TilaToDTO(this KohteenTila a)
		{
			return new TilaDTO
			{
				IdkohteenTila = a.IdkohteenTila,
				Kuvaus = a.Kuvaus,


			};
		}

        // Muutoshistoria helpperi

        public static MuutoshistoriaKohdeDTO MuutoshistoriaKohdeToDTO(this MuutoshistoriaKohde a)
        {
            return new MuutoshistoriaKohdeDTO
            {
                IdmuutoshistoriaKohde = a.IdmuutoshistoriaKohde,
                Nimi = a.Nimi,
                Kuvaus = a.Kuvaus,
                Sijainti = a.Sijainti,
                Tunnus = a.Tunnus,
                IdkohteenTila = a.IdkohteenTila,
                Muokattu = a.Muokattu,
				KohdeIdkohde = a.KohdeIdkohde,
                KayttajaIdkayttaja = a.KayttajaIdkayttaja,
                

            };
        }

        // muutoshistoriakohdeDTO to muutoshistoriakohde
        public static MuutoshistoriaKohde DTOtoMuutoshistoriaKohde(this MuutoshistoriaKohdeDTO a)
        {
            return new MuutoshistoriaKohde
            {
                IdmuutoshistoriaKohde = a.IdmuutoshistoriaKohde,
                Nimi = a.Nimi,
                Kuvaus = a.Kuvaus,
                Sijainti = a.Sijainti,
                Tunnus = a.Tunnus,
                IdkohteenTila = a.IdkohteenTila,
                Muokattu = a.Muokattu,
                KohdeIdkohde = a.KohdeIdkohde,
                KayttajaIdkayttaja = a.KayttajaIdkayttaja,

            };
        }

        public static VaatimusDTO VaatimusToDTO(this Vaatimu v)
		{
			return new VaatimusDTO
			{
				Idvaatimus = v.Idvaatimus,
				Kuvaus = v.Kuvaus,
				Pakollisuus = v.Pakollisuus,
				Taytetty = v.Taytetty,
				Idauditointi = v.Idauditointi,
				AuditointiSelite = v.IdauditointiNavigation.Selite
			};
		}

		public static AuditointipohjaDTO AuditointipohjaToDTO(this Auditointipohja a)
		{
			return new AuditointipohjaDTO
			{
				Idauditointipohja = a.Idauditointipohja,
				Selite = a.Selite,
				Luontiaika = a.Luontiaika,
				Idkayttaja = a.Idkayttaja,
				KayttajaNimi = a.IdkayttajaNavigation.Nimi,
				Idkohderyhma = a.Idkohderyhma,
				KohderyhmaNimi = a.IdkohderyhmaNavigation.Nimi,
				//Vaatimuspohjas = a.Vaatimuspohjas,
			};
		}

		public static VaatimuspohjaDTO VaatimuspohjaToDTO(this Vaatimuspohja v)
		{
			return new VaatimuspohjaDTO
			{
				Idvaatimuspohja = v.Idvaatimuspohja,
				Kuvaus = v.Kuvaus,
				Pakollisuus= v.Pakollisuus,	
				Idauditointipohja = v.Idauditointipohja,
				AuditointipohjaSelite = v.IdauditointipohjaNavigation.Selite
			};
		}
    }
}
