using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZealandKantine.models;
using ZealandKantine.Service;

namespace ZealandKantine.Pages
{
    public class AddItemPageModel : PageModel
    {

        private readonly ProductService _service;

        public AddItemPageModel(ProductService service)
        {
            _service = service;
        }

        [BindProperty]
        public Product Product { get; set; }

        public IActionResult OnGet()
        {
            // hvis ikke admin, send til login
            if (HttpContext.Session.GetString("IsAdmin") != "true")
            {
                return RedirectToPage("/Login");
            }

            Product = new Product();
            return Page();
        }

        public IActionResult OnPost()
        {
            // hvis man ikke er admin, send til login
            if (HttpContext.Session.GetString("IsAdmin") != "true")
            {
                return RedirectToPage("/Login");
            }

            if (ModelState.IsValid)
            {
                _service.AddProduct(Product.Name, Product.Price, false, Product.Quantity);
                return RedirectToPage("/Index");
            }
            return Page();
        }
    


        
    }
}
