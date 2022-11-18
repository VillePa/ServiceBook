using backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HuoltokirjaController : ControllerBase
    {
        private readonly ILogger<DbHuoltokirjaContext> _logger;
        private readonly DbHuoltokirjaContext _db;

        public HuoltokirjaController(ILogger<DbHuoltokirjaContext> logger, DbHuoltokirjaContext db)
        {
            _logger = logger;   
            _db = db;   
        }

        [HttpGet("/api/all")]
        public async Task<IEnumerable<Kayttaja>> Get()
        {

            return await _db.Kayttajas.OrderByDescending(i => i.Idkayttaja < 0).ToListAsync();

        }


    }
}
