namespace YellowWalk.Models;

// ============================================================
// DTO'lar (Data Transfer Object) — Tarayıcı ile sunucu arasında taşınan veri paketleri
//
// Neden doğrudan WalkRoute kullanmıyoruz?
//  - Tarayıcının Id / CreatedAt gibi alanları kendisi belirlemesini istemiyoruz
//  - WalkRoute ↔ RouteStop döngüsel ilişkisi JSON'a çevirirken sorun çıkarır
// ============================================================

// Tarayıcıdan "rotayı kaydet" isteğiyle gelen veri
public class SaveRouteRequest
{
    public string Name { get; set; } = "";
    public DateTime WalkDate { get; set; }
    public string TravelMode { get; set; } = "foot";
    public double DistanceMeters { get; set; }
    public double DurationSeconds { get; set; }
    public List<double[]> Geometry { get; set; } = new();   // [[lat,lng], ...]
    public List<StopDto> Stops { get; set; } = new();
}

public class StopDto
{
    public string Name { get; set; } = "";
    public double Lat { get; set; }
    public double Lng { get; set; }
}

// Tarayıcıya geri gönderilen kayıtlı rota
public class RouteDto
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public DateTime WalkDate { get; set; }
    public string TravelMode { get; set; } = "foot";
    public double DistanceMeters { get; set; }
    public double DurationSeconds { get; set; }
    public List<double[]> Geometry { get; set; } = new();
    public List<StopDto> Stops { get; set; } = new();
}
