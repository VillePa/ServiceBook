using backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using SharedLib;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KayttajaController : ControllerBase
    {
        private readonly ILogger<DbHuoltokirjaContext> _logger;
        private readonly DbHuoltokirjaContext _db;

        public KayttajaController(ILogger<DbHuoltokirjaContext> logger, DbHuoltokirjaContext db)
        {
            _logger = logger;   
            _db = db;   
        }

        [HttpGet("/api/all")]
        public async Task<IEnumerable<Kayttaja>> Get()
        {

            return await _db.Kayttajas.OrderByDescending(i => i.Idkayttaja < 0).ToListAsync();

        }

        [HttpGet("/api/kayttajatunnukset")]
        public async Task<IEnumerable<KayttajaDTO>> GetUsernames()
        {

            return await _db.Kayttajas.OrderByDescending(i=>i.Idkayttaja > 0).Select(i=>Helpers.KayttajaToDTO(i)).ToListAsync();    

            //var result = await _db.Kayttajas.OrderByDescending(i => i.Idkayttaja < 0).Select(i=>
            //    new Helpers.KayttajaDTO()
            //    {
            //        Nimi = i.Nimi,
            //        Kayttajatunnus = i.Kayttajatunnus,  
            //        Luotu= i.Luotu,
            //    }).ToListAsync();

            //if (result.Count == 0) return NotFound("Käyttäjiä ei löytynyt");

            //return Ok(result);

        }


    }
}
