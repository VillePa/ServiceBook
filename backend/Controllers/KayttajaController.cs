using backend.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SharedLib;
using System.Security.Claims;

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

        [HttpGet("/kayttaja/kaikki")]
        public async Task<IEnumerable<Kayttaja>> Get()
        {

            return await _db.Kayttajas.OrderByDescending(i => i.Idkayttaja < 0).ToListAsync();

        }

		[HttpGet("/kayttaja/{id}")]
		public async Task<IActionResult> GetSingle(int id)
		{
			if(id != null)
            {
				var kayttaja = await _db.Kayttajas.Where(i => i.Idkayttaja == id).FirstOrDefaultAsync();
                if(kayttaja != null)
                {
					return Ok(Helpers.KayttajaToDTO(kayttaja));
                }
                else
                {
                    return BadRequest("käyttäjää ei löydy");
                }
			}

			else
            {
				return BadRequest("käyttäjää ei löydy");

			}





		}

		[HttpGet("/kayttaja/kayttajatunnukset")]
        public async Task<IEnumerable<KayttajaDTO>> GetUsernames()
        {

            return await _db.Kayttajas.OrderByDescending(i=>i.Idkayttaja > 0).Select(i=>Helpers.KayttajaToDTO(i)).ToListAsync();    

        }

        [HttpPost("/kayttaja/poistaKaytosta"), Authorize]
        public async Task<ActionResult<bool>> SoftDelete(int value)
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var kayttaja = await _db.Kayttajas.Where(i => i.Idkayttaja == int.Parse(id)).FirstOrDefaultAsync();

            kayttaja.Poistettu = value;

            _db.Kayttajas.Update(kayttaja);
            await _db.SaveChangesAsync();

            return true;

        }

       


    }
}
