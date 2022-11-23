using SharedLib;
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
				KayttajanNimi = t.Kayttaja.Nimi,
				KohteenNimi = "Kohteen nimi"
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
				//Tila = a.Tila,
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
				//Tila = a.Tila,
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

    }
}
