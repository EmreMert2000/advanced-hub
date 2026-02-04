# Teknik Mülakat Soru Bankası

Bu doküman HTML, CSS, JavaScript ve SQL konularında sıkça sorulan mülakat sorularını ve kısa cevaplarını içerir.

## 1. HTML (HyperText Markup Language)

### **S: Semantic (Anlamsal) HTML nedir ve neden önemlidir?**

**C:** Semantic HTML, etiketlerin içeriğin anlamını taşıdığı yapıdır. Örneğin `<div>` yerine `<header>`, `<nav>`, `<article>`, `<footer>` kullanmak.

- **Önemi:** SEO (Arama Motoru Optimizasyonu) için daha iyidir, erişilebilirlik (ekran okuyucular) sağlar ve kodun okunabilirliğini artırır.

### **S: `localStorage`, `sessionStorage` ve `cookie` arasındaki farklar nelerdir?**

- **localStorage:** Veri tarayıcı kapansa bile silinmez. Kapasitesi büyüktür (~5-10MB). Sunucuya otomatik gönderilmez.
- **sessionStorage:** Veri sadece sekme açık olduğu sürece saklanır.
- **Cookie:** Hem sunucu hem istemci tarafından okunabilir. Her HTTP isteğinde sunucuya gider. Kapasitesi küçüktür (~4KB). Süresi dolunca silinir.

---

## 2. CSS (Cascading Style Sheets)

### **S: CSS Box Model (Kutu Modeli) nedir?**

**C:** Her HTML elementi bir kutu olarak kabul edilir. İçten dışa doğru 4 katman vardır:

1. **Content:** İçerik (Metin, resim vb.)
2. **Padding:** İçerik ile kenarlık arasındaki iç boşluk.
3. **Border:** Kenarlık.
4. **Margin:** Kutunun diğer kutularla arasındaki dış boşluk.

### **S: `position` değerleri nelerdir?**

- `static`: Varsayılan. Akışa uyar.
- `relative`: Normal konumuna göre yer değiştirir ama yerini korur.
- `absolute`: En yakın `relative` atasına göre konumlanır, akıştan çıkar.
- `fixed`: Ekrana (viewport) göre sabitlenir.
- `sticky`: Belirli bir kaydırma noktasına kadar relative, sonra fixed gibi davranır.

---

## 3. JavaScript

### **S: `var`, `let` ve `const` farkları nelerdir?**

- **var:** Function scope'tur. Hoisting (yukarı taşınma) vardır, tanımlanmadan önce kullanılırsa `undefined` döner. Tekrar tanımlanabilir.
- **let:** Block scope'tur. Hoisting vardır ama "Temporal Dead Zone" nedeniyle erişilemez. Değeri güncellenebilir.
- **const:** Block scope'tur. Değeri sonradan değiştirilemez (sabit).

### **S: Closure (Kapanım) nedir?**

**C:** Bir fonksiyonun, kendi kapsamı dışındaki değişkenlere (lexical scope) o fonksiyon çalışmayı bitirse bile erişebilme yeteneğidir. Hafızada veri tutmak veya private değişken simülasyonu için kullanılır.

### **S: Asenkron JavaScript nasıl çalışır (Promise, Async/Await)?**

JS tek thread'lidir. Asenkron işlemler (API istekleri, timerlar) Event Loop mekanizmasıyla yönetilir.

- **Callback:** Eski yöntem, "Callback Hell" sorunu vardır.
- **Promise:** Başarılı (resolve) veya hatalı (reject) olacağını garanti eden nesne. `.then()` ve `.catch()` ile yönetilir.
- **Async/Await:** Promise yapısının daha okunabilir, senkron gibi yazılan halidir.

---

## 4. SQL (Structured Query Language)

### **S: SQL'de `JOIN` türleri nelerdir?**

- **INNER JOIN:** Her iki tabloda da eşleşen kayıtları getirir.
- **LEFT JOIN:** Sol tablodaki tüm kayıtları, sağ tablodan ise eşleşenleri getirir (yoksa NULL).
- **RIGHT JOIN:** Sağ tablodaki tüm kayıtları, soldan eşleşenleri getirir.
- **FULL JOIN:** Eşleşme olsun olmasın iki tablodaki tüm kayıtları getirir.

### **S: `GROUP BY` ve `HAVING` nedir?**

- **GROUP BY:** Verileri belirli bir kolona göre gruplar (Örn: Her departmandaki çalışan sayısı).
- **HAVING:** Gruplanmış verilere filtre uygular (`WHERE` gruplamadan önce, `HAVING` sonra çalışır).

### **S: Index nedir ve neden kullanılır?**

**C:** Veritabanında arama işlemlerini hızlandıran bir veri yapısıdır (kitabın indeksi gibi). Okuma hızını artırır ancak yazma (INSERT/UPDATE) hızını biraz düşürebilir ve diskte yer kaplar.

### **S: ACID Prensipleri nedir?**

- **Atomicity:** İşlem ya tam yapılır ya hiç yapılmaz.
- **Consistency:** Veri tutarlılığı korunur.
- **Isolation:** İşlemler birbirini etkilemez.
- **Durability:** İşlem tamamlandığında veri kalıcı hale gelir.
