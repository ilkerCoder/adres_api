using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using adres_api.Data;
using System.ComponentModel.Design.Serialization;

namespace adres_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IlcelerController : ControllerBase
    {
        private readonly TrAdresContext _context;

        public IlcelerController(TrAdresContext context)
        {
            _context = context;
        }

        // İl adı veya il id'ye göre ilçeleri döndüren metot
        [HttpGet]
        public async Task<IActionResult> GetIlceler([FromQuery] string? iladi, [FromQuery] int? ilkod)
        {
            if (!string.IsNullOrEmpty(iladi))
            {
                iladi = iladi?.ToUpper() ?? iladi;
                // İl adı ile ilçeleri getir
                var ilceler = await _context.Ilcelers
                    .Where(u => u.IlAdi == iladi)
                    .Select(u => new { u.IlAdi, u.IlceAdi })
                    .ToListAsync();

                if (ilceler.Any())
                {
                    return Ok(ilceler);
                }
                return NotFound();
            }
            else if (ilkod.HasValue)
            {
                // İl ID ile ilçeleri getir
                var ilceler = await _context.Ilcelers
                    .Where(u => u.IlId == ilkod.Value)
                    .Select(u => new { u.IlAdi, u.IlceAdi })
                    .ToListAsync();

                if (ilceler.Any())
                {
                    return Ok(ilceler);
                }
                return NotFound();
            }
            else
            {
                // Parametre yoksa tüm ilçeleri getir
                var ilceler = await _context.Ilcelers
                    .Select(u => new { u.IlAdi, u.IlceAdi })
                    .ToListAsync();

                if (ilceler.Any())
                {
                    return Ok(ilceler);
                }
                return NotFound();
            }
        }
    }
}
