using System;
using System.Collections.Generic;

namespace MavickBackend.Models
{
    public partial class User
    {
        public User()
        {
            Sales = new HashSet<Sale>();
            UserAddresses = new HashSet<UserAddress>();
        }

        public long Id { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string? Photo { get; set; }
        public int Role { get; set; }

        public virtual Role RoleNavigation { get; set; } = null!;
        public virtual ICollection<Sale> Sales { get; set; }
        public virtual ICollection<UserAddress> UserAddresses { get; set; }
    }
}
