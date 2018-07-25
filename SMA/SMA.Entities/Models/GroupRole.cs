using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace SMA.Entities.Models
{
    public partial class GroupeRole : BaseEntity
    {

        [ForeignKey("GroupId")]
        public virtual Group Group { get; set; }
        public Int64 GroupId { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
        public Int64 RoleId { get; set; }

        public GroupeRole()
        {
        }
    }
}
