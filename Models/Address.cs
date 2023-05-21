using System;
using System.Collections.Generic;

namespace MavickBackend.Models
{
    public partial class Address
    {
        public Address()
        {
            UserAddresses = new HashSet<UserAddress>();
        }

        public long Id { get; set; }
        public string Direction { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string State { get; set; } = null!;
        public int Postalcode { get; set; }
        public long IdUser { get; set; }

        public virtual ICollection<UserAddress> UserAddresses { get; set; }
    }
}
