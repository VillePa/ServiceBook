using backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using SharedLib;

namespace backend.Controllers
{
	[ApiController]
    [Route("[controller]")]
    public class TarkastusController : ControllerBase
    {
        private readonly ILogger<DbHuoltokirjaContext> _logger;
        private readonly DbHuoltokirjaContext _db;

        public TarkastusController(ILogger<DbHuoltokirjaContext> logger, DbHuoltokirjaContext db)
        {
            _logger = logger;
            _db = db;
        }


        // Kaikki tarkastukset
        [HttpGet("/tarkastus/kaikki")]
        [HttpGet("/tarkastus/kaikki/{sortOrder?}")]
        public async Task<ActionResult<IEnumerable<TarkastusDTO>>> HaeKaikki(string? sortOrder)
        {

            if (sortOrder == null)
            {
                return await _db.Tarkastus
                .Include(t => t.Kayttaja)
                .Select(t => Helpers.TarkastusToDTO(t)).ToListAsync();
            }
            else if (sortOrder.Equals("asc"))
            {
                return await _db.Tarkastus
                .Include(t => t.Kayttaja)
                .OrderBy(t => t.Aikaleima)
                .Select(t => Helpers.TarkastusToDTO(t)).ToListAsync();
            }
            else if (sortOrder.Equals("desc"))
            {
                return await _db.Tarkastus
                .Include(t => t.Kayttaja)
                .OrderByDescending(t => t.Aikaleima)
                .Select(t => Helpers.TarkastusToDTO(t)).ToListAsync();
            }
            else return BadRequest();

        }

        // Kaikki kohteen tarkastukset
        [HttpGet("/tarkastus/kohde/{id}")]
        [HttpGet("/tarkastus/kohde/{id}/{sortOrder?}")]
        public async Task<ActionResult<IEnumerable<TarkastusDTO>>> HaeKaikkiKohteenTarkastukset(int id, string? sortOrder)
        {
            if (sortOrder == null)
            {
                return await _db.Tarkastus
                .Include(t => t.Kayttaja)
                .Where(t => t.Idkohde == id)
                .Select(t => Helpers.TarkastusToDTO(t))
                .ToListAsync();
            }
            else if (sortOrder.Equals("asc"))
            {
                return await _db.Tarkastus
                .Include(t => t.Kayttaja)
                .Where(t => t.Idkohde == id)
                .OrderBy(t => t.Aikaleima)
                .Select(t => Helpers.TarkastusToDTO(t))
                .ToListAsync();
            }
            else if (sortOrder.Equals("desc"))
            {
                return await _db.Tarkastus
                .Include(t => t.Kayttaja)
                .Where(t => t.Idkohde == id)
                .OrderByDescending(t => t.Aikaleima)
                .Select(t => Helpers.TarkastusToDTO(t))
                .ToListAsync();
            }
            else return BadRequest();
        }

        // Haetaan yksittäinen tarkastus

        [HttpGet("/tarkastus/{id}")]
        public async Task<IActionResult> HaeYksiTarkastus(int id)
        {

            var t = await _db.Tarkastus
                .Include(t => t.Kayttaja)
                .Where(t => t.Idtarkastus == id).FirstOrDefaultAsync();

            if (t == null)
            {
                return NotFound();
            }
            else return Ok(Helpers.TarkastusToDTO(t));
        }
        
        // Luodaan uusi tarkastus
        [HttpPost("/tarkastus")]
        public async Task<IActionResult> LisaaTarkastus([FromBody] TarkastusDTO t)
        {
            if (false == ModelState.IsValid)
            {
                return BadRequest();
            }

            Tarkastu newTarkastus = Helpers.DTOtoTarkastu(t);

            _db.Tarkastus.Add(newTarkastus);
            await _db.SaveChangesAsync();
            return Ok();
        }

        
    }
}
