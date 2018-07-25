using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace SMA.Entities.Models
{
    public partial class UserSMA : BaseEntity
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        [ForeignKey("GroupId")]
        public virtual Group Group { get; set; }
        public Int64 GroupId { get; set; }

        [ForeignKey("ImageId")]
        public virtual Image Image { get; set; }
        public Int64 ImageId { get; set; }

        public virtual List<Notification> Notifications { get; set; }
        public virtual List<Event> Events { get; set; }
        public virtual List<Feed> Feeds { get; set; }
        public virtual List<FeedComment> FeedComments { get; set; }
        public virtual List<Message> Messages { get; set; }
        public virtual List<StudentComment> StudentComments { get; set; }

        public UserSMA()
        {
            Notifications = new List<Notification>();
            Events = new List<Event>();
            Feeds = new List<Feed>();
            FeedComments = new List<FeedComment>();
            Messages = new List<Message>();
            StudentComments = new List<StudentComment>();

        }
    }
}
