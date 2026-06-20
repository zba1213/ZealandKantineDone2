using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZealandKantine.models;
using ZealandKantine.Service;

namespace ZealandKantine.Pages
{
    public class CartModel : PageModel
    {
        private readonly ProductService _productService;

        public CartModel(ProductService productService)
        {
            _productService = productService;
        }

        // Egenskaber til at holde kurvens indhold 
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
        public decimal TotalPrice { get; set; }

        // Hent kurven og totalprisen ved indlæsning af siden
        public void OnGet()
        {
            LoadCart();
        }

        // Opdater mængden af et produkt i kurven (den bruger du når du vil ændre altallet af et produkt i kurven.
        public IActionResult OnPostUpdateQuantity(int productId, int quantity)
        {
            var cart = GetCartFromSession();
            var item = cart.FirstOrDefault(c => c.ProductId == productId);
            if (item != null)
            {
                if (quantity <= 0)
                {
                    cart.Remove(item);
                }
                else
                {
                    item.Quantity = quantity;
                }
                SaveCartToSession(cart);
            }
            return RedirectToPage();
        }

        // Fjern et enkelt produkt fra kurven
        public IActionResult OnPostRemoveFromCart(int productId)
        {
            var cart = GetCartFromSession();
            var item = cart.FirstOrDefault(c => c.ProductId == productId);
            if (item != null)
            {
                cart.Remove(item);
                SaveCartToSession(cart);
            }
            return RedirectToPage();
        }

        // her Tømmer den kurven
        public IActionResult OnPostClearCart()
        {
            SaveCartToSession(new List<CartItem>());
            return RedirectToPage();
        }

        // Opdater kurven og totalprisen/effektiv totalpris
        private void LoadCart()
        {
            CartItems = GetCartFromSession();

            // Bruger EffectiveTotalPrice i stedet for TotalPrice
            TotalPrice = 0;
            foreach (var item in CartItems)
            {
                TotalPrice += item.EffectiveTotalPrice; 
            }
        }

        // Hent kurven fra session
        private List<CartItem> GetCartFromSession()
        {
            var cartJson = HttpContext.Session.GetString("Cart");
            if (string.IsNullOrEmpty(cartJson))
            {
                return new List<CartItem>();
            }
            return JsonSerializer.Deserialize<List<CartItem>>(cartJson) ?? new List<CartItem>();
        }

        // Gem kurven i session
        private void SaveCartToSession(List<CartItem> cart)
        {
            var cartJson = JsonSerializer.Serialize(cart);
            HttpContext.Session.SetString("Cart", cartJson);
        }



    }
}
