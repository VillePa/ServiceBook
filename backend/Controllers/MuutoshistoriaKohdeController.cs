using backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using SharedLib;
using System;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MuutoshistoriaKohdeController : ControllerBase
    {

        private readonly ILogger<DbHuoltokirjaContext> _logger;
        private readonly DbHuoltokirjaContext _db;

        public MuutoshistoriaKohdeController(ILogger<DbHuoltokirjaContext> logger, DbHuoltokirjaContext db)
        {
            _logger = logger;
            _db = db;
        }

        // Yhden kohteen kaikki muutokset/koko muutoshistoria
        [HttpGet("/history/{id}/all")]
        public async Task<ActionResult<IEnumerable<MuutoshistoriaKohdeDTO>>> GetAll(int id)
        {

            return await _db.MuutoshistoriaKohdes.OrderBy(a => a.IdmuutoshistoriaKohde).Where(a => a.KohdeIdkohde == id).Select(a => Helpers.MuutoshistoriaKohdeToDTO(a)).ToListAsync();

        }

        // Luodaan uusi muutos
        [HttpPost("/history")]
        public async Task<IActionResult> LisaaMuutos([FromBody] MuutoshistoriaKohdeDTO t)
        {
            if (false == ModelState.IsValid)
            {
                return BadRequest();
            }

            MuutoshistoriaKohde newMuutoshistoriaKohde = Helpers.DTOtoMuutoshistoriaKohde(t);

            _db.MuutoshistoriaKohdes.Add(newMuutoshistoriaKohde);
            await _db.SaveChangesAsync();
            return Ok();
        }

       

        
    }
}