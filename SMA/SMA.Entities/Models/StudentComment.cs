using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace SMA.Entities.Models
{
    public partial class StudentComment : BaseEntity
    {
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }

        [ForeignKey("StudentId")]
        public virtual Student Student { get; set; }
        public Int64 StudentId { get; set; }

        [ForeignKey("UserSMAId")]
        public virtual UserSMA UserSMA { get; set; }
        public Int64 UserSMAId { get; set; }
    }
}
