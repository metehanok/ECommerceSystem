# 🛒 ECommerceSystem

ASP.NET Core Web API ve saf HTML/JavaScript ile geliştirilen bir e-ticaret uygulamasıdır. Proje, **katmanlı mimari**, **Entity Framework Core**, **SQL** ve **Render** platformu kullanılarak oluşturulmuştur.

---

## 🌐 Live Demo

- 🔗 **Frontend & API (Tek bir uygulama):** [https://ecommercesystem-1.onrender.com](https://ecommercesystem-1.onrender.com)

---

## 📌 Genel Özellikler

- Müşteri kaydı ve giriş işlemleri
- Sipariş oluşturma ve listeleme
- Ürün listeleme (manuel SQL girişi ile destekleniyor)
- SQL Azure veritabanı bağlantısı
- Render üzerinde full-stack deploy
- Basit, anlaşılır HTML kullanıcı arayüzü

---

## 🧱 Proje Katmanları

```bash
ECommerceSystem/
├── Core/       # Entity modelleri ve arayüzler
├── Data/       # DbContext ve Repository katmanı
├── Service/    # İş kuralları ve servis sınıfları
├── WebAPI/     # API Controller'ları ve endpoint'ler
├── wwwroot/    # HTML, CSS ve JS dosyaları
