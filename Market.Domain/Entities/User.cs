using Market.Domain.Commons;

namespace Market.Domain.Entities
{
    public class User : Auditable
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public IEnumerable<Order> Orders { get; set; }
    }
}
