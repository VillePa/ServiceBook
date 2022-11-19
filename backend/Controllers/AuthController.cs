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

        #region Pbkdf2 salasanan cryptaukseen liittyvää 
        const int SaltByteSize = 24;
        const int HashByteSize = 20; // to match the size of the PBKDF2-HMAC-SHA-1 hash 
        const int Pbkdf2Iterations = 1000;
        const int IterationIndex = 0;
        const int SaltIndex = 1;
        const int Pbkdf2Index = 2;
        #endregion

        public AuthController(ILogger<DbHuoltokirjaContext> logger, DbHuoltokirjaContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpPost("register")]
        public async Task<ActionResult<Kayttaja>> Register(RegisterDTO req)
        {
            var k = await _db.Kayttajas.AnyAsync(n => n.Kayttajatunnus == req.Kayttajatunnus);

            if (k) return BadRequest("Käyttäjätunnus on jo käytössä");

            else
            {
                Kayttaja kayttaja = new()
                {
                    Nimi = req.Nimi,
                    Kayttajatunnus = req.Kayttajatunnus,
                    Salasana = HashPassword(req.Salasana),
                    Rooli = "user",
                    Luotu = DateTime.Now
                };

                _db.Kayttajas.Add(kayttaja);
                await _db.SaveChangesAsync();

                return Ok("Uusi käyttäjä "+kayttaja.Kayttajatunnus+"rekisteröitiin onnistuneesti!");
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginDTO req)
        {
            var kayttaja = await _db.Kayttajas.FirstOrDefaultAsync(i => i.Kayttajatunnus.Equals(req.Kayttajatunnus));

            if (kayttaja == null)
            {
                return BadRequest("Käyttäjätunnusta ei löytynyt");
            }

            else if (ValidatePassword(req.Salasana, kayttaja.Salasana))
            {
                return Ok("Kirjautuminen onnistui!");
            }

            else return BadRequest("Salasana väärin");
        }

        //Salttauksen luonti
        private static string GetRandomSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(12);
        }

        //salasanan kryptaus
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, GetRandomSalt());
        }

        //salasanan validointi
        public static bool ValidatePassword(string password, string correctHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, correctHash);
        }

        #region Pbkdf2
        //private string HashPassword(string password)
        //{
        //    var cryptoProvider = new RNGCryptoServiceProvider();
        //    byte[] salt = new byte[SaltByteSize];
        //    cryptoProvider.GetBytes(salt);

        //    var hash = GetPbkdf2Bytes(password, salt, Pbkdf2Iterations, HashByteSize);
        //    return Pbkdf2Iterations + ":" +
        //           Convert.ToBase64String(salt) + ":" +
        //           Convert.ToBase64String(hash);
        //}

        //private static bool SlowEquals(byte[] a, byte[] b)
        //{
        //    var diff = (uint)a.Length ^ (uint)b.Length;
        //    for (int i = 0; i < a.Length && i < b.Length; i++)
        //    {
        //        diff |= (uint)(a[i] ^ b[i]);
        //    }
        //    return diff == 0;
        //}

        //private static byte[] GetPbkdf2Bytes(string password, byte[] salt, int iterations, int outputBytes)
        //{
        //    var pbkdf2 = new Rfc2898DeriveBytes(password, salt);
        //    pbkdf2.IterationCount = iterations;
        //    return pbkdf2.GetBytes(outputBytes);
        //}


        //public static bool ValidatePassword(string password, string correctHash)
        //{
        //    char[] delimiter = { ':' };
        //    var split = correctHash.Split(delimiter);
        //    var iterations = Int32.Parse(split[IterationIndex]);
        //    var salt = Convert.FromBase64String(split[SaltIndex]);
        //    var hash = Convert.FromBase64String(split[Pbkdf2Index]);

        //    var testHash = GetPbkdf2Bytes(password, salt, iterations, hash.Length);
        //    return SlowEquals(hash, testHash);
        //}
        #endregion

        #region HMAC
        //private void CreatePasswordHash(string salasana, out byte[] salasanaHash, out byte[] salasanaSalt)
        //{
        //    using (var hmac = new HMACSHA256())
        //    {
        //        salasanaSalt = hmac.Key;
        //        salasanaHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(salasana));

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
        #endregion
    }
}
