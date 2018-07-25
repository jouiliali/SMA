using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace SMA.Entities.Models
{
    public partial class Testimonial : BaseEntity
    {
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }

        [ForeignKey("ParentId")]
        public virtual Parent Parent { get; set; }
        public Int64 ParentId { get; set; }

        [ForeignKey("TeacherId")]
        public virtual Teacher Teacher { get; set; }
        public Int64 TeacherId { get; set; }
    }
}
