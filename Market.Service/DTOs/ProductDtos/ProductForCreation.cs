namespace Market.Service.DTOs.ProductDtos
{
    public class ProductForCreation
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public long CategoryId { get; set; }
    }
}
