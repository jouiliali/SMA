using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMA.Entities.Models
{
    public partial class PunishmentType : BaseEntity
    {
        public string Libel { get; set; }

        public virtual List<Punishment> Punishments { get; set; }

        public PunishmentType()
        {
            Punishments = new List<Punishment>();
        }
    }
}
