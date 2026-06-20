using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZealandKantine.Service;
using ZealandKantine.Repo;
using ZealandKantine.models;
using System.Text.Json;

namespace ZealandKantine.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ProductService _productService;

        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
        public List<Product> FoodItems { get; set; } = new List<Product>();
        public List<Product> DrinkItems { get; set; } = new List<Product>();

        public IndexModel(ProductService productService)
        {
            _productService = productService;
        }

        // Hent alle produkter og kurv ved indlæsning af siden
        public IActionResult OnGet()
        {
            // Hvis man ikke er logget ind, sender den en til login-siden igen.
            if (HttpContext.Session.GetString("IsLoggedIn") != "true")
            {
                return RedirectToPage("/Login");
            }

            FoodItems = _productService.GetFood();
            DrinkItems = _productService.GetDrinks();
            LoadCartFromSession();
            return Page();

        }
        // Slet produkt metode, kun admin kan se slet knappen
        public IActionResult OnPostDeleteProduct(int id)
        {
            _productService.DeleteProduct(id);
            return RedirectToPage("/Index");
        }

        // Tilføj til kurv metode
        public IActionResult OnPostAddToCart(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            var cart = GetCartFromSession();

            var existingItem = cart.FirstOrDefault(c => c.ProductId == id);
            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                cart.Add(new CartItem
                {
                    ProductId = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = 1,
                    IsDrink = product.IsDrink
                });
            }

            SaveCartToSession(cart);
            return RedirectToPage("/Index");
        }

        // logud metode
        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Index");
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

        // Hent kurven fra session og opdater CartItems
        private void LoadCartFromSession()
        {
            CartItems = GetCartFromSession();
        }

        // Henter det samlede antal varer i kurven
        public int GetCartItemCount()
        {
            var cart = GetCartFromSession();
            return cart.Sum(c => c.Quantity);
        }
    }
}



