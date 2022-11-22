using backend.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
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
			if (id != null)
			{
				var kayttaja = await _db.Kayttajas.Where(i => i.Idkayttaja == id).FirstOrDefaultAsync();
				if (kayttaja != null)
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

		[HttpGet("/kayttaja/omatTiedot"), Authorize]
		public async Task<ActionResult<KayttajaDTO>> GetOwnData()
		{
			var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (id == null)
			{
				return BadRequest("Käyttäjää ei löydy");
			}
			var kayttaja = await _db.Kayttajas.Where(i => i.Idkayttaja == int.Parse(id)).FirstOrDefaultAsync();

			return Ok(kayttaja);

		}

		[HttpGet("/kayttaja/kayttajatunnukset")]
		public async Task<IEnumerable<KayttajaDTO>> GetUsernames()
		{

			return await _db.Kayttajas.OrderByDescending(i => i.Idkayttaja > 0).Select(i => Helpers.KayttajaToDTO(i)).ToListAsync();

		}

		[HttpPut("/kayttaja/muokkaa/{id}"), Authorize]
		public async Task<IActionResult> EditUser(KayttajaDTO item)
		{
			var k = await _db.Kayttajas.Where(i => i.Idkayttaja == item.Idkayttaja).FirstOrDefaultAsync();

			if (k == null)
			{
				return NotFound("käyttäjää ei löydy");
			}

			else if (k.Rooli == "admin")
			{
				return BadRequest("käyttäjän tietoja ei voi muokata");
			}

			k.Rooli = item.Rooli;
			k.Poistettu = item.Poistettu;

			_db.Kayttajas.Update(k);
			await _db.SaveChangesAsync();

			return Ok(k);
		}

		[HttpDelete("/kayttaja/poista/{id}")]
		public async Task<IActionResult> Delete(int? id)
		{

			var kayttaja = await _db.Kayttajas.Where(i => i.Idkayttaja == id).FirstOrDefaultAsync();

			if (kayttaja != null)
			{
				_db.Kayttajas.Remove(kayttaja);
				await _db.SaveChangesAsync();
				return NoContent();
			}
			return NotFound("käyttäjää ei löydy");


		}
	}
}
