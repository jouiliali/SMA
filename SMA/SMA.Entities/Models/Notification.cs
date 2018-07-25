using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace SMA.Entities.Models
{
    public partial class Notification : BaseEntity
    {
        public string Url { get; set; }
        public string Content { get; set; }

        [ForeignKey("UserSMAId")]
        public virtual UserSMA UserSMA { get; set; }
        public Int64 UserSMAId { get; set; }
    }
}
