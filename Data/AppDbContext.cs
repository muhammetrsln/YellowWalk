using Microsoft.EntityFrameworkCore;
using YellowWalk.Models;

namespace YellowWalk.Data;

// ============================================================
// AppDbContext — Veritabanı ile konuşan köprü sınıf
//
// WinForms'ta SqlConnection + SqlCommand ile elle SQL yazıyordun.
// EF Core'da bu sınıf o işi yapıyor:
//   _db.WalkRoutes.Add(rota);   →  INSERT INTO WalkRoutes ...
//   _db.WalkRoutes.ToList();    →  SELECT * FROM WalkRoutes
//   _db.SaveChanges();          →  Değişiklikleri SQL Server'a gönder
// ============================================================
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // Her DbSet = veritabanında bir tablo
    public DbSet<WalkRoute> WalkRoutes => Set<WalkRoute>();
    public DbSet<RouteStop> RouteStops => Set<RouteStop>();

    // Tablo ayrıntıları (sütun uzunlukları, ilişkiler)
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WalkRoute>(e =>
        {
            e.Property(r => r.Name).HasMaxLength(200).IsRequired();   // NVARCHAR(200) NOT NULL
            e.Property(r => r.TravelMode).HasMaxLength(20);
            e.Property(r => r.GeometryJson).HasColumnType("nvarchar(max)");

            // Rota silinince durakları da silinsin (ON DELETE CASCADE)
            e.HasMany(r => r.Stops)
             .WithOne(s => s.WalkRoute)
             .HasForeignKey(s => s.WalkRouteId)
             .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<RouteStop>(e =>
        {
            e.Property(s => s.Name).HasMaxLength(300).IsRequired();
        });
    }
}
