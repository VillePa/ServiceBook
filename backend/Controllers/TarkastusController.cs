using System.Security.Claims;
using backend.Data;
using Microsoft.AspNetCore.Authorization;
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
                .Include(t => t.IdkayttajaNavigation)
                .Include(t => t.IdkohdeNavigation)
                .Select(t => Helpers.TarkastusToDTO(t)).ToListAsync();
            }
            else if (sortOrder.Equals("asc"))
            {
                return await _db.Tarkastus
                .Include(t => t.IdkayttajaNavigation)
                .Include(t => t.IdkohdeNavigation)
                .OrderBy(t => t.Aikaleima)
                .Select(t => Helpers.TarkastusToDTO(t)).ToListAsync();
            }
            else if (sortOrder.Equals("desc"))
            {
                return await _db.Tarkastus
                .Include(t => t.IdkayttajaNavigation)
                .Include(t => t.IdkohdeNavigation)
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
                .Include(t => t.IdkayttajaNavigation)
                .Include(t => t.IdkohdeNavigation)
                .Where(t => t.Idkohde == id)
                .Select(t => Helpers.TarkastusToDTO(t))
                .ToListAsync();
            }
            else if (sortOrder.Equals("asc"))
            {
                return await _db.Tarkastus
                .Include(t => t.IdkayttajaNavigation)
                .Include(t => t.IdkohdeNavigation)
                .Where(t => t.Idkohde == id)
                .OrderBy(t => t.Aikaleima)
                .Select(t => Helpers.TarkastusToDTO(t))
                .ToListAsync();
            }
            else if (sortOrder.Equals("desc"))
            {
                return await _db.Tarkastus
                .Include(t => t.IdkayttajaNavigation)
                .Include(t => t.IdkohdeNavigation)
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
                .Include(t => t.IdkayttajaNavigation)
                .Include(t => t.IdkohdeNavigation)
                .Where(t => t.Idtarkastus == id).FirstOrDefaultAsync();

            if (t == null)
            {
                return NotFound();
            }
            else return Ok(Helpers.TarkastusToDTO(t));
        }
        
        // Luodaan uusi tarkastus
        [HttpPost("/tarkastus"), Authorize]
        public async Task<IActionResult> LisaaTarkastus([FromBody] TarkastusDTO t)
        {

            // Tarkistetaan käyttäjä
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (id == null)
            {
                return BadRequest("Käyttäjää ei löydy");
            }
            var kayttaja = await _db.Kayttajas.Where(i => i.Idkayttaja == int.Parse(id)).FirstOrDefaultAsync();

            if (kayttaja == null)
            {
                return BadRequest("Käyttäjää ei löydy");
            }

            // Luodaan uusi tarkastus
            if (false == ModelState.IsValid)
            {
                return BadRequest();
            }

            t.Idkayttaja = int.Parse(id);

            Tarkastu newTarkastus = Helpers.DTOtoTarkastu(t);

            _db.Tarkastus.Add(newTarkastus);
            await _db.SaveChangesAsync();

            // Muokataan kohteen tila
            var tilanMuutos = t.TilanMuutos;

            

            if (tilanMuutos != 0)
            {
                var kohde = await _db.Kohdes.FindAsync(t.Idkohde);

                if (kohde.IdkohteenTila != tilanMuutos)
                {
                    kohde.IdkohteenTila = tilanMuutos;
                    _db.Kohdes.Update(kohde);
                    await _db.SaveChangesAsync();
                }
            }


            return Ok();
        }

        
    }
}
