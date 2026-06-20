namespace ZealandKantine.models
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public bool IsDrink { get; set; }
        public decimal TotalPrice => Price * Quantity;

        // Total med rabat (kun for drikkevarer)
        public decimal EffectiveTotalPrice => IsDrink
            ? Price * Quantity * 0.90m  // 10% rabat på drikkevarer
            : Price * Quantity;          // Normal pris på mad
    }
}
