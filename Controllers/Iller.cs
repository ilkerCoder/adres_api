using adres_api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace adres_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IllerController : ControllerBase
    {
        private readonly TrAdresContext _context;

        public IllerController(TrAdresContext context)
        {
            _context = context;
        }

        // Tüm illeri veya ilkod parametresiyle belirtilen ili döndüren metot
        [HttpGet]
        public async Task<IActionResult> GetIller([FromQuery] int? ilkod)
        {
            // ilkod parametresi varsa belirli bir ili döndür
            if (ilkod.HasValue)
            {
                var il = await _context.Illers.FirstOrDefaultAsync(u => u.IlId == ilkod.Value);
                if (il != null)
                {
                    return Ok(il); // İl bulunduysa döndür
                }
                return NotFound(); // İl bulunamadıysa
            }

            // ilkod yoksa tüm illeri döndür
            var iller = await _context.Illers.ToListAsync();
            if (iller.Any())
            {
                return Ok(iller); // İller bulunduysa döndür
            }

            return NotFound(); // İller bulunamadıysa
        }
    }
}
