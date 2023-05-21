using System;
using System.Collections.Generic;

namespace MavickBackend.Models
{
    public partial class Sale
    {
        public Sale()
        {
            Concepts = new HashSet<Concept>();
        }

        public int Id { get; set; }
        public long IdUser { get; set; }
        public decimal Total { get; set; }
        public DateTime Date { get; set; }

        public virtual User IdUserNavigation { get; set; } = null!;
        public virtual ICollection<Concept> Concepts { get; set; }
    }
}
