using Market.Domain.Commons;
using Market.Domain.Enums;

namespace Market.Domain.Entities
{
    public class User : Auditable
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; } = UserRole.User;
        public ItemState State { get; set; } = ItemState.Created;

        public IEnumerable<Order> Orders { get; set; }
    }
}
