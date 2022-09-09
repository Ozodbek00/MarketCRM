using Market.Domain.Commons;

namespace Market.Domain.Entities
{
    public class Category : Auditable
    {
        public string Name { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }
}
