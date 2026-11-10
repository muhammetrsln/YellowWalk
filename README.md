# 🟡 YellowWalk

İstanbul için etkileşimli bir şehir keşfi ve rota planlama web uygulaması. Haritada gerçek zamanlı **"Fog of War" (savaş sisi)** mekaniği vardır: şehri gezdikçe sis dağılır, yürüyüş veya sürüş rotaları planlayabilir ve yolculuklarını arşivleyebilirsin.

## Özellikler

- 🌫️ Fog of War haritası: Gezdiğin bölgelerin üzerindeki sis açılır
- 🚶🚗 Yürüyüş ve sürüş rotası planlama
- 🔎 Adres ve yer arama (Photon ve Nominatim)
- 💾 Rotaları veritabanına kaydetme, listeleme ve silme
- 🗺️ OpenStreetMap altlığı üzerinde Leaflet haritası

## Teknolojiler

| Katman | Teknoloji |
| --- | --- |
| Backend | ASP.NET Core MVC (.NET 9) |
| Veritabanı | SQL Server, Entity Framework Core 9 |
| Harita | Leaflet, OpenStreetMap |
| Arayüz | Bootstrap, jQuery, Razor Views |

## Kurulum

### Gereksinimler
- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- SQL Server veya SQL Server Express (varsayılan örnek: `.\SQLEXPRESS`)

### Adımlar

```bash
git clone https://github.com/muhammetrsln/YellowWalk.git
cd YellowWalk
```

1. `appsettings.json` içindeki `ConnectionStrings:DefaultConnection` değerini kendi SQL Server bilgilerine göre düzenle.
2. Veritabanını oluştur:
   ```bash
   dotnet tool install --global dotnet-ef
   dotnet ef database update
   ```
3. Uygulamayı çalıştır:
   ```bash
   dotnet run
   ```
4. Terminalde yazan adresi tarayıcıda aç.

## API

| Metot | Adres | Açıklama |
| --- | --- | --- |
| GET | `/api/routes` | Kayıtlı tüm rotaları getirir |
| GET | `/api/routes/{id}` | Tek bir rotayı getirir |
| POST | `/api/routes` | Yeni rota kaydeder |
| DELETE | `/api/routes/{id}` | Rotayı siler |

## Proje Yapısı

```
Controllers/   MVC ve API controller'ları
Models/        Veri modelleri ve DTO'lar
Data/          AppDbContext (EF Core)
Migrations/    Veritabanı migration'ları
Views/         Razor görünümleri
wwwroot/       Statik dosyalar (CSS, JS, kütüphaneler)
```

## Kullanılan Dış Servisler

- [OpenStreetMap](https://www.openstreetmap.org/) harita karoları: © OpenStreetMap katkıda bulunanları
- [OSM Routing](https://routing.openstreetmap.de/) rota hesaplama
- [Photon](https://photon.komoot.io/) ve [Nominatim](https://nominatim.openstreetmap.org/) adres arama
- [Leaflet](https://leafletjs.com/)

Bu servislerin kullanım politikalarına uyulmalıdır. Yoğun kullanım için kendi sunucunu kurman önerilir.

## Lisans

Bu proje [MIT Lisansı](LICENSE) ile lisanslanmıştır.
