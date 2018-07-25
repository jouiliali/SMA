using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace SMA.Entities.Models
{
    public partial class Feed : BaseEntity
    {
        public string Libel { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }

        [ForeignKey("UserSMAId")]
        public virtual UserSMA UserSMA { get; set; }
        public Int64 UserSMAId { get; set; }

        [ForeignKey("FeedTypeId")]
        public virtual FeedType FeedType { get; set; }
        public Int64 FeedTypeId { get; set; }

        [ForeignKey("ImageId")]
        public virtual Image Image { get; set; }
        public Int64? ImageId { get; set; }

        public virtual ICollection<FeedComment> FeedComments { get; set; }

        public Feed()
        {
            FeedComments = new List<FeedComment>();
        }
    }
}
