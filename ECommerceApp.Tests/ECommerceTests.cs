using NUnit.Framework;
using ECommerceApp.Core;

namespace ECommerceApp.Tests
{
    [TestFixture]
    public class ECommerceTests
    {
        private Cart _cart;
        private OrderService _orderService;
        private Product _product;

        [SetUp]
        public void Setup()
        {
            _cart = new Cart();
            _orderService = new OrderService();
            _product = new Product { Id = 1, Name = "Laptop", Price = 50m, Stock = 5 };
        }

        // ==========================================
        // 1. BLACK BOX TESTS (Girdi/Çıktı Testleri)
        // ==========================================

        [Test] // Test Case 1: PASS
        public void AddProduct_IncreasesCartItemCount()
        {
            _cart.AddProduct(_product);
            Assert.That(_cart.Items.Count, Is.EqualTo(1));
        }

        [Test] // Test Case 2: FAIL (BUG 1 yakalandı)
        public void AddProduct_WithZeroStock_ShouldNotBeAdded()
        {
            var outOfStockProduct = new Product { Id = 2, Name = "Mouse", Price = 10m, Stock = 0 };
            
            _cart.AddProduct(outOfStockProduct);
            
            // Stok 0 olduğu için sepete eklenememesi gerekir ama kod ekliyor.
            Assert.That(_cart.Items.Count, Is.EqualTo(0), "Stokta olmayan ürün sepete eklenememeli.");
        }

        [Test] // Test Case 3: PASS
        public void PlaceOrder_EmptyCart_ReturnsFalse()
        {
            bool result = _orderService.PlaceOrder(_cart);
            Assert.That(result, Is.False);
        }

        // ==========================================
        // 2. WHITE BOX TESTS (İç Mantık/Dal Testleri)
        // ==========================================

        [Test] // Test Case 4: PASS
        public void GetTotal_Under100_ReturnsExactSum()
        {
            _cart.AddProduct(new Product { Price = 40m });
            _cart.AddProduct(new Product { Price = 50m }); // Toplam 90

            Assert.That(_cart.GetTotal(), Is.EqualTo(90m));
        }

        [Test] // Test Case 5: FAIL (BUG 2 yakalandı)
        public void GetTotal_Over100_ReturnsExactSum_WithoutWrongDiscount()
        {
            _cart.AddProduct(new Product { Price = 60m });
            _cart.AddProduct(new Product { Price = 50m }); // Toplam 110 olmalı
            
            // Kodda "total > 100 ise 10 çıkar" bug'ı olduğu için 100 dönecek ve test fail olacak.
            Assert.That(_cart.GetTotal(), Is.EqualTo(110m), "Sepet tutarı yanlış hesaplanıyor.");
        }

        [Test] // Test Case 6: PASS
        public void ClearCart_RemovesAllItems()
        {
            _cart.AddProduct(_product);
            _cart.Clear();
            Assert.That(_cart.Items.Count, Is.EqualTo(0));
        }

        // ==========================================
        // 3. GRAY BOX TESTS (Durum/Veri Testleri)
        // ==========================================

        [Test] // Test Case 7: FAIL (BUG 3 yakalandı)
        public void PlaceOrder_Success_ReducesProductStock()
        {
            _cart.AddProduct(_product);
            _orderService.PlaceOrder(_cart);

            // Başarılı sipariş sonrası stok 4'e düşmeli ama düşmüyor.
            Assert.That(_product.Stock, Is.EqualTo(4), "Sipariş sonrası ürün stoğu düşmedi.");
        }

        [Test] // Test Case 8: PASS
        public void ProcessPayment_ValidAmount_ReturnsTrue()
        {
            bool result = _orderService.ProcessPayment(50m);
            Assert.That(result, Is.True);
        }

        // ==========================================
        // 4. INTEGRATION TESTS (Modüller Arası Uyum)
        // ==========================================

        [Test] // Test Case 9: PASS
        public void FullECommerceFlow_ShouldCompleteSuccessfully()
        {
            _cart.AddProduct(_product);
            bool orderResult = _orderService.PlaceOrder(_cart);
            
            Assert.That(orderResult, Is.True);
        }

        [Test] // Test Case 10: FAIL (BUG 4 yakalandı)
        public void PlaceOrder_Success_ShouldClearCartAutomatically()
        {
            _cart.AddProduct(_product);
            _orderService.PlaceOrder(_cart);

            // Sipariş verilince sepetin otomatik boşalması gerekir ama Clear() çağrılmadığı için fail olur.
            Assert.That(_cart.Items.Count, Is.EqualTo(0), "Sipariş tamamlandıktan sonra sepet temizlenmedi.");
        }
    }
}