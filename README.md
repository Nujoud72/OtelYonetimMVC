# 🏨 Otel Yönetim Sistemi (OtelYonetimMVC)

Bu repository, **Yazılım Doğrulama ve Geçerleme (V&V)** dersi kapsamında geliştirilen  
**Otel Yönetim Sistemi** MVC tabanlı bir web uygulamasını içermektedir.

Projenin amacı, bir otelin temel operasyonel süreçlerini (oda yönetimi, rezervasyon takibi, ödeme ve check-out işlemleri) dijital ortamda güvenilir ve düzenli bir şekilde yönetmektir.

Proje, **ASP.NET Core MVC mimarisi** kullanılarak geliştirilmiş ve doğrulama & geçerleme süreçleri akademik standartlara uygun şekilde yürütülmüştür.

---

## 📁 Proje İçeriği

- **Controllers/** → MVC controller dosyaları  
- **Models/** → Uygulamada kullanılan veri modelleri  
- **Data/** → Veritabanı bağlantısı ve seed işlemleri  
- **Migrations/** → Entity Framework Core migration dosyaları  
- **OtelYonetimMVC.Tests/** → Unit ve Integration testlerini içeren test projesi  
- **Otelmvc ekranları/** → Uygulamaya ait arayüz ekran görüntüleri  

---

## 🧩 Uygulama Özellikleri

- Kullanıcı giriş ekranı  
- Rol bazlı yetkilendirme (Admin / Resepsiyon)  
- Admin paneli (Dashboard)  
- Oda yönetimi (listeleme, düzenleme, silme)  
- Rezervasyon yönetimi  
- Yeni rezervasyon oluşturma  
- Rezervasyon düzenleme ve silme  
- Ödeme alma işlemleri  
- Check-out işlemleri  
- Kullanıcıdan onay alınan kritik işlemler (silme vb.)

---

## ⚙️ Kullanılan Teknolojiler

Projede aşağıdaki teknolojiler ve araçlar kullanılmıştır:

- **ASP.NET Core MVC** → Uygulama mimarisi  
- **C#** → Sunucu tarafı programlama dili  
- **Entity Framework Core** → ORM ve veritabanı işlemleri  
- **Code First yaklaşımı** → Veritabanı tasarımı  
- **SQL Server / LocalDB** → Veritabanı yönetimi  
- **xUnit** → Unit test framework’ü  
- **Visual Studio** → Geliştirme ortamı  
- **HTML5 / CSS / Bootstrap** → Kullanıcı arayüzü  
- **Git & GitHub** → Versiyon kontrolü ve kaynak kod yönetimi  

---

## 🖼️ Uygulama Ekran Görüntüleri

### Giriş Sayfası
![Giriş Sayfası](Otelmvc%20ekranları/Giriş%20sayfası.png)

### Admin Dashboard
![Admin Dashboard](Otelmvc%20ekranları/Admin%20kısmındaki%20Dashboard%20ekranı.png)

### Admin – Oda Yönetimi
![Admin Oda Yönetimi](Otelmvc%20ekranları/Admin%20kısmındaki%20Oda%20Yönetim%20ekranı.png)

### Admin – Oda Silme
![Admin Oda Silme](Otelmvc%20ekranları/Admin%20kısmındaki%20oda%20silme%20sayfası.png)

### Resepsiyon Dashboard
![Resepsiyon Dashboard](Otelmvc%20ekranları/Resepsiyon%20kısmında%20dashboard%20ekranı.png)

### Resepsiyon – Rezervasyonlar
![Rezervasyonlar](Otelmvc%20ekranları/Resepsiyon%20kısmındaki%20rezervasyonlar%20ekranı.png)

### Resepsiyon – Yeni Rezervasyon
![Yeni Rezervasyon](Otelmvc%20ekranları/Resepsiyon%20kısmında%20yeni%20rezervasyon%20ekleme%20sayfası.png)

### Rezervasyon Düzenleme
![Rezervasyon Düzenleme](Otelmvc%20ekranları/Rezervasyon%20düzenleme%20sayfası.png)

### Rezervasyon Silme
![Rezervasyon Silme](Otelmvc%20ekranları/Rezervasyon%20silme%20sayfası.png)

### Rezervasyon Ödeme Alma
![Ödeme Alma](Otelmvc%20ekranları/Rezervasyon%20ödeme%20alma%20sayfası.png)

---

## 🧪 Test Süreci

Proje kapsamında aşağıdaki test seviyeleri uygulanmıştır:

- **Unit Testler** (xUnit ile)  
- **Integration Testler**  
- **System Testler**  
- **User Acceptance Test (UAT)**  

Test senaryoları ile fonksiyonel gereksinimler,  
**RTM (Requirements Traceability Matrix)** kullanılarak eşleştirilmiştir.

Tüm testler akademik standartlara uygun şekilde raporlanmıştır.

---

## 📌 Notlar

- Bu proje **akademik amaçlıdır**.  
- MVC mimarisi ve katmanlı yapı kullanılmıştır.  
- Test dokümanları ve raporlar, proje klasör yapısına uygun şekilde sunulmuştur.  
- Proje, Yazılım Doğrulama ve Geçerleme (V&V) dersinin gereksinimlerini karşılamaktadır.
