using backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using SharedLib;

namespace backend.Controllers
{
	[ApiController]
    [Route("[controller]")]
    public class KohdeController : ControllerBase
    {

        private readonly ILogger<DbHuoltokirjaContext> _logger;
        private readonly DbHuoltokirjaContext _db;

        public KohdeController(ILogger<DbHuoltokirjaContext> logger, DbHuoltokirjaContext db)
        {
            _logger = logger;
            _db = db;
        }

        // Kaikki kohteet
        [HttpGet("/kohde/all")]
        public async Task<ActionResult<IEnumerable<HuoltokohdeDTO>>> GetAll()
        {

            return await _db.Kohdes.OrderBy(a => a.Idkohde).Select(a => Helpers.KohdeToDTO(a)).ToListAsync();

        }

        // Luodaan uusi kohde
        [HttpPost("/kohde")]
        public async Task<IActionResult> LisaaKohde([FromBody] HuoltokohdeDTO t)
        {
            if (false == ModelState.IsValid)
            {
                return BadRequest();
            }

            Kohde newKohde = Helpers.DTOtoKohde(t);

            _db.Kohdes.Add(newKohde);
            await _db.SaveChangesAsync();
            return Ok();
        }

        // haetaan yksi kohde
        [HttpGet("/kohde/{id}")]
        public async Task<IActionResult> GetOne(int id)
        {

            var a = await _db.Kohdes.Where(a => a.Idkohde == id).FirstOrDefaultAsync();

            if (a == null)
            {
                return NotFound();
            }

            else
            {
                return Ok(Helpers.KohdeToDTO(a));
            }
        }

        // tänne sorttausta ja filtteröintiä to be continued
    }
}

