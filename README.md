# 🛒 Çok Katmanlı E-Ticaret Platformu

![.NET Core](https://img.shields.io/badge/.NET%20Core-512BD4?style=flat&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=flat&logo=c-sharp&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=flat&logo=microsoft-sql-server&logoColor=white)

Bu proje, **ASP.NET Core** kullanılarak **N-Tier (Çok Katmanlı)** mimari yapısına uygun olarak geliştirilmiş kapsamlı bir E-Ticaret sitesidir. SOLID prensipleri gözetilerek, test edilebilir ve ölçeklenebilir bir yapı hedeflenmiştir.

## 🏗️ Mimari Yapı

Proje aşağıdaki katmanlardan oluşmaktadır:

* **Core Layer (Varlık Katmanı):** Entity'ler, DTO'lar ve ortak arayüzler.
* **Data Access Layer (Veri Erişim Katmanı):** Entity Framework Core konfigürasyonları, Repository Pattern uygulamaları ve Migrations işlemleri.
* **Business Layer (İş Katmanı):** İş kuralları, validasyonlar ve servisler.
* **WebUI / API Layer:** Kullanıcı arayüzü (MVC) veya dış dünyaya açılan API endpointleri.

## 🚀 Kullanılan Teknolojiler ve Kütüphaneler

* **Dil:** C#
* **Framework:** ASP.NET Core 7.0 / 8.0 
* **Veritabanı:** MS SQL Server
* **ORM:** Entity Framework Core 
* **Frontend :** Bootstrap 5, jQuery, HTML5/CSS3.

## 📺 Proje Demo ve Tanıtım Videosu

Projenin kullanıcı arayüzü, sepet/satın alma süreçleri, ürün ekleme akışı ve Admin Paneli (kullanıcı, kategori, marka yönetimi) süreçlerinin detaylı anlatımını aşağıdaki görsele tıklayarak YouTube üzerinden izleyebilirsiniz:

[![Proje Tanıtım Videosu](https://img.youtube.com/vi/R61LVILHP6k/0.jpg)](https://www.youtube.com/watch?v=R61LVILHP6k)

### 🔑 Öne Çıkan Özellikler (Video İçeriği)
* **Kullanıcı Akışı:** Ürün listeleme, detay inceleme, dinamik sepet işlemleri ve sipariş tamamlama adımları `[00:00:21]`.
* **Ürün Yükleme & Güvenlik:** Kullanıcıların kendi yükledikleri ürünleri satın alamaması ve yeni eklenen ürünlerin admin onayına kadar pasif kalması `[00:01:00]`.
* **Admin Paneli Yetenekleri:** * Bekleyen ürünlerin incelenmesi, resim yüklenmesi ve aktifleştirilmesi `[00:03:15]`.
  * Kullanıcı yönetimi (Silme, listeleme ve roller) `[00:04:04]`.
  * Hiyerarşik Kategori yönetimi (Alt/Üst kategori eşlemeleri) `[00:04:57]`.
  * Dinamik marka ve ana sayfa slider yönetimi `[00:05:29]`.
