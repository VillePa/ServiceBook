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

        // Kaikki tarkastukset
        [HttpGet("/kohde/all")]
        public async Task<ActionResult<IEnumerable<HuoltokohdeDTO>>> GetAll()
        {

            return await _db.Kohdes.OrderBy(a => a.Idkohde).Select(a => Helpers.KohdeToDTO(a)).ToListAsync();

        }
    }
}

