# İş İlanı Özel Hazırlık Rehberi

Bu doküman, paylaşılan "Satış Öncesi Destek / İş Analisti" ilanı için özel olarak hazırlanmıştır.

## 1. Temel Kavramlar ve Sistemler

### **QMS (Quality Management System)**

- **Nedir:** Organizasyonun kalite hedeflerine ulaşması için gerekli süreçleri yöneten sistemdir (ISO 9001 standartları vb.).
- **Örnek Senaryo:** "Müşterimiz üretim hattındaki hataları azaltmak istiyor. QMS modülümüzle 'Düzeltici/Önleyici Faaliyet (DÖF)' takibi yaparak hataların kök nedenini bulup raporlayabiliriz."

### **DMS (Document Management System)**

- **Nedir:** Elektronik belgelerin (word, pdf, email) saklanması, yönetilmesi ve versiyonlanması.
- **Örnek:** Belge sürüm geçmişi, erişim yetkileri, OCR ile içerik arama, dijital imza entegrasyonu.

### **BPM (Business Process Management)**

- **Nedir:** İş süreçlerinin modellenmesi (akış şemaları), otomasyonu ve optimizasyonu.
- **Örnek Senaryo:** "Satınalma Talep Süreci: Personel talep girer -> Yönetici onaylar -> Satınalma teklif toplar -> Onaylanırsa sipariş verilir."

### **POC (Proof of Concept)**

- **Nedir:** "Kavram Kanıtı". Bir fikrin veya yazılım çözümünün uygulanabilir olduğunu göstermek için yapılan küçük ölçekli, süreli çalışma.
- **Nasıl Yönetilir:**
  1. Kapsam ve başarı kriterleri belirlenir (Müşteri neyi görürse "Başarılı" diyecek?).
  2. Süre kısıtı konur (Örn: 2 hafta).
  3. Kurulum ve veri girişi yapılır.
  4. Sunum/Demo ile kapanış yapılır.

---

## 2. Satış Öncesi (Pre-Sales) Süreçler

### **Demo Sunumu İpuçları**

- **Senaryo Bazlı Anlatım:** "Bu butona basınca bu olur" demek yerine "Ahmet Bey sabah işe geldiğinde onay bekleyen faturalarını burada görür..." şeklinde hikayeleştir.
- **Müşteri İhtiyacına Odaklan:** Analiz aşamasında öğrendiğin "acı noktalarına" (pain points) çözüm sun.
- **Soru Yönetimi:** Bilmediğin teknik bir soru gelirse "Bunu teknik ekibimle teyit edip size dönüş yapacağım" de. Asla yanlış bilgi verme.

### **Analiz ve Gereksinim Toplama**

- **Sorulacak Sorular:**
  - "Mevcut sürecinizde en çok zaman kaybedilen adım hangisi?"
  - "Hangi sistemlerle (ERP, CRM) entegrasyon gerekiyor?"
  - "Hangi raporları günlük/haftalık olarak almak istiyorsunuz?"

---

## 3. SQL Pratikleri (Temel Seviye)

İlanda "Temel seviyede SQL" istendiği için basit veri çekme ve filtreleme işlemlerine hakim olmalısın.

**Örnek Tablo: `Musteriler`**
| ID | Ad | Sehir | Ciro |
|----|-----|-------|------|
| 1 | ABC A.Ş. | İstanbul | 50000 |
| 2 | XYZ Ltd. | Ankara | 25000 |

#### 1. Tüm Müşterileri Listele

```sql
SELECT * FROM Musteriler;
```

#### 2. İstanbul'daki Müşterileri Bul

```sql
SELECT * FROM Musteriler WHERE Sehir = 'İstanbul';
```

#### 3. Cirosu 30.000'den Büyük Olanlar

```sql
SELECT Ad, Ciro FROM Musteriler WHERE Ciro > 30000;
```

#### 4. Toplam Müşteri Sayısı

```sql
SELECT COUNT(*) FROM Musteriler;
```

#### 5. Sıralama (En yüksek cirodan en düşüğe)

```sql
SELECT * FROM Musteriler ORDER BY Ciro DESC;
```

---

## 4. Olası Davranışsal Mülakat Soruları

1. **"Zor bir müşteriyi nasıl idare ettiniz?"**
   - **STAR Tekniği ile Cevapla:** Situation (Durum), Task (Görev), Action (Eylem), Result (Sonuç).
   - "Müşteri entegrasyonun gecikmesinden şikayetçiydi (Durum). Onu sakinleştirmek ve güveni tazelemek gerekiyordu (Görev). Haftalık düzenli durum toplantıları koydum ve şeffaf raporlama yaptım (Eylem). Müşteri projenin kontrol altında olduğunu hissedip rahatladı (Sonuç)."

2. **"Satış ekibi gerçekçi olmayan bir söz verirse ne yaparsın?"**
   - "Satış ekibiyle özel olarak görüşür, teknik kısıtları anlatırım. Müşteriye ise durumu 'Alternatif çözüm' olarak sunarak beklentiyi yönetirim."
