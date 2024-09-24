using adres_api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace adres_api.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class Sokaklar : ControllerBase
    {
        private readonly TrAdresContext _context;

        public Sokaklar(TrAdresContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetSokaklar([FromQuery] string? iladi, [FromQuery] string? mahalle, [FromQuery] int? ilkod)
        {
            var query = HttpContext.Request.Query;
            iladi = iladi?.ToUpper() ?? iladi;
            mahalle = mahalle?.ToUpper() + " MAHALLESİ" ?? mahalle;
            if (iladi is not null && mahalle is not null)
            {
                var sokaklar = await _context.Sokaklars
                    .Where(u => u.MahalleAdi == mahalle && u.IlAdi == iladi)
                    .Select(u => new { u.IlAdi, u.IlceAdi, u.MahalleAdi, u.SokakAdi })
                    .ToListAsync();

                return sokaklar.Any() ? Ok(sokaklar) : BadRequest("Yanlış il adı ve mahalle adı kombinasyonu");
            }
            else if (query.TryGetValue("ilkod", out var ilkodValue) && int.TryParse(ilkodValue, out int ilkodd) && query.TryGetValue("mahalle", out var mahalleValuee))
            {
                if (ilkodd < 0 || ilkodd > 81)
                {
                    return BadRequest("İl kodu '0'dan küçük veya '81'den büyük olamaz.");
                }

                var sokaklar = await _context.Sokaklars
                    .Where(u => u.MahalleAdi == mahalle && u.IlId == ilkodd)
                    .Select(u => new { u.IlAdi, u.IlceAdi, u.MahalleAdi, u.SokakAdi })
                    .ToListAsync();

                return sokaklar.Any() ? Ok(sokaklar) : BadRequest("Yanlış ilkod ve mahalle adı kombinasyonu");
            }

            return NotFound("Sorgunuzda bir hata var.");
        }
    }
}