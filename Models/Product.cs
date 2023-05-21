using System;
using System.Collections.Generic;

namespace MavickBackend.Models
{
    public partial class Product
    {
        public Product()
        {
            CategoryProducts = new HashSet<CategoryProduct>();
            Concepts = new HashSet<Concept>();
            SizeProducts = new HashSet<SizeProduct>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal UnitPrice { get; set; }
        public int Amount { get; set; }
        public string Photo { get; set; } = null!;
        public decimal Cost { get; set; }

        public virtual ICollection<CategoryProduct> CategoryProducts { get; set; }
        public virtual ICollection<Concept> Concepts { get; set; }
        public virtual ICollection<SizeProduct> SizeProducts { get; set; }
    }
}
