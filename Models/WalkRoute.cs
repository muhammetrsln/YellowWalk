namespace YellowWalk.Models;

// ============================================================
// WalkRoute — Veritabanındaki "WalkRoutes" tablosunun C# karşılığı
//
// Entity Framework (EF Core) bu sınıfa bakıp tabloyu OTOMATİK oluşturur.
// Her property = tablodaki bir sütun.
// SSMS'te açınca göreceğin tablo tam olarak bu alanlardan oluşur.
// ============================================================
public class WalkRoute
{
    public int Id { get; set; }                     // PRIMARY KEY (IDENTITY) — EF "Id" adından anlar

    public string Name { get; set; } = "";           // Rota adı (Örn: "Abimle Fatih turu")

    public DateTime WalkDate { get; set; }           // Yürüyüşün yapıldığı gün

    public string TravelMode { get; set; } = "foot"; // foot / car / ... (hangi modla gidildi)

    public double DistanceMeters { get; set; }       // Toplam mesafe (metre)

    public double DurationSeconds { get; set; }      // Tahmini süre (saniye)

    // Rotanın haritadaki çizgisi: [[lat,lng],[lat,lng],...] şeklinde JSON metin.
    // Binlerce nokta olabildiği için ayrı tablo yerine tek bir NVARCHAR(MAX) sütunda tutuyoruz.
    public string GeometryJson { get; set; } = "[]";

    public DateTime CreatedAt { get; set; } = DateTime.Now; // Kayıt zamanı

    // İLİŞKİ: Bir rotanın birden fazla durağı var (1'e çok / one-to-many)
    // SQL'deki FOREIGN KEY ilişkisinin C# tarafı
    public List<RouteStop> Stops { get; set; } = new();
}
