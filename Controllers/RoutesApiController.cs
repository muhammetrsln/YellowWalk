using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YellowWalk.Data;
using YellowWalk.Models;

namespace YellowWalk.Controllers;

// ============================================================
// RoutesApiController — Haritanın veritabanıyla konuştuğu API
//
// Bu bir "Web API" controller'ı: HTML sayfa değil, JSON veri döndürür.
// Tarayıcıdaki JavaScript fetch() ile bu adreslere istek atar:
//
//   GET    /api/routes       → Tüm kayıtlı rotaları getir     (SELECT)
//   GET    /api/routes/5     → 5 numaralı rotayı getir         (SELECT ... WHERE Id = 5)
//   POST   /api/routes       → Yeni rota kaydet                (INSERT)
//   DELETE /api/routes/5     → 5 numaralı rotayı sil           (DELETE)
// ============================================================
[ApiController]
[Route("api/routes")]
public class RoutesApiController : ControllerBase
{
    private readonly AppDbContext _db;

    // Dependency Injection: ASP.NET, Program.cs'te kaydettiğimiz AppDbContext'i
    // buraya otomatik verir (new AppDbContext() yazmamıza gerek yok)
    public RoutesApiController(AppDbContext db)
    {
        _db = db;
    }

    // GET /api/routes
    [HttpGet]
    public async Task<ActionResult<List<RouteDto>>> GetAll()
    {
        var routes = await _db.WalkRoutes
            .Include(r => r.Stops)               // Durakları da getir (SQL JOIN)
            .OrderByDescending(r => r.WalkDate)
            .ThenByDescending(r => r.Id)
            .AsNoTracking()
            .ToListAsync();

        return routes.Select(ToDto).ToList();
    }

    // GET /api/routes/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<RouteDto>> GetById(int id)
    {
        var route = await _db.WalkRoutes
            .Include(r => r.Stops)
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == id);

        if (route == null) return NotFound();
        return ToDto(route);
    }

    // POST /api/routes
    [HttpPost]
    public async Task<ActionResult<RouteDto>> Create(SaveRouteRequest request)
    {
        // Basit doğrulama (validation)
        if (string.IsNullOrWhiteSpace(request.Name))
            return BadRequest("Rota adı boş olamaz.");
        if (request.Stops.Count < 2 || request.Geometry.Count < 2)
            return BadRequest("Rota en az 2 durak ve çizilmiş bir yol içermeli.");

        var route = new WalkRoute
        {
            Name = request.Name.Trim(),
            WalkDate = request.WalkDate.Date,
            TravelMode = request.TravelMode,
            DistanceMeters = request.DistanceMeters,
            DurationSeconds = request.DurationSeconds,
            GeometryJson = JsonSerializer.Serialize(request.Geometry),
            CreatedAt = DateTime.Now,
            Stops = request.Stops.Select((s, i) => new RouteStop
            {
                OrderIndex = i + 1,
                Name = s.Name,
                Latitude = s.Lat,
                Longitude = s.Lng
            }).ToList()
        };

        _db.WalkRoutes.Add(route);
        await _db.SaveChangesAsync();   // ← Burada SQL Server'a INSERT gider

        return CreatedAtAction(nameof(GetById), new { id = route.Id }, ToDto(route));
    }

    // DELETE /api/routes/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var route = await _db.WalkRoutes.FindAsync(id);
        if (route == null) return NotFound();

        _db.WalkRoutes.Remove(route);   // Durakları CASCADE ile otomatik silinir
        await _db.SaveChangesAsync();
        return NoContent();
    }

    // Veritabanı nesnesini (Entity) tarayıcıya gidecek pakete (DTO) çevir
    private static RouteDto ToDto(WalkRoute r) => new()
    {
        Id = r.Id,
        Name = r.Name,
        WalkDate = r.WalkDate,
        TravelMode = r.TravelMode,
        DistanceMeters = r.DistanceMeters,
        DurationSeconds = r.DurationSeconds,
        Geometry = JsonSerializer.Deserialize<List<double[]>>(r.GeometryJson) ?? new(),
        Stops = r.Stops
            .OrderBy(s => s.OrderIndex)
            .Select(s => new StopDto { Name = s.Name, Lat = s.Latitude, Lng = s.Longitude })
            .ToList()
    };
}
