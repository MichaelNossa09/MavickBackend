using System;
using System.Collections.Generic;

namespace MavickBackend.Models
{
    public partial class CategoryProduct
    {
        public long Id { get; set; }
        public int IdCategory { get; set; }
        public int IdProduct { get; set; }

        public virtual Category IdCategoryNavigation { get; set; } = null!;
        public virtual Product IdProductNavigation { get; set; } = null!;
    }
}
