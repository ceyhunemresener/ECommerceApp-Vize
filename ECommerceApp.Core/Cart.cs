using System.Collections.Generic;
using System.Linq;

namespace ECommerceApp.Core
{
    public class Cart
    {
        private List<Product> _items = new List<Product>();
        public IReadOnlyList<Product> Items => _items.AsReadOnly();

        public void AddProduct(Product product)
        {
            // BUG 1: Stok kontrolü yok. Stokta olmayan (0) ürün sepete eklenebiliyor.
            _items.Add(product);
        }

        public decimal GetTotal()
        {
            decimal total = _items.Sum(item => item.Price);
            
            // BUG 2: Mantık hatası. Sepet tutarı 100 TL'yi geçerse yanlış hesaplama yapıyor (10 TL eksik döndürüyor).
            if (total > 100) 
            {
                return total - 10; 
            }
            return total;
        }

        public void Clear()
        {
            _items.Clear();
        }
    }
}