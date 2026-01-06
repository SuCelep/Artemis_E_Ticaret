# 🛒 Çok Katmanlı E-Ticaret Platformu

![.NET Core](https://img.shields.io/badge/.NET%20Core-512BD4?style=flat&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=flat&logo=c-sharp&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=flat&logo=microsoft-sql-server&logoColor=white)

Bu proje, **ASP.NET Core** kullanılarak **N-Tier (Çok Katmanlı)** mimari yapısına uygun olarak geliştirilmiş kapsamlı bir E-Ticaret sitesidir. SOLID prensipleri gözetilerek, test edilebilir ve ölçeklenebilir bir yapı hedeflenmiştir.

## 🏗️ Mimari Yapı

Proje aşağıdaki katmanlardan oluşmaktadır:

* **Core Layer (Varlık Katmanı):** Entity'ler, DTO'lar ve ortak arayüzler.
* **Data Access Layer (Veri Erişim Katmanı):** Entity Framework Core konfigürasyonları, Repository Pattern uygulamaları ve Migrations işlemleri.
* **Business Layer (İş Katmanı):** İş kuralları, validasyonlar (FluentValidation) ve servisler.
* **WebUI / API Layer:** Kullanıcı arayüzü (MVC) veya dış dünyaya açılan API endpointleri.

## 🚀 Kullanılan Teknolojiler ve Kütüphaneler

* **Dil:** C#
* **Framework:** ASP.NET Core 7.0 / 8.0 (Kullandığın sürümü yaz)
* **Veritabanı:** MS SQL Server
* **ORM:** Entity Framework Core (Code First yaklaşımı)
* **Frontend (Eğer MVC ise):** Bootstrap 5, jQuery, HTML5/CSS3.

## ⚙️ Kurulum ve Çalıştırma

Projeyi yerel makinenizde çalıştırmak için aşağıdaki adımları izleyin:
Projeyi Klonlayın.
appsettings.json dosyasındaki Connection String bilgisini kendi yerel SQL Server ayarlarınıza göre güncelleyin.
Package Manager Console üzerinden veya terminalden aşağıdaki komutu çalıştırarak veritabanını ve tabloları oluşturun.

### 1. Projeyi Klonlayın
```bash
git clone [https://github.com/KULLANICI_ADIN/REPO_ISMI.git](https://github.com/KULLANICI_ADIN/REPO_ISMI.git)
##2. Veritabanı Ayarları
appsettings.json dosyasındaki Connection String bilgisini kendi yerel SQL Server ayarlarınıza göre güncelleyin.

JSON

"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=EticaretDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}

3. Veritabanını Oluşturun (Migration)
Package Manager Console üzerinden veya terminalden aşağıdaki komutu çalıştırarak veritabanını ve tabloları oluşturun:

Bash

update-database
# veya .NET CLI kullanıyorsanız:
dotnet ef database update
