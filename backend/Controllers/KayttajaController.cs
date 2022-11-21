using backend.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SharedLib;
using System.Security.Claims;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KayttajaController : ControllerBase
    {
        private readonly ILogger<DbHuoltokirjaContext> _logger;
        private readonly DbHuoltokirjaContext _db;

        public KayttajaController(ILogger<DbHuoltokirjaContext> logger, DbHuoltokirjaContext db)
        {
            _logger = logger;   
            _db = db;   
        }

        [HttpGet("/kayttaja/kaikki")]
        public async Task<IEnumerable<Kayttaja>> Get()
        {

            return await _db.Kayttajas.OrderByDescending(i => i.Idkayttaja < 0).ToListAsync();

        }

        [HttpGet("/kayttaja/kayttajatunnukset")]
        public async Task<IEnumerable<KayttajaDTO>> GetUsernames()
        {

            return await _db.Kayttajas.OrderByDescending(i=>i.Idkayttaja > 0).Select(i=>Helpers.KayttajaToDTO(i)).ToListAsync();    

        }

       


    }
}
