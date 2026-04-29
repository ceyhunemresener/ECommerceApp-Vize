namespace ECommerceApp.Core
{
    public class OrderService
    {
        public bool PlaceOrder(Cart cart)
        {
            if (cart.Items.Count == 0)
                return false;

            decimal total = cart.GetTotal();
            bool paymentSuccess = ProcessPayment(total);

            if (paymentSuccess)
            {
                // BUG 3: Sipariş başarılı olunca ürünlerin stoğu düşmüyor.
                // BUG 4: Sipariş tamamlandıktan sonra sepet temizlenmiyor (cart.Clear() çağrılmıyor).
                return true;
            }
            return false;
        }

        public bool ProcessPayment(decimal amount)
        {
            return amount > 0;
        }
    }
}