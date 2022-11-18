using Azure.Core;
using backend.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using SharedLib;
using System.Security.Cryptography;
using System.Text;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<DbHuoltokirjaContext> _logger;
        private readonly DbHuoltokirjaContext _db;

        public AuthController(ILogger<DbHuoltokirjaContext> logger, DbHuoltokirjaContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpPost("register")]
        public async Task<ActionResult<Kayttaja>> Register(KayttajaDTO req)
        {
            var k = await _db.Kayttajas.AnyAsync(n => n.Kayttajatunnus == req.Kayttajatunnus);

            if (k) return BadRequest("Käyttäjätunnus on jo käytössä");

            else
            {
                Kayttaja kayttaja = new();

                //CreatePasswordHash(req.Salasana, out byte[] salasanaHash, out byte[] salasanaSalt);
                var password = CreateHash(req.Salasana);

                kayttaja.Nimi = req.Nimi;
                kayttaja.Kayttajatunnus = req.Kayttajatunnus;
                kayttaja.Salasana = password;
                //kayttaja.SalasanaSalt = Convert.ToBase64String(salasanaSalt);
                kayttaja.Rooli= req.Rooli;
                kayttaja.Luotu = DateTime.Now;



                _db.Kayttajas.Add(kayttaja);
                await _db.SaveChangesAsync();


                return Ok("Uusi käyttäjä rekisteröitiin onnistuneesti! " + kayttaja.Kayttajatunnus +" "+ kayttaja.Salasana);
            }
        }

        private string CreateHash(string salasana)
        {
            var hmac = SHA256.Create();
            var salasanaHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(salasana));

            return Convert.ToBase64String(salasanaHash);

            
        }

        //private void CreatePasswordHash(string salasana, out byte[] salasanaHash, out byte[] salasanaSalt)
        //{
        //    using (var hmac = new HMACSHA256())
        //    {
        //        salasanaSalt = hmac.Key;
        //        salasanaHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(salasana));

        //    }
        //}

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(KayttajaDTO req)
        {
            var kayttaja = await _db.Kayttajas.FirstOrDefaultAsync(i => i.Kayttajatunnus.Equals(req.Kayttajatunnus));

            if (kayttaja == null)
            {
                return BadRequest("Käyttäjätunnusta ei löytynyt");
            }

            var asked = CreateHash(req.Salasana);
            var stored = kayttaja.Salasana;

            if(VerifyPasswordHash(stored, asked))
            {
                return Ok("Kirjautuminen onnistui");
            }

            else return BadRequest("Salasana väärin");


        }

        private bool VerifyPasswordHash(string aSalasana, string kSalasana)
        {
            if (aSalasana.Equals(kSalasana))
                return true;
            else return false;
            
        }


        //[HttpPost("login")]
        //public async Task<ActionResult<string>> Login(KayttajaDTO req)
        //{
        //    var kayttaja = await _db.Kayttajas.FirstOrDefaultAsync(i => i.Kayttajatunnus.Equals(req.Kayttajatunnus));

        //    if (kayttaja == null)
        //    {
        //        return BadRequest("Käyttäjätunnusta ei löytynyt");
        //    }

        //    else if(!VerifyPasswordHash(req.Salasana, Encoding.UTF8.GetBytes(kayttaja.Salasana), Encoding.UTF8.GetBytes(kayttaja.SalasanaSalt)))
        //    {
        //        return BadRequest("Salasana väärin");
        //    }

        //    else
        //    {
        //        return Ok("Kirjautuminen onnistui!");
        //    }

        //}


        //private bool VerifyPasswordHash(string salasana, byte[] salasanaHash, byte[] salasanaSalt)
        //{
        //    using (var hmac = new HMACSHA256(salasanaSalt))
        //    {
        //        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(salasana));
        //        return computedHash.SequenceEqual(salasanaHash);
        //    }
        //}


        [HttpPost("rekkaa")]
        public async Task<ActionResult<Kayttaja>> Rekisteröidy(KayttajaDTO req)
        {
            var k = await _db.Kayttajas.AnyAsync(n => n.Kayttajatunnus == req.Kayttajatunnus);

            if (k) return BadRequest("Käyttäjätunnus on jo käytössä");

            else
            {
                Kayttaja kayttaja = new();

                var pw = BCrypt.Net.BCrypt.HashPassword(req.Salasana);

                kayttaja.Nimi = req.Nimi;
                kayttaja.Kayttajatunnus = req.Kayttajatunnus;
                kayttaja.Salasana = pw;
                //kayttaja.SalasanaSalt = Convert.ToBase64String(salasanaSalt);
                kayttaja.Rooli = req.Rooli;
                kayttaja.Luotu = DateTime.Now;

                _db.Kayttajas.Add(kayttaja);
                await _db.SaveChangesAsync();


                return Ok("Uusi käyttäjä rekisteröitiin onnistuneesti! " + kayttaja.Kayttajatunnus);
            }
        }


    }
}
