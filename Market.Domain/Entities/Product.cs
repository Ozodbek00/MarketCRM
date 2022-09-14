using Market.Domain.Commons;
using Market.Domain.Enums;

namespace Market.Domain.Entities
{
    public sealed class Product : Auditable
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public ItemState State { get; set; } = ItemState.Created;

        public long CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
