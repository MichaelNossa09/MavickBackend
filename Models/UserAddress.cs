using System;
using System.Collections.Generic;

namespace MavickBackend.Models
{
    public partial class UserAddress
    {
        public long Id { get; set; }
        public long IdUser { get; set; }
        public long IdAddress { get; set; }

        public virtual Address IdAddressNavigation { get; set; } = null!;
        public virtual User IdUserNavigation { get; set; } = null!;
    }
}
