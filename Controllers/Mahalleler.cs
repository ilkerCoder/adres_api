using adres_api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace adres_api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class MahallelerController : ControllerBase
{
    private readonly TrAdresContext _context;

    public MahallelerController(TrAdresContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetMahalleler([FromQuery] string? iladi, [FromQuery] string? ilce, [FromQuery] int? ilkod)
    {
        ilce = ilce?.ToUpper() ?? ilce;
        iladi = iladi?.ToUpper() ?? iladi;
        // Eğer 'iladi' ve 'ilce' parametreleri gönderildiyse
        if (!string.IsNullOrEmpty(iladi) && !string.IsNullOrEmpty(ilce))
        {
            var mahalleler = await _context.Mahallelers
                .Where(u => u.IlAdi == iladi && u.IlceAdi == ilce)
                .Select(u => new { u.IlAdi, u.IlceAdi, u.MahalleAdi })
                .ToListAsync();

            return mahalleler.Any() ? Ok(mahalleler) : BadRequest("Yanlış il adı ve ilçe adı kombinasyonu.");
        }
        // Eğer 'ilkod' parametresi gönderildiyse ve geçerli bir integer ise
        else if (ilkod.HasValue && ilkod >= 1 && ilkod <= 81 && !string.IsNullOrEmpty(ilce))
        {
            var mahalleler = await _context.Mahallelers
                .Where(u => u.IlId == ilkod && u.IlceAdi == ilce)
                .Select(u => new { u.IlAdi, u.IlceAdi, u.MahalleAdi })
                .ToListAsync();

            return mahalleler.Any() ? Ok(mahalleler) : BadRequest("Girdiğiniz ilceye ait mahalle bulunamadı");
        }

        // Eğer sorgu parametreleri hatalıysa
        if (iladi == null && ilkod == null && ilce == null)
        {
            return BadRequest("En az bir sorgu parametresi girmelisiniz: 'iladi', 'ilkod' veya 'ilce'.");
        }

        return BadRequest("Geçersiz sorgu parametreleri. 'iladi', 'ilkod' ve 'ilce' parametreleri kabul edilir.");
    }
}
