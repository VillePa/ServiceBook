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

    }
}
