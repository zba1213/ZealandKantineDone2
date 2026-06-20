using Microsoft.EntityFrameworkCore;
using ZealandKantine.models;
using System.Collections.Generic;
using System.Linq;
using System;
using ZealandKantine.Repo;
namespace ZealandKantine.Repo

{
    public class ProductRepo
    {
        private readonly ZealandDBContext _context;

        public ProductRepo(ZealandDBContext context)
        {
            _context = context;
        }

        // Tilføj et produkt
        public void Add(Product product)
        {
            product.TotalPrice = product.Price * product.Quantity;
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        // Hent alle produkter
        public List<Product> GetAll()
        {
            return _context.Products.ToList();
        }

        // Hent alle drikkevarer
        public List<Product> GetDrinks()
        {
            return _context.Products.Where(p => p.IsDrink).ToList();
        }

        // Hent alle madvarer
        public List<Product> GetFood()
        {
            return _context.Products.Where(p => !p.IsDrink).ToList();
        }

        // GetById metode
        public Product GetById(int id)
        {
            return _context.Products.Find(id);
        }

        // sletter et product baseret på ID
        public void Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }

    }
}
