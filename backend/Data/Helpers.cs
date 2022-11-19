using SharedLib;
namespace backend.Data

{
    public static class Helpers
    {
        public static KayttajaDTO ToDTO(this Kayttaja item)
        {
            return new KayttajaDTO
            {
                //Idkayttaja = item.Idkayttaja,
                Nimi = item.Nimi,
                Kayttajatunnus = item.Kayttajatunnus,
                Salasana = item.Salasana.ToString(),
                //Luotu = item.Luotu,
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
            };
        }

        // tarkastusDTO to Tarkastus class
        public static Tarkastu DTOtoTarkastu(this TarkastusDTO t)
        {
            return new Tarkastu
            {
                Idtarkastus = t.Idtarkastus,
                Aikaleima = t.Aikaleima,
                Syy = t.Syy,
                Havainnot = t.Havainnot,
                TilanMuutos = t.TilanMuutos,
                Idkohde = t.Idkohde,
                Idkayttaja = t.Idkayttaja,
            };
        }

    }
}
