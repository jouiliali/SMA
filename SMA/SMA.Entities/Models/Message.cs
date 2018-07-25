using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace SMA.Entities.Models
{
    public partial class Message : BaseEntity
    {
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsRed { get; set; }

        [ForeignKey("ParentId")]
        public virtual Parent Parent { get; set; }
        public Int64 ParentId { get; set; }

        [ForeignKey("UserSMAId")]
        public virtual UserSMA UserSMA { get; set; }
        public Int64 UserSMAId { get; set; }
    }
}
