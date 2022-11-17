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
        private readonly ILogger<SqldbContext> _logger;
        private readonly SqldbContext _db;

        public HuoltokirjaController(ILogger<SqldbContext> logger, SqldbContext db)
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
