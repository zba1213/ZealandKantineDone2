using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ZealandKantine.Pages
{
    public class loginModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; }

        // BindProperty gør det muligt at binde form data til disse properties, så vi kan få fat i brugernavn og password når formen submittes
        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        // OnGet tjekker om brugeren allerede er logget ind, hvis ja, send til forsiden
        public IActionResult OnGet()
        {
            // Hvis brugeren allerede er logget ind, send til forsiden
            if (HttpContext.Session.GetString("IsLoggedIn") == "true")
            {
                return RedirectToPage("/Index");
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            // her sker det egentlige login, tjekker hardcoded brugere og sætter session variabler
            if (Username == "admin" && Password == "admin123")
            {
                HttpContext.Session.SetString("IsLoggedIn", "true");
                HttpContext.Session.SetString("IsAdmin", "true");
                HttpContext.Session.SetString("Username", Username);
                HttpContext.Session.SetString("UserRole", "Admin");

                return RedirectToPage("/Index");
            }
            // USER LOGIN
            else if (Username == "user" && Password == "user123")
            {
                HttpContext.Session.SetString("IsLoggedIn", "true");
                HttpContext.Session.SetString("IsAdmin", "false");
                HttpContext.Session.SetString("Username", Username);
                HttpContext.Session.SetString("UserRole", "User");

                return RedirectToPage("/Index");
            }
            else
            {
                ErrorMessage = "Ugyldigt brugernavn eller adgangskode!";
                return Page();
            }
        }

        // logud metode, ryd session og send til login
        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Login");
        }
    }


}



