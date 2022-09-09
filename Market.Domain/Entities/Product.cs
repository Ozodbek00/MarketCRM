using Market.Domain.Commons;

namespace Market.Domain.Entities
{
    public sealed class Product : Auditable
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public long CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
