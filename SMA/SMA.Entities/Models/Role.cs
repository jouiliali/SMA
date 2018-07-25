using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMA.Entities.Models
{
    public partial class Role : BaseEntity
    {
        public string Name { get; set; }
        public virtual List<GroupeRole> GroupeRoles { get; set; }

        public Role()
        {
            GroupeRoles = new List<GroupeRole>();
        }
    }
}
