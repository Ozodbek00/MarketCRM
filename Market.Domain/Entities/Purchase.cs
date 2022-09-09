using Market.Domain.Commons;

namespace Market.Domain.Entities
{
    public class Purchase : Auditable
    {
        public long OrderId { get; set; }
        public Order Order { get; set; }

        public long ProductId { get; set; }
        public Product Product { get; set; }
    }
}
