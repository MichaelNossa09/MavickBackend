using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;

namespace MavickBackend.Models
{
    public partial class SizeProduct
    {
        public long Id { get; set; }

        [Required]
        public int IdSize { get; set; }

        [Required]
        public int IdProduct { get; set; }

        public virtual Product IdProductNavigation { get; set; } = null!;
        public virtual Size IdSizeNavigation { get; set; } = null!;
    }
}
