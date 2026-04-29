# Vize Projesi Test Raporu

Bu projede temel bir e-ticaret sistemi (Ürün seçimi, Sepet, Sipariş ve Ödeme) geliştirilmiş ve NUnit kullanılarak Unit (White Box, Black Box), Gray Box ve Integration testleri yazılmıştır. Sistemde bilerek bırakılan hatalar (buglar) yazılan test senaryoları ile başarılı bir şekilde yakalanmıştır.

## Test Sonuçları (Toplam 11 Test Case)
- **Başarılı (Pass) Test Sayısı:** 7
- **Başarısız (Fail) Test Sayısı:** 4

---

## Başarısız (Fail) Olan Testler ve Nedenleri

### 1. `AddProduct_WithZeroStock_ShouldNotBeAdded` (Black Box Test)
- **Beklenen Durum:** Stoğu 0 olan bir ürün sepete eklendiğinde sistemin bunu reddetmesi (sepetteki ürün sayısının artmaması).
- **Hata (Bug):** `Cart.cs` içerisindeki `AddProduct` metodunda ürün stoğu kontrol edilmemektedir. Stok 0 olsa bile listeye ekleme yapılmaktadır.

### 2. `GetTotal_Over100_ReturnsExactSum_WithoutWrongDiscount` (White Box Test)
- **Beklenen Durum:** Sepetteki ürünlerin fiyat toplamlarının doğru ve eksiksiz yansıması.
- **Hata (Bug):** `Cart.cs` içerisindeki `GetTotal` metodunda mantıksal bir hata yapılmıştır. Toplam tutar 100'ü geçtiğinde, koda bilerek eklenen `if (total > 100) return total - 10;` bloğu yüzünden sepet tutarı 10 eksik hesaplanmaktadır.

### 3. `PlaceOrder_Success_ReducesProductStock` (Gray Box Test)
- **Beklenen Durum:** Sipariş işlemi başarıyla tamamlandıktan sonra, satın alınan ürünlerin stok miktarının düşmesi.
- **Hata (Bug):** `OrderService.cs` içerisindeki `PlaceOrder` metodunda, ödeme başarılı olduktan sonra sepet içindeki ürünlerin `Stock` property'sini güncelleyen bir kod parçası (iş mantığı) eksiktir.

### 4. `PlaceOrder_Success_ShouldClearCartAutomatically` (Integration Test)
- **Beklenen Durum:** Başarılı bir sipariş ve ödeme akışından sonra müşterinin sepetinin sıfırlanması.
- **Hata (Bug):** `OrderService.cs` içerisindeki `PlaceOrder` metodu başarılı (`true`) dönmeden önce `cart.Clear()` metodunu çağırmayı unutmaktadır. Bu entegrasyon eksikliği nedeniyle sipariş verilmesine rağmen ürünler sepette kalmaya devam etmektedir.