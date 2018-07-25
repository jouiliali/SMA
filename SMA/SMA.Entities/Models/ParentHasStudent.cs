using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace SMA.Entities.Models
{
    public partial class ParentHasStudent : BaseEntity
    {
        [ForeignKey("ParentId")]
        public virtual Parent Parent { get; set; }
        public Int64 ParentId { get; set; }

        [ForeignKey("StudentId")]
        public virtual Student Student { get; set; }
        public Int64 StudentId { get; set; }
    }
}
