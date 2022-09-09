using Market.Domain.Commons;

namespace Market.Domain.Entities
{
    public class Order : Auditable
    {
        public long UserId { get; set; }
        public User User { get; set; }
    }
}
