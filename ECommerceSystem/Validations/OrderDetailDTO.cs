namespace ECommerceSystem.Validations
{
    public class OrderDetailDTO
    {
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }

}
