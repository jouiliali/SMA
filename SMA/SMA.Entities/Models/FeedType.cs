using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMA.Entities.Models
{
    public partial class FeedType : BaseEntity
    {
        public string Libel { get; set; }

        public virtual List<Feed> Feeds { get; set; }

        public FeedType()
        {
            Feeds = new List<Feed>();
        }
    }
}
