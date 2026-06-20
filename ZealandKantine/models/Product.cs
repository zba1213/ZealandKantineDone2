namespace ZealandKantine.models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }        
        public decimal Price { get; set; }      
        public bool IsDrink { get; set; }        
        public int Quantity { get; set; }        
        public decimal TotalPrice { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, Price: {Price}, IsDrink: {IsDrink}, Quantity: {Quantity}, TotalPrice: {TotalPrice}";
        }


    }
}
