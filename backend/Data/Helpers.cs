using SharedLib;
namespace backend.Data

{
    public static class Helpers
    {
        public static KayttajaDTO KayttajaToDTO(this Kayttaja item)
        {
            return new KayttajaDTO
            {
                //Idkayttaja = item.Idkayttaja,
                Nimi = item.Nimi,
                Kayttajatunnus = item.Kayttajatunnus,
                Luotu = item.Luotu,
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
                //KohteenNimi = t.Kohde.Nimi
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
                Tila = a.Tila,
                Luotu = a.Luotu,
                Idkayttaja = a.Idkayttaja,
                Idkohderyhma = a.Idkohderyhma

            };
        }

    }
}
