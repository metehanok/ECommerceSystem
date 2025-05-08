# ?? ECommerceSystem

ASP.NET Core Web API ve saf HTML/JavaScript ile geli�tirilen bir e-ticaret uygulamas�d�r. Proje, **katmanl� mimari**, **Entity Framework Core**, **PostgreSQL** ve **Render** platformu kullan�larak olu�turulmu�tur.

---

## ?? Live Demo

- ?? **Frontend & API (Tek bir uygulama):** [https://ecommercesystem-1.onrender.com](https://ecommercesystem-1.onrender.com)

---

## ?? Genel �zellikler

- M��teri kayd� ve giri� i�lemleri
- Sipari� olu�turma ve listeleme
- �r�n listeleme (manuel SQL giri�i ile destekleniyor)
- SQL Azure veritaban� ba�lant�s�
- Render �zerinde full-stack deploy
- Basit, anla��l�r HTML kullan�c� aray�z�

---

## ?? Proje Katmanlar�

```bash
ECommerceSystem/
??? Core/       # Entity modelleri ve aray�zler
??? Data/       # DbContext ve Repository katman�
??? Service/    # �� kurallar� ve servis s�n�flar�
??? WebAPI/     # API Controller'lar� ve endpoint'ler
??? wwwroot/    # HTML, CSS ve JS dosyalar�
