using Microsoft.EntityFrameworkCore;
using ZealandKantine.Repo;
using ZealandKantine.Service;

namespace ZealandKantine
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

         
            builder.Services.AddRazorPages();


            builder.Services.AddSession(); // denne linje er vigtigt da den tilføjer session support


            // Add services to the container.
            builder.Services.AddRazorPages();

            // Registrer din database context med andre ord den henter connection string fra appsettings.json og bruger den til at konfigurere Entity Framework
            builder.Services.AddDbContext<ZealandDBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Registrer dine services og repositories
            builder.Services.AddScoped<ProductRepo>();
            builder.Services.AddScoped<ProductService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseSession(); // vigtigt også da denne linje aktiverer session middleware

            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapRazorPages()
               .WithStaticAssets();

            app.Run();
        }
    }
}
