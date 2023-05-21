using System;
using System.Collections.Generic;

namespace MavickBackend.Models
{
    public partial class Size
    {
        public Size()
        {
            SizeProducts = new HashSet<SizeProduct>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<SizeProduct> SizeProducts { get; set; }
    }
}
