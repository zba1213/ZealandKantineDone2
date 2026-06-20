using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZealandKantine.models;
using ZealandKantine.Service;

namespace ZealandKantine.Pages
{
    public class AddDrinkModel : PageModel
    {
        private readonly ProductService _service;
        public AddDrinkModel(ProductService service)
        {
            _service = service;
        }

        [BindProperty]
        public Product Product { get; set; }

        
        public IActionResult OnGet()
        {
            if(HttpContext.Session.GetString("IsAdmin") != "true")
            {
                return RedirectToPage("/Login");
            }

            Product = new Product();
            return Page();
        }

        // tejkker først om man er admin og så tillder den tilføje en drink og sendes til index
        public IActionResult OnPost()
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
            {
                return RedirectToPage("/Login");
            }
            if (ModelState.IsValid)
            {
                _service.AddProduct(Product.Name, Product.Price, true, Product.Quantity);
                return RedirectToPage("/Index");
            }
            return Page();
        }
    }
}
