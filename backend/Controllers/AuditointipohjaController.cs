using backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using SharedLib;
using Microsoft.AspNetCore.Authorization;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuditointipohjaController : ControllerBase
    {
        private readonly ILogger<DbHuoltokirjaContext> _logger;
        private readonly DbHuoltokirjaContext _db;
        private readonly IConfiguration _conf;


        public AuditointipohjaController(ILogger<DbHuoltokirjaContext> logger, DbHuoltokirjaContext db, IConfiguration conf)
        {
            _logger = logger;
            _db = db;
            _conf = conf;
		}

		[HttpGet("/auditointipohja/all")]
        public async Task<IEnumerable<AuditointipohjaDTO>> Get()
        {

            return await _db.Auditointipohjas
                .Include(k=>k.IdkayttajaNavigation)
                .Include(k=>k.IdkohderyhmaNavigation)
                .Select(k=> Helpers.AuditointipohjaToDTO(k)).ToListAsync();

        }

	
		[HttpPost("/auditointipohja/add")]
        public async Task<ActionResult<AuditointipohjaDTO>> Add(AuditointipohjaDTO req)
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (req == null || id == null)
            {
                return BadRequest("tiedot tyhjät");
            }
            else
            {
                Auditointipohja a = new()
                {
                    Selite = req.Selite,
                    Luontiaika = DateTime.Now,
                    Idkayttaja = int.Parse(id),
                    Idkohderyhma = req.Idkohderyhma,
                };
                _db.Auditointipohjas.Add(a);
                await _db.SaveChangesAsync();
                
                return Ok(a);   

            }
        }

		[HttpPut("/auditointipohja/edit"), Authorize]
		public async Task<IActionResult> EditAuditointipohja(AuditointipohjaDTO req)
		{
			var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var v = await _db.Auditointipohjas.Where(i => i.Idauditointipohja == req.Idauditointipohja).FirstOrDefaultAsync();

			if (req == null || v == null)
			{
				return BadRequest("ei tietoja");
			}

            v.Idauditointipohja = v.Idauditointipohja;
			v.Selite = req.Selite;
            v.Luontiaika = DateTime.Now;
			v.Idkayttaja = int.Parse(id);
            v.Idkohderyhma = req.Idkohderyhma;

			_db.Auditointipohjas.Update(v);
			await _db.SaveChangesAsync();

			return Ok(v);

		}

		[HttpDelete("/auditointipohja/{id}")]
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return BadRequest("id:tä ei annettu");
			}

			var a = await _db.Auditointipohjas.Where(i => i.Idauditointipohja == id).FirstOrDefaultAsync();

			if (a == null)
			{
				return BadRequest("tietoja ei löydy");
			}

			_db.Auditointipohjas.Remove(a);
			await _db.SaveChangesAsync();
            return NoContent(); 
		}


	}
}
