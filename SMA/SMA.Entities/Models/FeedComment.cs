using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace SMA.Entities.Models
{
    public partial class FeedComment : BaseEntity
    {
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }

        [ForeignKey("FeedId")]
        public Feed Feed { get; set; }
        public Int64 FeedId { get; set; }

        [ForeignKey("UserSMAId")]
        public UserSMA UserSMA { get; set; }
        public Int64 UserSMAId { get; set; }
    }
}
