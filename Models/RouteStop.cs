namespace YellowWalk.Models;

// ============================================================
// RouteStop — "RouteStops" tablosu
// Bir rotadaki her önemli durak (Kariye Camii, Fatih Camii, Eminönü...)
// ============================================================
public class RouteStop
{
    public int Id { get; set; }

    public int WalkRouteId { get; set; }        // FOREIGN KEY → WalkRoutes.Id
    public WalkRoute? WalkRoute { get; set; }   // Navigation property (ilişkili rota)

    public int OrderIndex { get; set; }         // Kaçıncı durak (1, 2, 3...)

    public string Name { get; set; } = "";      // Mekan adı

    public double Latitude { get; set; }        // Enlem
    public double Longitude { get; set; }       // Boylam
}
