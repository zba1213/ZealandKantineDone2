using ZealandKantine.models;
using ZealandKantine.Repo;

namespace ZealandKantine.Service
{
    public class ProductService
    {
        private readonly ProductRepo _repo;

        public ProductService(ProductRepo repo)
        {
            _repo = repo;
        }

        // Tilføj et nyt produkt
        public void AddProduct(string name, decimal price, bool isDrink, int quantity = 1)
        {
            var product = new Product
            {
                Name = name,
                Price = price,
                IsDrink = isDrink,
                Quantity = quantity
            };
            _repo.Add(product);
        }

        // Hent alle produkter
        public List<Product> GetAllProducts()
        {
            return _repo.GetAll();
        }

        // Hent alle drikkevarer
        public List<Product> GetDrinks()
        {
            return _repo.GetDrinks();
        }

        // Hent alle madvarer
        public List<Product> GetFood()
        {
            return _repo.GetFood();
        }

        //  Hent et produkt baseret på ID
        public Product GetProductById(int id)
        {
            return _repo.GetById(id);
        }

        // Slet et produkt baseret på ID
        public void DeleteProduct(int id)
        {
            _repo.Delete(id);
        }

       



    }
}



