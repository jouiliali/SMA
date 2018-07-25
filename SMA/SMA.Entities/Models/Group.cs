using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMA.Entities.Models
{
    public partial class Group : BaseEntity
    {
        public string Name { get; set; }
        public virtual List<UserSMA> UserSMAs { get; set; }
        public virtual List<GroupeRole> GroupeRoles { get; set; }

        public Group()
        {
            UserSMAs = new List<UserSMA>();
            GroupeRoles = new List<GroupeRole>();
        }
    }
}
