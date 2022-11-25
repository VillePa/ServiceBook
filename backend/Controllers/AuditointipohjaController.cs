using backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using SharedLib;

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
        public async Task<IEnumerable<Auditointipohja>> Get()
        {

            return await _db.Auditointipohjas.OrderByDescending(i => i.Idauditointipohja).ToListAsync();

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


    }
}
