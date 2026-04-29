
# 🛒 ECommerceApp Test Automation

C# ve NUnit kullanılarak geliştirilmiş temel bir e-ticaret simülasyonu. Proje, sistem içerisine bilerek yerleştirilmiş mantıksal hataları (bug) barındırmakta ve bu hataları 4 farklı test stratejisi (Black Box, White Box, Gray Box, Integration) ile yakalayan bir otomasyon altyapısı sunmaktadır.

## 🖥️✨ Test Çıktıları & Akış

### 🧪 Terminal Test Sonuçları (Özet)

```text
NUnit Adapter 5.0.0.0: Test execution started
Running all tests in ECommerceApp.Tests.dll
NUnit3TestExecutor discovered 11 of 11 NUnit test cases...

Test özeti: toplam: 11; başarısız: 4; başarılı: 7; atlandı: 0; süre: 1,4s
❌ 4 test bilinen bug'ları yakalayarak FAIL verdi.
✅ 7 test normal akışta PASS verdi.
🎯 Sistemdeki bilinen hatalar testlerle başarılı bir şekilde raporlanmıştır.

🔍 Uygulanan Test Stratejileri
1. Black Box (Girdi/Çıktı Testleri): Stok kontrolü ve sepet doğrulama senaryoları.

2. White Box (İç Mantık Testleri): Sepet toplam tutarındaki indirim hesaplama hataları.

3. Gray Box (Durum Testleri): Sipariş sonrası veritabanı/stok güncellemelerinin kontrolü.

4. Integration Test (Modüller Arası Uyum): Sepet, ürün ve sipariş servislerinin uçtan uca etkileşimi.

🔧 Kullanılan Temel Sınıflar
🛒 Cart.cs (İş Mantığı)

C#
public class Cart
{
    // BUG 1: Stokta olmayan ürün sepete eklenebiliyor.
    public void AddProduct(Product product) { ... }

    // BUG 2: 100 TL üzeri alışverişlerde yanlış indirim uygulanıyor.
    public decimal GetTotal() { ... }
}
📦 OrderService.cs (Sipariş Yönetimi)

C#
public class OrderService
{
    // BUG 3: Sipariş sonrası stok düşmüyor.
    // BUG 4: Sipariş tamamlandıktan sonra sepet temizlenmiyor.
    public bool PlaceOrder(Cart cart) { ... }
}
🧱 Sistemdeki her bir hata, NUnit test sınıflarında Assert metodlarıyla yakalanmıştır.

⚙️ Uygulama Özellikleri
🛰️ Ürün nesnelerini ve stok durumlarını modelleme
🔄 E-ticaret sepet yönetimi (Ekleme, Toplam Tutar, Temizleme)
📁 Ödeme ve Sipariş süreçleri simülasyonu
📅 Toplam 11 adet test senaryosu (Test Cases)
🔎 Bilinçli hataların otomasyon ile tespiti (NUnit Asserts)
🧾 Hangi testin neden kaldığını (fail) açıklayan Markdown rapor çıktısı

📂 Proje Klasör Yapısı
Plaintext
ECommerceApp
│
├── ECommerceApp.Core/
│   ├── Product.cs
│   ├── Cart.cs
│   └── OrderService.cs
│
├── ECommerceApp.Tests/
│   └── ECommerceTests.cs
│
├── ECommerceApp.sln
└── Rapor.md
📦 Kodlar ve testler Separation of Concerns prensibine uygun ayrılmıştır.

👤 Geliştirici Bilgileri
Ceyhun Emre Şener
📘 Yazılım Geliştirme Vize Ödevi kapsamında hazırlanmıştır.
