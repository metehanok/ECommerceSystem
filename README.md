# ?? ECommerceSystem

ASP.NET Core Web API ve saf HTML/JavaScript ile geliþtirilen bir e-ticaret uygulamasýdýr. Proje, **katmanlý mimari**, **Entity Framework Core**, **PostgreSQL** ve **Render** platformu kullanýlarak oluþturulmuþtur.

---

## ?? Live Demo

- ?? **Frontend & API (Tek bir uygulama):** [https://ecommercesystem-1.onrender.com](https://ecommercesystem-1.onrender.com)

---

## ?? Genel Özellikler

- Müþteri kaydý ve giriþ iþlemleri
- Sipariþ oluþturma ve listeleme
- Ürün listeleme (manuel SQL giriþi ile destekleniyor)
- SQL Azure veritabaný baðlantýsý
- Render üzerinde full-stack deploy
- Basit, anlaþýlýr HTML kullanýcý arayüzü

---

## ?? Proje Katmanlarý

```bash
ECommerceSystem/
??? Core/       # Entity modelleri ve arayüzler
??? Data/       # DbContext ve Repository katmaný
??? Service/    # Ýþ kurallarý ve servis sýnýflarý
??? WebAPI/     # API Controller'larý ve endpoint'ler
??? wwwroot/    # HTML, CSS ve JS dosyalarý
