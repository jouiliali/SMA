using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace SMA.Entities.Models
{
    public partial class Image : BaseEntity
    {
        public string Url { get; set; }
        public string Alt { get; set; }

        //[ForeignKey("SchoolHeadId")]
        //public SchoolHead SchoolHead { get; set; }
        //public Int64? SchoolHeadId { get; set; }

        //[ForeignKey("TeacherId")]
        //public Teacher Teacher { get; set; }
        //public Int64? TeacherId { get; set; }

        //[ForeignKey("FeedId")]
        //public Feed Feed { get; set; }
        //public Int64? FeedId { get; set; }

        //[ForeignKey("StudentId")]
        //public Student Student { get; set; }
        //public Int64? StudentId { get; set; }


        public virtual List<Feed> Feeds { get; set; }
        public virtual List<UserSMA> Users { get; set; }

        public Image()
        {
            Feeds = new List<Feed>();
            Users = new List<UserSMA>();
        }
    }
}
