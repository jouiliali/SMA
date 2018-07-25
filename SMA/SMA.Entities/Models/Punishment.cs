using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace SMA.Entities.Models
{
    public partial class Punishment : BaseEntity
    {
        public string Note { get; set; }

        [ForeignKey("TeacherId")]
        public virtual Teacher Teacher { get; set; }
        public Int64 TeacherId { get; set; }

        [ForeignKey("StudentId")]
        public virtual Student Student { get; set; }
        public Int64 StudentId { get; set; }

        [ForeignKey("SchoolYearId")]
        public virtual SchoolYear SchoolYear { get; set; }
        public Int64 SchoolYearId { get; set; }

        [ForeignKey("PunishmentTypeId")]
        public virtual PunishmentType PunishmentType { get; set; }
        public Int64 PunishmentTypeId { get; set; }
    }
}
