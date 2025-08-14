# Event Manager

Event Manager, etkinliklerin listelenmesi, oluşturulması ve yönetilmesi için geliştirilmiş bir web uygulamasıdır. Uygulama Docker ile konteynerize olarak veya lokal geliştirme ortamında manuel şekilde çalıştırılabilir.

> **Not**: Bu proje değerlendirme amaçlı **FullCalendar Premium** kullanır. Ticari kullanım veya production dağıtımı için uygun değildir.

---

## 🚀 Çalıştırma Yöntemleri

### Yöntem 1: Docker ile Çalıştırma

**Gereksinimler**
- Docker Desktop veya Docker Engine

**Adımlar**
1. Kurulu değilse Docker Desktop’ı yükleyin ve çalıştırın.
2. Proje kök dizininde `.env` dosyası oluşturun. Örnek içerik için `.env.example` dosyasını kopyalayın:
3. Gerekli gördüğünüz ortam değişkenlerini `.env` içinde güncelleyin (varsayılanlarla da çalışabilir).
4. Proje kökünde aşağıdaki komutu çalıştırın:
   docker compose --env-file .env -f ops/docker-compose.yml up --build
5. Build tamamlandıktan sonra uygulamaya şu adresten erişin:
   - http://localhost:5187

> **İpucu**: İlk kurulumda Docker'ın imajları indirip başlatması birkaç dakika sürebilir. Loglarda **Application started. Press Ctrl+C to shut down.** mesajını gördükten sonra uygulamaya erişmeyi deneyin.

---

### Yöntem 2: Lokal Geliştirme Ortamında Çalıştırma

**Gereksinimler**
- SQL Server
- .NET SDK
- Entity Framework Core Tools  
  - Global kurulum:
    dotnet tool install --global dotnet-ef

  - Projeye özel kurulum:
    dotnet tool install --local dotnet-ef

**Adımlar**
1. Uygulama dizinine geçin:
2. `appsettings.json` içindeki **DefaultConnection** değerini kendi SQL Server bağlantınıza göre ayarlayın.
3. Veritabanını oluşturun/güncelleyin:
   dotnet ef database update
4. Uygulamayı başlatın:
   dotnet run
   
   veya geliştirme sırasında canlı yenileme için:
   dotnet watch

---

## 🔧 Sorun Giderme (Kısa)

- **Port meşgul**: 5187 portu doluysa `ASPNETCORE_URLS` veya compose dosyasında port eşlemesini değiştirin.
- **SQL bağlantı hatası**: Connection string değerlerini ve SQL Server’ın çalıştığını kontrol edin.

---

## 🧱 Teknoloji Yığını

- ASP.NET Core (MVC)
- Entity Framework Core
- SQL Server
- Docker & Docker Compose