using System;
using Microsoft.EntityFrameworkCore;
using ZealandKantine.Repo;
using ZealandKantine.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddRazorPages();
        builder.Services.AddSession();

        builder.Services.AddDbContext<ZealandDBContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddScoped<ProductRepo>();
        builder.Services.AddScoped<ProductService>();

        var app = builder.Build();

        // Minimal fix: ensure the database exists on startup (development scenario)
        // This will create the database/schema if it doesn't exist.
        // For production or controlled schema evolution use Database.Migrate() and EF migrations.
        using (var scope = app.Services.CreateScope())
        {
            try
            {
                var db = scope.ServiceProvider.GetRequiredService<ZealandDBContext>();
                db.Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Database creation check failed: " + ex.Message);
                throw;
            }
        }

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseSession();
        app.UseRouting();
        app.UseAuthorization();
        app.MapStaticAssets();
        app.MapRazorPages()
           .WithStaticAssets();

        app.Run();
    }
}


//using Microsoft.EntityFrameworkCore;
//using ZealandKantine.Repo;
//using ZealandKantine.Service;

//namespace ZealandKantine
//{
//    public class Program
//    {
//        public static void Main(string[] args)
//        {
//            var builder = WebApplication.CreateBuilder(args);


//            builder.Services.AddRazorPages();


//            builder.Services.AddSession(); // denne linje er vigtigt da den tilføjer session support


//            // Add services to the container.
//            builder.Services.AddRazorPages();

//            // Registrer din database context med andre ord den henter connection string fra appsettings.json og bruger den til at konfigurere Entity Framework
//            builder.Services.AddDbContext<ZealandDBContext>(options =>
//                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//            // Registrer dine services og repositories
//            builder.Services.AddScoped<ProductRepo>();
//            builder.Services.AddScoped<ProductService>();

//            var app = builder.Build();

//            // Configure the HTTP request pipeline.
//            if (!app.Environment.IsDevelopment())
//            {
//                app.UseExceptionHandler("/Error");
//                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//                app.UseHsts();
//            }

//            app.UseHttpsRedirection();

//            app.UseSession(); // vigtigt også da denne linje aktiverer session middleware

//            app.UseRouting();

//            app.UseAuthorization();

//            app.MapStaticAssets();
//            app.MapRazorPages()
//               .WithStaticAssets();

//            app.Run();
//        }
//    }
//}
