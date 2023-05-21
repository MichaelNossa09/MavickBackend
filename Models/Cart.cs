using System;
using System.Collections.Generic;

namespace MavickBackend.Models
{
    public partial class Cart
    {
        public long Id { get; set; }
        public long IdUser { get; set; }
        public int IdProduct { get; set; }
        public int Amount { get; set; }

        public virtual Product IdProductNavigation { get; set; } = null!;
        public virtual User IdUserNavigation { get; set; } = null!;
    }
}
