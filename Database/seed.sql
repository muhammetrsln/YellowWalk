-- YellowWalk ornek veri (opsiyonel)
-- Once `dotnet ef database update` ile tablolari olusturun, sonra bu dosyayi
-- SSMS'te acip calistirin. Veritabani adi appsettings.json icindeki ile ayni olmalidir.
USE YellowWalkDb;
GO

IF NOT EXISTS (SELECT 1 FROM WalkRoutes WHERE Name = N'Örnek: Galata - Ayasofya')
BEGIN
    INSERT INTO WalkRoutes (Name, WalkDate, TravelMode, DistanceMeters, DurationSeconds, GeometryJson, CreatedAt)
    VALUES (
        N'Örnek: Galata - Ayasofya',
        '2026-01-01',
        N'foot',
        3200,
        2700,
        N'[[41.0256,28.9744],[41.0230,28.9738],[41.0200,28.9733],[41.0170,28.9700],[41.0125,28.9745],[41.0086,28.9802]]',
        SYSDATETIME()
    );

    DECLARE @RouteId INT = SCOPE_IDENTITY();

    INSERT INTO RouteStops (WalkRouteId, OrderIndex, Name, Latitude, Longitude)
    VALUES
        (@RouteId, 1, N'Galata Kulesi', 41.0256, 28.9744),
        (@RouteId, 2, N'Galata Köprüsü', 41.0200, 28.9733),
        (@RouteId, 3, N'Eminönü', 41.0170, 28.9700),
        (@RouteId, 4, N'Ayasofya', 41.0086, 28.9802);
END
GO
