using backend.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SharedLib;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace backend.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class AuditointiController : ControllerBase
	{
		private readonly ILogger<DbHuoltokirjaContext> _logger;
		private readonly DbHuoltokirjaContext _db;
		private readonly IConfiguration _conf;

		public AuditointiController(ILogger<DbHuoltokirjaContext> logger, DbHuoltokirjaContext db, IConfiguration conf)
		{
			_logger = logger;
			_db = db;
			_conf = conf;
		}


		[HttpGet("/auditointi/all")]
		public async Task<IEnumerable<AuditointiDTO>> Get()
		{
			return await _db.Auditointis
				.Include(a => a.IdkohdeNavigation)
				.Include(a => a.IdkayttajaNavigation)
				.Select(a => Helpers.AuditointiToDTO(a)).ToListAsync();
		}

		//[HttpGet("/auditointi/{id}")]
		//public string Get(int id)
		//{
		//	return "value";
		//}

		[HttpPost("/auditointi/add"), Authorize]
		public async Task<ActionResult<Auditointi>> AddAuditointi(AuditointiDTO req)
		{
			int kayttaja = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
			
			Auditointi a = new()
			{
				Luotu = DateTime.Now,
				Selite = req.Selite,
				Lopputulos = req.Lopputulos,
				Idkohde = req.Idkohde,
				Idkayttaja = kayttaja,
				
			};

			_db.Auditointis.Add(a);
			await _db.SaveChangesAsync();

			//muutetaan auditoinnin kohteen tila, jos tarvetta
			var kohde = await _db.Kohdes.Where(i => i.Idkohde == a.Idkohde).FirstOrDefaultAsync();

			if(a.Lopputulos == 0)
			{
				kohde.IdkohteenTila = 2;
			}
			else
			{
				kohde.IdkohteenTila = 1;
			}

			//tallennetaan contextiin!
			

			//Tallennetaan vaatimukset vaatimus-tauluun
			foreach (var item in req.Vaatimukset)
			{
				Vaatimu v = new()
				{
					Kuvaus = item.Kuvaus,
					Pakollisuus = item.Pakollisuus,
					Taytetty = item.Taytetty,
					Idauditointi = a.Idauditointi
				};
				_db.Vaatimus.Add(v);
			}
			await _db.SaveChangesAsync();

			return Ok();
		}

		//[HttpPut("/auditointi/{id}")]
		//public void Put(int id, [FromBody] string value)
		//{
		//}

		//[HttpDelete("/auditointi/{id}")]
		//public async Task<IActionResult> Delete(int id)
		//{
		//return NoContent();
		//}
	}
}
