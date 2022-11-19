﻿using backend.Data;
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
        public async Task<ActionResult<IEnumerable<TarkastusDTO>>> HaeKaikki()
        {

            return await _db.Tarkastus.OrderByDescending(t => t.Aikaleima).Select(t => new TarkastusDTO
            {
                Idtarkastus = t.Idtarkastus,
                Aikaleima = t.Aikaleima,
                Syy = t.Syy,
                Havainnot = t.Havainnot,
                TilanMuutos = t.TilanMuutos,
                Idkohde = t.Idkohde,
                Idkayttaja = t.Idkayttaja,
            }).ToListAsync();

        }

        // Kaikki tarkastukset nouosevassa järjestyksessä
        [HttpGet("/tarkastus/kaikki/nouseva")]
        public async Task<ActionResult<IEnumerable<TarkastusDTO>>> HaeKaikkiNousevaAika()
        {

            return await _db.Tarkastus.OrderBy(t => t.Aikaleima).Select(t => new TarkastusDTO
            {
                Idtarkastus = t.Idtarkastus,
                Aikaleima = t.Aikaleima,
                Syy = t.Syy,
                Havainnot = t.Havainnot,
                TilanMuutos = t.TilanMuutos,
                Idkohde = t.Idkohde,
                Idkayttaja = t.Idkayttaja,
            }).ToListAsync();

        }

        // Kaikki kohteen tarkastukset
        [HttpGet("/tarkastus/kohde/{id}")]
        public async Task<ActionResult<IEnumerable<TarkastusDTO>>> HaeKaikkiKohteenTarkastukset(int id)
        {

            return await _db.Tarkastus.Where(t => t.Idkohde == id).OrderByDescending(t => t.Aikaleima).Select(t => new TarkastusDTO
            {
                Idtarkastus = t.Idtarkastus,
                Aikaleima = t.Aikaleima,
                Syy = t.Syy,
                Havainnot = t.Havainnot,
                TilanMuutos = t.TilanMuutos,
                Idkohde = t.Idkohde,
                Idkayttaja = t.Idkayttaja,
            }).ToListAsync();

        }

        // Haetaan yksittäinen tarkastus

        [HttpGet("/tarkastus/{id}")]
        public async Task<IActionResult> HaeYksiTarkastus(int id)
        {

            var t = await _db.Tarkastus.Where(t => t.Idtarkastus == id).FirstOrDefaultAsync();

            if (t == null)
            {
                return NotFound();
            }
            else return Ok(new TarkastusDTO
            {
                Idtarkastus = t.Idtarkastus,
                Aikaleima = t.Aikaleima,
                Syy = t.Syy,
                Havainnot = t.Havainnot,
                TilanMuutos = t.TilanMuutos,
                Idkohde = t.Idkohde,
                Idkayttaja = t.Idkayttaja,
            });
        }

        // Luodaan uusi tarkastus
        [HttpPost("/tarkastus")]
        public async Task<IActionResult> LisaaTarkastus([FromBody] TarkastusDTO t)
        {
            if (false == ModelState.IsValid)
            {
                return BadRequest();
            }

            Tarkastu newTarkastus = new Tarkastu
            {
                Idtarkastus = t.Idtarkastus,
                Aikaleima = t.Aikaleima,
                Syy = t.Syy,
                Havainnot = t.Havainnot,
                TilanMuutos = t.TilanMuutos,
                Idkohde = t.Idkohde,
                Idkayttaja = t.Idkayttaja,
            };

            _db.Tarkastus.Add(newTarkastus);
            await _db.SaveChangesAsync();
            return Ok();
        }


    }
}
