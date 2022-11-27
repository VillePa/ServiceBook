using backend.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedLib;
using System.Security.Claims;

namespace backend.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class VaatimusController : ControllerBase
	{
		private readonly ILogger<DbHuoltokirjaContext> _logger;
		private readonly DbHuoltokirjaContext _db;
		private readonly IConfiguration _conf;

		public VaatimusController(ILogger<DbHuoltokirjaContext> logger, DbHuoltokirjaContext db, IConfiguration conf)
		{
			_logger = logger;
			_db = db;
			_conf = conf;
		}

		[HttpGet("/vaatimus/all")]
		public async Task<IEnumerable<Vaatimu>> Get()
		{
			return await _db.Vaatimus.OrderBy(i => i.Idvaatimus).ToListAsync();
		}


		[HttpPost("/vaatimus/add")]
		public async Task<ActionResult<Vaatimu>> Add(VaatimusDTO req)
		{
			//var id = User.FindFirstValue(ClaimTypes.NameIdentifier);

			if (req == null)
			{
				return BadRequest("tiedot tyhjät");
			}
			else
			{
				Vaatimu v = new()
				{
					Kuvaus = req.Kuvaus,
					Pakollisuus = req.Pakollisuus,
					Taytetty = req.Taytetty,
					Idauditointi = req.Idauditointi,
				};
				_db.Vaatimus.Add(v);
				await _db.SaveChangesAsync();

				return Ok(v);

			}
		}
	}
}
