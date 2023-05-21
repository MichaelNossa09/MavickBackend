using System;
using System.Collections.Generic;

namespace MavickBackend.Models
{
    public partial class Category
    {
        public Category()
        {
            CategoryProducts = new HashSet<CategoryProduct>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<CategoryProduct> CategoryProducts { get; set; }
    }
}
